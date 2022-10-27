using Android.OS;
using AndroidTestApp.Models;
using Java.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Markup;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AndroidTestApp
{
    public class TestString
    {
        public string Name { get; set; }
        public string TeacherName { get; set; }
        public string SubjectName { get; set; }
    }
    
    public partial class AllTestsPage : ContentPage
    {
        private List<Test> Tests { get; set; }
        public AllTestsPage()
        {
            InitializeComponent();

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
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
            var dataTemplate = new DataTemplate(() =>
            {
                var grid = new Grid();
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.3, GridUnitType.Star) });
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
            Tests = await App.TestService.GetTestAsync();
            List<Subject> subjects = await App.SubjectService.GetSubjectAsync();
            List<Teacher> teachers = await App.TeacherService.GetTeacherAsync();
            List<TestString> testStrings = new List<TestString>();
            foreach (Test item in Tests)
            {
                testStrings.Add(new TestString
                {
                    Name = item.Name,
                    SubjectName = subjects.Find(subject => subject.Id == item.SubjectId).Name,
                    TeacherName = teachers.Find(teacher => teacher.Id == item.TeacherId).TeacherName
                });

            }
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
                    new Label
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
                    },
                    listView

                }
            };

        }
       
        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
           
            TestString testString = (TestString)e.Item;
            Test test = Tests.Find(test => test.Name == testString.Name);
            Navigation.PushAsync(new OneTestPage(test, testString));
        }

    }
}