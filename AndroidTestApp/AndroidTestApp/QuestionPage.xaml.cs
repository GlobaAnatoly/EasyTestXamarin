using AndroidTestApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Android.App.Assist.AssistStructure;
using static Android.Content.ClipData;

namespace AndroidTestApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]


    public partial class QuestionPage : ContentPage
    {

        public Test Test { get; set; }
        public List<Question> Questions { get; set; }
        public List<AnswerVariant> AnswerVariants { get; set; }
        private Question question;
        public int QuestionCount { get; set; }
        public int Score { get; set; }
        public QuestionPage(Test test, int questionCount, int score)
        {
            InitializeComponent();
            Test = test;
            QuestionCount = questionCount;
            Score = score;
            question = new Question();
            //NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetHasBackButton(this, false);
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            Questions = await App.QuestionService.GetQuestionAsync();
            Questions = Questions.FindAll(question => question.TestId == Test.Id);
            question = Questions[QuestionCount];

            questionName.Text = question.Name;
            variantsLabel.Text = "Варианты ответа";
            if (question.Picture != null)
            {
                var stream = new MemoryStream(question.Picture);
                image.Source = ImageSource.FromStream(() => stream);
            }
            AnswerVariants = await App.AnswerVariantService.GetAnswerVariantAsync();
            AnswerVariants = AnswerVariants.FindAll(answer => answer.IdQuestion == question.Id);
            if (question.QuestionTypeId == 1)
            {
                collectionView.SelectionMode = SelectionMode.Single;
                collectionView.IsVisible = true;
                collectionView.ItemsSource = AnswerVariants;
                infoLabel.Text = "Выберете один вариант ответа";
            }
            if (question.QuestionTypeId == 2)
            {
                collectionView.SelectionMode = SelectionMode.Multiple;
                collectionView.IsVisible = true;
                collectionView.ItemsSource = AnswerVariants;
                answerButton.IsVisible = true;
                infoLabel.Text = "Выберете несколько вариантов ответов";
            }
            if (question.QuestionTypeId == 3)
            {
                collectionView.IsVisible = false;
                answerEntry.IsVisible = true;
                answerButton.IsVisible = true;
                infoLabel.Text = "Введите свой вариант ответа";
            }
        }

        private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (question.QuestionTypeId == 1)
            {
                var selected = e.CurrentSelection[0] as AnswerVariant;
                if (selected != null)
                {
                    var answer = await DisplayAlert("Ответ", $"Сохранить ответ?", "Да", "Нет");
                    if (answer)
                    {
                        if (selected.IsCorrect)
                        {
                            Score += question.Value;
                            QuestionCount += 1;
                        }
                        else if (!selected.IsCorrect)
                        {
                            Score += 0;
                            QuestionCount += 1;
                        }
                        if (QuestionCount == Questions.Count)
                        {
                            await Navigation.PushAsync(new TestResultPage(Test, Score));
                        }
                        else
                            await Navigation.PushAsync(new QuestionPage(Test, QuestionCount, Score));
                    }
                }
            }


        }

        private async void answerButton_Clicked(object sender, EventArgs e)
        {
            if (question.QuestionTypeId == 2)
            {
                List<AnswerVariant> rightAnswers = new List<AnswerVariant>();
                foreach (var item in collectionView.SelectedItems)
                {
                    rightAnswers.Add(item as AnswerVariant);
                }
                var selected = collectionView.SelectedItems as List<AnswerVariant>;
                if (rightAnswers.Count == 0)
                {
                    await DisplayAlert("Ошибка", $"Выберете хотя бы один ответ", "ОК");
                }
                else if (rightAnswers.Count != 0)
                {
                    var answer = await DisplayAlert("Ответ", $"Сохранить ответ?", "Да", "Нет");
                    if (answer)
                    {
                        int count = 0, answersCount = 0;
                        foreach (AnswerVariant answerV in AnswerVariants)
                        {
                            if (answerV.IsCorrect)
                                count++;
                        }
                        foreach (AnswerVariant item in rightAnswers)
                        {
                            if (item.IsCorrect)
                                answersCount++;
                        }
                        if (count == answersCount)
                            Score += question.Value;
                        QuestionCount++;
                        if (QuestionCount == Questions.Count)
                        {
                            await Navigation.PushAsync(new TestResultPage(Test, Score));
                        }
                        else
                            await Navigation.PushAsync(new QuestionPage(Test, QuestionCount, Score));
                    }
                    
                }
            }

            if (question.QuestionTypeId == 3)
            {
                if (answerEntry.Text == null)
                {
                    await DisplayAlert("Ошибка", $"Введите вариант ответа", "ОК");
                }
                else if (answerEntry.Text == "")
                    await DisplayAlert("Ошибка", $"Введите вариант ответа", "ОК");
                else
                {
                    var answer = await DisplayAlert("Ответ", $"Сохранить ответ?", "Да", "Нет");
                    if(answer)
                    {
                        string answerStr = answerEntry.Text.Trim().ToLower();
                        if (answerStr == AnswerVariants[0].Name.Trim().ToLower())
                            Score += question.Value;
                        QuestionCount++;
                        if (QuestionCount == Questions.Count)
                        {
                            await Navigation.PushAsync(new TestResultPage(Test, Score));
                        }
                        else
                            await Navigation.PushAsync(new QuestionPage(Test, QuestionCount, Score));
                    }
                    
                }
            }
        }
    }
}