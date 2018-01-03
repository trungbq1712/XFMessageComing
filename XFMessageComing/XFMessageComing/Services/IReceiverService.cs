using System;
using XFMessageComing.Models;

namespace XFMessageComing.Services
{
    public interface IReceiverService
    {
        //test
        void Register(Action<DataResponse<OTP>> action);
    }
}
