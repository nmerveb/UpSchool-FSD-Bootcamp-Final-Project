using Microsoft.AspNetCore.Identity;
using Scraper.Domain.Common;
using Scraper.Domain.Entities;

namespace Scraper.Domain.Identity
{
    public class User : IdentityUser<string>, IEntityBase<string>, ICreatedByEntity, IModifiedByEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public string? CreatedByUserId { get; set; }

        public DateTimeOffset? ModifiedOn { get; set; }
        public string? ModifiedByUserId { get; set; }
        public ICollection<Order> Orders { get; set; }

        public User()
        {
            Orders = new List<Order>();
        }

    }
}
