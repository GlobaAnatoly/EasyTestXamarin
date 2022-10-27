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
    public class SubjectController
    {
        static HttpClient client = new HttpClient();
        static JsonSerializerOptions serializerOptions;
        private static string apiPath = "http://10.0.2.2:5232/api/subjects/";
        public SubjectController()
        {
            client = new HttpClient();
            serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };
        }
        public async Task<List<Subject>> GetSubjectAsync()
        {
            List<Subject> subjects = new List<Subject>();
            try
            {
                HttpResponseMessage response = await client.GetAsync(String.Concat(apiPath));
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    subjects = JsonSerializer.Deserialize<List<Subject>>(content, serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return subjects;
        }
        public async Task<Subject> GetSubjectByIdAsync(int id)
        {
            Subject SubjectsTmp = new Subject();
            try
            {
                HttpResponseMessage response = await client.GetAsync(String.Concat(apiPath, id.ToString()));
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    SubjectsTmp = JsonSerializer.Deserialize<Subject>(content, serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return SubjectsTmp;
        }
        public async Task PostSubjectAsync(Subject Subject)
        {
            try
            {
                string json = JsonSerializer.Serialize<Subject>(Subject, serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(apiPath, content);
                if (response.IsSuccessStatusCode)
                    Debug.WriteLine(@"/tPOST: Subject saved");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }
        public async Task PutSubjectAsync(Subject Subject)
        {
            try
            {
                string json = JsonSerializer.Serialize<Subject>(Subject, serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(string.Concat(apiPath, Subject.Id), content);
                if (response.IsSuccessStatusCode)
                    Debug.WriteLine(@"/tPUT: Subject saved");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }
        public async Task DeleteSubjectAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync(string.Concat(apiPath, id));
                if (response.IsSuccessStatusCode)
                    Debug.WriteLine(@"/tDELETE: Subject deleted");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }
    }
}

