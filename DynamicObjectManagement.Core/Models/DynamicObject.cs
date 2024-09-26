using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DynamicObjectManagement.Core.Models
{
    public class DynamicObject : BaseObject
    {
        [Required(ErrorMessage = "ObjectType is required.")]
        public int ObjectType { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Object data can not be empty or null.")]
        public string ObjectData { get; set; }
    }
}
