using Microsoft.AspNetCore.Mvc;
using NetappGpt.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace NetappGpt.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SvmsController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;

    public SvmsController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("NetAppClient");
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = true
        };
    }

    // ----------------- GET ALL SVMS -----------------
    [HttpGet]
    public IActionResult GetSvms()
    {
        var sampleJson = new
        {
            records = new[]
            {
                new {
                    uuid = "svm-111",
                    name = "svm1",
                    state = "running",
                    ipspace = "Default"
                },
                new {
                    uuid = "svm-222",
                    name = "svm2",
                    state = "running",
                    ipspace = "Default"
                }
            },
            num_records = 2
        };

        return Ok(sampleJson);
    }

    // ----------------- CREATE SVM -----------------
    [HttpPost]
    public IActionResult CreateSvm([FromBody] CreateSvmRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var createdSvm = new
        {
            uuid = Guid.NewGuid().ToString(),
            name = request.Name,
            state = "running",
            ipspace = request.Ipspace
        };

        return CreatedAtAction(nameof(GetSvms), createdSvm);

        /*
        var json = JsonSerializer.Serialize(request, _jsonOptions);
        var response = await _httpClient.PostAsync("/api/svm/svms",
            new StringContent(json, Encoding.UTF8, "application/json"));

        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
            return StatusCode((int)response.StatusCode, new { error = content });

        var result = JsonSerializer.Deserialize<JsonDocument>(content, _jsonOptions);
        return Created("", result);
        */
    }
}
