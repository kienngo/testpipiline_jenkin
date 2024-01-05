using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiseman.PJC.Gen2.RDB.Core;

namespace Wiseman.PJC.Service.GroupSettings.RDB.Entities
{
    public class GroupReadByCodeEntity
    {
        [Column(DbType.Varchar2, size: 26)]
        public string ID { get; set; }

        [Column(DbType.Int32, size: 10)]
        public int LOCKVERSION { get; set; }

        [Column(DbType.Char, size: 1)]
        public string IS_DELETED { get; set; }
    }
}
