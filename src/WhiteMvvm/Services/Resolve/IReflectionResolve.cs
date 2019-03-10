using System;
using WhiteMvvm.Bases;
using Xamarin.Forms;

namespace WhiteMvvm.Services.Resolve
{
    public interface IReflectionResolve
    {
        Type GetTypeFromAssembly(Type typeFrom, string folderNameToBeReplaced, string replacedFolderName,
            string fileNameToBeReplaced, string replacedFileName);
        Page GetPageInstance(Type pageType);
        BaseViewModel CreateViewModel(Type viewModelType);
        BaseViewModel GetViewModelInstance(Type viewModelType);
        Page CreatePage(Type viewModelType);
    }
}
