using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using TaskBag;

namespace TestBag
{
   

    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void ViewRepl()
        {
            app.Repl();
        }

        [Test]
        public void InitializeNote_CheckFields()
        {
            string noteName = "Заголовок";
            string noteText = "Описание";
            Note note = new Note();
            note.Note_Name = noteName;
            note.Text = noteText;
            StringAssert.Contains(note.Note_Name, noteName);
            StringAssert.Contains(note.Text, noteText);
        }


        [Test]
        public void AddNote_in_list()
        {
            app.Query(c => c.Id("toolbar"));
            app.Tap(c => c.Marked("Add"));
            app.EnterText(c => c.Marked("NoResourceEntry-14"), "Title1");
            app.EnterText(c => c.Marked("NoResourceEntry-15"), "Description1");
            app.DismissKeyboard();
            app.Tap(c => c.Marked("Save"));
        }

        [Test]
        public void Login()
        {
            app.Query(c => c.Id("toolbar"));
            app.Tap(c => c.Marked("Ещё"));
            app.Tap(c => c.Marked("title"));
            app.EnterText(c => c.Marked("NoResourceEntry-12"), "email@new.com");
            app.EnterText(c => c.Marked("NoResourceEntry-13"), "rrrrr5");
            app.DismissKeyboard();
            app.Tap(c => c.Marked("SingUp"));
            app.Tap(c => c.Marked("Login"));
        }
    }

}

