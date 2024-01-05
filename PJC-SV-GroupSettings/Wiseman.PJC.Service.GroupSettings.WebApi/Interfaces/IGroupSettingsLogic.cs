using Wiseman.PJC.Gen2.Http.Message.Responses;
using Wiseman.PJC.Service.GroupSettings.RDB.Entities;
using Wiseman.PJC.Service.GroupSettings.Http.Message.RequestContents;
using Wiseman.PJC.Service.GroupSettings.Http.Message.ResponseContents;
using Wiseman.PJC.Service.GroupSettings.WebApi.Entities;

namespace Wiseman.PJC.Service.GroupSettings.WebApi.Interfaces
{
    public interface IGroupSettingsLogic : IDisposable
    {
        Result<List<GroupCategoryResponseContent>> GetGroupManagementAsync(string groupCategoryCode = "",
                                                                    string groupTani = "",
                                                                    string areaCorpId = "",
                                                                    string facilityGroupId = "",
                                                                    string facilityId = "",
                                                                    string groupCode = "",
                                                                    string validFlag = "",
                                                                    int? kijunbi = 0,
                                                                    bool kijunbiFlag = false,
                                                                    string groupManagementCode = "",
                                                                    string postId = "",
                                                                    int? limit = 1000,
                                                                    int? offset = 0);

        Result<List<GroupManagementResponseContent>> GetGroupManagementById(string id = "");
        string ReadNewCodeGroupManagement(string facilityid, string facilitygroupid);
        Result<GroupManagementEntity> ReadByCodeGroupManagement(string groupManagementCode, string facilityid, string facilitygroupid);
        Result<GroupManagementResponseContent> CreateGroupManagementAsync(GroupManagementPostContent content);
        Result<GroupManagementResponseContent> CreateHeaderGroupManagementAsync(GroupManagementPostContent content);
        Result<GroupManagementResponseContent> UpdateGroupManagementAsync(GroupManagementPostContent content);
        Result<CountResponseContent> DeleteGroupManagementAsync(string id = "");

        Result<List<GroupCategoryResponseContent>> GetGroupCategory(string groupTani = "",
                                                                    string groupCategoryCode = "",
                                                                    string searchWord = "",
                                                                    bool searchFlag = false,
                                                                    string areaCorpId = "",
                                                                    string facilityGroupId = "",
                                                                    string facilityId = "",
                                                                    string postId = "",
                                                                    string categoryCode = "",
                                                                    short? limit = 1000,
                                                                    short? offset = 0);

        Result<List<GroupCategoryResponseContent>> GetGroupCategoryById(string id = "");

        Result<List<GroupCategoryResponseContent>> GetGroupCategoryByPostId(string postId = "");

        Result<GroupCategoryResponseContent> CreateGroupCategory(GroupCategoryRequestContent content);

        Result<GroupCategoryResponseContent> CreateAndHeaderUpdateGroupCategory(GroupCategoryRequestContent content);

        Result<GroupCategoryResponseContent> UpdateGroupCategory(GroupCategoryRequestContent content);

        Result<CountResponseContent> DeleteGroupCategoryAsync(string id);

        Result<List<CategoryResponseContent>> GetListCategorySearch(string CategoryCode = "",
                               short limit = 1000,
                               short offset = 0);

        Result<List<GroupCategoryGroupRequestContent>> GetGroupAsync(string searchString = "",
                                                                bool searchFlag = false,
                                                                string groupCategoryCode = "",
                                                                string groupTani = "",
                                                                string groupCode = "",
                                                                string postId = "",
                                                                string validFlag = "",
                                                                int? kijunbi = 0,
                                                                bool kijunbiFlag = false,
                                                                int? limit = null,
                                                                int? offset = null);
        Result<List<GroupCategoryGroupRequestContent>> GetGroupByIdAsync(string id = "");
        Result<GroupCategoryGroupRequestContent> CreateGroupAsync(GroupRequestContent content);
        Result<GroupCategoryGroupRequestContent> CreateAndHeaderUpdateGroupAsync(GroupRequestContent content);
        Result<GroupCategoryGroupRequestContent> UpdateGroupAsync(GroupRequestContent content);
        Result<CountResponseContent> DeleteGroupAsync(string id);

        string ReadNewCodeGroup(string groupcategoryid);
        Result<GroupReadByCodeEntity> ReadByCodeGroup(string groupcode, string facilityid, string facilitygroupid, string areacorpid);

        Result<GroupCategoryResultEntity> ReadByCodeGroupCategory(string facilityid, string groupcategorycode, string facilitygroupid, string areacorpid);
        string ReadNewCodeGroupCategory(string facilityid);

        Result<CountResponseContent> DeleteGroupPatientAsync(List<string> idList);
        Result<List<GroupCategoryResponseContent>> GetGroupPatientAsync(string groupCategoryCode = "",
                                                                string groupTani = "",
                                                                string areaCorpId = "",
                                                                string facilityGroupId = "",
                                                                string facilityId = "",
                                                                string groupCode = "",
                                                                string validFlag = "",
                                                                string groupManagementCode = "",
                                                                int? kijunbi = 0,
                                                                bool kijunbiFlag = false,
                                                                string patientId = "",
                                                                string postId = "",
                                                                int? limit = 1000,
                                                                int? offset = 0);

        Result<List<GroupPatientResponseContent>> GetGroupPatientByPostId(string postId);

        Result<List<GroupCategoryResponseContent>> GetUnregistGroupAsync(string searchWord = "",
                                                                        bool searchFlag = false,
                                                                        string areaCorpId = "",
                                                                        string facilityGroupId = "",
                                                                        string facilityId = "",
                                                                        int? kijunbi = 0,
                                                                        string patientId = "",
                                                                        string postId = "",
                                                                        int? limit = 1000,
                                                                        int? offset = 0);
        Result<List<GroupPatientResponseContent>> GetGroupPatientById(string id);

        string GetGroupManagementForCreateGroupPatient(string groupId);

        Result<List<GroupPatientResponseContent>> CreateGroupPatient(List<GroupPatientPostContent> content);

        Result<List<GroupPatientResponseContent>> UpdateGroupPatient(List<GroupPatientPostContent> content);
    }
}
