using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace NetappGpt.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BlockchainController : ControllerBase
{
    private readonly JsonSerializerOptions _jsonOptions;

    public BlockchainController()
    {
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = true
        };
    }

    // ✅ Get list of blockchains
    [HttpGet("chains")]
    public IActionResult GetBlockchains()
    {
        var sampleChains = new[]
        {
            new { id = "btc", name = "Bitcoin", priceUsd = 65000, marketCap = "1.2T", change24h = "+2.5%" },
            new { id = "eth", name = "Ethereum", priceUsd = 3200, marketCap = "380B", change24h = "-1.2%" },
            new { id = "sol", name = "Solana", priceUsd = 150, marketCap = "70B", change24h = "+5.6%" }
        };

        return Ok(new { records = sampleChains, num_records = sampleChains.Length });
    }

    // ✅ Get blockchain details by id
    [HttpGet("chains/{id}")]
    public IActionResult GetBlockchain(string id)
    {
        var chain = new { id = id, name = "Ethereum", priceUsd = 3200, marketCap = "380B", rank = 2 };
        return Ok(chain);
    }

    // ✅ Compare two blockchains
    [HttpGet("compare")]
    public IActionResult CompareBlockchains([FromQuery] string chain1, [FromQuery] string chain2)
    {
        var comparison = new
        {
            chain1 = new { id = chain1, priceUsd = 65000, txSpeed = "7 tps", fees = "high" },
            chain2 = new { id = chain2, priceUsd = 3200, txSpeed = "15 tps", fees = "medium" },
            verdict = $"{chain2} is faster, but {chain1} has higher market cap."
        };

        return Ok(comparison);
    }

    // ✅ Get token info
    [HttpGet("tokens/{symbol}")]
    public IActionResult GetToken(string symbol)
    {
        var token = new
        {
            symbol = symbol.ToUpper(),
            name = "USDT Tether",
            blockchain = "Ethereum",
            priceUsd = 1.00,
            supply = "95B"
        };

        return Ok(token);
    }

    // ✅ Price movement (up/down)
    [HttpGet("price-movement/{id}")]
    public IActionResult GetPriceMovement(string id)
    {
        var sampleData = new
        {
            id = id,
            currentPrice = 3200,
            change1h = "-0.3%",
            change24h = "+2.1%",
            change7d = "+10%"
        };

        return Ok(sampleData);
    }

    [HttpPost("watchlist")]
    public IActionResult AddToWatchlist([FromBody] TokenWatchRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Symbol))
        {
            return BadRequest(new { message = "Token symbol is required." });
        }

        var addedToken = new
        {
            symbol = request.Symbol.ToUpper(),
            addedAt = DateTime.UtcNow,
            status = "added_to_watchlist"
        };

        return CreatedAtAction(nameof(GetToken), new { symbol = request.Symbol }, addedToken);
    }
}

// ✅ Sample Request Model
public class TokenWatchRequest
{
    public string Symbol { get; set; } = string.Empty;
}
