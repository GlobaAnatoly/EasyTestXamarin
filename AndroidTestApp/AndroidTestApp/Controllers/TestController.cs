using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using AndroidTestApp.Models;
using System.Text.Json;
using System.Diagnostics;

namespace AndroidTestApp.Controllers
{
    public class TestController 
    {
        static HttpClient client = new HttpClient();
       static JsonSerializerOptions serializerOptions;
        private static string apiPath = "http://10.0.2.2:5232/api";
        public TestController()
        {
            client = new HttpClient();
            serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };
        }
        public async Task<List<Test>> GetTestAsync()
        {
             List<Test> tests = new List<Test>();
            try
            {
                HttpResponseMessage response = await client.GetAsync(String.Concat(apiPath, "/tests/"));
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    tests = JsonSerializer.Deserialize<List<Test>>(content, serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return tests;
        }
        public async Task<Test> GetTestByIdAsync(int id)
        {
            Test testTmp = new Test();
            try
            {
                HttpResponseMessage response = await client.GetAsync(String.Concat(apiPath, "/tests/", id.ToString()));
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    testTmp = JsonSerializer.Deserialize<Test>(content, serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return testTmp;
        }
        public async Task PostTestAsync(Test test)
        {
            try
            {
                string json = JsonSerializer.Serialize<Test>(test, serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(string.Concat(apiPath, "/tests/"), content);
                if (response.IsSuccessStatusCode)
                    Debug.WriteLine(@"/tPOST: Test saved");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }
       public async Task PutTestAsync(Test test)
        {
            try
            {
                string json = JsonSerializer.Serialize<Test>(test, serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(string.Concat(apiPath, "/tests/", test.Id), content);
                if (response.IsSuccessStatusCode)
                    Debug.WriteLine(@"/tPUT: Test saved");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }
        public async Task DeleteTestAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync(string.Concat(apiPath, "/tests/", id));
                if(response.IsSuccessStatusCode)
                    Debug.WriteLine(@"/tDELETE: Test deleted");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }
    }
}
