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
    public partial class OneTestPage : ContentPage
    {
        public Test Test { get; set; }
        public TestString TestString { get; set; }
        public OneTestPage(Test test, TestString testString)
        {
            InitializeComponent();
            Test = test;
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
                Text = String.Concat("Название теста: ", Test.Name),
                Style = LabelStyle,

            };
            var subjectLabel = new Label
            {
                Text = String.Concat("Название предмета: ", TestString.SubjectName),
                Style=LabelStyle,
            };
            var teacherLabel = new Label
            {
                Text = String.Concat("Автор теста: ", TestString.TeacherName),
                Style=LabelStyle,
            };
            var selectTestButton = new Button
            {
                Text = "Выбрать"
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
                    selectTestButton,
                }
            };
        }

        private async void SelectTestButton_Clicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Подтвердить", "Выбрать этот тест?", "Да", "Нет");
            if (answer)
            {
                List<StudentsTest> studentsTests = await App.StudentsTestService.GetStudentsTestAsync();
                StudentsTest studentsTest = studentsTests.Find(studentsTest => studentsTest.TestId == Test.Id && studentsTest.StudentId == App.Student.Id);
                if (studentsTest != null)
                {
                    await Navigation.PushAsync(new OneStudentsTestPage(studentsTest, TestString));
                }
                else
                {
                    studentsTest = new StudentsTest
                    {
                        Id = 0,
                        StudentId = App.Student.Id,
                        TestId = Test.Id,
                        Result = 0,
                        AttemtsLeft = 0,
                        Time = DateTime.Now
                    };
                    await App.StudentsTestService.PostStudentsTestAsync(studentsTest);
                    studentsTests = await App.StudentsTestService.GetStudentsTestAsync();
                    studentsTest = studentsTests.Find(studentsTest => studentsTest.TestId == Test.Id && studentsTest.StudentId == App.Student.Id);
                    await Navigation.PushAsync(new OneStudentsTestPage(studentsTest, TestString));
                }
                
                
            }
        }
    }
}