using System;
using System.Collections.Generic;
using System.Text;

namespace HbCampaignModule.Domain.Model
{
    public class BaseDto
    {
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
