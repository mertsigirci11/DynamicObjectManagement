using Azure.Core;
using DynamicObjectManagement.Core.DTOs;
using DynamicObjectManagement.Core.Models;
using DynamicObjectManagement.Service.Enums;
using DynamicObjectManagement.Service.Validators;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text;
using System.Text.Json;

namespace DynamicObjectManagement.API.Middlewares
{
    public class ObjectValidateHandler
    {
        private readonly RequestDelegate _requestDelegate;

        public ObjectValidateHandler(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            httpContext.Request.EnableBuffering();
            httpContext.Response.ContentType = "application/json";

            using (var reader = new StreamReader(httpContext.Request.Body, Encoding.UTF8, leaveOpen: true))
            {
                string httpContextBody = await reader.ReadToEndAsync();
                httpContext.Request.Body.Position = 0;

                bool isModelValid = true;
                List<string> errorMessages = new();
                
                if (!httpContextBody.IsNullOrEmpty())
                {
                    ObjectValidator objectValidator = httpContext.RequestServices.GetRequiredService<ObjectValidator>();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    };
                    DynamicObject dynamicObject = JsonSerializer.Deserialize<DynamicObject>(httpContextBody,options);
                    
                    ValidateModel(dynamicObject.ObjectType, dynamicObject, objectValidator,  ref isModelValid, ref errorMessages);
                }
                 
                if(!isModelValid)
                {               
                    httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest; //ClientSideException
                    var response = CustomResponseDto<NoContentDto>.Fail(httpContext.Response.StatusCode, errorMessages);
                    var jsonResponse = JsonSerializer.Serialize(response);
                    await httpContext.Response.WriteAsync(jsonResponse);
                    return;
                }
            }
            await _requestDelegate(httpContext);
        }

        private void ValidateModel(int objectType, DynamicObject dynamicObject, ObjectValidator objectValidator ,  ref bool isValid, ref List<string> errorMessages)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            //Customer validation
            if (objectType == (int)ObjectTypesEnum.Customer)
            {
                Customer customer = null;
                try
                {
                    customer = JsonSerializer.Deserialize<Customer>(dynamicObject.ObjectData, options);
                    
                }
                catch (JsonException)
                {

                    throw new MissingFieldException(ExceptionResponseConstants.CUSTOMER_MISSINGFIELDS_EX);
                }
                isValid = objectValidator.ValidateObject(customer, ref errorMessages);
                return;
            }

            //Product validation
            if (objectType == (int)ObjectTypesEnum.Product)
            {
                Product product = JsonSerializer.Deserialize<Product>(dynamicObject.ObjectData, options);
                isValid = objectValidator.ValidateObject(product, ref errorMessages);
            }

            //Order validation
            if (objectType == (int)ObjectTypesEnum.Order)
            {
                Order order = JsonSerializer.Deserialize<Order>(dynamicObject.ObjectData, options);
                isValid = objectValidator.ValidateObject(order, ref errorMessages);
            }

            //OrderedProduct validation
            if (objectType == (int)ObjectTypesEnum.OrderedProducts)
            {
                OrderedProducts orderedProducts = JsonSerializer.Deserialize<OrderedProducts>(dynamicObject.ObjectData, options);
                isValid = objectValidator.ValidateObject(orderedProducts, ref errorMessages);
            }

            errorMessages.Add($"Object type :{objectType} , the object that you wrote must have correct object type");
            isValid = false;
        }
    }
}
