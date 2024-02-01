using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq; // For JObject

[ApiController]
[Route("api/[controller]")]
public class AreaController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly string _firebaseBaseUrl = "https://your-project-id.firebaseio.com/"; // Replace with your Firebase project ID

    public AreaController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }

    [HttpPost]
    public async Task<IActionResult> CreateArea([FromBody] AreaModel area)
    {
        try
        {
            var json = JsonConvert.SerializeObject(area);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_firebaseBaseUrl}/areas.json", content);

            response.EnsureSuccessStatusCode();

            return Ok("Area created successfully");
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to create area: {ex.Message}");
        }
    }

    [HttpPut("{areaId}")]
    public async Task<IActionResult> UpdateArea(string areaId, [FromBody] AreaModel updatedArea)
    {
        try
        {
            var json = JsonConvert.SerializeObject(updatedArea);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{_firebaseBaseUrl}/areas/{areaId}.json", content);

            response.EnsureSuccessStatusCode();

            return Ok("Area updated successfully");
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to update area: {ex.Message}");
        }
    }

    [HttpDelete("{areaId}")]
    public async Task<IActionResult> DeleteArea(string areaId)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"{_firebaseBaseUrl}/areas/{areaId}.json");

            response.EnsureSuccessStatusCode();

            return Ok("Area deleted successfully");
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to delete area: {ex.Message}");
        }
    }
}