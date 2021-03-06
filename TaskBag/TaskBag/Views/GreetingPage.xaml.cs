﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskBag.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaskBag.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GreetingPage : ContentPage
    {
        public GreetingPage()
        {
            InitializeComponent();
            BindingContext = new GreetingPageViewModel();
            Congratulations.Text = Resource.Congratulations;
        }
    }
}