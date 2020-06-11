using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaskBag
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotePage : ContentPage
    {
        public NotePage()
        {
            InitializeComponent();
            SaveToolbar.Text = Resource.SaveToolbar;
            DeleteToolbar.Text = Resource.DeleteToolbar;
            Title.Placeholder = Resource.Title;
            Description.Placeholder = Resource.Description;
        }
        private void SaveNote(object sender, EventArgs e)
        {
            var note = (Note)BindingContext;
            if (!String.IsNullOrEmpty(note.Note_Name))
            {
                App.Database.SaveItem(note);
            }
            this.Navigation.PopAsync();
        }
        private void DeleteNote(object sender, EventArgs e)
        {
            var note = (Note)BindingContext;
            App.Database.DeleteItem(note.Id);
            this.Navigation.PopAsync();
        }
        private void Cancel(object sender, EventArgs e)
        {
            this.Navigation.PopAsync();
        }
    }
}