using System.Collections.Generic;
using System.ComponentModel;
using Wiseman.PJC.Gen2.DataAnnotations;
using Wiseman.PJC.Gen2.ObjectModel.Interfaces;
using Wiseman.PJC.Service.GroupSettings.Http.Message.Interfaces;
using Wiseman.PJC.Service.GroupSettings.Http.Message.RequestContent;
using IID_IdType = System.String;

namespace Wiseman.PJC.Service.GroupSettings.Http.Message.RequestContents
{
    public class GroupCategoryRequestContent : IID, ILockVersion
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
        /// グループ分類コード
        /// </summary>
        [StringLength(4)]
        public string GroupCategoryCode { get; set; }

        /// <summary>
        /// グループ分類名
        /// </summary>
        [Required]
        [StringLength(20)]
        public string GroupCategoryName { get; set; }

        /// <summary>
        /// フリガナ
        /// </summary>
        [StringLength(20)]
        public string GroupCategoryKana { get; set; }

        /// <summary>
        /// 略称
        /// </summary>
        [StringLength(10)]
        public string GroupCategoryRyakusho { get; set; }

        /// <summary>
        /// グループ管理単位
        /// </summary>
        [Required]
        [StringLength(1)]
        public string GroupTani { get; set; }

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
        /// 削除フラグ
        /// </summary>
        [StringLength(1)]
        public string IsDeleted { get; set; }

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
        /// カテゴリー選択
        /// </summary>
        public List<CategorySelectedRequestContent> CategorySelectedRequestContent { get; set; } = new List<CategorySelectedRequestContent>();

    }
}
