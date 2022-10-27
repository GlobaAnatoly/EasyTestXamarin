using AndroidTestApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Behaviors;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Android.Provider.CalendarContract;

namespace AndroidTestApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            var toolbarItem = new ToolbarItem
            {
                Text = "Зарегестрироваться"
            };
            toolbarItem.Clicked += OnSignUpButtonClicked;
            //ToolbarItems.Add(toolbarItem);
            var LabelStyle = new Style(typeof(Label))
            {
                Setters = {
                        new Setter {Property = Label.TextColorProperty, Value = Color.Red}
                }
            };
            messageLabel = new Label
            {
                Style = LabelStyle
            };
            loginEntry = new Entry
            {
                Placeholder = "Введите логин"
            };
            passwordEntry = new Entry
            {
                IsPassword = true
            };
            var loginButton = new Button
            {
                Text = "Войти"
            };
            loginButton.Clicked += OnLoginButtonClicked;

            Title = "Авторизация";
            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.StartAndExpand,
                Children = {
                    new Label { Text = "Введите логин" },
                    loginEntry,
                    new Label { Text = "Введите Пароль" },
                    passwordEntry,
                    loginButton,
                    messageLabel
                }
            };
            var validStyle = new Style(typeof(Entry));
            validStyle.Setters.Add(new Setter
            {
                Property = Entry.TextColorProperty,
                Value = Color.Green
            });

            var invalidStyle = new Style(typeof(Entry));
            invalidStyle.Setters.Add(new Setter
            {
                Property = Entry.TextColorProperty,
                Value = Color.Red
            });
            var textValidationBehavior = new TextValidationBehavior
            {
                InvalidStyle = invalidStyle,
                ValidStyle = validStyle,
                Flags = ValidationFlags.ValidateOnValueChanging,
                MinimumLength = 4,

            };
            var textValidationBehavior2 = new TextValidationBehavior
            {
                InvalidStyle = invalidStyle,
                ValidStyle = validStyle,
                Flags = ValidationFlags.ValidateOnValueChanging,
                MinimumLength = 4,

            };
            passwordEntry.Behaviors.Add(textValidationBehavior2);
            loginEntry.Behaviors.Add(textValidationBehavior);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

           
        }
        async void OnSignUpButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPage());

        }
        async void OnLoginButtonClicked(object sender, EventArgs e)
        {

            string login = loginEntry.Text;
            string password = passwordEntry.Text;
            List<Student> students = await App.StudentService.GetStudentAsync();
            Student student = students.Find(x => x.StudentLogin == login);
            MD5CryptoServiceProvider cryptoServiceProvider = new MD5CryptoServiceProvider();
            StringBuilder sb = new StringBuilder();
            if (student != null)
            {
                byte[] passwordByte = cryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < student.StudentPassword.Length; i++)
                {
                    sb.Append(student.StudentPassword[i].ToString("X2"));
                }
                if (student != null && student.StudentPassword.SequenceEqual(cryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(password))))
                {
                    App.IsLoggedIn = true;
                    App.Student = student;
                    Navigation.InsertPageBefore(new MainPage(), this);
                    await Navigation.PopAsync();
                }
                else
                {
                    messageLabel.Text = "Неверный пароль";
                    passwordEntry.Text = string.Empty;
                }
            }
            else
            {
                messageLabel.Text = "Неверные данные";
                passwordEntry.Text = string.Empty;
            }

        }
        protected override bool OnBackButtonPressed()
        {
            //return true to prevent back, return false to just do something before going back. 
            return true;
        }
    }
}