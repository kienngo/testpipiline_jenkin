using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiseman.PJC.Gen2.RDB.Core;

namespace Wiseman.PJC.Service.GroupSettings.RDB.Entities
{
    public class GroupManagementDetailSettingsEntity : GroupManagementEntity
    {
        //GRP_GROUPPATIENT_TBL
        [Column(DbType.Varchar2, false, null, 26)]
        public string PAT_ID { get; set; }

        [Column(DbType.Long, true, null, 10)]
        public long? PAT_LOCKVERSION { get; set; }

        [Column(DbType.Varchar2, true, null, 26)]
        public string PAT_UPDATE_ACCOUNTID { get; set; }

        [Column(DbType.Varchar2, true, null, 512)]
        public string PAT_UPDATE_LOGINID { get; set; }

        [Column(DbType.Varchar2, true, null, 26)]
        public string PAT_UPDATE_FACILITYID { get; set; }

        [Column(DbType.TimeStamp, true, null, 20)]
        public DateTime PAT_UPDATE_TIMESTAMP { get; set; }

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

        [Column(DbType.Varchar2, size: 32)]
        public string PAT_POST_ID { get; set; }

        [Column(DbType.Varchar2, size: 45)]
        public string PAT_LASTUPDATER_NAME { get; set; }

        [Column(DbType.Varchar2, size: 26)]
        public string PAT_LASTUPDATER_ID { get; set; }

        //GRP_GROUPSTAFF_TBL
        [Column(DbType.Varchar2, false, null, 26)]
        public string STAFF_ID { get; set; }

        [Column(DbType.Long, true, null, 10)]
        public long? STAFF_LOCKVERSION { get; set; }

        [Column(DbType.Varchar2, true, null, 26)]
        public string STAFF_UPDATE_ACCOUNTID { get; set; }

        [Column(DbType.Varchar2, true, null, 512)]
        public string STAFF_UPDATE_LOGINID { get; set; }

        [Column(DbType.Varchar2, true, null, 26)]
        public string STAFF_UPDATE_FACILITYID { get; set; }

        [Column(DbType.TimeStamp, true, null, 20)]
        public DateTime STAFF_UPDATE_TIMESTAMP { get; set; }

        [Column(DbType.Varchar2, size: 26)]
        public string STAFF_AREACORP_ID { get; set; }

        [Column(DbType.Varchar2, size: 26)]
        public string STAFF_FACILITYGROUP_ID { get; set; }

        [Column(DbType.Varchar2, size: 26)]
        public string STAFF_FACILITY_ID { get; set; }

        [Column(DbType.Char, size: 4)]
        public string STAFF_GROUPSTAFF_CODE { get; set; }

        [Column(DbType.Varchar2, size: 26)]
        public string STAFF_GROUPMANAGEMENT_ID { get; set; }

        [Column(DbType.Varchar2, size: 26)]
        public string STAFF_STAFF_ID { get; set; }

        [Column(DbType.Int32, size: 8)]
        public int STAFF_STARTDATE { get; set; }

        [Column(DbType.Int32, size: 8)]
        public int STAFF_ENDDATE { get; set; }

        [Column(DbType.Int32, size: 4)]
        public int STAFF_DISPLAY_ORDER { get; set; }

        [Column(DbType.Varchar2, size: 32)]
        public string STAFF_POST_ID { get; set; }

        [Column(DbType.Varchar2, size: 45)]
        public string STAFF_LASTUPDATER_NAME { get; set; }

        [Column(DbType.Varchar2, size: 26)]
        public string STAFF_LASTUPDATER_ID { get; set; }
    }
}
