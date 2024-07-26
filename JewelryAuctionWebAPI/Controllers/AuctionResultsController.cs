using JewelryAuctionBusiness;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace JewelryAuctionWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionResultsController : ControllerBase
    {
        private readonly AuctionResultBusiness _auctionResultBusiness;

        public AuctionResultsController(AuctionResultBusiness auctionResultBusiness)
        {
            _auctionResultBusiness = auctionResultBusiness;
        }
        [Authorize(Roles = "Manager, Admin")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _auctionResultBusiness.GetAllAuctionResultsAsync();
            return GenerateActionResult(result);
        }

        [HttpGet("{key}")]
        public async Task<IActionResult> Get(int key)
        {
            var result = await _auctionResultBusiness.GetAuctionResultByIdAsync(key);
            return GenerateActionResult(result);
        }

        private IActionResult GenerateActionResult(IBusinessResult result)
        {
            switch (result.Status)
            {
                case 400:
                    return BadRequest(result);
                case 404:
                    return NotFound(result);
                case 200:
                    return Ok(result.Data);
                default:
                    return StatusCode(500, "An internal server error occurred. Please try again later.");
            }
        }
    }
}