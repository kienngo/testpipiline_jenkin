using System;
using System.Collections.Generic;
using Wiseman.PJC.Service.GroupSettings.RDB.Entities;

namespace Wiseman.PJC.Service.GroupSettings.RDB.Interfaces
{
    public interface ICategoryAccess : IDisposable
    {
        IList<CategoryEntity> GetAsync(string categoryCode = "",
                               short limit = 1000,
                               short offset = 0);
    }
}
