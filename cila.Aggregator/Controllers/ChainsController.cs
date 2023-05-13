using cila.Domain.Database;
using cila.Domain.Database.Documents;
using cila.Domain.Database.Services;
using Microsoft.AspNetCore.Mvc;

namespace cil_aggregator.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChainsController : ControllerBase
{
    private readonly ChainsService chains;

    public ChainsController(ChainsService db)
    {
        chains = db;
    }

    [HttpGet]
    public IEnumerable<ChainDocument> GetAl()
    {
        return chains.GetAll();
    }

    [HttpGet("/tokens")]
    public IEnumerable<string> GetTokens()
    {
        return chains.GetAll().Select(x=> x.Symbol);
    }
}