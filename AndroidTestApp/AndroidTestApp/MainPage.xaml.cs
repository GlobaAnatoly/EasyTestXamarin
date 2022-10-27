using Android.Graphics.Fonts;
using AndroidTestApp.Controllers;
using AndroidTestApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AndroidTestApp
{
    public partial class MainPage : ContentPage
    {
        public Student Student { get; private set; }
        public MainPage()
        {
            InitializeComponent();
            studentName = new Label
            {
                Margin = new Thickness
                {
                    Top = 10,
                    Bottom = 10
                },
                Style = new Style(typeof(Label))
                {
                    Setters =
                    {
                        new Setter
                        {
                            Property = Label.FontSizeProperty, Value = 25
                        },
                        new Setter {
                            Property = Label.HorizontalTextAlignmentProperty, Value = TextAlignment.Center
                        },
                        new Setter{
                            Property= Label.FontAttributesProperty, Value = FontAttributes.Bold
                        }
                    }
                }
            };

            studentsTestButton = new Button
            {
                Text = "Перейти"
            }; ;
            allTestsButton = new Button
            {
                Text = "Перейти"
            };
            allTestsButton.Clicked += AllTestsButton_Clicked;
            studentsTestButton.Clicked += StudentsTestButton_Clicked;
            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.StartAndExpand,
                Children =
                {
                    studentName,
                    new Label{Text = "Пройденные тесты", FontSize = 15, FontAttributes = FontAttributes.Italic},
                    studentsTestButton,
                    new Label{Text = "Все тесты", FontSize = 15, FontAttributes = FontAttributes.Italic},
                    allTestsButton,
                }
            };

            if (App.IsLoggedIn && App.Student != null)
            {
                Student = (Student)App.Student;
                studentName.Text = String.Concat("Здравствуй ", Student.StudentName);
            }
        }

        private async void StudentsTestButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StudentTestsPage(Student.Id));
        }

        private async void AllTestsButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AllTestsPage());
        }

        //protected override async void OnAppearing()
        //{
        //    base.OnAppearing();

        //}

        private void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            App.IsLoggedIn = false;
            App.Student = null;
            Navigation.InsertPageBefore(new LoginPage(), this);
            Navigation.PopAsync();
        }
    }
}
