using System.Net.WebSockets;
using Wiseman.PJC.Service.GroupSettings.Http.Message.RequestContents;using Wiseman.PJC.Service.GroupSettings.Http.Message.ResponseContents;
using Wiseman.PJC.Service.GroupSettings.RDB.Entities;

namespace Wiseman.PJC.Service.GroupSettings.WebApi.Logics
{
    public class ReplaceGroupManagementEntity
    {
        public static List<GroupCategoryResponseContent> CreateList(IList<GroupManagementListDetailEntity> list)
        {
            if (list.Count == 0)
            {
                return null;
            }

            var returnlist = new List<GroupCategoryResponseContent>();

            var result = new GroupCategoryResponseContent();
            var gr = new GroupResponseContent();
            var grMng = new GroupManagementResponseContent();

            string groupCategoryId = "";
            string groupId = "";
            string groupManagementId = "";

            foreach (var item in list)
            {
                if (groupCategoryId != item.Id)
                {
                    if (!string.IsNullOrWhiteSpace(groupCategoryId))
                    {
                        returnlist.Add(result);
                    }

                    groupCategoryId = item.Id;
                    result = new GroupCategoryResponseContent();
                    result.Id = item.Id;
                    result.LockVersion = item.LockVersion;
                    result.AreaCorpId = item.AREACORP_ID;
                    result.FacilityGroupId = item.FACILITYGROUP_ID;
                    result.FacilityId = item.FACILITY_ID;
                    result.GroupCategoryCode = item.GROUPCATEGORY_CODE;
                    result.GroupCategoryName = item.GROUPCATEGORY_NAME;
                    result.GroupCategoryKana = item.GROUPCATEGORY_KANA;
                    result.GroupCategoryRyakusho = item.GROUPCATEGORY_RYAKUSHO;
                    result.GroupTani = item.GROUPTANI;
                    result.DisplayOrder = item.DISPLAY_ORDER;
                    result.IsDeleted = item.IS_DELETED;
                    result.UpdateAccountid = item.Update_AccountId;
                    result.UpdateLoginid = item.Update_LoginId;
                    result.UpdateFacilityid = item.Update_FacilityId;
                    result.UpdateTimestamp = item.Update_Timestamp;
                    result.PostID = item.POST_ID;
                    result.LastUpdaterId = item.LASTUPDATER_ID;
                }

                if (!string.IsNullOrEmpty(item.DETAIL_ID) && groupId != item.DETAIL_ID)
                {
                    groupId = item.DETAIL_ID;
                    gr = new GroupResponseContent();

                    gr.Id = item.DETAIL_ID;
                    gr.LockVersion = item.DETAIL_LOCKVERSION;
                    gr.AreaCorpId = item.DETAIL_AREACORP_ID;
                    gr.FacilityGroupId = item.DETAIL_FACILITYGROUP_ID;
                    gr.FacilityId = item.DETAIL_FACILITY_ID;
                    gr.GroupCode = item.DETAIL_GROUP_CODE;
                    gr.GroupCategoryId = item.DETAIL_GROUPCATEGORY_ID;
                    gr.GroupName = item.DETAIL_GROUP_NAME;
                    gr.GroupKana = item.DETAIL_GROUP_KANA;
                    gr.GroupRyakusho = item.DETAIL_GROUP_RYAKUSHO;
                    gr.ValidFlag = item.DETAIL_VALID_FLAG;
                    gr.Remarks = item.DETAIL_REMARKS;
                    gr.DisplayOrder = item.DETAIL_DISPLAY_ORDER;
                    gr.IsDeleted = item.DETAIL_IS_DELETED;
                    gr.UpdateAccountid = item.DETAIL_UPDATE_ACCOUNTID;
                    gr.UpdateLoginid = item.DETAIL_UPDATE_LOGINID;
                    gr.UpdateFacilityid = item.DETAIL_UPDATE_FACILITYID;
                    gr.UpdateTimestamp = item.DETAIL_UPDATE_TIMESTAMP;
                    gr.PostID = item.DETAIL_POST_ID;
                    gr.LastUpdaterName = item.DETAIL_LASTUPDATER_NAME;
                    gr.LastUpdaterId = item.DETAIL_LASTUPDATER_ID;

                    result.GroupResponseContent.Add(gr);
                }

                if (!string.IsNullOrEmpty(item.MNG_ID) && groupManagementId != item.MNG_ID)
                {
                    groupManagementId = item.DETAIL_ID;
                    grMng = new GroupManagementResponseContent();

                    grMng.Id = item.MNG_ID;
                    grMng.LockVersion = item.MNG_LOCKVERSION;
                    grMng.AreaCorpId = item.MNG_AREACORP_ID;
                    grMng.FacilityGroupId = item.MNG_FACILITYGROUP_ID;
                    grMng.FacilityId = item.MNG_FACILITY_ID;
                    grMng.GroupManagementCode = item.MNG_GROUPMANAGEMENT_CODE;
                    grMng.GroupId = item.MNG_GROUP_ID;
                    grMng.StartDate = item.MNG_STARTDATE;
                    grMng.EndDate = item.MNG_ENDDATE;
                    grMng.IsDeleted = item.MNG_IS_DELETED;
                    grMng.UpdateAccountid = item.MNG_UPDATE_ACCOUNTID;
                    grMng.UpdateLoginid = item.MNG_UPDATE_LOGINID;
                    grMng.UpdateFacilityid = item.MNG_UPDATE_FACILITYID;
                    grMng.UpdateTimestamp = item.MNG_UPDATE_TIMESTAMP;
                    grMng.PostID = item.MNG_POST_ID;
                    grMng.LastUpdaterName = item.MNG_LASTUPDATER_NAME;
                    grMng.LastUpdaterId = item.MNG_LASTUPDATER_ID;

                    gr.GroupManagementResponseContent.Add(grMng);
                }
            }

            if (!string.IsNullOrWhiteSpace(groupCategoryId))
            {
                returnlist.Add(result);
            }

            return returnlist;
        }

