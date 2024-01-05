using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiseman.PJC.Gen2.RDB.Core;

namespace Wiseman.PJC.Service.GroupSettings.RDB.Entities
{
    public class GroupCategoryResultEntity : StandardColumns
    {
        //GRP_GROUPCATEGORY_TBL
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
        public string UPDATE_ACCOUNTID​ { get; set; }

        [Column(DbType.Varchar2, size: 50)]
        public string UPDATE_LOGINID​ { get; set; }

        [Column(DbType.Varchar2, size: 50)]
        public string UPDATE_FACILIT​YID { get; set; }

        [Column(DbType.Date)]
        public DateTime UPDATE_TIMESTAMP { get; set; }

        [Column(DbType.Varchar2, size: 32)]
        public string POST_ID { get; set; }

        [Column(DbType.Varchar2, size: 45)]
        public string LASTUPDATER_NAME { get; set; }

        [Column(DbType.Varchar2, size: 26)]
        public string LASTUPDATER_ID { get; set; }

        // GRP_CATEGORYSELECTED_TBL
        [Column(DbType.Varchar2, size: 26)]
        public string CAT_ID { get; set; }

        [Column(DbType.Int32, size: 10)]
        public int CAT_LOCKVERSION​ { get; set; }

        [Column(DbType.Char, size: 3)]
        public string CAT_CATEGORYSELECTED_CODE { get; set; }

        [Column(DbType.Varchar2, size: 20)]
        public string CAT_CATEGORYSELECTED_NAME { get; set; }

        [Column(DbType.Varchar2, size: 26)]
        public string CAT_GROUPCATEGORY_ID { get; set; }

        [Column(DbType.Varchar2, size: 26)]
        public string CAT_CATEGORY_ID { get; set; }

        [Column(DbType.Varchar2, size: 50)]
        public string CAT_UPDATE_ACCOUNTID​ { get; set; }

        [Column(DbType.Varchar2, size: 50)]
        public string CAT_UPDATE_LOGINID​ { get; set; }

        [Column(DbType.Varchar2, size: 50)]
        public string CAT_UPDATE_FACILIT​YID { get; set; }

        [Column(DbType.Date)]
        public DateTime CAT_UPDATE_TIMESTAMP { get; set; }

        [Column(DbType.Varchar2, size: 32)]
        public string CAT_POST_ID { get; set; }

        [Column(DbType.Varchar2, size: 45)]
        public string CAT_LASTUPDATER_NAME { get; set; }

        [Column(DbType.Varchar2, size: 26)]
        public string CAT_LASTUPDATER_ID { get; set; }

        [Column(DbType.Int32, size: 4)]
        public int NUM_OF_GROUP { get; set; }

    }
}
