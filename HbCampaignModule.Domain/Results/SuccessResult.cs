using System;
using System.Collections.Generic;
using System.Text;

namespace HbCampaignModule.Domain.Results
{
    public class SuccessResult:Result
    {
        public SuccessResult(string message, HttpResponse httpCode) : base(true, message, httpCode)
        {
        }
        public SuccessResult() : base(true)
        {
        }
    }
}
