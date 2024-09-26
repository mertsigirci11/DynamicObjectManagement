using DynamicObjectManagement.Core.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DynamicObjectManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomController : ControllerBase
    {
        [NonAction]
        public async Task<IActionResult> CreateActionResult<T>(CustomResponseDto<T> customResponseDto) where T : class
        {
            return new ObjectResult(customResponseDto)
            {
                StatusCode = customResponseDto.StatusCode
            };
        }
    }
}
