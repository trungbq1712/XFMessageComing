using Acr.UserDialogs;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XFMessageComing.Models;
using XFMessageComing.Services;

namespace XFMessageComing.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public DelegateCommand VerifyCommand { get; set; }
        public DelegateCommand RegisterCommand { get; set; }
        private IReceiverService _receiverService;
        private string _otpCode;
        public string OtpCode
        {
            get => _otpCode;
            set => SetProperty(ref _otpCode, value);
        }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get => _phoneNumber;
            set => SetProperty(ref _phoneNumber, value);
        }

        public MainPageViewModel(INavigationService navigationService, IReceiverService receiverService) : base (navigationService)
        {
            Title = "Main Page";
            RegisterCommand = new DelegateCommand(OnRegister);
            VerifyCommand = new DelegateCommand(OnVerify);
            _receiverService = receiverService;
        }

        private void OnVerify()
        {
            
        }

        private void OnRegister()
        {
            //call api send sms

            var confirmConfig = new ConfirmConfig()
            {
                Title = "Registration",
                Message = $"Are you sure to register with phone number {PhoneNumber}?",
                OkText = "Yes",
                CancelText = "No",
                OnAction = (confirm) =>
                {
                    if (confirm == true)
                    {
                        bool result_ApiSendSms = true;
                        if (result_ApiSendSms)
                        {
                            _receiverService.Register(OnReceiveOTP);
                        }
                    }
                    else
                    {
                        var toastConfig = new ToastConfig("Ok,thanks!");
                        toastConfig.SetDuration(3000);
                        //toastConfig.SetBackgroundColor(System.Drawing.Color.FromArgb(12, 131, 193));
                        
                        toastConfig.SetPosition(ToastPosition.Top);
                        UserDialogs.Instance.Toast(toastConfig);
                    }
                }
            };
            UserDialogs.Instance.Confirm(confirmConfig);
        }

        private void OnReceiveOTP(DataResponse<OTP> dataResponse)
        {
            string format = "Your code verify is ";
            if (dataResponse.Success == true)
            {
                if (dataResponse.Data.Message.IndexOf(format) >= 0)
                {
                    string result = dataResponse.Data.Message.Substring(format.Length, 4);
                    OtpCode = result;
                }
            }
        }
    }
}
