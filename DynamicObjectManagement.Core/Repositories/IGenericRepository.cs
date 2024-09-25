using DynamicObjectManagement.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObjectManagement.Core.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        //Get All
        IQueryable<TEntity> GetAllAsync();

        //Get Object By ObjectId
        Task<TEntity> GetByIdAsync(int objectId);

        //Add Single Object
        Task AddAsync(TEntity entity);

        //Add Multiple Object
        Task AddRangeAsync(IEnumerable<TEntity> entities);

        //Update Single Object
        void Update(TEntity entity);

        //Update Multiple Object
        void UpdateRange(IEnumerable<TEntity> entities);

        //Remove Single Object
        void Remove(TEntity entity);

        //Remove Multiple Object
        void RemoveRange(IEnumerable<TEntity> entities);

        //Where
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression);
    }
}
