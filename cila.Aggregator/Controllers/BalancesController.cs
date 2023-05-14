
using Microsoft.AspNetCore.Mvc;
using cila.Domain.Database.Services;
using cila.Domain.Database.Documents;

namespace cila.Readmodel.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BalancesController : ControllerBase
{
    private readonly BalancesService balances;

    public BalancesController(BalancesService balances)
    {
        this.balances = balances;
    }

    [HttpGet]
    public IEnumerable<BalanceDocument> GetAll()
    {
        return balances.GetAll();
    }

    [HttpGet("{id}")]
    public IEnumerable<BalanceDocument> GetByAccount(string id)
    {
        return balances.GetAllAccountBalances(id);
    }

    [HttpGet("/dao/{id}")]
    public IEnumerable<BalanceDocument> GetByDao(string id)
    {
        return balances.GetAllDaoBalances(id);
    }
    
    [HttpGet("/dao/{daoId}/account/{accountId}")]
    public IEnumerable<BalanceDocument> GetByDao(string daoId, string accountId)
    {
        return balances.GetAllAccountBalancesInDao(daoId, accountId);
    }
}
