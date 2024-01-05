using System;
using System.Collections.Generic;
using System.Text;
using Wiseman.PJC.Service.GroupSettings.Http.Message.Enums;

namespace Wiseman.PJC.Service.GroupSettings.Http.Message.Interfaces
{
    public interface IAction
    {
        ActionFlag ActionFlag { get; set; }
    }
}
