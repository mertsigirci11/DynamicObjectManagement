using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObjectManagement.Service.Exceptions
{
    public class MissingModelFieldException : Exception
    {
        public MissingModelFieldException(string msg) : base(msg) { }
    }
}
