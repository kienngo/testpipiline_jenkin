using System.Collections.Generic;
using Wiseman.PJC.Service.GroupSettings.Http.Message.RequestContents;
using Wiseman.PJC.Service.GroupSettings.Http.Message.ResponseContents;
using Wiseman.PJC.Service.GroupSettings.RDB.Entities;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace Wiseman.PJC.Service.GroupSettings.WebApi.Logics
{
    public class ReplaceGroupPatientEntity
    {
        public static GroupPatientEntity ConvertResquestToEntity(GroupPatientPostContent content)
        {
            return new GroupPatientEntity()
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
                PATIENT_ID = content.PatientId,
                GROUPPATIENT_CODE = content.GroupPatientCode
            };
        }

        public static GroupPatientEntity ConvertResponseToEntity(GroupPatientResponseContent content)
        {
            return new GroupPatientEntity()
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
                Update_AccountId = content.UpdateAccountid,
                Update_FacilityId = content.UpdateFacilityid,
                Update_LoginId = content.UpdateLoginid,
                Update_Timestamp = content.UpdateTimestamp,
                PATIENT_ID = content.PatientId,
                GROUPPATIENT_CODE = content.GroupPatientCode
            };
        }

        public static GroupPatientJnlEntity ConvertToGroupPatientJnl(GroupPatientEntity content, string operation)
        {
            return new GroupPatientJnlEntity()
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
                STARTDATE = content.STARTDATE,
                GROUPPATIENT_CODE = content.GROUPPATIENT_CODE,
                PATIENT_ID = content.PATIENT_ID
            };
        }
        /// <summary>
        /// リード用のEntityから返却用のEntityに変換
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<GroupCategoryResponseContent> CreateListGroupCategory(IList<GroupPatientResultEntity> list)
        {

            if (list.Count == 0)
            {
                return null;
            }
            var groupCategoryId = "";
            var result = new List<GroupCategoryResponseContent>();
            int groupIndex = -1;
            int groupMngIndex = -1;
            int groupPatIndex = -1;

            foreach (var item in list)
            {
                if (groupCategoryId != item.Id)
                {
                    groupCategoryId = item.Id;

                    var groupCategory = new GroupCategoryResponseContent();
                    groupCategory.Id = item.Id;
                    groupCategory.LockVersion = item.LockVersion;
                    groupCategory.AreaCorpId = item.AREACORP_ID;
                    groupCategory.FacilityGroupId = item.FACILITYGROUP_ID;
                    groupCategory.FacilityId = item.FACILITY_ID;
                    groupCategory.GroupCategoryCode = item.GROUPCATEGORY_CODE;
                    groupCategory.GroupCategoryName = item.GROUPCATEGORY_NAME;
                    groupCategory.GroupCategoryKana = item.GROUPCATEGORY_KANA;
                    groupCategory.GroupCategoryRyakusho = item.GROUPCATEGORY_RYAKUSHO;
                    groupCategory.GroupTani = item.GROUPTANI;
                    groupCategory.DisplayOrder = item.DISPLAY_ORDER;
                    groupCategory.IsDeleted = item.IS_DELETED;
                    groupCategory.UpdateAccountid = item.Update_AccountId;
                    groupCategory.UpdateLoginid = item.Update_LoginId;
                    groupCategory.UpdateFacilityid = item.Update_FacilityId;
                    groupCategory.UpdateTimestamp = item.Update_Timestamp;
                    groupCategory.PostID = item.POST_ID;
                    groupCategory.LastUpdaterName = item.LASTUPDATER_NAME;
                    groupCategory.LastUpdaterId = item.LASTUPDATER_ID;

                    result.Add(groupCategory);
                };

                // Mapping Group
                if (result.Last().GroupResponseContent == null)
                {
                    var detailItem = new GroupResponseContent();
                    
                    detailItem.Id = item.DETAIL_ID;
                    detailItem.LockVersion = item.DETAIL_LOCKVERSION;
                    detailItem.AreaCorpId = item.DETAIL_AREACORP_ID;
                    detailItem.FacilityGroupId = item.DETAIL_FACILITYGROUP_ID;
                    detailItem.FacilityId = item.DETAIL_FACILITY_ID;
                    detailItem.GroupCode = item.DETAIL_GROUP_CODE;
                    detailItem.GroupCategoryId = item.DETAIL_GROUPCATEGORY_ID;
                    detailItem.GroupName = item.DETAIL_GROUP_NAME;
                    detailItem.GroupKana = item.DETAIL_GROUP_KANA;
                    detailItem.GroupRyakusho = item.DETAIL_GROUP_RYAKUSHO;
                    detailItem.ValidFlag = item.DETAIL_VALID_FLAG;
                    detailItem.Remarks = item.DETAIL_REMARKS;
                    detailItem.DisplayOrder = item.DETAIL_DISPLAY_ORDER;
                    detailItem.IsDeleted = item.DETAIL_IS_DELETED;
                    detailItem.UpdateAccountid = item.DETAIL_UPDATE_ACCOUNTID​;
                    detailItem.UpdateLoginid = item.DETAIL_UPDATE_LOGINID​;
                    detailItem.UpdateFacilityid = item.DETAIL_UPDATE_FACILIT​YID;
                    detailItem.UpdateTimestamp = item.DETAIL_UPDATE_TIMESTAMP;
                    detailItem.PostID = item.DETAIL_POST_ID;
                    detailItem.LastUpdaterName = item.DETAIL_LASTUPDATER_NAME;
                    detailItem.LastUpdaterId = item.DETAIL_LASTUPDATER_ID;

                    result.Last().GroupResponseContent = new List<GroupResponseContent>();
                    result.Last().GroupResponseContent.Add(detailItem);

                    groupIndex = result.Last().GroupResponseContent.Count - 1;
                }
                else
                {

                    groupIndex = result.Last().GroupResponseContent.FindIndex(x => x.Id == item.DETAIL_ID);

                    if(groupIndex < 0)
                    {
                        var detailItem = new GroupResponseContent();

                        detailItem.Id = item.DETAIL_ID;
                        detailItem.LockVersion = item.DETAIL_LOCKVERSION;
                        detailItem.AreaCorpId = item.DETAIL_AREACORP_ID;
                        detailItem.FacilityGroupId = item.DETAIL_FACILITYGROUP_ID;
                        detailItem.FacilityId = item.DETAIL_FACILITY_ID;
                        detailItem.GroupCode = item.DETAIL_GROUP_CODE;
                        detailItem.GroupCategoryId = item.DETAIL_GROUPCATEGORY_ID;
                        detailItem.GroupName = item.DETAIL_GROUP_NAME;
                        detailItem.GroupKana = item.DETAIL_GROUP_KANA;
                        detailItem.GroupRyakusho = item.DETAIL_GROUP_RYAKUSHO;
                        detailItem.ValidFlag = item.DETAIL_VALID_FLAG;
                        detailItem.Remarks = item.DETAIL_REMARKS;
                        detailItem.DisplayOrder = item.DETAIL_DISPLAY_ORDER;
                        detailItem.IsDeleted = item.DETAIL_IS_DELETED;
                        detailItem.UpdateAccountid = item.DETAIL_UPDATE_ACCOUNTID​;
                        detailItem.UpdateLoginid = item.DETAIL_UPDATE_LOGINID​;
                        detailItem.UpdateFacilityid = item.DETAIL_UPDATE_FACILIT​YID;
                        detailItem.UpdateTimestamp = item.DETAIL_UPDATE_TIMESTAMP;
                        detailItem.PostID = item.DETAIL_POST_ID;
                        detailItem.LastUpdaterName = item.DETAIL_LASTUPDATER_NAME;
                        detailItem.LastUpdaterId = item.DETAIL_LASTUPDATER_ID;

                        result.Last().GroupResponseContent.Add(detailItem);

                        groupIndex = result.Last().GroupResponseContent.Count - 1;
                    }
                }

                // Mapping Group Management
                if (result.Last().GroupResponseContent[groupIndex].GroupManagementResponseContent == null)
                {
                    var groupManagement = new GroupManagementResponseContent();

                    groupManagement.Id = item.MNG_ID;
                    groupManagement.LockVersion = item.MNG_LOCKVERSION;
                    groupManagement.AreaCorpId = item.MNG_AREACORP_ID;
                    groupManagement.FacilityGroupId = item.MNG_FACILITYGROUP_ID;
                    groupManagement.FacilityId = item.MNG_FACILITY_ID;
                    groupManagement.GroupManagementCode = item.MNG_GROUPMANAGEMENT_CODE;
                    groupManagement.GroupId = item.MNG_GROUP_ID;
                    groupManagement.StartDate = item.MNG_STARTDATE;
                    groupManagement.EndDate = item.MNG_ENDDATE;
                    groupManagement.IsDeleted = item.MNG_IS_DELETED;
                    groupManagement.UpdateAccountid = item.MNG_UPDATE_ACCOUNTID;
                    groupManagement.UpdateLoginid = item.MNG_UPDATE_LOGINID;
                    groupManagement.UpdateFacilityid = item.MNG_UPDATE_FACILITYID;
                    groupManagement.UpdateTimestamp = item.MNG_UPDATE_TIMESTAMP;
                    groupManagement.PostID = item.MNG_POST_ID;
                    groupManagement.LastUpdaterName = item.MNG_LASTUPDATER_NAME;
                    groupManagement.LastUpdaterId = item.MNG_LASTUPDATER_ID;

                    result.Last().GroupResponseContent[groupIndex].GroupManagementResponseContent = new List<GroupManagementResponseContent>();
                    result.Last().GroupResponseContent[groupIndex].GroupManagementResponseContent.Add(groupManagement);

                    groupMngIndex = 0;
                }
                else
                {
                    groupMngIndex = result.Last().GroupResponseContent[groupIndex].GroupManagementResponseContent.FindIndex(x => x.Id == item.MNG_ID);
                    if (groupMngIndex < 0)
                    {
                        var groupManagement = new GroupManagementResponseContent();

                        groupManagement.Id = item.MNG_ID;
                        groupManagement.LockVersion = item.MNG_LOCKVERSION;
                        groupManagement.AreaCorpId = item.MNG_AREACORP_ID;
                        groupManagement.FacilityGroupId = item.MNG_FACILITYGROUP_ID;
                        groupManagement.FacilityId = item.MNG_FACILITY_ID;
                        groupManagement.GroupManagementCode = item.MNG_GROUPMANAGEMENT_CODE;
                        groupManagement.GroupId = item.MNG_GROUP_ID;
                        groupManagement.StartDate = item.MNG_STARTDATE;
                        groupManagement.EndDate = item.MNG_ENDDATE;
                        groupManagement.IsDeleted = item.MNG_IS_DELETED;
                        groupManagement.UpdateAccountid = item.MNG_UPDATE_ACCOUNTID;
                        groupManagement.UpdateLoginid = item.MNG_UPDATE_LOGINID;
                        groupManagement.UpdateFacilityid = item.MNG_UPDATE_FACILITYID;
                        groupManagement.UpdateTimestamp = item.MNG_UPDATE_TIMESTAMP;
                        groupManagement.PostID = item.MNG_POST_ID;
                        groupManagement.LastUpdaterName = item.MNG_LASTUPDATER_NAME;
                        groupManagement.LastUpdaterId = item.MNG_LASTUPDATER_ID;

                        result.Last().GroupResponseContent[groupIndex].GroupManagementResponseContent.Add(groupManagement);

                        groupMngIndex = result.Last().GroupResponseContent[groupIndex].GroupManagementResponseContent.Count - 1;
                    }
                }

                // Mapping Group Patient
                if (result.Last().GroupResponseContent[groupIndex].GroupManagementResponseContent[groupMngIndex].GroupPatientResponseContent == null)
                {
                    var groupPatient = new GroupPatientResponseContent();

                    groupPatient.Id = item.PAT_ID;
                    groupPatient.LockVersion = item.PAT_LOCKVERSION;
                    groupPatient.AreaCorpId = item.PAT_AREACORP_ID;
                    groupPatient.FacilityGroupId = item.PAT_FACILITYGROUP_ID;
                    groupPatient.FacilityId = item.PAT_FACILITY_ID;
                    groupPatient.GroupPatientCode = item.PAT_GROUPPATIENT_CODE;
                    groupPatient.GroupManagementId = item.PAT_GROUPMANAGEMENT_ID;
                    groupPatient.PatientId = item.PAT_PATIENT_ID;
                    groupPatient.StartDate = item.PAT_STARTDATE;
                    groupPatient.EndDate = item.PAT_ENDDATE;
                    groupPatient.DisplayOrder = item.PAT_DISPLAY_ORDER;
                    groupPatient.UpdateAccountid = item.PAT_UPDATE_ACCOUNTID​;
                    groupPatient.UpdateLoginid = item.PAT_UPDATE_LOGINID​;
                    groupPatient.UpdateFacilityid = item.PAT_UPDATE_FACILIT​YID;
                    groupPatient.UpdateTimestamp = item.PAT_UPDATE_TIMESTAMP;
                    groupPatient.PostID = item.PAT_POST_ID;
                    groupPatient.LastUpdaterName = item.PAT_LASTUPDATER_NAME;
                    groupPatient.LastUpdaterId = item.PAT_LASTUPDATER_ID;

                    result.Last().GroupResponseContent[groupIndex].GroupManagementResponseContent[groupMngIndex].GroupPatientResponseContent = new List<GroupPatientResponseContent>();
                    result.Last().GroupResponseContent[groupIndex].GroupManagementResponseContent[groupMngIndex].GroupPatientResponseContent.Add(groupPatient);

                    groupPatIndex = 0;
                }
                else
                {
                    groupPatIndex = result.Last().GroupResponseContent[groupIndex].GroupManagementResponseContent[groupMngIndex].GroupPatientResponseContent.FindIndex(x => x.Id == item.PAT_ID);
                    if (groupPatIndex < 0)
                    {
                        var groupPatient = new GroupPatientResponseContent();

                        groupPatient.Id = item.PAT_ID;
                        groupPatient.LockVersion = item.PAT_LOCKVERSION;
                        groupPatient.AreaCorpId = item.PAT_AREACORP_ID;
                        groupPatient.FacilityGroupId = item.PAT_FACILITYGROUP_ID;
                        groupPatient.FacilityId = item.PAT_FACILITY_ID;
                        groupPatient.GroupPatientCode = item.PAT_GROUPPATIENT_CODE;
                        groupPatient.GroupManagementId = item.PAT_GROUPMANAGEMENT_ID;
                        groupPatient.PatientId = item.PAT_PATIENT_ID;
                        groupPatient.StartDate = item.PAT_STARTDATE;
                        groupPatient.EndDate = item.PAT_ENDDATE;
                        groupPatient.DisplayOrder = item.PAT_DISPLAY_ORDER;
                        groupPatient.UpdateAccountid = item.PAT_UPDATE_ACCOUNTID​;
                        groupPatient.UpdateLoginid = item.PAT_UPDATE_LOGINID​;
                        groupPatient.UpdateFacilityid = item.PAT_UPDATE_FACILIT​YID;
                        groupPatient.UpdateTimestamp = item.PAT_UPDATE_TIMESTAMP;
                        groupPatient.PostID = item.PAT_POST_ID;
                        groupPatient.LastUpdaterName = item.PAT_LASTUPDATER_NAME;
                        groupPatient.LastUpdaterId = item.PAT_LASTUPDATER_ID;

                        result.Last().GroupResponseContent[groupIndex].GroupManagementResponseContent[groupMngIndex].GroupPatientResponseContent.Add(groupPatient);
                        groupPatIndex = result.Last().GroupResponseContent[groupIndex].GroupManagementResponseContent[groupMngIndex].GroupPatientResponseContent.Count - 1;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// リスト エンティティから応答を返すように変換する
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<GroupPatientResponseContent> CreateListPatientResponse(IList<GroupPatientEntity> entity)
        {
            if (entity == null || !entity.Any()) return null;

            var result = new List<GroupPatientResponseContent>();

            foreach(var item in entity)
            {
                var groupPatient = new GroupPatientResponseContent();

                groupPatient.Id = item.Id;
                groupPatient.LockVersion = item.LockVersion;
                groupPatient.AreaCorpId = item.AREACORP_ID;
                groupPatient.FacilityGroupId = item.FACILITYGROUP_ID;
                groupPatient.FacilityId = item.FACILITY_ID;
                groupPatient.GroupPatientCode = item.GROUPPATIENT_CODE;
                groupPatient.GroupManagementId = item.GROUPMANAGEMENT_ID;
                groupPatient.PatientId = item.PATIENT_ID;
                groupPatient.StartDate = item.STARTDATE;
                groupPatient.EndDate = item.ENDDATE;
                groupPatient.DisplayOrder = item.DISPLAY_ORDER;
                groupPatient.UpdateAccountid = item.Update_AccountId;
                groupPatient.UpdateLoginid = item.Update_LoginId;
                groupPatient.UpdateFacilityid = item.Update_FacilityId;
                groupPatient.UpdateTimestamp = item.Update_Timestamp;
                groupPatient.PostID = item.POST_ID;
                groupPatient.LastUpdaterName = item.LASTUPDATER_NAME;
                groupPatient.LastUpdaterId = item.LASTUPDATER_ID;

                result.Add(groupPatient);
            }
            return result;
        }

        /// <summary>
        /// リスト エンティティから応答を返すように変換する
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static GroupPatientResponseContent CreatePatientResponse(GroupPatientEntity entity)
        {
            if (entity == null) return null;

            var groupPatient = new GroupPatientResponseContent();

            groupPatient.Id = entity.Id;
            groupPatient.LockVersion = entity.LockVersion;
            groupPatient.AreaCorpId = entity.AREACORP_ID;
            groupPatient.FacilityGroupId = entity.FACILITYGROUP_ID;
            groupPatient.FacilityId = entity.FACILITY_ID;
            groupPatient.GroupPatientCode = entity.GROUPPATIENT_CODE;
            groupPatient.GroupManagementId = entity.GROUPMANAGEMENT_ID;
            groupPatient.PatientId = entity.PATIENT_ID;
            groupPatient.StartDate = entity.STARTDATE;
            groupPatient.EndDate = entity.ENDDATE;
            groupPatient.DisplayOrder = entity.DISPLAY_ORDER;
            groupPatient.UpdateAccountid = entity.Update_AccountId;
            groupPatient.UpdateLoginid = entity.Update_LoginId;
            groupPatient.UpdateFacilityid = entity.Update_FacilityId;
            groupPatient.UpdateTimestamp = entity.Update_Timestamp;
            groupPatient.PostID = entity.POST_ID;
            groupPatient.LastUpdaterName = entity.LASTUPDATER_NAME;
            groupPatient.LastUpdaterId = entity.LASTUPDATER_ID;

            return groupPatient;
        }
    }
}
