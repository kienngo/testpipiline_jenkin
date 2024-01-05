using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiseman.PJC.Gen2.RDB.Core;

namespace Wiseman.PJC.Service.GroupSettings.RDB.Entities
{
    public class CategorySelectedJnlEntity : CategorySelectedEntity
    {
        /// <summary>
        /// 操作種類
        /// </summary>
        [Column(DbType.Char, size: 1)]
        public string OPERATION { get; set; }

        /// <summary>
        /// 履歴SEQ
        /// </summary>
        [Column(DbType.Int32, size: 4)]
        public int HISTORY_SEQ { get; set; }

        /// <summary>
        /// 元データID
        /// </summary>
        [Column(DbType.Varchar2, size: 26)]
        public string REC_ID { get; set; }
    }
}
