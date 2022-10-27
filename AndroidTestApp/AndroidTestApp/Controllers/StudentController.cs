using AndroidTestApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AndroidTestApp.Controllers
{
    public class StudentController
    {
        static HttpClient client = new HttpClient();
        static JsonSerializerOptions serializerOptions;
        private static string apiPath = "http://10.0.2.2:5232/api/students/";
        public StudentController()
        {
            client = new HttpClient();
            serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };
        }
        public async Task<List<Student>> GetStudentAsync()
        {
            List<Student> Students = new List<Student>();
            try
            {
                HttpResponseMessage response = await client.GetAsync(String.Concat(apiPath));
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Students = JsonSerializer.Deserialize<List<Student>>(content, serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return Students;
        }
        public async Task<Student> GetStudentByIdAsync(int id)
        {
            Student StudentsTmp = new Student();
            try
            {
                HttpResponseMessage response = await client.GetAsync(String.Concat(apiPath, id.ToString()));
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    StudentsTmp = JsonSerializer.Deserialize<Student>(content, serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return StudentsTmp;
        }
        public async Task PostStudentAsync(Student Student)
        {
            try
            {
                string json = JsonSerializer.Serialize<Student>(Student, serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(String.Concat(apiPath), content);
                if (response.IsSuccessStatusCode)
                    Debug.WriteLine(@"/tPOST: Student saved");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }
        public async Task PutStudentAsync(Student Student)
        {
            try
            {
                string json = JsonSerializer.Serialize<Student>(Student, serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(string.Concat(apiPath, Student.Id), content);
                if (response.IsSuccessStatusCode)
                    Debug.WriteLine(@"/tPUT: Student saved");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }
        public async Task DeleteStudentAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync(string.Concat(apiPath, id));
                if (response.IsSuccessStatusCode)
                    Debug.WriteLine(@"/tDELETE: Student deleted");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }
    }
}
