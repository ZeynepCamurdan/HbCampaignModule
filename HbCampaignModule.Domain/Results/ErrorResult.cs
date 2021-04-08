using System;
using System.Collections.Generic;
using System.Text;

namespace HbCampaignModule.Domain.Results
{
    public class ErrorResult:Result
    {
        public ErrorResult(string message,HttpResponse httpCode) : base(false, message,httpCode)
        {
        }
        public ErrorResult() : base(false)
        {
        }
    }
}
