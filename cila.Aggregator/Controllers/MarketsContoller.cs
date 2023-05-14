using cila.Domain.Database;
using cila.Domain.Database.Documents;
using cila.Domain.Database.Services;
using Microsoft.AspNetCore.Mvc;

namespace cil_aggregator.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MarketsController : ControllerBase
{
    private readonly MarketsService marketsService;

    public MarketsController(MarketsService db)
    {
        this.marketsService = db;
    }

    [HttpGet]
    public IEnumerable<MarketDocument> GetAll()
    {
        marketsService.CreateNew(new MarketDocument{
            Id = "MARKET_ID",
            Owner = "0x8aeab625b8c29a087158fb44215a6852277ab35b",
            Asset1 = new AssetItem(){
                Symbol = "SepoiaETH",
                Value = 300,
            },
            Asset2 = new AssetItem(){
                Symbol = "GoerliETH",
                Value = 500,
            }
        });
        return marketsService.GetAll();
    }

    [HttpGet("{id}")]
    public MarketDocument Get(string id)
    {
        return marketsService.Get(id);
    }
}