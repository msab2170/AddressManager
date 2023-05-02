using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AddressManager.Models
{
    // Dependent (child)
    [PrimaryKey(nameof(Id), nameof(UserId))]
    public class Address
    {
     
        public int Id { get; set; }

        [ForeignKey(nameof(UserId))]
        public int UserId { get; set; }

        [StringLength(5)]
        public String Zipcode { get; set; }
        [Display(Name = "Street Address")]
        [StringLength(50)]
        public String Address1 { get; set; }
        [Display(Name = "Detail Address")]
        [StringLength(50)]
        public String? Address2 { get; set; }
        public User User { get; set; } = null!;
    }
}
