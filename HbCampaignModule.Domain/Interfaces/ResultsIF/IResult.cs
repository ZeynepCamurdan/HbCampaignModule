using System;
using System.Collections.Generic;
using System.Text;

namespace HbCampaignModule.Domain.ResultsIF
{
    public interface IResult
    {
        bool Success { get; }
        string Message { get; }
    }
}
