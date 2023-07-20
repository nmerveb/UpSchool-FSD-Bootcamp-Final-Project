using Scraper.Domain.Identity;

namespace Scraper.Application.Common.Models
{
    public class CreateUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public CreateUserDto(string firstName,string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public User MapToUser()
        {
            return new User()
            {
                Email = this.Email,
                FirstName = this.FirstName,
                LastName = this.LastName,
                UserName = this.Email,
                CreatedOn = DateTimeOffset.Now,
            };
        }
    }
}
