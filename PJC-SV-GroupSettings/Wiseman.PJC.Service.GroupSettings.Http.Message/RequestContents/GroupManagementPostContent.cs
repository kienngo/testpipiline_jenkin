using System.Collections.Generic;
using System.ComponentModel;
using Wiseman.PJC.Gen2.DataAnnotations;
using Wiseman.PJC.Gen2.ObjectModel.Interfaces;
using IID_IdType = System.String;

namespace Wiseman.PJC.Service.GroupSettings.Http.Message.RequestContents
{
    public class GroupManagementPostContent : IID, ILockVersion
    {
        /// <summary>
        /// サロゲートキー
        /// </summary>
        [StringLength(26)]
        public IID_IdType Id { get; set; }

        /// <summary>
        /// ロックバージョン
        /// </summary>
        [MinLength(1)]
        [MaxLength(10)]
        [DefaultValue(0)]
        public long? LockVersion { get; set; }

        /// <summary>
        /// 地域法人グループ
        /// </summary>
        [Required]
        [StringLength(26)]
        public string AreaCorpId { get; set; }

        /// <summary>
        /// 医療機関・施設グループID
        /// </summary>
        [Required]
        [StringLength(26)]
        public string FacilityGroupId { get; set; }

        /// <summary>
        /// 医療機関・施設ID
        /// </summary>
        [Required]
        [StringLength(26)]
        public string FacilityId { get; set; }

        /// <summary>
        /// グループ管理コード
        /// </summary>
        [StringLength(4)]
        public string GroupManagementCode { get; set; }

        /// <summary>
        /// グループID
        /// </summary>
        [Required]
        [StringLength(26)]
        public string GroupId { get; set; }

        /// <summary>
        /// 開始日
        /// </summary>
        [Required]
        public int StartDate { get; set; }

        /// <summary>
        /// 終了日
        /// </summary>
        [MinLength(8)]
        [MaxLength(8)]
        [DefaultValue(99999999)]
        public int EndDate { get; set; }

        /// <summary>
        /// 削除フラグ
        /// </summary>
        [StringLength(1)]
        public string IsDeleted { get; set; }

        /// <summary>
        /// POST識別子
        /// </summary>
        [StringLength(32)]
        public string PostID { get; set; }

        /// <summary>
        /// 最終更新者名
        /// </summary>
        [StringLength(45)]
        public string LastUpdaterName { get; set; }

        /// <summary>
        /// 最終更新職員ID
        /// </summary>
        [StringLength(26)]
        public string LastUpdaterId { get; set; }

        /// <summary>
        /// グループ患者
        /// </summary>
        public List<GroupPatientPostContent> GroupPatientPostContent { get; set; } = new List<GroupPatientPostContent>();

        /// <summary>
        /// グループ職員
        /// </summary>
        public List<GroupStaffPostContent> GroupStaffPostContent { get; set; } = new List<GroupStaffPostContent>();
    }
}
