using System;
using System.Collections.Generic;
using System.Text;

namespace XFMessageComing.Models
{
    public class DataResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
    public class DataResponse<T> : DataResponse
    {
        public T Data { get; set; }
    }
}
