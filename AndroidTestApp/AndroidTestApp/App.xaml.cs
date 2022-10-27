using AndroidTestApp.Controllers;
using AndroidTestApp.Models;
using AndroidTestApp.Service;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AndroidTestApp
{
    public partial class App : Application
    {
        public static Student Student { get; set; }
        public static bool IsLoggedIn { get; set; }
        public static ApiTestService TestService { get; private set; }
        public static ApiTeacherService TeacherService { get; private set; }
        public static ApiSubjectService SubjectService { get; private set; }
        public static ApiStudentsTestService StudentsTestService { get; private set; }
        public static ApiStudentService StudentService { get; private set; }
        public static ApiQuestionTypeService QuestionTypeService { get; private set; }
        public static ApiQuestionService QuestionService { get; private set; }
        public static ApiAnswerVariantService AnswerVariantService { get; private set; }
        public App()
        {
            InitializeComponent();
            TestService = new ApiTestService(new TestController());
            SubjectService = new ApiSubjectService(new SubjectController());
            StudentsTestService = new ApiStudentsTestService(new StudentsTestController());
            StudentService = new ApiStudentService(new StudentController());
            QuestionTypeService = new ApiQuestionTypeService(new QuestionTypeController());
            QuestionService = new ApiQuestionService(new QuestionController());
            AnswerVariantService = new ApiAnswerVariantService(new AnswerVariantController());
            TeacherService = new ApiTeacherService(new TeacherController());
            //MainPage = new MainPage();
            if (!IsLoggedIn)
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                Student = new Student();
                MainPage = new NavigationPage(new AndroidTestApp.MainPage());
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
