using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace c_vee.Web.Common
{
    public class HttpHelper
    {
        public static Tuple<string, StringContent> BuildReqContent<T>(HttpClient client, string _baseURL, T model, string PartOfUrl)
        {
            client.BaseAddress = new Uri(_baseURL);
            client.DefaultRequestHeaders.Accept.Clear();
            var serializedModel = JsonConvert.SerializeObject(model);
            var currentUrl = Path.Combine(client.BaseAddress.ToString(), PartOfUrl);

            var content = new StringContent(serializedModel, Encoding.UTF8, "application/json");
            return new Tuple<string, StringContent>(currentUrl, content);
        }

        public static async Task<Tuple<string, HttpResponseMessage>> PostContentAsync<T>(string _baseURL, T model, string PartOfUrl)
        {
            var response = new HttpResponseMessage();
            using (var client = new HttpClient())
            {
                var contents = BuildReqContent<T>(client, _baseURL, model, PartOfUrl);
                response = await client.PostAsync(contents.Item1, contents.Item2);
            }
            var responseObj = await response.Content.ReadAsStringAsync();
            return new Tuple<string, HttpResponseMessage>(responseObj, response);
        }
        public static async Task<Tuple<string, HttpResponseMessage>> PostContentAsync<T>(string _baseURL, T model, string token, string PartOfUrl)
        {
            var response = new HttpResponseMessage();
            using (var client = new HttpClient())
            {
                var contents = BuildReqContent<T>(client, _baseURL, model, PartOfUrl);
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                response = await client.PostAsync(contents.Item1, contents.Item2);
            }
            var responseObj = await response.Content.ReadAsStringAsync();
            return new Tuple<string, HttpResponseMessage>(responseObj, response);
        }
        public static async Task<Tuple<string, HttpResponseMessage>> PatchContentAsync<T>(string _baseURL, T model, string token, string PartOfUrl)
        {

            var response = new HttpResponseMessage();
            using (var client = new HttpClient())
            {
                var contents = BuildReqContent<T>(client, _baseURL, model, PartOfUrl);
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                response = await client.PatchAsync(contents.Item1, contents.Item2);
            }
            var responseObj = await response.Content.ReadAsStringAsync();
            return new Tuple<string, HttpResponseMessage>(responseObj, response);
        }

        public static async Task<Tuple<string, HttpResponseMessage>> GetContentWithoutTokenAsync<T>(string _baseURL, T model, string PartOfUrl)
        {
            var response = new HttpResponseMessage();
            using (var client = new HttpClient())
            {
                var contents = BuildReqContent<T>(client, _baseURL, model, PartOfUrl);
                response = await client.GetAsync(contents.Item1);
            }
            var responseObj = await response.Content.ReadAsStringAsync();
            return new Tuple<string, HttpResponseMessage>(responseObj, response);
        }
        public static async Task<Tuple<string, HttpResponseMessage>> GetContentWithTokenAsync<T>(string _baseURL, T model, string token, string PartOfUrl)
        {
            var response = new HttpResponseMessage();
            using (var client = new HttpClient())
            {
                var contents = BuildReqContent<T>(client, _baseURL, model, PartOfUrl);
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                response = await client.GetAsync(contents.Item1);
            }
            var responseObj = await response.Content.ReadAsStringAsync();
            return new Tuple<string, HttpResponseMessage>(responseObj, response);
        }
        public static async Task<Tuple<string, HttpResponseMessage>> PutContentAsync<T>(string _baseURL, T model, string token, string PartOfUrl)
        {
            var response = new HttpResponseMessage();
            using (var client = new HttpClient())
            {
                var contents = BuildReqContent<T>(client, _baseURL, model, PartOfUrl);
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                response = await client.PutAsync(contents.Item1, contents.Item2);
            }
            var responseObj = await response.Content.ReadAsStringAsync();
            return new Tuple<string, HttpResponseMessage>(responseObj, response);
        }
        public static async Task<Tuple<string, HttpResponseMessage>> UploadFileAsync<T>(string _baseURL, T model, string token, string PartOfUrl)
        {
            var response = new HttpResponseMessage();
            using (var client = new HttpClient())
            {
                MultipartFormDataContent form = new MultipartFormDataContent();
                IFormFile file = model as IFormFile;
                HttpContent content = new StringContent(file.FileName);
                client.BaseAddress = new Uri(_baseURL);
                var currentUrl = Path.Combine(client.BaseAddress.ToString(), PartOfUrl);
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                var filecontent = CreateFileContent(file.OpenReadStream(), file.FileName, file.ContentType);
                form.Add(filecontent);

                response = await client.PatchAsync(currentUrl, form);
            }
            var responseObj = await response.Content.ReadAsStringAsync();
            return new Tuple<string, HttpResponseMessage>(responseObj, response);
        }
        private static StreamContent CreateFileContent(Stream stream, string fileName, string contentType)
        {
            var fileContent = new StreamContent(stream);
            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "UserPhoto",
                FileName = fileName
            };

            fileContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            return fileContent;
        }

    }
}
