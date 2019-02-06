using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WhiteMvvm.Services.Dialog
{
    public class DialogService : IDialogService
    {
        /// <summary>
        /// void method use to show dialog to user with message, title and one button
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="buttonLabel"></param>
        /// <returns></returns>
        public Task ShowAlertAsync(string message, string title, string buttonLabel)
        {            
            return UserDialogs.Instance.AlertAsync(message, title, buttonLabel);
        }
        /// <summary>
        /// boolean method that return whatever user select ok or cancel with option to change ok and cancel labels
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="cancelText"></param>
        /// <param name="okText"></param>
        /// <returns></returns>
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
        /// <summary>
        /// show loading indicator with mask type
        /// </summary>
        /// <param name="maskType"></param>
        public void ShowLoading(MaskType maskType)
        {
            UserDialogs.Instance.ShowLoading(maskType: maskType);
        }
        /// <summary>
        /// Hide Loading indicator
        /// </summary>
        public void HideLoading()
        {
            UserDialogs.Instance.HideLoading();
        }
    }
}
