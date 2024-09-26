using DynamicObjectManagement.Core.Models;
using DynamicObjectManagement.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DynamicObjectManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DynamicObjectsController : CustomController
    {
        private readonly IDynamicObjectService _dynamicObjectService;

        public DynamicObjectsController(IDynamicObjectService dynamicObjectService)
        {
            _dynamicObjectService = dynamicObjectService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDynamicObjects()
        {
            var response = await _dynamicObjectService.GetAllAsync();

            return await CreateActionResult(response);
        }

        [HttpGet("object-type/{objectType}")]
        public async Task<IActionResult> GetAllSameObjectType(int objectType)
        {
            var response = await _dynamicObjectService.GetAllSameObjectTypeAsync(objectType);

            return await CreateActionResult(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDynamicObjectById(int id)
        {
            var response = await _dynamicObjectService.GetByIdAsync(id);

            return await CreateActionResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddDynamicObject([FromBody] DynamicObject dynamicObject)
        {
            var response = await _dynamicObjectService.AddAsync(dynamicObject);

            return await CreateActionResult(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDynamicObject(int id)
        {
            var response = await _dynamicObjectService.RemoveAsync(id);

            return await CreateActionResult(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDynamicObject([FromBody] DynamicObject dynamicObject, int id)
        {
            var response = await _dynamicObjectService.Update(dynamicObject, id);

            return await CreateActionResult(response);
        }

    }
}
