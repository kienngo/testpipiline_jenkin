using System;
using Wiseman.PJC.Gen2.RDB.Core;

namespace Wiseman.PJC.Service.GroupSettings.RDB.Entities
{
    public class GroupManagementListDetailEntity : StandardColumns
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

        //GRP_GROUP_TBL
        [Column(DbType.Varchar2, false, null, 26)]
        public string DETAIL_ID { get; set; }

        [Column(DbType.Long, true, null, 10)]
        public long? DETAIL_LOCKVERSION​ { get; set; }

        [Column(DbType.Varchar2, true, null, 26)]
        public string DETAIL_UPDATE_ACCOUNTID​ { get; set; }

        [Column(DbType.Varchar2, true, null, 512)]
        public string DETAIL_UPDATE_LOGINID​ { get; set; }

        [Column(DbType.Varchar2, true, null, 26)]
        public string DETAIL_UPDATE_FACILIT​YID { get; set; }

        [Column(DbType.TimeStamp, true, null, 20)]
        public DateTime DETAIL_UPDATE_TIMESTAMP { get; set; }

        [Column(DbType.Varchar2, size: 26)]
        public string DETAIL_AREACORP_ID { get; set; }

        [Column(DbType.Varchar2, size: 26)]
        public string DETAIL_FACILITYGROUP_ID { get; set; }

        [Column(DbType.Varchar2, size: 26)]
        public string DETAIL_FACILITY_ID { get; set; }

        [Column(DbType.Char, size: 4)]
        public string DETAIL_GROUP_CODE { get; set; }

        [Column(DbType.Varchar2, size: 26)]
        public string DETAIL_GROUPCATEGORY_ID { get; set; }

        [Column(DbType.Varchar2, size: 20)]
        public string DETAIL_GROUP_NAME { get; set; }

        [Column(DbType.Varchar2, size: 32)]
        public string DETAIL_GROUP_KANA { get; set; }

        [Column(DbType.Varchar2, size: 20)]
        public string DETAIL_GROUP_RYAKUSHO { get; set; }

        [Column(DbType.Char, size: 1)]
        public string DETAIL_VALID_FLAG { get; set; }

        [Column(DbType.Varchar2, size: 40)]
        public string DETAIL_REMARKS { get; set; }

        [Column(DbType.Int32, size: 4)]
        public int DETAIL_DISPLAY_ORDER { get; set; }

        [Column(DbType.Char, size: 1)]
        public string DETAIL_IS_DELETED { get; set; }

        [Column(DbType.Varchar2, size: 32)]
        public string DETAIL_POST_ID { get; set; }

        [Column(DbType.Varchar2, size: 45)]
        public string DETAIL_LASTUPDATER_NAME { get; set; }

        [Column(DbType.Varchar2, size: 26)]
        public string DETAIL_LASTUPDATER_ID { get; set; }

        //GRP_GROUPMANAGEMENT_TBL
        [Column(DbType.Varchar2, false, null, 26)]
        public string MNG_ID { get; set; }

        [Column(DbType.Long, true, null, 10)]
        public long? MNG_LOCKVERSION​ { get; set; }

        [Column(DbType.Varchar2, true, null, 26)]
        public string MNG_UPDATE_ACCOUNTID​ { get; set; }

        [Column(DbType.Varchar2, true, null, 512)]
        public string MNG_UPDATE_LOGINID​ { get; set; }

        [Column(DbType.Varchar2, true, null, 26)]
        public string MNG_UPDATE_FACILIT​YID { get; set; }

        [Column(DbType.TimeStamp, true, null, 20)]
        public DateTime MNG_UPDATE_TIMESTAMP { get; set; }

        [Column(DbType.Varchar2, size: 26)]
        public string MNG_AREACORP_ID { get; set; }

        [Column(DbType.Varchar2, size: 26)]
        public string MNG_FACILITYGROUP_ID { get; set; }

        [Column(DbType.Varchar2, size: 26)]
        public string MNG_FACILITY_ID { get; set; }

        [Column(DbType.Char, size: 4)]
        public string MNG_GROUPMANAGEMENT_CODE { get; set; }

        [Column(DbType.Varchar2, size: 26)]
        public string MNG_GROUP_ID { get; set; }

        [Column(DbType.Int32, size: 8)]
        public int MNG_STARTDATE { get; set; }

        [Column(DbType.Int32, size: 8)]
        public int MNG_ENDDATE { get; set; }

        [Column(DbType.Char, size: 1)]
        public string MNG_IS_DELETED { get; set; }

        [Column(DbType.Varchar2, size: 32)]
        public string MNG_POST_ID { get; set; }

        [Column(DbType.Varchar2, size: 45)]
        public string MNG_LASTUPDATER_NAME { get; set; }

        [Column(DbType.Varchar2, size: 26)]
        public string MNG_LASTUPDATER_ID { get; set; }
    }
}
