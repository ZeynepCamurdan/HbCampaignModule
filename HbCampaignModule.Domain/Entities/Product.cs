using System;
using System.Collections.Generic;
using System.Text;

namespace HbCampaignModule.Domain.Entities
{
    public class Product : BaseEntity
    {
        public int Id { get; set; }
        public string ProductCode { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
