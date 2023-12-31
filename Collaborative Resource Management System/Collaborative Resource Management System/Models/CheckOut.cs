﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Collaborative_Resource_Management_System.Models
{
    public class CheckOut
    {
        public int CheckoutID { get; set; }
        public int InventoryItemID { get; set; }
        public DateTime CheckOutDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public float TotalPrice { get; set; }
        public int DepartmentID { get; set; }
        public string? Notes { get; set; }

        // Navigation properties
        public virtual IdentityUser AspNetUser { get; set; }
        public virtual InventoryItem Item { get; set; }
        public virtual Department Department { get; set; }
    }
}
