using System;
using System.Collections.Generic;
using System.Text;

namespace XFMessageComing.Models
{
    public class OTP
    {
        public string Sender { get; set; }
        public string Message { get; set; }
        public DateTime? ReceiveDate { get; set; }
        public bool? IsOpen { get; set; }
    }
}
