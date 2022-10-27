using AndroidTestApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static System.Net.Mime.MediaTypeNames;

namespace AndroidTestApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StudentTestsPage : ContentPage
    {
        public int StudentId { get;private set; }
        public List<StudentsTest> StudentsTests { get; private set; }
        public StudentTestsPage(int studentId)
        {
            InitializeComponent();
            StudentId = studentId;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            StudentsTests = await App.StudentsTestService.GetStudentsTestAsync();
            StudentsTests = StudentsTests.FindAll(test => test.StudentId == StudentId);
            var listLabel = new Label
            {
                Text = "Список тестов",
                Margin = new Thickness
                {
                    Top = 10,
                    Bottom = 10,
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
            var infoLabel = new Label
            {
                Text = "Чтобы перейти к тесту, нажмите на него",
                FontAttributes = FontAttributes.Italic,
                FontSize = 15,
                HorizontalOptions = LayoutOptions.Start,
                Margin = new Thickness
                {
                    Left = 10,
                    Bottom = 10,
                }
            };
            var dataTemplate = new DataTemplate(() =>
            {
                var grid = new Grid();
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.3, GridUnitType.Star)});
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.35, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.35, GridUnitType.Star) });

                //var tapGestureRecognizer = new TapGestureRecognizer();
                //tapGestureRecognizer.Tapped += ViewCell_Tapped;
                //grid.GestureRecognizers.Add(tapGestureRecognizer);
                var nameLabel = new Label
                {
                    FontAttributes = FontAttributes.Bold,
                    Margin = new Thickness
                    {
                        Left = 10,
                    }
                };
                var subjectLabel = new Label
                {

                };
                var teacherLabel = new Label
                {
                    Margin = new Thickness
                    {
                        Right = 10,
                    }
                };
                nameLabel.SetBinding(Label.TextProperty, "Name");
                subjectLabel.SetBinding(Label.TextProperty, "SubjectName");
                teacherLabel.SetBinding(Label.TextProperty, "TeacherName");
                grid.Children.Add(nameLabel);
                grid.Children.Add(teacherLabel, 1, 0);
                grid.Children.Add(subjectLabel, 2, 0);

                return new ViewCell
                {
                    View = grid,

                };
            });
            List<TestString> testStrings = new List<TestString>();
            if (StudentsTests.Count > 0)
            {
                List<Test> tests = await App.TestService.GetTestAsync();
                List<Subject> subjects = await App.SubjectService.GetSubjectAsync();
                List<Teacher> teachers = await App.TeacherService.GetTeacherAsync();
                testStrings = new List<TestString>();
                foreach (StudentsTest item in StudentsTests)
                {
                    string testName = tests.Find(test => test.Id == item.TestId).Name;
                    string subjectName = subjects.Find(subject => subject.Id == (tests.Find(test => test.Id == item.TestId).SubjectId)).Name;
                    string teacherName = teachers.Find(teacher => teacher.Id == (tests.Find(test => test.Id == item.TestId).TeacherId)).TeacherName;
                    testStrings.Add(new TestString
                    {
                        Name = testName,
                        SubjectName = subjectName,
                        TeacherName = teacherName
                    });

                }
            }
            else
                infoLabel.Text = "Вы еще не прошли ни одного теста";
            var listView = new ListView
            {
                ItemsSource = testStrings,
                ItemTemplate = dataTemplate,
                Margin = new Thickness(0, 10, 0, 0),
                SeparatorColor = Color.FromHex("#80CBC4")
            };
            listView.ItemTapped += ListView_ItemTapped;
            Content = new StackLayout
            {
                Children =
                {
                    listLabel,
                    infoLabel,
                    listView
                }
            };
        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            TestString testString = (TestString)e.Item;
            List<Test> tests = await App.TestService.GetTestAsync();
            StudentsTest studentsTest = StudentsTests.Find(studentsTest => studentsTest.TestId == (tests.Find(test => test.Name == testString.Name)).Id);
            await Navigation.PushAsync(new OneStudentsTestPage(studentsTest, testString));
        }
    }
}