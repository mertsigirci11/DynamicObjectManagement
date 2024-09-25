using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObjectManagement.Core.Models
{
    public class Product : BaseObject
    {
        [MinLength(2, ErrorMessage = "Product name should consist of at least 2 characters.")]
        [MaxLength(50, ErrorMessage = "Product name should not consist of more than 50 characters.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Product name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Product price is required.")]
        [Range(1,double.MaxValue, ErrorMessage = "Product price should be at least 1.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Product quantity is required.")]
        [Range(1, double.MaxValue, ErrorMessage = "Product quantity should be at least 1.")]
        public int Quantity { get; set; }
    }
}
