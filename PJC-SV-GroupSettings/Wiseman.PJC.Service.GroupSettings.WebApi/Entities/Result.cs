using Wiseman.PJC.Service.GroupSettings.Http.Message.Enums;

namespace Wiseman.PJC.Service.GroupSettings.WebApi.Entities
{    /// <summary>
     /// Modelの結果返却クラス
     /// </summary>
     /// <typeparam name="T"></typeparam>
    public class Result<T>
    {
        /// <summary>
        /// 結果
        /// </summary>
        public State State { get; set; }

        /// <summary>
        /// エラーメッセージ
        /// </summary>
        public string? ErrorJson { get; set; }

        /// <summary>
        /// 成功時の値
        /// </summary>
        public T? Content { get; set; }
    }
}
