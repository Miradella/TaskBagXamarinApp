using System;
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
    public partial class AuthPage : ContentPage
    {
        public AuthPage()
        {
            InitializeComponent();

            // bind the context to the view model
            BindingContext = new AuthPageViewModel();
        }
    }
}