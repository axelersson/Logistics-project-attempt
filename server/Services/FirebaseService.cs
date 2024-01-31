using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;
public class FirebaseService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseAddress;

    public FirebaseService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _baseAddress = "https://testingdotnetandfirebase-default-rtdb.europe-west1.firebasedatabase.app/"; // Your Firebase project URL
    }

    public async Task<HttpResponseMessage> PostDataAsync<T>(string path, T data)
    {
        var fullPath = $"{_baseAddress}{path}.json"; // Firebase requires the .json suffix
        
    var content = new StringContent(
        JsonConvert.SerializeObject(data), 
        Encoding.UTF8, 
        MediaTypeHeaderValue.Parse("application/json"));
        return await _httpClient.PostAsync(fullPath, content);
    }
}
