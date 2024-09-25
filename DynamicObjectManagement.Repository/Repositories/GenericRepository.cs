using DynamicObjectManagement.Core.DTOs;
using DynamicObjectManagement.Core.Repositories;
using DynamicObjectManagement.Repository.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObjectManagement.Repository.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly AppDbContext _appDbContext;
        protected readonly DbSet<TEntity> _dbSet;

        public GenericRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _dbSet = _appDbContext.Set<TEntity>();
        }
        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public IQueryable<TEntity> GetAllAsync()
        {
            return _dbSet.AsNoTracking().AsQueryable();
        }

        public async Task<TEntity> GetByIdAsync(int objectId)
        {
            return await _dbSet.FindAsync(objectId);
        }

        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
        }

        public IQueryable<TEntity> Where(System.Linq.Expressions.Expression<Func<TEntity, bool>> expression)
        {
            return _dbSet.Where(expression);
        }
    }
}
