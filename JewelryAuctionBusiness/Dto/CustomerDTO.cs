using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using JewelryAuctionData.Entity;

namespace JewelryAuctionData.Dto
{
    public class CustomerDTO
    {
        [Key]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Customer name is required")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        public string Phone { get; set; }

        public int? CompanyId { get; set; }
        public CompanyDTO? Company  { get; set; }

        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
    }
    public class CompanyDTO
    {
        [Key]
        public int CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public string? Address { get; set; }
        public string? Discription { get; set; }
        public string? Email { get; set; }
    }
}
