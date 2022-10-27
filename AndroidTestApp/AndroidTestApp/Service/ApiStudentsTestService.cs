using AndroidTestApp.Controllers;
using AndroidTestApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AndroidTestApp.Service
{
    public class ApiStudentsTestService
    {
        StudentsTestController StudentsTestController;
        public ApiStudentsTestService(StudentsTestController controller)
        {
            StudentsTestController = controller;
        }
        public Task<List<StudentsTest>> GetStudentsTestAsync()
        {
            return StudentsTestController.GetStudentsTestAsync();
        }
        public Task PostStudentsTestAsync(StudentsTest StudentsTest)
        {
            return StudentsTestController.PostStudentsTestAsync(StudentsTest);
        }
        public Task PutStudentsTestAsync(StudentsTest StudentsTest)
        {
            return StudentsTestController.PutStudentsTestAsync(StudentsTest);
        }

        public Task<StudentsTest> GetStudentsTestByIdAsync(int id)
        {
            return StudentsTestController.GetStudentsTestByIdAsync(id);
        }
        public Task DeleteStudentsTestAsync(int id)
        {
            return StudentsTestController.DeleteStudentsTestAsync(id);
        }
    }
}
