using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BadmintonReservationData.Base;

namespace JewelryAuctionData.Entity
{
    public partial class Jewelry: BaseEntity
    {
        public Jewelry()
        {
            AuctionSections = new HashSet<AuctionSection>();
            Payments = new HashSet<Payment>();
            RequestAuctionDetails = new HashSet<RequestAuctionDetail>();
            RequestAuctions = new HashSet<RequestAuction>();
        }
        [Key]
        public int JewelryId { get; set; }
        public string? JewelryName { get; set; }
        public string? Discription { get; set; }

        public virtual ICollection<AuctionSection> AuctionSections { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<RequestAuctionDetail> RequestAuctionDetails { get; set; }
        public virtual ICollection<RequestAuction> RequestAuctions { get; set; }
    }
}
