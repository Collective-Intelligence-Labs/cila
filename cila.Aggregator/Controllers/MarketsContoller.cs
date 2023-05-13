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
        return marketsService.GetAll();
    }

    [HttpGet("{id}")]
    public MarketDocument Get(string id)
    {
        return marketsService.Get(id);
    }
}