using DynamicObjectManagement.Core.DTOs;
using DynamicObjectManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObjectManagement.Core.Services
{
    public interface IDynamicObjectService : IGenericService<DynamicObject>
    {
        Task<CustomResponseDto<IEnumerable<DynamicObject>>> GetAllSameObjectTypeAsync();
    }
}
