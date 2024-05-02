using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;
using testCleintPart.Contracts;
using testCleintPart.Models;

namespace testCleintPart.Services
{
    public class ImageApiService : IImageApiService
    {
        private readonly HttpClient _httpClient;
        private const string _baseUrl = "https://localhost:7271";

        public ImageApiService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(_baseUrl)
            };
        }

        public async Task<List<ImageData>> GetAllImages()
        {
            var response = await _httpClient.GetAsync("/api/Image/getAllImages");
            response.EnsureSuccessStatusCode();
            var responseResult = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ImageData>>(responseResult);
        }

        public async Task<bool> SendImage(ImageData imageData)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/api/Image/saveImg", imageData);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> EditImage(ImageData imageData)
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync("/api/Image/editImg", imageData);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteImage(string imageDataPath)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"/api/Image/deleteImg?imageDataPath={imageDataPath}");
            return response.IsSuccessStatusCode;
        }
    }
}
