using AndroidTestApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AndroidTestApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestResultPage : ContentPage
    {
        private Test Test { get; set; }
        private int Score { get; set; }
        private StudentsTest StudentsTest { get; set; }
        public TestResultPage(Test test, int score)
        {
            InitializeComponent();
            Test = test;
            Score = score;
            StudentsTest = new StudentsTest();
            NavigationPage.SetHasBackButton(this, false);
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var StudentsTests = await App.StudentsTestService.GetStudentsTestAsync();
            StudentsTest = StudentsTests.Find(test => test.TestId == Test.Id && test.StudentId == App.Student.Id);
            var LabelStyle = new Style(typeof(Style))
            {
                Setters = {
                        new Setter {Property = Label.FontAttributesProperty, Value = FontAttributes.Italic},
                        new Setter {Property = Label.FontSizeProperty, Value = 20},
                        new Setter{Property = Label.HorizontalOptionsProperty, Value = LayoutOptions.Start},
                        new Setter{Property = Label.MarginProperty, Value = new Thickness(10, 10, 0,10)},
                }

            };
            var titleLabel = new Label()
            {
                Text = "Информация о тесте",
                FontAttributes = FontAttributes.Bold,
                FontSize = 25,
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(10, 10, 0, 0)
            };
            var nameLabel = new Label
            {
                Text = String.Concat("Название теста: ", Test.Name),
                Style = LabelStyle,

            };
            var lastResultLabel = new Label
            {
                Text = String.Concat("Результат прошлого прохождения: ", StudentsTest.Result),
                Style = LabelStyle,
            };
            var resultLabel = new Label
            {
                Text = String.Concat("Результат: ", Score),
                Style = LabelStyle,
            };
            var attemtptsLabel = new Label
            {
                Text = String.Concat("Оставшиеся попытки: ", (StudentsTest.AttemtsLeft) - 1),
                Style = LabelStyle,
            };
            DateTime dateTime = (DateTime)StudentsTest.Time;
            var timeLabel = new Label
            {
                Text = String.Concat("Дата прошлого прохождения: ", dateTime.ToString("dd/MM/yyyy hh:mm tt")),
                Style = LabelStyle,
            };
            var selectTestButton = new Button
            {
                Text = "Вернуться к списку тестов"
            };
            selectTestButton.Clicked += SelectTestButton_Clicked;
            Content = new StackLayout
            {

                Children =
                {
                    titleLabel,
                    nameLabel,
                    resultLabel,
                    attemtptsLabel,
                    timeLabel,
                    selectTestButton,
                }
            };
        }

        private async void SelectTestButton_Clicked(object sender, EventArgs e)
        {
            int newScore = 0;
            DateTime newDate;
            if (StudentsTest.Result > Score)
            {
                newScore = (int)StudentsTest.Result;
                newDate = (DateTime)StudentsTest.Time;
            }

            else
            {
                newScore = Score;
                newDate = DateTime.Now;
            }
            StudentsTest studentsTestTmp = new StudentsTest
            {
                Id = StudentsTest.Id,
                TestId = StudentsTest.TestId,
                StudentId = StudentsTest.StudentId,
                AttemtsLeft = (StudentsTest.AttemtsLeft) - 1,
                Result = newScore,
                Time = newDate
            };
            //if (await DisplayAlert("Test", $"Id {studentsTestTmp.Id}\n" +
            //    $"TestId {studentsTestTmp.TestId}\n" +
            //    $"StudentId {studentsTestTmp.StudentId}\n" +
            //    $"Attempts {studentsTestTmp.AttemtsLeft}\n" +
            //    $"Result Last {StudentsTest.Result}" +
            //    $"Result New {studentsTestTmp.Result}\n" +
            //    $"Time {studentsTestTmp.Time}", "OK", "NO"))
            //{
            await App.StudentsTestService.PutStudentsTestAsync(studentsTestTmp);
            await Navigation.PushAsync(new MainPage());
        }


    }
}