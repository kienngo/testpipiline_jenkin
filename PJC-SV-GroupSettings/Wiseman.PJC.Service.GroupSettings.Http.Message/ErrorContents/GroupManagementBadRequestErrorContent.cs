using Wiseman.PJC.Gen2.Http.Message.ErrorTypes;

namespace Wiseman.PJC.Service.GroupSettings.Http.Message.ErrorContents
{
    public class GroupManagementBadRequestErrorContent : AllStandardErrorResponseContent
    {
        /// <summary>
        /// 一意制約エラー
        /// </summary>
        public BasicError ErrorCode1001 { get; set; }
        /// <summary>
        /// 不明なエラー
        /// </summary>
        public BasicError ErrorCode1002 { get; set; }
    }
}
