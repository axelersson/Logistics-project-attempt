using Newtonsoft.Json;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;

public class FirebaseService

{
    private readonly HttpClient _httpClient;
    private readonly string _baseAddress;
    private readonly ILogger<UserController> _logger;
    public FirebaseService(HttpClient httpClient, ILogger<UserController> logger)
    {
        _httpClient = httpClient;
        _baseAddress = "https://testingdotnetandfirebase-default-rtdb.europe-west1.firebasedatabase.app/"; // Your Firebase project URL
        _logger = logger;
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

public async Task<HttpResponseMessage> DeleteDataAsync(string path)
{
    _logger.LogError($"An error occurred when calling DeleteAsync");
    try
    {
        var fullPath = $"{_baseAddress}{path}.json";
        _logger.LogInformation($"Attempting to delete: {fullPath}");
        return await _httpClient.DeleteAsync(fullPath);
    }
    catch (Exception ex)
    {
        _logger.LogError($"An error occurred when calling DeleteAsync: {ex}");
        throw; // Re-throw the exception to handle it further up the call stack
    }
}

public async Task<HttpResponseMessage> PutDataAsync<T>(string path, T data)
{
    var fullPath = $"{_baseAddress}{path}.json"; // Firebase requires the .json suffix
    var content = new StringContent(
        JsonConvert.SerializeObject(data),
        Encoding.UTF8,
        "application/json");

    return await _httpClient.PutAsync(fullPath, content);
}


}
