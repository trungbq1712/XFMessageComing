using System;
using XFMessageComing.Models;

namespace XFMessageComing.Services
{
    public interface IReceiverService
    {
        void Register(Action<DataResponse<OTP>> action);
    }
}
