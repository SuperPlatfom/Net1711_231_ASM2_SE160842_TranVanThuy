using System;
using System.Collections.Generic;
using BadmintonReservationData.Base;

namespace JewelryAuctionData.Entity
{
    public partial class Account : BaseEntity
    {
        public int CustomerId { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Role { get; set; }

        public virtual Customer Customer { get; set; } = null!;
    }
}
