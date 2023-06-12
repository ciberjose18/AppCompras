﻿using AppCompras.Vistas;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.SharedTransitions;

namespace AppCompras
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new SharedTransitionNavigationPage(new Compras());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
