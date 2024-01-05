using System;
using System.Collections.Generic;
using System.Text;

namespace Wiseman.PJC.Service.GroupSettings.Http.Message.Enums
{
    public enum State
    {
        Success = 0,
        CodeUsed = 1,
        Conflict = 2,
        NotFound = 3,
    }
}
