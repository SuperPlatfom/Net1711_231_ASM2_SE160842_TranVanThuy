using System.ComponentModel.DataAnnotations;
using JewelryAuctionData.Dto;

namespace JewelryAuctionBusiness.Dto;

public class RequestAuctionDetailsDto
{
    [Key] public int RequestDetailID { get; set; }
    public int RequestID { get; set; }
    public int CustomerID { get; set; }
    public string CustomerName { get; set; }
    public int JewelryID { get; set; }
    public string JewelryName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string Status { get; set; }  // Example values might include 'Pending', 'Approved', 'Rejected'
}
public class CreateJewelryAndAuctionDto
{
    public int CustomerID { get; set; }
    public string? JewelryName { get; set; }
    public string? Discription { get; set; }
    public int Quantity  { get; set; }
    public decimal Price { get; set; }
    public string Status  { get; set; }
}