using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BadmintonReservationData.Base;

namespace JewelryAuctionData.Entity
{
    public partial class Bidder: BaseEntity
    {
        public Bidder()
        {
            AuctionResults = new HashSet<AuctionResult>();
            AuctionSections = new HashSet<AuctionSection>();
        }
        [Key]
        public int BidderId { get; set; }
        public int? CustomerId { get; set; }
        public decimal? CurrentBidPrice { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual ICollection<AuctionResult> AuctionResults { get; set; }
        public virtual ICollection<AuctionSection> AuctionSections { get; set; }
    }
}
