using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObjectManagement.Service.Enums
{
    public static class ExceptionResponseConstants
    {
        public static readonly string CUSTOMER_MISSINGFIELDS_EX = "ObjectData field should contain 'Name', 'Surname', 'Age' fields.";
        public static readonly string PRODUCT_MISSINGFIELDS_EX = "ObjectData field should contain 'Name', 'Price', 'Quantity' fields.";
        public static readonly string ORDER_MISSINGFIELDS_EX = "ObjectData field should contain 'CustomerId' field.";
        public static readonly string ORDEREDPRODUCT_MISSINGFIELDS_EX = "ObjectData field should contain 'OrderId', 'ProductId', 'ProductPrice' , 'Quantity' fields.";
    }
}
