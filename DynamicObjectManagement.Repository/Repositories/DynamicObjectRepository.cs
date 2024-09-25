using DynamicObjectManagement.Core.Models;
using DynamicObjectManagement.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObjectManagement.Repository.Repositories
{
    public class DynamicObjectRepository : GenericRepository<DynamicObject>, IDynamicObjectRepository
    {
        public DynamicObjectRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public IQueryable<DynamicObject> GetAllSameObjectTypeAsync(int objectTypeId)
        {
            return _appDbContext.DynamicObjects.Where(x => x.ObjectType == objectTypeId);
        }
    }
}
