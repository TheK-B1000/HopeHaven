﻿using System.ComponentModel.DataAnnotations;

namespace Collaborative_Resource_Management_System.Models
{
    public class CheckOut
    {
        public int CheckoutID { get; set; }
        public int UserID { get; set; }
        public int ItemID { get; set; }
        public DateTime CheckOutDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public float Price { get; set; }
        public int DepartmentID { get; set; }
        public string Notes { get; set; }

        // Navigation properties
        public virtual User User { get; set; }
        public virtual InventoryItem Item { get; set; }
        public virtual Department Department { get; set; }
    }
}
