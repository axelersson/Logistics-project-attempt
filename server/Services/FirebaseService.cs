using Newtonsoft.Json;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;

public class FirebaseService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseAddress;

    public FirebaseService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _baseAddress = "https://testingdotnetandfirebase-default-rtdb.europe-west1.firebasedatabase.app/"; // Your Firebase project URL
    }

    public async Task<HttpResponseMessage> GetDataAsync(string path)
    {
        var fullPath = $"{_baseAddress}{path}.json"; // Firebase requires the .json suffix
        return await _httpClient.GetAsync(fullPath);
    }

    public async Task<HttpResponseMessage> PostDataAsync<T>(string path, T data)
    {
        var fullPath = $"{_baseAddress}{path}.json"; // Firebase requires the .json suffix
        var content = new StringContent(
            JsonConvert.SerializeObject(data), 
            Encoding.UTF8, 
            "application/json"); // Simplified media type setting
        return await _httpClient.PostAsync(fullPath, content);
    }
}
