using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiseman.PJC.Gen2.RDB.Core;

namespace Wiseman.PJC.Service.GroupSettings.RDB.Entities
{
    public class GroupPatientResultEntity : StandardColumns
    {
        // GRP_GROUPCATEGORY_TBL

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

        [Column(DbType.Varchar2, size: 32)]
        public string POST_ID { get; set; }

        [Column(DbType.Varchar2, size: 45)]
        public string LASTUPDATER_NAME { get; set; }

        [Column(DbType.Varchar2, size: 26)]
        public string LASTUPDATER_ID { get; set; }

        //GRP_GROUP_TBL
        [Column(DbType.Varchar2, size: 26)]
        public string DETAIL_ID { get; set; }

        [Column(DbType.Int32, size: 10)]
        public int DETAIL_LOCKVERSION​​ { get; set; }

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

        [Column(DbType.Varchar2, size: 20)]
        public string DETAIL_GROUP_KANA { get; set; }

        [Column(DbType.Varchar2, size: 10)]
        public string DETAIL_GROUP_RYAKUSHO { get; set; }

        [Column(DbType.Char, size: 1)]
        public string DETAIL_VALID_FLAG { get; set; }

        [Column(DbType.Varchar2, size: 40)]
        public string DETAIL_REMARKS { get; set; }

        [Column(DbType.Int32, size: 4)]
        public int DETAIL_DISPLAY_ORDER { get; set; }

        [Column(DbType.Char, size: 1)]
        public string DETAIL_IS_DELETED { get; set; }

        [Column(DbType.Varchar2, true, null, 50)]
        public string DETAIL_UPDATE_ACCOUNTID { get; set; }

        [Column(DbType.Varchar2, true, null, 50)]
        public string DETAIL_UPDATE_LOGINID { get; set; }

        [Column(DbType.Varchar2, true, null, 50)]
        public string DETAIL_UPDATE_FACILITYID { get; set; }

        [Column(DbType.Date, true, null)]
        public DateTime DETAIL_UPDATE_TIMESTAMP { get; set; }

        [Column(DbType.Varchar2, size: 32)]
        public string DETAIL_POST_ID { get; set; }

        [Column(DbType.Varchar2, size: 45)]
        public string DETAIL_LASTUPDATER_NAME { get; set; }

        [Column(DbType.Varchar2, size: 26)]
        public string DETAIL_LASTUPDATER_ID { get; set; }

        //GRP_GROUPMANAGEMENT_TBL
        [Column(DbType.Varchar2, size: 26)]
        public string MNG_ID { get; set; }

        [Column(DbType.Int32, size: 10)]
        public int MNG_LOCKVERSION​ { get; set; }

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

        [Column(DbType.Varchar2, true, null, 50)]
        public string MNG_UPDATE_ACCOUNTID { get; set; }

        [Column(DbType.Varchar2, true, null, 50)]
        public string MNG_UPDATE_LOGINID { get; set; }

        [Column(DbType.Varchar2, true, null, 50)]
        public string MNG_UPDATE_FACILITYID { get; set; }

        [Column(DbType.Date, true, null)]
        public DateTime MNG_UPDATE_TIMESTAMP { get; set; }

        [Column(DbType.Varchar2, size: 32)]
        public string MNG_POST_ID { get; set; }

        [Column(DbType.Varchar2, size: 45)]
        public string MNG_LASTUPDATER_NAME { get; set; }

        [Column(DbType.Varchar2, size: 26)]
        public string MNG_LASTUPDATER_ID { get; set; }

        // GRP_GROUPPATIENT_TBL
        [Column(DbType.Varchar2, size: 26)]
        public string PAT_ID { get; set; }

        [Column(DbType.Int32, size: 10)]
        public int PAT_LOCKVERSION​ { get; set; }

        [Column(DbType.Varchar2, size: 26)]
        public string PAT_AREACORP_ID { get; set; }

        [Column(DbType.Varchar2, size: 26)]
        public string PAT_FACILITYGROUP_ID { get; set; }

        [Column(DbType.Varchar2, size: 26)]
        public string PAT_FACILITY_ID { get; set; }

        [Column(DbType.Char, size: 4)]
        public string PAT_GROUPPATIENT_CODE { get; set; }

        [Column(DbType.Varchar2, size: 26)]
        public string PAT_GROUPMANAGEMENT_ID { get; set; }

        [Column(DbType.Varchar2, size: 26)]
        public string PAT_PATIENT_ID { get; set; }

        [Column(DbType.Int32, size: 8)]
        public int PAT_STARTDATE { get; set; }

        [Column(DbType.Int32, size: 8)]
        public int PAT_ENDDATE { get; set; }

        [Column(DbType.Int32, size: 4)]
        public int PAT_DISPLAY_ORDER { get; set; }

        [Column(DbType.Varchar2, true, null, 50)]
        public string PAT_UPDATE_ACCOUNTID { get; set; }

        [Column(DbType.Varchar2, true, null, 50)]
        public string PAT_UPDATE_LOGINID { get; set; }

        [Column(DbType.Varchar2, true, null, 50)]
        public string PAT_UPDATE_FACILITYID { get; set; }

        [Column(DbType.Date, true, null)]
        public DateTime PAT_UPDATE_TIMESTAMP { get; set; }

        [Column(DbType.Varchar2, size: 32)]
        public string PAT_POST_ID { get; set; }

        [Column(DbType.Varchar2, size: 45)]
        public string PAT_LASTUPDATER_NAME { get; set; }

        [Column(DbType.Varchar2, size: 26)]
        public string PAT_LASTUPDATER_ID { get; set; }
    }
}
