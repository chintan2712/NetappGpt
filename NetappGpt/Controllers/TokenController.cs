using Microsoft.AspNetCore.Mvc;

namespace NetappGpt.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TokenController : ControllerBase
{
    // ✅ Get token details
    [HttpGet("{symbol}")]
    public IActionResult GetTokenDetails(string symbol)
    {
        var tokens = new Dictionary<string, object>
        {
            ["usdt"] = new
            {
                symbol = "USDT",
                name = "Tether",
                blockchain = "Ethereum",
                contractAddress = "0xdAC17F958D2ee523a2206206994597C13D831ec7",
                decimals = 6,
                type = "Stablecoin",
                priceUsd = 1.00,
                supply = "95B",
                holders = 4_000_000
            },
            ["bonk"] = new
            {
                symbol = "BONK",
                name = "Bonk",
                blockchain = "Solana",
                contractAddress = "So11111111111111111111111111111111111111112",
                decimals = 5,
                type = "Meme Coin",
                priceUsd = 0.00002,
                supply = "60T",
                holders = 900_000
            },
            ["apt"] = new
            {
                symbol = "APT",
                name = "Aptos Token",
                blockchain = "Aptos",
                contractAddress = "0x1::aptos_coin::AptosCoin",
                decimals = 8,
                type = "Utility / Governance",
                priceUsd = 9.5,
                supply = "1.1B",
                holders = 250_000
            }
        };

        if (!tokens.ContainsKey(symbol.ToLower()))
            return NotFound(new { message = $"Token '{symbol}' not found" });

        return Ok(tokens[symbol.ToLower()]);
    }

    // ✅ Get all tokens on a blockchain
    [HttpGet("blockchain/{chainId}")]
    public IActionResult GetTokensByBlockchain(string chainId)
    {
        var blockchainTokens = new Dictionary<string, List<object>>
        {
            ["sol"] = new List<object>
            {
                new { symbol = "BONK", name = "Bonk", type = "Meme Coin" },
                new { symbol = "USDC", name = "USD Coin", type = "Stablecoin" }
            },
            ["eth"] = new List<object>
            {
                new { symbol = "USDT", name = "Tether", type = "Stablecoin" },
                new { symbol = "UNI", name = "Uniswap", type = "Governance" }
            },
            ["apt"] = new List<object>
            {
                new { symbol = "APT", name = "Aptos Token", type = "Utility" }
            }
        };

        if (!blockchainTokens.ContainsKey(chainId.ToLower()))
            return NotFound(new { message = $"No tokens found for blockchain '{chainId}'" });

        return Ok(new { blockchain = chainId, tokens = blockchainTokens[chainId.ToLower()] });
    }

    // ✅ Get token price history (mocked)
    [HttpGet("{symbol}/price-history")]
    public IActionResult GetTokenPriceHistory(string symbol)
    {
        var sampleHistory = new[]
        {
            new { date = "2025-09-20", priceUsd = 8.5 },
            new { date = "2025-09-21", priceUsd = 8.9 },
            new { date = "2025-09-22", priceUsd = 9.1 },
            new { date = "2025-09-23", priceUsd = 9.3 },
            new { date = "2025-09-24", priceUsd = 9.5 }
        };

        return Ok(new { symbol = symbol.ToUpper(), history = sampleHistory });
    }
}
