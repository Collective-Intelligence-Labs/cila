using cila.Domain.Database;
using cila.Domain.Database.Documents;
using cila.Domain.Database.Services;
using Microsoft.AspNetCore.Mvc;

namespace cil_aggregator.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OperationsController : ControllerBase
{
    private readonly OperationsService operationsService;

    public OperationsController(OperationsService db)
    {
        operationsService = db;
    }

    [HttpGet(Name = "GetAll")]
    public IEnumerable<OperationDocument> GetAl()
    {
        return operationsService.FindAllOperations();
    }

    [HttpGet("{id}")]
    public OperationDocument Get(string id)
    {
        return operationsService.FindOne(id);
    }
}

public class NftDto
{
    public string Hash { get; set; }
    
    public string Owner { get; set; }
}
