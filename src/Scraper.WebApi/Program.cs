using Scraper.Infrastructure;
using Scraper.Application;
using Scraper.WebApi.Hubs;

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddSignalR();

    builder.Services.AddApplicationServices();
    builder.Services.AddInfrastructure(builder.Configuration, builder.Environment.WebRootPath);

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.MapHub<ScraperLogHub>("/Hubs/ScraperLogHub");
    app.MapHub<OrderListHub>("/Hubs/OrderListHub");

    app.Run();

}
catch
{
}

