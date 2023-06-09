﻿using Scraper.Domain.Common;

namespace Scraper.Domain.Entities
{
    public class Product : EntityBase<Guid>
    {
        public Guid OrderId { get; set; }

        public Order Order { get; set; }

        public string Name { get; set; }

        public string Picture { get; set; }

        public bool IsOnSale { get; set; }

        public string Price { get; set; }

        public string? SalePrice { get; set; }

    }
}
