using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BadmintonReservationData.Base;

namespace JewelryAuctionData.Entity
{
    public partial class Company: BaseEntity
    {
        public Company()
        {
            Customers = new HashSet<Customer>();
        }
        [Key]
        public int CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public string? Address { get; set; }
        public string? Discription { get; set; }
        public string? Email { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
    }
}
