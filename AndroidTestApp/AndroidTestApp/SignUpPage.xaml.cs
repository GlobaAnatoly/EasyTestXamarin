using AndroidTestApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AndroidTestApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
            var toolbarItem = new ToolbarItem
            {
                Text = "Войти"
            };
            toolbarItem.Clicked += OnEnterButtonClicked;

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
            nameEntry = new Entry
            {
                Placeholder = "Введите ФИО"
            };
            loginEntry = new Entry
            {
                Placeholder = "Введите логин"
            };
            passwordEntry = new Entry
            {
                IsPassword = true
            };
            passwordConfirmEntry = new Entry
            {
                IsPassword = true
            };
            var registerButton = new Button
            {
                Text = "зарегистрироваться"
            };
            registerButton.Clicked += OnRegisterButtonClicked;

            Title = "Регистрация";
            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.StartAndExpand,
                Children = {
                    new Label { Text = "Введите ФИО" },
                    nameEntry,
                    new Label { Text = "Введите логин" },
                    loginEntry,
                    new Label { Text = "Пароль" },
                    passwordEntry,
                    new Label { Text = "Подтвердите пароль" },
                    passwordConfirmEntry,
                    registerButton,
                    messageLabel
                }
            };
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            
        }
        protected override bool OnBackButtonPressed()
        {
            //return true to prevent back, return false to just do something before going back. 
            return true;
        }

        private async void OnEnterButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }

        private async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            if (loginEntry.Text ==null || nameEntry.Text == null || passwordEntry.Text == null || passwordConfirmEntry.Text == null)
            {
                messageLabel.Text = "Заполните все поля";
            }
            else if (loginEntry.Text.Length <= 4 || passwordEntry.Text.Length <= 4 || nameEntry.Text.Length <= 4 ||passwordConfirmEntry.Text.Length <= 4 || passwordEntry.Text != passwordConfirmEntry.Text)
            {
                if (passwordEntry.Text != passwordConfirmEntry.Text)
                {
                    messageLabel.Text = "Пароли не совпадают";
                }
                if (loginEntry.Text.Length <= 4 || passwordEntry.Text.Length <= 4 || nameEntry.Text.Length <= 4)
                {
                    messageLabel.Text = "Некорректно введенные данные \n" +
                        "Поля должны быть длиннее 4 символов";
                }
            }
            else if (loginEntry.Text.Length > 4 && passwordEntry.Text.Length > 4 && passwordConfirmEntry.Text.Length > 4 && nameEntry.Text.Length > 4 )
            {
                string login = loginEntry.Text;
                string password = passwordEntry.Text;
                string name = nameEntry.Text;
                Student student = new Student
                {
                    Id = 0,
                    StudentName = name,
                    StudentLogin = login,
                    StudentPassword = null,
                    StudentPasswordStr = password,
                };
                await App.StudentService.PostStudentAsync(new Student
                {
                    StudentName = name,
                    StudentLogin = login,
                    StudentPassword = new byte[16],
                    StudentPasswordStr = password,
                });
                App.IsLoggedIn = true;
                Navigation.InsertPageBefore(new MainPage(), this);
                await Navigation.PopAsync();
            }

        }

    }
}