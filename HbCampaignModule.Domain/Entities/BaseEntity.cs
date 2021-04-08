using System;
using System.Collections.Generic;
using System.Text;

namespace HbCampaignModule.Domain.Entities
{
    public class BaseEntity
    {
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