        public static List<GroupManagementResponseContent> CreateDetail(IList<GroupManagementDetailSettingsEntity> list)
        {
            if (list.Count == 0)
            {
                return null;
            }
            var returnlist = new List<GroupManagementResponseContent>();

            var result = new GroupManagementResponseContent();

            string headerid = "";
            string patientId = "";
            string staffId = "";

            foreach (var item in list)
            {
                if (headerid != item.Id)
                {
                    if (!string.IsNullOrWhiteSpace(headerid))
                    {
                        returnlist.Add(result);
                    }

                    headerid = item.Id;
                    result = new GroupManagementResponseContent();
                    result.Id = item.Id;
                    result.LockVersion = item.LockVersion;
                    result.AreaCorpId = item.AREACORP_ID;
                    result.FacilityGroupId = item.FACILITYGROUP_ID;
                    result.FacilityId = item.FACILITY_ID;
                    result.GroupManagementCode = item.GROUPMANAGEMENT_CODE;
                    result.GroupId = item.GROUP_ID;
                    result.StartDate = item.STARTDATE;
                    result.EndDate = item.ENDDATE;
                    result.IsDeleted = item.IS_DELETED;
                    result.UpdateAccountid = item.Update_AccountId;
                    result.UpdateLoginid = item.Update_LoginId;
                    result.UpdateFacilityid = item.Update_FacilityId;
                    result.UpdateTimestamp = item.Update_Timestamp;
                    result.PostID = item.POST_ID;
                    result.LastUpdaterName = item.LASTUPDATER_NAME;
                    result.LastUpdaterId = item.LASTUPDATER_ID;
                }

                if (!string.IsNullOrEmpty(item.PAT_ID) && patientId != item.PAT_ID)
                {
                    patientId = item.PAT_ID;
                    var pat = new GroupPatientResponseContent();

                    pat.Id = item.PAT_ID;
                    pat.LockVersion = item.PAT_LOCKVERSION;
                    pat.AreaCorpId = item.PAT_AREACORP_ID;
                    pat.FacilityGroupId = item.PAT_FACILITYGROUP_ID;
                    pat.FacilityId = item.PAT_FACILITY_ID;
                    pat.GroupPatientCode = item.PAT_GROUPPATIENT_CODE;
                    pat.GroupManagementId = item.PAT_GROUPMANAGEMENT_ID;
                    pat.PatientId = item.PAT_PATIENT_ID;
                    pat.StartDate = item.PAT_STARTDATE;
                    pat.EndDate = item.PAT_ENDDATE;
                    pat.DisplayOrder = item.PAT_DISPLAY_ORDER;
                    pat.UpdateAccountid = item.PAT_UPDATE_ACCOUNTID​;
                    pat.UpdateLoginid = item.PAT_UPDATE_LOGINID​;
                    pat.UpdateFacilityid = item.PAT_UPDATE_FACILIT​YID;
                    pat.UpdateTimestamp = item.PAT_UPDATE_TIMESTAMP;
                    pat.PostID = item.PAT_POST_ID;
                    pat.LastUpdaterName = item.PAT_LASTUPDATER_NAME;
                    pat.LastUpdaterId = item.PAT_LASTUPDATER_ID;

                    result.GroupPatientResponseContent.Add(pat);
                }

                if (!string.IsNullOrEmpty(item.STAFF_ID) && staffId != item.STAFF_ID)
                {
                    staffId = item.STAFF_ID;
                    var staff = new GroupStaffResponseContent();

                    staff.Id = item.STAFF_ID;
                    staff.LockVersion = item.STAFF_LOCKVERSION;
                    staff.AreaCorpId = item.STAFF_AREACORP_ID;
                    staff.FacilityGroupId = item.STAFF_FACILITYGROUP_ID;
                    staff.FacilityId = item.STAFF_FACILITY_ID;
                    staff.GroupStaffCode = item.STAFF_GROUPSTAFF_CODE;
                    staff.GroupManagementId = item.STAFF_GROUPMANAGEMENT_ID;
                    staff.StaffId = item.STAFF_STAFF_ID;
                    staff.StartDate = item.STAFF_STARTDATE;
                    staff.EndDate = item.STAFF_ENDDATE;
                    staff.DisplayOrder = item.STAFF_DISPLAY_ORDER;
                    staff.UpdateAccountid = item.STAFF_UPDATE_ACCOUNTID​;
                    staff.UpdateLoginid = item.STAFF_UPDATE_LOGINID​;
                    staff.UpdateFacilityid = item.STAFF_UPDATE_FACILIT​YID;
                    staff.UpdateTimestamp = item.STAFF_UPDATE_TIMESTAMP;
                    staff.PostID = item.STAFF_POST_ID;
                    staff.LastUpdaterName = item.STAFF_LASTUPDATER_NAME;
                    staff.LastUpdaterId = item.STAFF_LASTUPDATER_ID;

                    result.GroupStaffResponseContent.Add(staff);
                }
            }

            if (!string.IsNullOrWhiteSpace(headerid))
            {
                returnlist.Add(result);
            }

            return returnlist;
        }

