using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Anaprosy.Data.Models
{
    public class InventoryItemDM
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [Column(TypeName = "decimal(32,8)")]
        public decimal Quantity { get; set; }

        public Guid ProductId { get; set; }
        public ProductDM Product { get; set; }
        public Guid InventoryId { get; set; }
        public InventoryDM Inventory { get; set; }
    }
}