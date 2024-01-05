using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiseman.PJC.Service.GroupSettings.RDB.Entities;

namespace Wiseman.PJC.Service.GroupSettings.RDB.Interfaces
{
    public interface ICategorySelectedJnlAccess : IDisposable
    {
        int Create(CategorySelectedJnlEntity content);
    }
}
