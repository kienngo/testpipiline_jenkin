using Wiseman.PJC.Gen2.Http.Message.ErrorTypes;
using Wiseman.PJC.Service.GroupSettings.Http.Message.ErrorContents;

namespace Wiseman.PJC.Service.GroupSettings.Http.Message.ErrorContent
{
    /// <summary>
    /// サンプルBadRequestエラーコンテンツ
    /// </summary>
    public class GroupPatientBadRequestErrorContent : AllStandardErrorResponseContent
    {
        /// <summary>
        /// 一意制約エラー
        /// </summary>
        public BasicError ErrorCode1001 { get; set; }
        /// <summary>
        /// 不明なエラー
        /// </summary>
        public BasicError ErrorCode1002 { get; set; }
        /// <summary>
        /// 選択したグループは既に登録済です
        /// </summary>
        public BasicError ErrorCode1003 { get; set; }
    }
}
