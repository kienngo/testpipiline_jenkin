using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiseman.PJC.Gen2.ObjectModel.Interfaces;
using Wiseman.PJC.Gen2.RDB.Core;

namespace Wiseman.PJC.Service.GroupSettings.RDB.Entities
{
    public class GroupCategoryEntity : StandardColumns
    {
        [Column(DbType.Varchar2, size: 26)]
        public string AREACORP_ID { get; set; }

        [Column(DbType.Varchar2, size: 26)]
        public string FACILITYGROUP_ID { get; set; }

        [Column(DbType.Varchar2, size: 26)]
        public string FACILITY_ID { get; set; }
   
        [Column(DbType.Char, size: 4)]
        public string GROUPCATEGORY_CODE { get; set; }
     
        [Column(DbType.Varchar2, size: 20)]
        public string GROUPCATEGORY_NAME { get; set; }
   
        [Column(DbType.Varchar2, size: 26)]
        public string GROUPCATEGORY_KANA { get; set; }
      
        [Column(DbType.Varchar2, size: 10)]
        public string GROUPCATEGORY_RYAKUSHO { get; set; }
     
        [Column(DbType.Char, size: 1)]
        public string GROUPTANI { get; set; }
       
        [Column(DbType.Int32, size: 4)]
        public int DISPLAY_ORDER { get; set; }
    
        [Column(DbType.Char, size: 1)]
        public string IS_DELETED { get; set; }
     
        [Column(DbType.Varchar2, size: 26)]
        public string POST_ID { get; set; }
     
        [Column(DbType.Varchar2, size: 45)]
        public string LASTUPDATER_NAME { get; set; }
      
        [Column(DbType.Varchar2, size: 26)]
        public string LASTUPDATER_ID { get; set; }

    }
}
