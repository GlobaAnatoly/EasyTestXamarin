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
    public class QuestionController
    {
        static HttpClient client = new HttpClient();
        static JsonSerializerOptions serializerOptions;
        private static string apiPath = "http://10.0.2.2:5232/api/questions/";
        public QuestionController()
        {
            client = new HttpClient();
            serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };
        }
        public async Task<List<Question>> GetQuestionAsync()
        {
            List<Question> Questions = new List<Question>();
            try
            {
                HttpResponseMessage response = await client.GetAsync(String.Concat(apiPath));
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Questions = JsonSerializer.Deserialize<List<Question>>(content, serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return Questions;
        }
        public async Task<Question> GetQuestionByIdAsync(int id)
        {
            Question QuestionsTmp = new Question();
            try
            {
                HttpResponseMessage response = await client.GetAsync(String.Concat(apiPath, id.ToString()));
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    QuestionsTmp = JsonSerializer.Deserialize<Question>(content, serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return QuestionsTmp;
        }
        public async Task PostQuestionAsync(Question Question)
        {
            try
            {
                string json = JsonSerializer.Serialize<Question>(Question, serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(apiPath, content);
                if (response.IsSuccessStatusCode)
                    Debug.WriteLine(@"/tPOST: Question saved");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }
        public async Task PutQuestionAsync(Question Question)
        {
            try
            {
                string json = JsonSerializer.Serialize<Question>(Question, serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(string.Concat(apiPath, Question.Id), content);
                if (response.IsSuccessStatusCode)
                    Debug.WriteLine(@"/tPUT: Question saved");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }
        public async Task DeleteQuestionAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync(string.Concat(apiPath, id));
                if (response.IsSuccessStatusCode)
                    Debug.WriteLine(@"/tDELETE: Question deleted");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }
    }
}
