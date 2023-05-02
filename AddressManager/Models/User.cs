﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AddressManager.Models
{
    [PrimaryKey(nameof(Id))]
    public class User
    {

        public int Id { get; set; }
        [StringLength(20)]
        public string Password { get; set; }
        [StringLength(20)]
        public string Name { get; set; }
        [StringLength(50)]
        [Required]
        public string? Email { get; set; }
        [StringLength(20)]
        public string? Phone { get; set; }
        public ICollection<Address> Addresses { get; } = new List<Address>(); // Collection navigation containing dependents
        
    }
}
