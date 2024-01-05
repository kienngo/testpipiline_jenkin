using System;
using System.Collections.Generic;
using Wiseman.PJC.Gen2.ObjectModel.Interfaces;
using IID_IdType = System.String;

namespace Wiseman.PJC.Service.GroupSettings.Http.Message.ResponseContents
{
    public class GroupCategoryResponseContent : IID, ILockVersion
    {
        public IID_IdType Id { get; set; }
        public long? LockVersion { get; set; }
        public string AreaCorpId { get; set; }
        public string FacilityGroupId { get; set; }
        public string FacilityId { get; set; }
        public string GroupCategoryCode { get; set; }
        public string GroupCategoryName { get; set; }
        public string GroupCategoryKana { get; set; }
        public string GroupCategoryRyakusho { get; set; }
        public string GroupTani { get; set; }
        public int DisplayOrder { get; set; }
        public string IsDeleted { get; set; }
        public string UpdateAccountid { get; set; }
        public string UpdateLoginid { get; set; }
        public string UpdateFacilityid { get; set; }
        public DateTime UpdateTimestamp { get; set; }
        public string PostID { get; set; }
        public string LastUpdaterName { get; set; }
        public string LastUpdaterId { get; set; }
        public int NumOfGroup { get; set; }
        public List<CategorySelectedResponseContent> CategorySelectedResponseContent { get; set; } = new List<CategorySelectedResponseContent>();
        public List<GroupResponseContent> GroupResponseContent { get; set; } = new List<GroupResponseContent>();
    }
}
