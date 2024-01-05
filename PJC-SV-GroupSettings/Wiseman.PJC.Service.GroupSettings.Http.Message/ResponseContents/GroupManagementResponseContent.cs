﻿using System;
using System.Collections.Generic;
using Wiseman.PJC.Gen2.ObjectModel.Interfaces;
using IID_IdType = System.String;

namespace Wiseman.PJC.Service.GroupSettings.Http.Message.ResponseContents
{
    public class GroupManagementResponseContent : IID, ILockVersion
    {
        public IID_IdType Id { get; set; }
        public long? LockVersion { get; set; }
        public string AreaCorpId { get; set; }
        public string FacilityGroupId { get; set; }
        public string FacilityId { get; set; }
        public string GroupManagementCode { get; set; }
        public string GroupId { get; set; }
        public int StartDate { get; set; }
        public int EndDate { get; set; }
        public string IsDeleted { get; set; }
        public string UpdateAccountid { get; set; }
        public string UpdateLoginid { get; set; }
        public string UpdateFacilityid { get; set; }
        public DateTime UpdateTimestamp { get; set; }
        public string PostID { get; set; }
        public string LastUpdaterName { get; set; }
        public string LastUpdaterId { get; set; }

        public List<GroupPatientResponseContent> GroupPatientResponseContent { get; set; } = new List<GroupPatientResponseContent>();
        public List<GroupStaffResponseContent> GroupStaffResponseContent { get; set; } = new List<GroupStaffResponseContent>();
    }
}
