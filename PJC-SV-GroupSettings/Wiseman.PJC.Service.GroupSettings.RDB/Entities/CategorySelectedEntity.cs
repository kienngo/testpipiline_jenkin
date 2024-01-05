using System;
using Wiseman.PJC.Gen2.RDB.Core;

namespace Wiseman.PJC.Service.GroupSettings.RDB.Entities
{
    public class CategorySelectedEntity : StandardColumns
    {
        [Column(DbType.Char, size: 3)]
        public string CATEGORYSELECTED_CODE { get; set; }
              
       // public string CATEGORYSELECTED_NAME { get; set; }

        [Column(DbType.Varchar2, size: 26)]
        public string GROUPCATEGORY_ID { get; set; }

        [Column(DbType.Varchar2, size: 26)]
        public string CATEGORY_ID { get; set; }

        [Column(DbType.Varchar2, size: 32)]
        public string POST_ID { get; set; }

        [Column(DbType.Varchar2, size: 45)]
        public string LASTUPDATER_NAME { get; set; }

        [Column(DbType.Varchar2, size: 26)]
        public string LASTUPDATER_ID { get; set; }
    }
}
