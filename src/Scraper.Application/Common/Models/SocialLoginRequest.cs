using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraper.Application.Common.Models
{
    public class SocialLoginRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public SocialLoginRequest(string email, string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
    }
}
