using System;
using System.Collections.Generic;
using Wiseman.PJC.Gen2.ObjectModel.Interfaces;
using IID_IdType = System.String;

namespace Wiseman.PJC.Service.GroupSettings.Http.Message.ResponseContents
{
    public class GroupResponseContent : IID, ILockVersion
    {
        public IID_IdType Id { get; set; }
        public long? LockVersion { get; set; }
        public string AreaCorpId { get; set; }
        public string FacilityGroupId { get; set; }
        public string FacilityId { get; set; }
        public string GroupCode { get; set; }
        public string GroupCategoryId { get; set; }
        public string GroupName { get; set; }
        public string GroupKana { get; set; }
        public string GroupRyakusho { get; set; }
        public string ValidFlag { get; set; }
        public string Remarks { get; set; }
        public int DisplayOrder { get; set; }
        public string IsDeleted { get; set; }
        public string UpdateAccountid { get; set; }
        public string UpdateLoginid { get; set; }
        public string UpdateFacilityid { get; set; }
        public DateTime UpdateTimestamp { get; set; }
        public string PostID { get; set; }
        public string LastUpdaterName { get; set; }
        public string LastUpdaterId { get; set; }
        public int NumofMember { get; set; }
        public List<GroupManagementResponseContent> GroupManagementResponseContent { get; set; } = new List<GroupManagementResponseContent>();
    }
}
