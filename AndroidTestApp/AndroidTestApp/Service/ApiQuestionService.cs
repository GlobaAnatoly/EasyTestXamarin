using AndroidTestApp.Controllers;
using AndroidTestApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AndroidTestApp.Service
{
    public class ApiQuestionService
    {
        QuestionController QuestionController;
        public ApiQuestionService(QuestionController controller)
        {
            QuestionController = controller;
        }
        public Task<List<Question>> GetQuestionAsync()
        {
            return QuestionController.GetQuestionAsync();
        }
        public Task PostQuestionAsync(Question Question)
        {
            return QuestionController.PostQuestionAsync(Question);
        }
        public Task PutQuestionAsync(Question Question)
        {
            return QuestionController.PutQuestionAsync(Question);
        }

        public Task<Question> GetQuestionByIdAsync(int id)
        {
            return QuestionController.GetQuestionByIdAsync(id);
        }
        public Task DeleteQuestionAsync(int id)
        {
            return QuestionController.DeleteQuestionAsync(id);
        }
    }
}
