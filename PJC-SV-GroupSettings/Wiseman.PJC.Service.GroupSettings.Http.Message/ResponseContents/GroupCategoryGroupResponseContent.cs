using System.Collections.Generic;
using Wiseman.PJC.Service.GroupSettings.Http.Message.RequestContents;

namespace Wiseman.PJC.Service.GroupSettings.Http.Message.ResponseContents
{
    public class GroupCategoryGroupRequestContent : GroupCategoryResponseContent
    {
        public List<GroupResponseContent> GroupResponseContent { get; set; } = new List<GroupResponseContent>();
    }
}
