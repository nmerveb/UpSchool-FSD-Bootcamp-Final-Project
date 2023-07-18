using Scraper.Domain.Common;

namespace Scraper.Domain.Entities
{
    public class User: EntityBase<Guid>
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ICollection<Order> Orders { get; set; }

        public User()
        {
            Orders = new List<Order>();
        }
    }
}
