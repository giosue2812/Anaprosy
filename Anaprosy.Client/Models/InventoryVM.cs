using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Anaprosy.Client.Models
{
    public class InventoryVM
    {
        public Guid Id { get; set; }

        public DateTime? Date { get; set; }

        public List<InventoryItemVM> Items { get; set; }
    }
}