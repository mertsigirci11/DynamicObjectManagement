using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObjectManagement.Core.Models
{
    public class DynamicObject : BaseObject
    {
        public int ObjectType { get; set; }
        public string ObjectData { get; set; }
    }
}
