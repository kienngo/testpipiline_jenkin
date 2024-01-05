using Wiseman.PJC.Service.GroupSettings.Http.Message.RequestContents;
using Wiseman.PJC.Service.GroupSettings.Http.Message.ResponseContents;
using Wiseman.PJC.Service.GroupSettings.RDB.Entities;

namespace Wiseman.PJC.Service.GroupSettings.WebApi.Logics
{
    public class ReplaceGroupStaffEntity
    {
        public static GroupStaffEntity ConvertResquestToEntity(GroupStaffPostContent content)
        {
            return new GroupStaffEntity()
            {
                Id = content.Id,
                STARTDATE = content.StartDate,
                GROUPMANAGEMENT_ID = content.GroupManagementId,
                FACILITYGROUP_ID = content.FacilityGroupId,
                ENDDATE = content.EndDate,
                DISPLAY_ORDER = content.DisplayOrder,
                AREACORP_ID = content.AreaCorpId,
                FACILITY_ID = content.FacilityId,
                LASTUPDATER_ID = content.LastUpdaterId,
                LASTUPDATER_NAME = content.LastUpdaterName,
                LockVersion = content.LockVersion,
                POST_ID = content.PostID,
                GROUPSTAFF_CODE = content.GroupStaffCode,
                STAFF_ID = content.StaffId
            };
        }

        public static GroupStaffEntity ConvertResponseToEntity(GroupStaffResponseContent content)
        {
            return new GroupStaffEntity()
            {
                Id = content.Id,
                STARTDATE = content.StartDate,
                STAFF_ID = content.StaffId,
                GROUPSTAFF_CODE = content.GroupStaffCode,
                GROUPMANAGEMENT_ID = content.GroupManagementId,
                FACILITYGROUP_ID = content.FacilityGroupId,
                ENDDATE = content.EndDate,
                DISPLAY_ORDER = content.DisplayOrder,
                AREACORP_ID = content.AreaCorpId,
                FACILITY_ID = content.FacilityId,
                LASTUPDATER_ID = content.LastUpdaterId,
                LASTUPDATER_NAME = content.LastUpdaterName,
                LockVersion = content.LockVersion,
                POST_ID = content.PostID,
                Update_AccountId = content.UpdateAccountid,
                Update_FacilityId = content.UpdateFacilityid,
                Update_LoginId = content.UpdateLoginid,
                Update_Timestamp = content.UpdateTimestamp
            };
        }

        public static GroupStaffJnlEntity ConvertToGroupStaffJnl(GroupStaffEntity content, string operation)
        {
            return new GroupStaffJnlEntity()
            {
                LockVersion = content.LockVersion,
                OPERATION = operation,
                REC_ID = content.Id,
                FACILITY_ID = content.FACILITY_ID,
                AREACORP_ID = content.AREACORP_ID,
                POST_ID = content.POST_ID,
                Update_AccountId = content.Update_AccountId,
                Update_LoginId = content.Update_LoginId,
                Update_FacilityId = content.Update_FacilityId,
                Update_Timestamp = content.Update_Timestamp,
                LASTUPDATER_NAME = content.LASTUPDATER_NAME,
                LASTUPDATER_ID = content.LASTUPDATER_ID,
                DISPLAY_ORDER = content.DISPLAY_ORDER,
                ENDDATE = content.ENDDATE,
                FACILITYGROUP_ID = content.FACILITYGROUP_ID,
                GROUPMANAGEMENT_ID = content.GROUPMANAGEMENT_ID,
                GROUPSTAFF_CODE = content.GROUPSTAFF_CODE,
                STAFF_ID = content.STAFF_ID,
                STARTDATE = content.STARTDATE
            };
        }
    }
}
