using AndroidTestApp.Controllers;
using AndroidTestApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AndroidTestApp.Service
{
    public class ApiQuestionTypeService
    {
        QuestionTypeController QuestionTypeController;
        public ApiQuestionTypeService(QuestionTypeController controller)
        {
            QuestionTypeController = controller;
        }
        public Task<List<QuestionType>> GetQuestionTypeAsync()
        {
            return QuestionTypeController.GetQuestionTypeAsync();
        }
        public Task PostQuestionTypeAsync(QuestionType QuestionType)
        {
            return QuestionTypeController.PostQuestionTypeAsync(QuestionType);
        }
        public Task PutQuestionTypeAsync(QuestionType QuestionType)
        {
            return QuestionTypeController.PutQuestionTypeAsync(QuestionType);
        }

        public Task<QuestionType> GetQuestionTypeByIdAsync(int id)
        {
            return QuestionTypeController.GetQuestionTypeByIdAsync(id);
        }
        public Task DeleteQuestionTypeAsync(int id)
        {
            return QuestionTypeController.DeleteQuestionTypeAsync(id);
        }
    }
}
