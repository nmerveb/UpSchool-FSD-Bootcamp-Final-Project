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

        public AuthenticationManager(UserManager<User> userManager)
        {
            _userManager = userManager;
        }


        public async Task<string> CreateUserAsync(CreateUserDto createUserDto, CancellationToken cancellationToken)
        {
            var user  =  createUserDto.MapToUser();

            var validateUser =  await _userManager.Users.AnyAsync(x => x.Email == user.Email, cancellationToken);

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
    }
}
