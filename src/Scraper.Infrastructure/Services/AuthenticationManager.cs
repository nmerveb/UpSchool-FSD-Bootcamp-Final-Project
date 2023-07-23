using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scraper.Application.Common.Interfaces;
using Scraper.Application.Common.Models;
using Scraper.Domain.Identity;

namespace Scraper.Infrastructure.Services
{
    public class AuthenticationManager : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJwtService _jwtService;

        public AuthenticationManager(UserManager<User> userManager, SignInManager<User> signInManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
        }


        public async Task<string> CreateUserAsync(CreateUserDto createUserDto, CancellationToken cancellationToken)
        {
            var user  =  createUserDto.MapToUser();

            var validateUser = await CheckIfUserExist(user.Email);

            if (validateUser)
            {
                throw new Exception("There is already an user with given email.");

            }

            var identityResult = await _userManager.CreateAsync(user);

            if (!identityResult.Succeeded)
            {

                throw new Exception("identity error");
            }

            return user.Id;
        }

        public async Task<string> GenerateEmailActivationToken(string userId, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<JwtDto> LoginAsync(AuthLoginRequest authLoginRequest, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(authLoginRequest.Email);

            var validateUser = await CheckIfUserExist(authLoginRequest.Email);
            
            if(!validateUser)
            {
                throw new Exception("You need to register.");
            }

            var loginResult = await _signInManager.PasswordSignInAsync(user, authLoginRequest.Password, false,false);

            if (!loginResult.Succeeded)
            {
                throw new Exception("Login error");
            }

            return _jwtService.Generate(user.Id, user.Email, user.FirstName, user.LastName);

        }

        public async Task<JwtDto> SocialLoginAsync(SocialLoginRequest socialLoginRequest, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(socialLoginRequest.Email);

            if(user is not null)
            {
                return _jwtService.Generate(user.Id, user.Email, user.FirstName, user.LastName);
            }

            user = new User()
            {
                UserName = socialLoginRequest.Email,
                Email = socialLoginRequest.Email,
                EmailConfirmed = true,
                FirstName = socialLoginRequest.FirstName,
                LastName = socialLoginRequest.LastName,
                CreatedOn = DateTimeOffset.Now,
            };

            var identityResult =  await _userManager.CreateAsync(user);

            var getCreatedUser = await _userManager.FindByEmailAsync(socialLoginRequest.Email);

            if (!identityResult.Succeeded)
            {
                throw new Exception("Login error");
            }

            return _jwtService.Generate(getCreatedUser.Id, user.Email, user.FirstName, user.LastName);

        }

        public  Task<bool> CheckIfUserExist(string userEmail)
        {
            return  _userManager.Users.AnyAsync(x => x.Email == userEmail);
        }
    }
}
