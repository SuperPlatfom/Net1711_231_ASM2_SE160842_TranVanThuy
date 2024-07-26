using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BadmintonReservationData.Base;

namespace JewelryAuctionData.Entity
{
    public partial class RequestAuction: BaseEntity
    {
        public RequestAuction()
        {
            RequestAuctionDetails = new HashSet<RequestAuctionDetail>();
        }

        [Key] public int RequestId { get; set; }
        public int? CustomerId { get; set; }
        public int? JewelryId { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual Jewelry? Jewelry { get; set; }
        public virtual ICollection<RequestAuctionDetail> RequestAuctionDetails { get; set; }
    }
}
