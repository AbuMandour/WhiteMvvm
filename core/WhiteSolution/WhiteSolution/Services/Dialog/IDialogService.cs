using System.Threading.Tasks;

namespace WhiteSolution.Services.Dialog
{
    public interface IDialogService
    {
        Task ShowAlertAsync(string message, string title, string buttonLabel);
        Task<bool> ShowConfirmMessageAsync(string message, string title = "Confirm", string cancelText = "Cancel", string okText = "Ok");
        void ShowLoading();
        void HideLoading();
    }
}