using AndroidTestApp.Controllers;
using AndroidTestApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AndroidTestApp.Service
{
    public class ApiSubjectService
    {
        SubjectController SubjectController;
        public ApiSubjectService(SubjectController controller)
        {
            SubjectController = controller;
        }
        public Task<List<Subject>> GetSubjectAsync()
        {
            return SubjectController.GetSubjectAsync();
        }
        public Task PostSubjectAsync(Subject Subject)
        {
            return SubjectController.PostSubjectAsync(Subject);
        }
        public Task PutSubjectAsync(Subject Subject)
        {
            return SubjectController.PutSubjectAsync(Subject);
        }

        public Task<Subject> GetSubjectByIdAsync(int id)
        {
            return SubjectController.GetSubjectByIdAsync(id);
        }
        public Task DeleteSubjectAsync(int id)
        {
            return SubjectController.DeleteSubjectAsync(id);
        }
    }
}
