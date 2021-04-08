using System;
using System.Collections.Generic;
using System.Text;

namespace HbCampaignModule.Domain.Results
{
    public class ErrorDataResult<T>:DataResult<T>
    {
        public ErrorDataResult(T data, string message, HttpResponse httpResponse) : base(data, false, message, httpResponse)
        {
        }
        public ErrorDataResult(T data) : base(data, false)
        {
        }
        public ErrorDataResult(string message, HttpResponse httpResponse) : base(default, false, message, httpResponse)
        {

        }
        public ErrorDataResult() : base(default, false)
        {

        }
    }
}
