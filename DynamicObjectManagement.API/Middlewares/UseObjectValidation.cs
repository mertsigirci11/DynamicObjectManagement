using Microsoft.AspNetCore.Builder;

namespace DynamicObjectManagement.API.Middlewares
{
    public static class UseObjectValidation
    {
        public static IApplicationBuilder UseObjectValidator(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ObjectValidateHandler>();
        }
    }
}
