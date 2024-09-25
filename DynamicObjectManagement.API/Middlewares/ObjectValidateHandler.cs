using DynamicObjectManagement.Core.DTOs;
using DynamicObjectManagement.Core.Models;
using DynamicObjectManagement.Service.Enums;
using DynamicObjectManagement.Service.Validators;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;

namespace DynamicObjectManagement.API.Middlewares
{
    public class ObjectValidateHandler
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly ObjectValidator _objectValidator;

        public ObjectValidateHandler(RequestDelegate requestDelegate, ObjectValidator objectValidator)
        {
            _requestDelegate = requestDelegate;
            _objectValidator = objectValidator;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            httpContext.Request.EnableBuffering();
            using(var reader = new StreamReader(httpContext.Request.Body))
            {
                string httpContextBody = await reader.ReadToEndAsync();
                DynamicObject dynamicObject = JsonSerializer.Deserialize<DynamicObject>(httpContextBody);
                int objectType = dynamicObject.ObjectType;
                bool isModelValid = false;
                List<string> errorMessages = new();

                ValidateModel(objectType, dynamicObject, isModelValid, errorMessages);

                if(!isModelValid)
                {               
                    httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest; //ClientSideException
                    var response = CustomResponseDto<NoContentDto>.Fail(httpContext.Response.StatusCode, errorMessages);
                    await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
                }
            }
            await _requestDelegate(httpContext);
        }

        private void ValidateModel(int objectType, DynamicObject dynamicObject, bool isValid, List<string> errorMessages)
        {
            //Customer validation
            if (objectType == (int)ObjectTypesEnum.Customer)
            {
                Customer customer = JsonSerializer.Deserialize<Customer>(dynamicObject.ObjectData);
                isValid = _objectValidator.ValidateObject(customer, errorMessages);
            }

            //Product validation
            if (objectType == (int)ObjectTypesEnum.Product)
            {
                Product product = JsonSerializer.Deserialize<Product>(dynamicObject.ObjectData);
                isValid = _objectValidator.ValidateObject(product, errorMessages);
            }

            //Order validation
            if (objectType == (int)ObjectTypesEnum.Order)
            {
                Order order = JsonSerializer.Deserialize<Order>(dynamicObject.ObjectData);
                isValid = _objectValidator.ValidateObject(order, errorMessages);
            }

            //OrderedProduct validation
            if (objectType == (int)ObjectTypesEnum.OrderedProducts)
            {
                OrderedProducts orderedProducts = JsonSerializer.Deserialize<OrderedProducts>(dynamicObject.ObjectData);
                isValid = _objectValidator.ValidateObject(orderedProducts, errorMessages);
            }
        }
    }
}
