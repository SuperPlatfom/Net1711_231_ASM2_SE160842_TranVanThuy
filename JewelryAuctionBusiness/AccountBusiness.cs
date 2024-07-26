using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JewelryAuctionBusiness.Dto;
using JewelryAuctionData;
using JewelryAuctionData.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace JewelryAuctionBusiness;

public class AccountBusiness
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;

    public AccountBusiness(UnitOfWork unitOfWork, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
    }

    public async Task<IBusinessResult> LoginAsync(LoginDTO loginDto)
    {
        var account = await _unitOfWork.AccountRepository.LoginAsync(loginDto.Username, loginDto.Password);

        if (account == null)
        {
            return new BusinessResult(401, "Account incorrect Username or Password.", null);
        }

        var token = GenerateJwtToken(account.Username, account.Role);
        return new BusinessResult(200, "Login successfully.", token);
    }

    private string GenerateJwtToken(string username, string role)
    {
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
        var securityKey = new SymmetricSecurityKey(key);
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim("username", username),
            new Claim(ClaimTypes.Role, role)
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
