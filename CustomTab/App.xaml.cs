﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CustomTab
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();
            MainPage = new NavigationPage(new Views.Market.Version2.MarketMainPage());
            //MainPage = new NavigationPage(new TabbedPageCustom());
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
