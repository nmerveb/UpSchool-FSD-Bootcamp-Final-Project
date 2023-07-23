using Scraper.Infrastructure;
using Scraper.Application;
using Scraper.WebApi.Hubs;
using Scraper.Domain.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

    // Add services to the container.

    builder.Services.AddControllers();

    // Jwt config
    builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

    builder.Services.Configure<GoogleSettings>(builder.Configuration.GetSection("GoogleSettings"));


    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
    builder.Services.Configure<ApiBehaviorOptions>(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    
    builder.Services.AddSwaggerGen(setupAction =>
    {
        setupAction.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            Description = $"Input your Bearer token in this format - Bearer token to access this API",
        });
        setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer",
                },
            }, new List<string>()
        },
    });
    });


    builder.Services.AddApplicationServices();
    builder.Services.AddInfrastructure(builder.Configuration, builder.Environment.WebRootPath);


    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
       .AddJwtBearer(o =>
       {
           o.RequireHttpsMetadata = false;
           o.SaveToken = false;
           o.TokenValidationParameters = new TokenValidationParameters
           {
               ValidateIssuerSigningKey = true,
               ValidateIssuer = true,
               ValidateAudience = true,
               ValidateLifetime = true,
               ClockSkew = TimeSpan.Zero,
               ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
               ValidAudience = builder.Configuration["JwtSettings:Audience"],
               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]))
           };

           o.Events = new JwtBearerEvents
           {
               OnMessageReceived = context =>
               {
                   var accessToken = context.Request.Query["access_token"];
                   // If the request is for our hub...
                   var path = context.HttpContext.Request.Path;
                   if (!string.IsNullOrEmpty(accessToken) &&
                       (path.StartsWithSegments("/Hubs/ScraperLogHub")))
                   {
                       // Read the token out of the query string
                       context.Token = accessToken;
                   }
                   return Task.CompletedTask;
               }
           };
       });

    builder.Services.AddSignalR();

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll",
            builder => builder
                .AllowAnyMethod()
                .AllowCredentials()
                .SetIsOriginAllowed((host) => true)
                .AllowAnyHeader());
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseStaticFiles();

    app.UseHttpsRedirection();

    app.UseCors("AllowAll");

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    app.MapHub<ScraperLogHub>("/Hubs/ScraperLogHub");
    app.MapHub<OrderListHub>("/Hubs/OrderListHub");

    app.Run();

}
catch
{
}

