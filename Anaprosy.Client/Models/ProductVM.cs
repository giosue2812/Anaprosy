using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anaprosy.Client.Models
{
    public class ProductVM
    {
        public Guid Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; }
    }
}