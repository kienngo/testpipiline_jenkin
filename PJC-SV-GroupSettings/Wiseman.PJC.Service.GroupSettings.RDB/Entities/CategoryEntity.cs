using Wiseman.PJC.Gen2.RDB.Core;

namespace Wiseman.PJC.Service.GroupSettings.RDB.Entities
{
    public class CategoryEntity : StandardColumns
    {
        [Column(DbType.Varchar2, size: 26)]
        public string AREACORP_ID { get; set; }

        [Column(DbType.Varchar2, size: 26)]
        public string FACILITYGROUP_ID { get; set; }

        [Column(DbType.Varchar2, size: 26)]
        public string FACILITY_ID { get; set; }

        [Column(DbType.Char, size: 3)]
        public string CATEGORY_CODE { get; set; }

        [Column(DbType.Varchar2, size: 20)]
        public string CATEGORY_NAME { get; set; }

        [Column(DbType.Char, size: 1)]
        public string IS_DELETED { get; set; }

        [Column(DbType.Varchar2, size: 32)]
        public string POST_ID { get; set; }

        [Column(DbType.Varchar2, size: 45)]
        public string LASTUPDATER_NAME { get; set; }

        [Column(DbType.Varchar2, size: 26)]
        public string LASTUPDATER_ID { get; set; }
    }
}
