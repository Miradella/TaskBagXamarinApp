using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaskBag
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Auth : ContentPage
    {
        public Auth()
        {
            //InitializeComponent();
            string pageXAML = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n" +
                "<ContentPage xmlns=\"http://xamarin.com/schemas/2014/forms\"\n" +
                "xmlns:x=\"http://schemas.microsoft.com/winfx/2009/xaml\"\n" +
                "x:Class=\"TaskBag.Auth\"\n" +
                "Title=\"Authorisation\">\n";
            if (App.User_email != null)
            {
                pageXAML+="<Label Text=\"Вы авторизованы как "+App.User_email+"\" HorizontalOptions=\"Center\" VerticalOptions=\"CenterAndExpand\" />" +
                    "<Button x:Name=\"button1\" Text=\"Выйти\" Clicked=\"Button_Click\" />" +
                "</ContentPage>";
            }
            else pageXAML+= "<Label Text=\"Вы не авторизованы\" HorizontalOptions=\"Center\" VerticalOptions=\"CenterAndExpand\" />" +
                "</ContentPage>";
            this.LoadFromXaml(pageXAML);
        }
        private async void Button_Click(object sender, EventArgs e)
        {
            App.User_email = null;
            await Navigation.PushAsync(new NotePage());
        }
    }
}