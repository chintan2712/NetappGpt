using Microsoft.AspNetCore.Mvc;
using NetappGpt.Models;
using System.Net.Http;
using System.Text.Json;

namespace NetappGpt.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VolumesController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;

    public VolumesController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("NetAppClient");
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = true
        };
    }

    [HttpGet]
    public Task<IActionResult> GetVolumes()
    {
        var sampleJson = new
        {
            records = new[]
            {
                new {
                    uuid = "12345",
                    name = "vol1",
                    size = "100GB",
                    state = "online",
                    svm = new { name = "svm1" }
                },
                new {
                    uuid = "67890",
                    name = "vol2",
                    size = "200GB",
                    state = "online",
                    svm = new { name = "svm2" }
                }
            },
            num_records = 2
        };

        return Task.FromResult<IActionResult>(Ok(sampleJson));

        //var response = await _httpClient.GetAsync("/api/storage/volumes");

        //var content = await response.Content.ReadAsStringAsync();

        //if (!response.IsSuccessStatusCode)
        //    return StatusCode((int)response.StatusCode, new { error = content });

        //var json = JsonSerializer.Deserialize<object>(content, _jsonOptions);
        //return Ok(json);
    }

    [HttpGet("filter")]
    public async Task<IActionResult> GetFilteredVolumes([FromQuery] Dictionary<string, string>? filters)
    {
        string queryString = filters != null && filters.Any()
            ? "?" + string.Join("&", filters.Select(kv => $"{kv.Key}={Uri.EscapeDataString(kv.Value)}"))
            : string.Empty;

        var response = await _httpClient.GetAsync($"/api/storage/volumes{queryString}");
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            return StatusCode((int)response.StatusCode, new { error = content });
        }

        // Deserialize into something meaningful instead of object
        var json = JsonSerializer.Deserialize<JsonDocument>(content, _jsonOptions);
        return Ok(json);
    }

    [HttpPost]
    public IActionResult CreateVolume([FromBody] CreateVolumeRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdVolume = new
        {
            uuid = Guid.NewGuid().ToString(),
            name = request.Name,
            size = request.Size,
            state = "online",
            svm = new { name = request.SvmName }
        };

        return CreatedAtAction(nameof(GetVolumes), createdVolume);
    }

}
