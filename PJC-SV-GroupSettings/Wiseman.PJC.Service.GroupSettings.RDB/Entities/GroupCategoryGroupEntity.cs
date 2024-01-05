using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiseman.PJC.Gen2.ObjectModel.Interfaces;
using Wiseman.PJC.Gen2.RDB.Core;

namespace Wiseman.PJC.Service.GroupSettings.RDB.Entities
{
    public class GroupCategoryGroupEntity
    {
        // GRP_GROUPCATEGORY_TBL
        [Column(DbType.Varchar2, size: 26)]
        public string ID { get; set; }

        [Column(DbType.Long, size: 10)]
        public long? LOCKVERSION { get; set; }

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

        [Column(DbType.Varchar2, size: 20)]
        public string GROUPCATEGORY_KANA { get; set; }

        [Column(DbType.Varchar2, size: 10)]
        public string GROUPCATEGORY_RYAKUSHO { get; set; }

        [Column(DbType.Char, size: 1)]
        public string GROUPTANI { get; set; }

        [Column(DbType.Int32, size: 4)]
        public int DISPLAY_ORDER { get; set; }

        [Column(DbType.Char, size: 1)]
        public string IS_DELETED { get; set; }

        [Column(DbType.Varchar2, size: 50)]
        public string UPDATE_ACCOUNTID { get; set; }

        [Column(DbType.Varchar2, size: 50)]
        public string UPDATE_LOGINID { get; set; }

        [Column(DbType.Varchar2, size: 50)]
        public string UPDATE_FACILITYID { get; set; }

        [Column(DbType.Date)]
        public DateTime UPDATE_TIMESTAMP { get; set; }

        [Column(DbType.Varchar2, size: 26)]
        public string POST_ID { get; set; }

        [Column(DbType.Varchar2, size: 45)]
        public string LASTUPDATER_NAME { get; set; }

        [Column(DbType.Varchar2, size: 26)]
        public string LASTUPDATER_ID { get; set; }

        //GRP_GROUP_TBL
        [Column(DbType.Varchar2, size: 26)]
        public string GRP_ID { get; set; }

        [Column(DbType.Long, size: 10)]
        public long? GRP_LOCKVERSION { get; set; }

        [Column(DbType.Varchar2, size: 26)]
        public string GRP_AREACORP_ID { get; set; }

        [Column(DbType.Varchar2, size: 26)]
        public string GRP_FACILITYGROUP_ID { get; set; }

        [Column(DbType.Varchar2, size: 26)]
        public string GRP_FACILITY_ID { get; set; }

        [Column(DbType.Char, size: 4)]
        public string GRP_GROUP_CODE { get; set; }

        [Column(DbType.Varchar2, size: 26)]
        public string GRP_GROUPCATEGORY_ID { get; set; }

        [Column(DbType.Varchar2, size: 20)]
        public string GRP_GROUP_NAME { get; set; }

        [Column(DbType.Varchar2, size: 32)]
        public string GRP_GROUP_KANA { get; set; }

        [Column(DbType.Varchar2, size: 20)]
        public string GRP_GROUP_RYAKUSHO { get; set; }

        [Column(DbType.Char, size: 1)]
        public string GRP_VALID_FLAG { get; set; }

        [Column(DbType.Varchar2, size: 40)]
        public string GRP_REMARKS { get; set; }

        [Column(DbType.Int32, size: 4)]
        public int GRP_DISPLAY_ORDER { get; set; }

        [Column(DbType.Char, size: 1)]
        public string GRP_IS_DELETED { get; set; }

        [Column(DbType.Varchar2, size: 50)]
        public string GRP_UPDATE_ACCOUNTID { get; set; }

        [Column(DbType.Varchar2, size: 50)]
        public string GRP_UPDATE_LOGINID { get; set; }

        [Column(DbType.Varchar2, size: 50)]
        public string GRP_UPDATE_FACILITYID { get; set; }

        [Column(DbType.Date)]
        public DateTime GRP_UPDATE_TIMESTAMP { get; set; }

        [Column(DbType.Varchar2, size: 32)]
        public string GRP_POST_ID { get; set; }

        [Column(DbType.Varchar2, size: 45)]
        public string GRP_LASTUPDATER_NAME { get; set; }

        [Column(DbType.Varchar2, size: 26)]
        public string GRP_LASTUPDATER_ID { get; set; }

        [Column(DbType.Int32, size: 4)]
        public int NUM_OF_MEMBER { get; set; }      
    }
}
