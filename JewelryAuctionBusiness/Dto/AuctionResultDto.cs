using System.ComponentModel.DataAnnotations;

namespace JewelryAuctionBusiness.Dto;

public class AuctionResultDto
{
    [Key] public int AuctionResultId { get; set; }
    public int? AuctionId { get; set; }
    public int? BidderId { get; set; }
    public decimal? Amount { get; set; }
    public DateTime? TransactionTime { get; set; }
    public decimal? FinalPrice { get; set; }
    public  AuctionSectionDto? Auction { get; set; }
    public  BidderDto? Bidder { get; set; }
}