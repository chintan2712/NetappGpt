using Microsoft.AspNetCore.Mvc;

namespace NetappGpt.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    // ✅ Get token details by transaction id (mock data)
    [HttpGet("{txId}")]
    public IActionResult GetTransactionDetails(string txId)
    {
        // Mock sample lookup
        var sampleTx = new
        {
            transactionId = txId,
            blockchain = "Ethereum",
            timestamp = DateTime.UtcNow.AddMinutes(-15),
            from = "0xAbC123...def",
            to = "0xDeF456...abc",
            status = "success",
            tokens = new[]
            {
                new {
                    symbol = "USDT",
                    name = "Tether",
                    type = "Stablecoin",
                    amount = 1000.0,
                    contractAddress = "0xdAC17F958D2ee523a2206206994597C13D831ec7",
                    decimals = 6
                },
                new {
                    symbol = "ETH",
                    name = "Ethereum",
                    type = "Native Token",
                    amount = 0.05,
                    contractAddress = "native",
                    decimals = 18
                }
            }
        };

        return Ok(sampleTx);
    }
}
