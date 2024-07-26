using JewelryAuctionBusiness;
using JewelryAuctionBusiness.Dto;
using Microsoft.AspNetCore.Mvc;

namespace JewelryAuctionWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly AccountBusiness _accountService;

    public AuthenticationController(AccountBusiness accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
    {
        var loginResult = await _accountService.LoginAsync(loginDto);
        return CreateResponse(loginResult);
    }

    private IActionResult CreateResponse(IBusinessResult result)
    {
        return result.Status switch
        {
            400 => BadRequest(result.Message),
            401 => Unauthorized(result.Message),
            404 => NotFound(result.Message),
            200 => Ok(new { token = result.Data, message = result.Message }),
            _ => StatusCode(500, "An internal server error occurred. Please try again later.")
        };
    }
}
