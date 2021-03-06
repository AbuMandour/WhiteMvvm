﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WhiteMvvm;
using WhiteMvvm.Bases;
using WhiteSample.ViewModels;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace WhiteSample
{
    public partial class App : WhiteApplication
    {
        public App()
        {
            InitializeComponent();
            ViewModelLocator.Init();
            SetHomePage<HomeViewModel>(isNavigationPage: true);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