        public static GroupManagementEntity CreateEntityPost(GroupManagementPostContent content)
        {
            return new GroupManagementEntity()
            {
                Id = content.Id,
                LockVersion = 0,
                FACILITY_ID = content.FacilityId,
                AREACORP_ID = content.AreaCorpId,
                POST_ID = content.PostID,
                IS_DELETED = "0",
                FACILITYGROUP_ID = content.FacilityGroupId,
                LASTUPDATER_NAME = content.LastUpdaterName,
                LASTUPDATER_ID = content.LastUpdaterId,
                ENDDATE = content.EndDate,
                STARTDATE = content.StartDate,
                GROUPMANAGEMENT_CODE = content.GroupManagementCode,
                GROUP_ID = content.GroupId,
            };
        }

        public static GroupManagementEntity CreateEntityPut(GroupManagementPostContent content)
        {
            return new GroupManagementEntity()
            {
                Id = content.Id,
                LockVersion = content.LockVersion,
                FACILITY_ID = content.FacilityId,
                AREACORP_ID = content.AreaCorpId,
                POST_ID = content.PostID,
                IS_DELETED = "0",
                FACILITYGROUP_ID = content.FacilityGroupId,
                LASTUPDATER_NAME = content.LastUpdaterName,
                LASTUPDATER_ID = content.LastUpdaterId,
                ENDDATE = content.EndDate,
                STARTDATE = content.StartDate,
                GROUPMANAGEMENT_CODE = content.GroupManagementCode,
                GROUP_ID = content.GroupId,
            };
        }

        public static GroupManagementEntity ConvertResponseToEntity(GroupManagementResponseContent content)
        {
            return new GroupManagementEntity()
            {
                Id = content.Id,
                LockVersion = content.LockVersion,
                FACILITY_ID = content.FacilityId,
                AREACORP_ID = content.AreaCorpId,
                POST_ID = content.PostID,
                IS_DELETED = "0",
                FACILITYGROUP_ID = content.FacilityGroupId,
                LASTUPDATER_NAME = content.LastUpdaterName,
                LASTUPDATER_ID = content.LastUpdaterId,
                ENDDATE = content.EndDate,
                STARTDATE = content.StartDate,
                GROUPMANAGEMENT_CODE = content.GroupManagementCode,
                GROUP_ID = content.GroupId,
            };
        }

        public static GroupManagementPostContent ConvertPatientPostToManagementPost(GroupPatientPostContent content, string groupManagementCode)
        {
            return new GroupManagementPostContent()
            {
                LockVersion = 0,
                AreaCorpId = content.AreaCorpId,
                FacilityGroupId = content.FacilityGroupId,
                FacilityId = content.FacilityId,
                GroupId = content.Group_Id,
                GroupManagementCode = groupManagementCode,
                StartDate = 0,
                EndDate = 99999999,
                IsDeleted = "0",
                PostID = content.PostID,
                LastUpdaterName = content.LastUpdaterName,
                LastUpdaterId = content.LastUpdaterId
            };
        }
        public static GroupManagementEntity ConvertPatientPostToManagementEntity(GroupPatientPostContent content, string groupManagementCode)
        {
            return new GroupManagementEntity()
            {
                LockVersion = 0,
                FACILITY_ID = content.FacilityId,
                AREACORP_ID = content.AreaCorpId,
                POST_ID = content.PostID,
                IS_DELETED = "0",
                FACILITYGROUP_ID = content.FacilityGroupId,
                LASTUPDATER_NAME = content.LastUpdaterName,
                LASTUPDATER_ID = content.LastUpdaterId,
                STARTDATE = 0,
                ENDDATE = 99999999,
                GROUPMANAGEMENT_CODE = groupManagementCode,
                GROUP_ID = content.Group_Id,
            };
        }
    }
}
