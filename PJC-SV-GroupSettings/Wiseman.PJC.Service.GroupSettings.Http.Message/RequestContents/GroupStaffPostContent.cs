using System.ComponentModel;
using Wiseman.PJC.Gen2.DataAnnotations;
using Wiseman.PJC.Gen2.ObjectModel.Interfaces;
using Wiseman.PJC.Service.GroupSettings.Http.Message.Enums;
using Wiseman.PJC.Service.GroupSettings.Http.Message.Interfaces;
using IID_IdType = System.String;

namespace Wiseman.PJC.Service.GroupSettings.Http.Message.RequestContents
{
    public class GroupStaffPostContent : IAction, IID, ILockVersion
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
        /// グループ職員コード
        /// </summary>
        [Required]
        [StringLength(4)]
        public string GroupStaffCode { get; set; }

        /// <summary>
        /// グループ管理ID
        /// </summary>
        [StringLength(26)]
        public string GroupManagementId { get; set; }

        /// <summary>
        /// 職員ID
        /// </summary>
        [Required]
        [StringLength(26)]
        public string StaffId { get; set; }

        /// <summary>
        /// 開始日
        /// </summary>
        public int StartDate { get; set; }

        /// <summary>
        /// 終了日
        /// </summary>
        [DefaultValue(99999999)]
        public int EndDate { get; set; }

        /// <summary>
        /// 表示順
        /// </summary>
        [Required]
        [MaxLength(4)]
        [DefaultValue(9999)]
        public int DisplayOrder { get; set; }

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
        /// 行に対する操作
        /// </summary>
        public ActionFlag ActionFlag { get; set; }
    }
}
