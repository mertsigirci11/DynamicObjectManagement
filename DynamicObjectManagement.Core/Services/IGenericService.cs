using DynamicObjectManagement.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObjectManagement.Core.Services
{
    public interface IGenericService<TEntity> where TEntity : class
    {
        //Get All
        Task<CustomResponseDto<IEnumerable<TEntity>>> GetAllAsync();

        //Get Object By ObjectId
        Task<CustomResponseDto<TEntity>> GetByIdAsync(int objectId);

        //Add Single Object
        Task<CustomResponseDto<TEntity>> AddAsync(TEntity entity);

        //Add Multiple Object
        Task<CustomResponseDto<IEnumerable<TEntity>>> AddRangeAsync(IEnumerable<TEntity> entities);

        //Remove Single Object
        Task<CustomResponseDto<NoContentDto>> RemoveAsync(int id);

        //Remove Multiple Object
        Task<CustomResponseDto<NoContentDto>> RemoveRangeAsync(IEnumerable<TEntity> entities);

        //Update Single Object
        Task<CustomResponseDto<NoContentDto>> Update(TEntity entity, int id);

        //Where
        Task<CustomResponseDto<IEnumerable<TEntity>>> Where(Expression<Func<TEntity, bool>> expression);
    }
}
