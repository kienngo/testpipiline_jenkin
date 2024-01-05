using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiseman.PJC.Gen2.ObjectModel.Interfaces;
using Wiseman.PJC.Gen2.RDB.Core;

namespace Wiseman.PJC.Service.GroupSettings.RDB.Entities
{
    public class GroupCategoryDeleteEntity : StandardColumns
    {
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [Column(DbType.Char, size: 1)]
        public string Is_Deleted { get; set; }
    }
}
