using System;
using System.Collections.Generic;
using System.Text;

namespace HbCampaignModule.Domain.Results
{
    public class SuccessDataResult<T>:DataResult<T>
    {
        public SuccessDataResult(T data,string message , HttpResponse httpResponse) : base(data, true, message, httpResponse)
        {
        }
        public SuccessDataResult(T data) : base(data, true)
        {
        }
        public SuccessDataResult(string message, HttpResponse httpResponse) : base(default, true,message, httpResponse)
        {

        }
        public SuccessDataResult(): base(default,true)
        {

        }
    }
}
