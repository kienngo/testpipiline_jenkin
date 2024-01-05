using System;
using Wiseman.PJC.Gen2.ObjectModel.Interfaces;
using IID_IdType = System.String;

namespace Wiseman.PJC.Service.GroupSettings.Http.Message.ResponseContents
{
    public class CategoryResponseContent : IID, ILockVersion
    {
        public IID_IdType Id { get; set; }

        public long? LockVersion { get; set; }

        public string AreaCorpId { get; set; }

        public string FacilityGroupId { get; set; }

        public string FacilityId { get; set; }

        public string CategoryCode { get; set; }

        public string CategoryName { get; set; }

        public string IsDeleted { get; set; }

        public string UpdateAccountid​ { get; set; }

        public string UpdateLoginid​ { get; set; }

        public string UpdateFacilit​Yid { get; set; }

        public DateTime UpdateTimestamp { get; set; }

        public string PostId { get; set; }

        public string LastupdaterName { get; set; }

        public string LastupdaterId { get; set; }
    }
}
