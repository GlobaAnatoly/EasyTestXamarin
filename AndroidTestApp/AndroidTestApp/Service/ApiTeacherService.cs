using AndroidTestApp.Controllers;
using AndroidTestApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AndroidTestApp.Service
{
    public class ApiTeacherService
    {
        TeacherController teacherController;
        public ApiTeacherService(TeacherController controller)
        {
            teacherController = controller;
        }
        public Task<List<Teacher>> GetTeacherAsync()
        {
            return teacherController.GetTeacherAsync();
        }
        public Task PostTeacherAsync(Teacher teacher)
        {
            return teacherController.PostTeacherAsync(teacher);
        }
        public Task PutTeacherAsync(Teacher teacher)
        {
            return teacherController.PutTeacherAsync(teacher);
        }
        
        public Task<Teacher> GetTeacherByIdAsync(int id)
        {
            return teacherController.GetTeacherByIdAsync(id);
        }
        public Task DeleteTeacherAsync(int id)
        {
            return teacherController.DeleteTeacherAsync(id);
        }
    }
}
