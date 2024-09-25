using DynamicObjectManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObjectManagement.Core.Repositories
{
    public interface IDynamicObjectRepository : IGenericRepository<DynamicObject>
    {
        //Get All Same Object Types
        IQueryable<DynamicObject> GetAllSameObjectTypeAsync(int objectTypeId);
    }
}
