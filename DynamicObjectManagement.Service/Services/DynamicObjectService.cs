using DynamicObjectManagement.Core.DTOs;
using DynamicObjectManagement.Core.Models;
using DynamicObjectManagement.Core.Repositories;
using DynamicObjectManagement.Core.Services;
using DynamicObjectManagement.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObjectManagement.Service.Services
{
    public class DynamicObjectService : GenericService<DynamicObject>, IDynamicObjectService
    {
        private readonly IDynamicObjectRepository _dynamicObjectRepository;
        public DynamicObjectService(IUnitOfWork unitOfWork, IGenericRepository<DynamicObject> genericRepository, IDynamicObjectRepository dynamicObjectRepository) : base(unitOfWork, genericRepository)
        {
            _dynamicObjectRepository = dynamicObjectRepository;
        }

        public async Task<CustomResponseDto<IEnumerable<DynamicObject>>> GetAllSameObjectTypeAsync(int objectTypeId)
        {
            var entities = _dynamicObjectRepository.GetAllSameObjectTypeAsync(objectTypeId);

            if (entities == null)
            {
                return CustomResponseDto<IEnumerable<DynamicObject>>.Fail((int)HttpStatusCode.NotFound, "Datas have not found.");
            }
                                                                              //await entities.ToListAsync() olabilir
            return CustomResponseDto<IEnumerable<DynamicObject>>.Success((int)HttpStatusCode.OK, entities);
        }
    }
}
