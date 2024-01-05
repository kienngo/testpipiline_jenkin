using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiseman.PJC.Gen2.RDB.Core;

namespace Wiseman.PJC.Service.GroupSettings.RDB.Entities
{
    public class GroupPatientEntity : StandardColumns
    {
        [Column(DbType.Varchar2, size: 26)]
        public string AREACORP_ID { get; set; }

        [Column(DbType.Varchar2, size: 26)]
        public string FACILITYGROUP_ID { get; set; }

        [Column(DbType.Varchar2, size: 26)]
        public string FACILITY_ID { get; set; }

        [Column(DbType.Char, size: 4)]
        public string GROUPPATIENT_CODE { get; set; }

        [Column(DbType.Varchar2, size: 26)]
        public string GROUPMANAGEMENT_ID { get; set; }

        [Column(DbType.Varchar2, size: 26)]
        public string PATIENT_ID { get; set; }

        [Column(DbType.Int32, size: 8)]
        public int STARTDATE { get; set; }

        [Column(DbType.Int32, size: 8)]
        public int ENDDATE { get; set; }

        [Column(DbType.Int32, size: 4)]
        public int DISPLAY_ORDER { get; set; }

        [Column(DbType.Varchar2, size: 32)]
        public string POST_ID { get; set; }

        [Column(DbType.Varchar2, size: 45)]
        public string LASTUPDATER_NAME { get; set; }

        [Column(DbType.Varchar2, size: 26)]
        public string LASTUPDATER_ID { get; set; }
    }
}
