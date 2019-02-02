using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WhiteSolution.Services.Dialog
{
    public class DialogService : IDialogService
    {
        public Task ShowAlertAsync(string message, string title, string buttonLabel)
        {            
            return UserDialogs.Instance.AlertAsync(message, title, buttonLabel);
        }
        public Task<bool> ShowConfirmMessageAsync(string message, string title = "Confirm", string cancelText = "Cancel",string okText = "Ok")
        {
            var confirmConfig = new ConfirmConfig()
            {
                Message = message,
                Title = title,
                CancelText = cancelText,
                OkText = okText,                
            };
            return UserDialogs.Instance.ConfirmAsync(confirmConfig);
        }
        public void ShowLoading()
        {
            UserDialogs.Instance.ShowLoading(maskType: MaskType.None);
        }
        public void HideLoading()
        {
            UserDialogs.Instance.HideLoading();
        }
    }
}
