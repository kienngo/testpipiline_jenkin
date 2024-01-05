using System;
using System.Collections.Generic;
using System.Text;
using Wiseman.PJC.Gen2.Http.Message.ErrorTypes;

namespace Wiseman.PJC.Service.GroupSettings.Http.Message.Interfaces
{
    public interface IMustBeUniqueProperty
    {
        /// <summary>一意制約エラー</summary>
        BasicError ErrorCode1001 { get; set; }
    }
}
