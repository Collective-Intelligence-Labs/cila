
using Microsoft.AspNetCore.Mvc;
using cila.Domain.Database.Services;
using cila.Domain.Database.Documents;

namespace cila.Readmodel.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NftController : ControllerBase
{
    private readonly NftService nftService;

    public NftController(NftService nftService)
    {
        this.nftService = nftService;
    }

    [HttpGet]
    public IEnumerable<NFTDocument> GetAll()
    {
        return nftService.FindAllNfts();
    }

    [HttpGet("{id}")]
    public NFTDocument Get(string id)
    {
        return nftService.FindOneNft(id);
    }

    [HttpGet("/owner/{id}")]
    public IEnumerable<NFTDocument> GetByOwner(string id)
    {
        return nftService.FindAllNfts(id);
    }

}
