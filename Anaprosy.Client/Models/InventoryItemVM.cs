using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Anaprosy.Client.Models
{
    public class InventoryItemVM
    {
        public Guid Id { get; set; }

        [Column(TypeName = "decimal(32,8)")]
        public decimal Quantity { get; set; }
        public Guid ProductId { get; set; }
        public ProductVM Product { get; set; }
        public Guid InventoryId { get; set; }
        public InventoryVM Inventory { get; set; }
    }
}