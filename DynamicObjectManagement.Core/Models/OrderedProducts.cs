using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObjectManagement.Core.Models
{
    public class OrderedProducts : BaseObject
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product price is required.")]
        [Range(1, double.MaxValue, ErrorMessage = "Product price should be at least 1.")]
        public decimal ProductPrice { get; set; }

        [Required(ErrorMessage = "Product quantity is required.")]
        [Range(1, double.MaxValue, ErrorMessage = "Product quantity should be at least 1.")]
        public int Quantity { get; set; }
    }
}
