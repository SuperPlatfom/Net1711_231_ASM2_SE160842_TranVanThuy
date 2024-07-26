using System.ComponentModel.DataAnnotations;
using JewelryAuctionData.Dto;

namespace JewelryAuctionBusiness.Dto;
public class RequestAuctionDTO
{
    [Key] public int RequestID { get; set; }
    public int CustomerID { get; set; }
    public int JewelryID { get; set; }
    public JewelryDTO Jewelry { get; set; }
    public CustomerDTO Customer { get; set; }
}

public partial class JewelryDTO
{
    [Key]public int JewelryId { get; set; }
    public string? JewelryName { get; set; }
    public string? Discription { get; set; }
}