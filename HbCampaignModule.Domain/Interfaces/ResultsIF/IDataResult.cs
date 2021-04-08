using System;
using System.Collections.Generic;
using System.Text;

namespace HbCampaignModule.Domain.ResultsIF
{
    public interface IDataResult<out T>:IResult
    {
        T Data { get; }
    }
}
