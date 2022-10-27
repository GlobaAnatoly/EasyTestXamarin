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
    public class StudentsTestController
    {
        static HttpClient client = new HttpClient();
        static JsonSerializerOptions serializerOptions;
        private static string apiPath = "http://10.0.2.2:5232/api/studentstests/";
        public StudentsTestController()
        {
            client = new HttpClient();
            serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };
        }
        public async Task<List<StudentsTest>> GetStudentsTestAsync()
        {
            List<StudentsTest> StudentsTests = new List<StudentsTest>();
            try
            {
                HttpResponseMessage response = await client.GetAsync(String.Concat(apiPath));
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    StudentsTests = JsonSerializer.Deserialize<List<StudentsTest>>(content, serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return StudentsTests;
        }
        public async Task<StudentsTest> GetStudentsTestByIdAsync(int id)
        {
            StudentsTest StudentsTestsTmp = new StudentsTest();
            try
            {
                HttpResponseMessage response = await client.GetAsync(String.Concat(apiPath, id.ToString()));
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    StudentsTestsTmp = JsonSerializer.Deserialize<StudentsTest>(content, serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return StudentsTestsTmp;
        }
        public async Task PostStudentsTestAsync(StudentsTest StudentsTest)
        {
            try
            {
                string json = JsonSerializer.Serialize<StudentsTest>(StudentsTest, serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(apiPath, content);
                if (response.IsSuccessStatusCode)
                    Debug.WriteLine(@"/tPOST: StudentsTest saved");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }
        public async Task PutStudentsTestAsync(StudentsTest StudentsTest)
        {
            try
            {
                string json = JsonSerializer.Serialize<StudentsTest>(StudentsTest, serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(string.Concat(apiPath, StudentsTest.Id), content);
                if (response.IsSuccessStatusCode)
                    Debug.WriteLine(@"/tPUT: StudentsTest saved");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }
        public async Task DeleteStudentsTestAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync(string.Concat(apiPath, id));
                if (response.IsSuccessStatusCode)
                    Debug.WriteLine(@"/tDELETE: StudentsTest deleted");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }
    }
}
