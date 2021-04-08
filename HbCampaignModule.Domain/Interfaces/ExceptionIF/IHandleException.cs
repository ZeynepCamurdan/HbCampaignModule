using HbCampaignModule.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HbCampaignModule.Domain.Interfaces.ExceptionIF
{
    public interface IHandleException<T>
    {
        Response<T> CheckCreateException(T model);
    }
}
