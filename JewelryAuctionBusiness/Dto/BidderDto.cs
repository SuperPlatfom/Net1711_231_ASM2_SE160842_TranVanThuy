using System.ComponentModel.DataAnnotations;
using JewelryAuctionData.Dto;

namespace JewelryAuctionBusiness.Dto;

public class BidderDto
{
    [Key]
    public int BidderId { get; set; }
    public int? CustomerId { get; set; }
    public decimal? CurrentBidPrice { get; set; }
    public CustomerDTO CustomerDto { get; set; }
}

public class BidderAuction
{
    public int BidderId { get; set; }
    public decimal? CurrentBidPrice { get; set; }
    public int AuctionId { get; set; }
    public int CustomerId { get; set; }
}