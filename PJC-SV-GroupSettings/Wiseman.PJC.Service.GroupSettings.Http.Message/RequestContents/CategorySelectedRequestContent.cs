using System;
using System.ComponentModel;
using Wiseman.PJC.Gen2.DataAnnotations;
using Wiseman.PJC.Gen2.ObjectModel.Interfaces;
using Wiseman.PJC.Service.GroupSettings.Http.Message.Enums;
using Wiseman.PJC.Service.GroupSettings.Http.Message.Interfaces;

namespace Wiseman.PJC.Service.GroupSettings.Http.Message.RequestContent
{
    public class CategorySelectedRequestContent : IID, ILockVersion, IAction
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
        /// 行に対する操作
        /// </summary>
        public ActionFlag ActionFlag { get; set; }
        
        /// <summary>
        /// カテゴリー選択コード
        /// </summary>
        [Required]
        [StringLength(3)]
        public string CategoryselectedCode { get; set; }

        /// <summary>
        /// カテゴリーID
        /// </summary>
        [Required]
        [StringLength(26)]
        public string CategoryId { get; set; }

        /// <summary>
        /// グループ分類ID
        /// </summary>
        [StringLength(26)]
        public string GroupcategoryId { get; set; }

        /// <summary>
        /// 更新アカウントID
        /// </summary>
        [StringLength(50)]
        public string UpdateAccountid { get; set; }

        /// <summary>
        /// 更新ログインID
        /// </summary>
        [StringLength(50)]
        public string UpdateLoginid { get; set; }

        /// <summary>
        /// 更新事業所名ID
        /// </summary>
        [StringLength(50)]
        public string UpdateFacilityid { get; set; }

        /// <summary>
        /// 最終更新日時
        /// </summary>
        public DateTime UpdateTimestamp { get; set; }

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
