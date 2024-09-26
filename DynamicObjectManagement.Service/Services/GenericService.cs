using DynamicObjectManagement.Core.DTOs;
using DynamicObjectManagement.Core.Models;
using DynamicObjectManagement.Core.Repositories;
using DynamicObjectManagement.Core.Services;
using DynamicObjectManagement.Core.UnitOfWorks;
using DynamicObjectManagement.Service.Exceptions;
using DynamicObjectManagement.Service.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DynamicObjectManagement.Service.Services
{
    public class GenericService<TEntity> : IGenericService<TEntity> where TEntity : class
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IGenericRepository<TEntity> _genericRepository;

        public GenericService(IUnitOfWork unitOfWork, IGenericRepository<TEntity> genericRepository)
        {
            _unitOfWork = unitOfWork;
            _genericRepository = genericRepository;
        }

        public async Task<CustomResponseDto<TEntity>> AddAsync(TEntity entity)
        {
            await _genericRepository.AddAsync(entity);
            await _unitOfWork.CommitAsync();

            return CustomResponseDto<TEntity>.Success((int)HttpStatusCode.Created, entity);
        }

        public async Task<CustomResponseDto<IEnumerable<TEntity>>> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _genericRepository.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();

            return CustomResponseDto<IEnumerable<TEntity>>.Success((int)HttpStatusCode.Created, entities);
        }

        public async Task<CustomResponseDto<IEnumerable<TEntity>>> GetAllAsync()
        {
            var entities = await _genericRepository.GetAllAsync().ToListAsync();
            
            return CustomResponseDto<IEnumerable<TEntity>>.Success((int)HttpStatusCode.OK, entities);
        }

        public async Task<CustomResponseDto<TEntity>> GetByIdAsync(int objectId)
        {
            var entity = await _genericRepository.GetByIdAsync(objectId);

            if (entity == null)
            {
                throw new NotFoundException($"{typeof(TEntity).Name}({objectId}) not found.");
            }
            
            return CustomResponseDto<TEntity>.Success((int)HttpStatusCode.OK, entity);
        }

        public async Task<CustomResponseDto<NoContentDto>> RemoveAsync(int id)
        {
            var entityToBeDeleted = await _genericRepository.GetByIdAsync(id);
            
            if (entityToBeDeleted == null)
            {
                return CustomResponseDto<NoContentDto>.Fail((int)HttpStatusCode.NotFound, "Data has not found.");
            }

            _genericRepository.Remove(entityToBeDeleted);
            _unitOfWork.Commit();

            return CustomResponseDto<NoContentDto>.Success((int)HttpStatusCode.NoContent);
        }

        public async Task<CustomResponseDto<NoContentDto>> RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                return CustomResponseDto<NoContentDto>.Fail((int)HttpStatusCode.NotFound, "Datas have not found.");
            }
            _genericRepository.RemoveRange(entities);
            _unitOfWork.Commit();

            return CustomResponseDto<NoContentDto>.Success((int)HttpStatusCode.NoContent);
        }

        public async Task<CustomResponseDto<NoContentDto>> Update(TEntity entity, int id)
        {
            var entityToBeUpdated =  await _genericRepository.GetByIdAsync(id);
            
            if(entityToBeUpdated == null)
            {
                return CustomResponseDto<NoContentDto>.Fail((int)HttpStatusCode.NotFound, "Data has not found.");
            }

            var properties = typeof(TEntity).GetProperties();
            foreach (var property in properties)
            {
                if (property.CanWrite)
                {
                    var newValue = property.GetValue(entity);
                    property.SetValue(entityToBeUpdated, newValue);
                }
            }

            _genericRepository.Update(entityToBeUpdated);
            _unitOfWork.Commit();

            return CustomResponseDto<NoContentDto>.Success((int)HttpStatusCode.NoContent);
        }

        public async Task<CustomResponseDto<IEnumerable<TEntity>>> Where(Expression<Func<TEntity, bool>> expression)
        {
            var entities = _genericRepository.Where(expression);

            if (entities == null)
            {
                return CustomResponseDto<IEnumerable<TEntity>>.Fail((int)HttpStatusCode.NotFound, "Datas have not found.");
            }

                                                                        //await entities.ToListAsync() olabilir
            return CustomResponseDto<IEnumerable<TEntity>>.Success((int)HttpStatusCode.OK, entities);
        }
    }
}
