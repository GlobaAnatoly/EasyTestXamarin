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
    public class AnswerVariantController
    {
        static HttpClient client = new HttpClient();
        static JsonSerializerOptions serializerOptions;
        private static string apiPath = "http://10.0.2.2:5232/api/answerVariants/";
        public AnswerVariantController()
        {
            client = new HttpClient();
            serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };
        }
        public async Task<List<AnswerVariant>> GetAnswerVariantAsync()
        {
            List<AnswerVariant> AnswerVariants = new List<AnswerVariant>();
            try
            {
                HttpResponseMessage response = await client.GetAsync(String.Concat(apiPath));
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    AnswerVariants = JsonSerializer.Deserialize<List<AnswerVariant>>(content, serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return AnswerVariants;
        }
        public async Task<AnswerVariant> GetAnswerVariantByIdAsync(int id)
        {
            AnswerVariant AnswerVariantsTmp = new AnswerVariant();
            try
            {
                HttpResponseMessage response = await client.GetAsync(String.Concat(apiPath, id.ToString()));
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    AnswerVariantsTmp = JsonSerializer.Deserialize<AnswerVariant>(content, serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return AnswerVariantsTmp;
        }
        public async Task PostAnswerVariantAsync(AnswerVariant AnswerVariant)
        {
            try
            {
                string json = JsonSerializer.Serialize<AnswerVariant>(AnswerVariant, serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(apiPath, content);
                if (response.IsSuccessStatusCode)
                    Debug.WriteLine(@"/tPOST: AnswerVariant saved");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }
        public async Task PutAnswerVariantAsync(AnswerVariant AnswerVariant)
        {
            try
            {
                string json = JsonSerializer.Serialize<AnswerVariant>(AnswerVariant, serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(string.Concat(apiPath, AnswerVariant.Id), content);
                if (response.IsSuccessStatusCode)
                    Debug.WriteLine(@"/tPUT: AnswerVariant saved");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }
        public async Task DeleteAnswerVariantAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync(string.Concat(apiPath, id));
                if (response.IsSuccessStatusCode)
                    Debug.WriteLine(@"/tDELETE: AnswerVariant deleted");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }
    }
}
