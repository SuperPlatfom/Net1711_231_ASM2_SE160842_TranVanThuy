using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BadmintonReservationData.Base;

namespace JewelryAuctionData.Entity
{
    public partial class AuctionSection: BaseEntity
    {
        public AuctionSection()
        {
            AuctionResults = new HashSet<AuctionResult>();
        }

        [Key]
        public int AuctionId { get; set; }
        public int? JewelryId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? Status { get; set; }
        public string? Discription { get; set; }
        public decimal? InitialPrice { get; set; }
        public int? BidderId { get; set; }
        public int? RequestDetailId { get; set; }

        public virtual Bidder? Bidder { get; set; }
        public virtual Jewelry? Jewelry { get; set; }
        public virtual RequestAuctionDetail? RequestDetail { get; set; }
        public virtual ICollection<AuctionResult> AuctionResults { get; set; }
    }
}
