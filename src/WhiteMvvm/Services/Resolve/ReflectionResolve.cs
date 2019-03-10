using System;
using System.Globalization;
using System.Linq.Expressions;
using WhiteMvvm.Bases;
using WhiteMvvm.Configuration;
using Xamarin.Forms;

namespace WhiteMvvm.Services.Resolve
{
    public class ReflectionResolve : IReflectionResolve
    {
        public Type GetTypeFromAssembly(Type typeFrom , string folderNameToBeReplaced , string replacedFolderName, string fileNameToBeReplaced, string replacedFileName)
        {

            if (typeFrom == null || string.IsNullOrEmpty(typeFrom.Namespace) ||
                string.IsNullOrEmpty(typeFrom.Name))
            {
                throw new Exception($"Cannot locate page type for this view model");
            }
            var fileName = typeFrom.Name.Replace(fileNameToBeReplaced, replacedFileName);
            var namespaceName = typeFrom.Namespace.Replace(folderNameToBeReplaced, replacedFolderName);

            var fileFullName = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", namespaceName, fileName);
            var assemblyName = typeFrom.Assembly.FullName;
            var fileAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", fileFullName, assemblyName);

            var newType = Type.GetType(fileAssemblyName);
            return newType;
        }
        public Page GetPageInstance(Type pageType)
        {            
            if (pageType == null)
            {
                throw new Exception($"Cannot locate page type for this view model");
            }
            var pageFunc = Expression.Lambda<Func<Page>>(Expression.New(pageType)).Compile();
            return pageFunc();
        }
        public Page CreatePage(Type viewModelType)
        {
            var viewFolderName = ConfigurationManager.Current.ViewsFolderName;
            var viewModelFolderName = ConfigurationManager.Current.ViewModelFolderName;
            var viewFileName = ConfigurationManager.Current.ViewsFileName;
            var viewModelFileName = ConfigurationManager.Current.ViewModelFileName;

            var pageType = GetTypeFromAssembly(viewModelType, viewModelFolderName, viewFolderName, viewModelFileName, viewFileName);
            var page = GetPageInstance(pageType);
            return page;
        }
        public BaseViewModel CreateViewModel(Type pageType)
        {
            var viewFolderName = ConfigurationManager.Current.ViewsFolderName;
            var viewModelFolderName = ConfigurationManager.Current.ViewModelFolderName;
            var viewFileName = ConfigurationManager.Current.ViewsFileName;
            var viewModelFileName = ConfigurationManager.Current.ViewModelFileName;

            var viewmodelType = GetTypeFromAssembly(pageType, viewFolderName, viewModelFolderName,  viewFileName, viewModelFileName);
            var viewModel = GetViewModelInstance(viewmodelType);
            return viewModel;
        }
        public BaseViewModel GetViewModelInstance(Type viewModelType)
        {
            if (viewModelType == null)
            {
                throw new Exception($"Cannot locate view model type for this page");
            }
            var viewModel = BaseLocator.Instance.Resolve(viewModelType) as BaseViewModel;
            return viewModel;
        }
    }
}
