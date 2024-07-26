using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BadmintonReservationData.Base;

namespace JewelryAuctionData.Entity
{
    public partial class Payment : BaseEntity
    {
        [Key] public int PaymentId { get; set; }
        public int? AuctionResultId { get; set; }
        public string? PaymentMethod { get; set; }
        public decimal? TotalPrice { get; set; }
        public DateTime? PaymentTime { get; set; }
        public int? CustomerId { get; set; }
        public decimal? FinalPrice { get; set; }
        public int? JewelryId { get; set; }
        public decimal? Fees { get; set; }
        public decimal? Percent { get; set; }
        public string? PaymentStatus { get; set; }

        public virtual AuctionResult? AuctionResult { get; set; }
        public virtual Customer? Customer { get; set; }
        public virtual Jewelry? Jewelry { get; set; }
    }
}