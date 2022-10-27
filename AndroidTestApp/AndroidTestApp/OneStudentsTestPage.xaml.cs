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
    public partial class OneStudentsTestPage : ContentPage
    {
        public StudentsTest StudentsTest { get; set; }  
        public TestString TestString { get; set; }
        public OneStudentsTestPage(StudentsTest studentsTest, TestString testString)
        {
            InitializeComponent();
            StudentsTest = studentsTest;
            TestString = testString;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
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
                Text = String.Concat("Название теста: ", TestString.Name),
                Style = LabelStyle,

            };
            var subjectLabel = new Label
            {
                Text = String.Concat("Название предмета: ", TestString.SubjectName),
                Style = LabelStyle,
            };
            var teacherLabel = new Label
            {
                Text = String.Concat("Автор теста: ", TestString.TeacherName),
                Style = LabelStyle,
            };
            var resultLabel = new Label
            {
                Text = String.Concat("Результат: ", StudentsTest.Result),
                Style = LabelStyle,
            };
            var attemtptsLabel = new Label
            {
                Text = String.Concat("Оставшиеся попытки: ", StudentsTest.AttemtsLeft),
                Style = LabelStyle,
            };
            DateTime dateTime = (DateTime)StudentsTest.Time;
            var timeLabel = new Label
            {
                Text = String.Concat("Дата прохождения: ", dateTime.ToString("dd/MM/yyyy hh:mm tt")),
                Style = LabelStyle,
            };
            var selectTestButton = new Button
            {
                Text = "Начать прохождение"
            };
            selectTestButton.Clicked += SelectTestButton_Clicked;
            Content = new StackLayout
            {

                Children =
                {
                    titleLabel,
                    nameLabel,
                    teacherLabel,
                    subjectLabel,
                    resultLabel,
                    attemtptsLabel,
                    timeLabel,
                    selectTestButton,
                }
            };
        }

        private async void SelectTestButton_Clicked(object sender, EventArgs e)
        {
            
            if(StudentsTest.AttemtsLeft == 0)
            {
                await DisplayAlert("Ошибка", "У Вас не осталось попыток на прохождение теста", "Ок");
            }
            else
            {
                var answer = await DisplayAlert("Подтвердить", "Начать этот тест?", "Да", "Нет");
                if (answer)
                {
                    Test test = await App.TestService.GetTestByIdAsync((int)StudentsTest.TestId);
                    await Navigation.PushAsync(new QuestionPage(test, 0, 0));
                }
            }
           
        }
    }
}