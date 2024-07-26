using System.ComponentModel.DataAnnotations;
using JewelryAuctionData.Dto;
using JewelryAuctionData.Entity;

namespace JewelryAuctionBusiness.Dto;

public class AuctionSectionDto
{
    [Key]public int AuctionID { get; set; } 
    public int? JewelryID { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public string Status { get; set; }
    public string Discription { get; set; }
    public decimal? InitialPrice { get; set; }
    public int? BidderID { get; set; }
    public int? RequestDetailID { get; set; }
    public BidderDto  BidderDto { get; set; }
    public JewelryDTO  JewelryDto { get; set; }
}

public class AuctionSectionUpdateDto
{
    public int AuctionID { get; set; } 
    public int? JewelryID { get; set; }
    [Required(ErrorMessage = "Start Time is required.")]
    public DateTime? StartTime { get; set; }

    [Required(ErrorMessage = "End Time is required.")]
    public DateTime? EndTime { get; set; }

    [Required(ErrorMessage = "Status is required.")]
    [StringLength(50, ErrorMessage = "Status can't be longer than 50 characters.")]
    public string Status { get; set; }

    [StringLength(500, ErrorMessage = "Description can't be longer than 500 characters.")]
    public string Discription { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Initial Price must be a positive number.")]
    public decimal? InitialPrice { get; set; }
    public int? BidderID { get; set; }
    public int? RequestDetailID { get; set; }
}
