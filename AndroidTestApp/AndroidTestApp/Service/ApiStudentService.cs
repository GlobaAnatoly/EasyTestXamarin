using AndroidTestApp.Controllers;
using AndroidTestApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AndroidTestApp.Service
{
   public class ApiStudentService
    {
        StudentController StudentController;
        public ApiStudentService(StudentController controller)
        {
            StudentController = controller;
        }
        public Task<List<Student>> GetStudentAsync()
        {
            return StudentController.GetStudentAsync();
        }
        public Task PostStudentAsync(Student Student)
        {
            return StudentController.PostStudentAsync(Student);
        }
        public Task PutStudentAsync(Student Student)
        {
            return StudentController.PutStudentAsync(Student);
        }

        public Task<Student> GetStudentByIdAsync(int id)
        {
            return StudentController.GetStudentByIdAsync(id);
        }
        public Task DeleteStudentAsync(int id)
        {
            return StudentController.DeleteStudentAsync(id);
        }
    }
}
