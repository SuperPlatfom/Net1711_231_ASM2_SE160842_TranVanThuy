namespace JewelryAuctionBusiness.Dto;

public class AccountDTO
{
    public int CustomerId { get; set; }
    public string Username { get; set; }
    public string Role { get; set; }
}

public class LoginDTO
{
    public string Username { get; set; }
    public string Password { get; set; }
}