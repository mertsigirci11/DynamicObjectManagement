using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObjectManagement.Service.Exceptions
{
    public class ClientSideException : Exception
    {
        public ClientSideException(string msg) : base(msg) { } 
    }
}
