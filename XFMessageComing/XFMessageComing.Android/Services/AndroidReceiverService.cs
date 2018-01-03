using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Telephony;
using Android.Views;
using Android.Widget;
using XFMessageComing.Models;
using XFMessageComing.Services;

namespace XFMessageComing.Droid.Services
{
    [BroadcastReceiver]
    public class AndroidReceiverService : BroadcastReceiver, IReceiverService
    {
        public static readonly string INTENT_ACTION = "android.provider.Telephony.SMS_RECEIVED";

        public Action<DataResponse<OTP>> _onReceiveOTP;
        public static AndroidReceiverService Instance { get; private set; }

        public AndroidReceiverService()
        {
            Instance = this;
        }
        public override void OnReceive(Context context, Intent intent)
        {
            if (intent.Action != INTENT_ACTION)
                return;
            var bundle = intent.Extras;
            if (bundle == null)
                return;


            DataResponse<OTP> dataResponse = new DataResponse<OTP>();
            dataResponse.Success = false;
            dataResponse.Message = "Pls fill code verify manually";
            try
            {
                var pdus = bundle.Get("pdus");
                // var castedPdus = JNIEnv.GetArray(pdus.Handle);
                var castedPdus = JNIEnv.GetArray<Java.Lang.Object>(pdus.Handle);
                var msgs = new SmsMessage[castedPdus.Length];
                var sb = new StringBuilder();
                string s = "";
                string msg = "";

                string sender = null;
                for (var i = 0; i < msgs.Length; i++)
                {
                    var bytes = new byte[JNIEnv.GetArrayLength(castedPdus[i].Handle)];
                    JNIEnv.CopyArray(castedPdus[i].Handle, bytes);
                    string format = bundle.GetString("format");
                    msgs[i] = SmsMessage.CreateFromPdu(bytes, format);
                    if (sender == null)
                    {
                        sender = msgs[i].OriginatingAddress;
                    }
                    msg += msgs[i].MessageBody;
                    s += msgs[i].OriginatingAddress;
                    sb.Append(string.Format("SMS From: {0}{1}Body: {2}{1}", msgs[i].OriginatingAddress, System.Environment.NewLine, msgs[i].MessageBody));
                    //Toast.MakeText(context, sb.ToString(), ToastLength.Long).Show();


                }
                dataResponse.Success = true;
                dataResponse.Data = new OTP { IsOpen = false, Message = msg, Sender = s, ReceiveDate = DateTime.Now };
                _onReceiveOTP?.Invoke(dataResponse);
            }
            catch
            {
                _onReceiveOTP?.Invoke(dataResponse);
            }
        }

        public void Register(Action<DataResponse<OTP>> action)
        {
            _onReceiveOTP = action;
        }
    }
}