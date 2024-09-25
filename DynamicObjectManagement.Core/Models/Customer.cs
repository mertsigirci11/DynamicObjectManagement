using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObjectManagement.Core.Models
{
    public class Customer : BaseObject
    {
        [MinLength(2,ErrorMessage = "Customer name should consist of at least 2 characters.")]
        [MaxLength(30, ErrorMessage = "Customer name should not consist of more than 30 characters.")]
        [Required(AllowEmptyStrings = false, ErrorMessage ="Customer name is required.")]
        public string Name { get; set; }

        [MinLength(2, ErrorMessage = "Customer surname should consist of at least 2 characters.")]
        [MaxLength(30, ErrorMessage = "Customer surname should not consist of more than 30 characters.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Customer surname is required.")]
        public string Surname { get; set; }

        [Range(18, Double.MaxValue, ErrorMessage ="Customer age should be older or equal than 18.")]
        public int Age { get; set; }
    }
}
