using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiseman.PJC.Gen2.ObjectModel.Interfaces;
using Wiseman.PJC.Service.GroupSettings.Http.Message.RequestContents;

namespace Wiseman.PJC.Service.GroupSettings.Http.Message.ResponseContents
{
    /// <summary>
    /// サンプルレスポンスコンテンツクラス
    /// </summary>
    public class CategorySelectedResponseContent : IID
    {
        /// <summary>
        /// サロゲートキー
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// ロックバージョン
        /// </summary>
        public long? LockVersion { get; set; }
        /// <summary>
        /// カテゴリー選択コード
        /// </summary>
        public string CategoryselectedCode { get; set; }
        /// <summary>
        /// グループ分類ID
        /// </summary>
        public string GroupcategoryId { get; set; }
        /// <summary>
        /// カテゴリーID
        /// </summary>
        public string CategoryId { get; set; }
        /// <summary>
        /// 更新アカウントID
        /// </summary>
        public string UpdateAccountid { get; set; }
        /// <summary>
        /// 更新ログインID
        /// </summary>
        public string UpdateLoginid { get; set; }
        /// <summary>
        /// 更新事業所名ID
        /// </summary>
        public string UpdateFacilityid { get; set; }
        /// <summary>
        /// 最終更新日時
        /// </summary>
        public DateTime UpdateTimestamp { get; set; }
        /// <summary>
        /// POST識別子
        /// </summary>
        public string PostID { get; set; }
        /// <summary>
        /// 最終更新者名
        /// </summary>
        public string LastUpdaterName { get; set; }
        /// <summary>
        /// 最終更新職員ID
        /// </summary>
        public string LastUpdaterId { get; set; }
        /// <summary>
        /// グループ分類名
        /// </summary>
        public string CategoryselectedName { get; set; }
    }
}
