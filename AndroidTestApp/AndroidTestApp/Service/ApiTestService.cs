using AndroidTestApp.Controllers;
using AndroidTestApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AndroidTestApp.Service
{
    public class ApiTestService
    {
        TestController testController;

        public ApiTestService(TestController controller)
        {
            testController = controller;
        }
        public Task<List<Test>> GetTestAsync()
        {
            return testController.GetTestAsync();
        }
        public Task PostTestAsync(Test test)
        {
            return testController.PostTestAsync(test);
        }
        public Task PutTestAsync(Test test)
        {
            return testController.PutTestAsync(test);
        }
        public Task<Test> GetTestByIdAsync(int id)
        {
            return testController.GetTestByIdAsync(id);
        }
        public Task DeleteTestAsync(int id)
        {
            return testController.DeleteTestAsync(id);
        }
    }
}
