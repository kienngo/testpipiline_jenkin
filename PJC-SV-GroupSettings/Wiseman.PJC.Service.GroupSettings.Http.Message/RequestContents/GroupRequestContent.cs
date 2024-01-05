using System.ComponentModel;
using Wiseman.PJC.Gen2.DataAnnotations;
using Wiseman.PJC.Gen2.ObjectModel.Interfaces;

namespace Wiseman.PJC.Service.GroupSettings.Http.Message.RequestContents
{
    public class GroupRequestContent : IID, ILockVersion
    {
        /// <summary>
        /// サロゲートキー
        /// </summary>
        [StringLength(26)]
        public string Id { get; set; }

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
        /// グループコード
        /// </summary>
        [StringLength(4)]
        public string GroupCode { get; set; }

        /// <summary>
        /// グループ分類ID
        /// </summary>
        [Required]
        [StringLength(26)]
        public string GroupCategoryId { get; set; }

        /// <summary>
        /// グループ名
        /// </summary>
        [Required]
        [StringLength(20)]
        public string GroupName { get; set; }

        /// <summary>
        /// フリガナ
        /// </summary>
        [StringLength(20)]
        public string GroupKana { get; set; }

        /// <summary>
        /// 略称
        /// </summary>
        [StringLength(10)]
        public string GroupRyakusho { get; set; }

        /// <summary>
        /// 有効無効
        /// </summary>
        [Required]
        [StringLength(1)]
        public string ValidFlag { get; set; }

        /// <summary>
        /// 備考
        /// </summary>
        [StringLength(40)]
        public string Remarks { get; set; }

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
    }
}
