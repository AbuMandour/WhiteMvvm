using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WhiteSolution.Services.Dialog
{
    public class DialogMockService : IDialogService
    {
        public Task ShowAlertAsync(string message, string title, string buttonLabel)
        {
            return Task.CompletedTask;
        }

        public Task<bool> ShowConfirmMessageAsync(string message, string title = "Confirm", string cancelText = "Cancel",
            string okText = "Ok")
        {
            return Task.FromResult(true);
        }

        public void ShowLoading()
        {
            
        }

        public void HideLoading()
        {
            
        }
    }
}
