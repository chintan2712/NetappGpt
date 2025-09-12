using Microsoft.AspNetCore.Mvc;

namespace NetappGpt.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SvmsController : ControllerBase
{
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
}
