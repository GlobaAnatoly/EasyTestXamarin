using AndroidTestApp.Controllers;
using AndroidTestApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AndroidTestApp.Service
{
    public class ApiAnswerVariantService
    {
        AnswerVariantController AnswerVariantController;
        public ApiAnswerVariantService(AnswerVariantController controller)
        {
            AnswerVariantController = controller;
        }
        public Task<List<AnswerVariant>> GetAnswerVariantAsync()
        {
            return AnswerVariantController.GetAnswerVariantAsync();
        }
        public Task PostAnswerVariantAsync(AnswerVariant AnswerVariant)
        {
            return AnswerVariantController.PostAnswerVariantAsync(AnswerVariant);
        }
        public Task PutAnswerVariantAsync(AnswerVariant AnswerVariant)
        {
            return AnswerVariantController.PutAnswerVariantAsync(AnswerVariant);
        }

        public Task<AnswerVariant> GetAnswerVariantByIdAsync(int id)
        {
            return AnswerVariantController.GetAnswerVariantByIdAsync(id);
        }
        public Task DeleteAnswerVariantAsync(int id)
        {
            return AnswerVariantController.DeleteAnswerVariantAsync(id);
        }
    }
}
