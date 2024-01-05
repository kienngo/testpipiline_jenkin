using Wiseman.PJC.Gen2.ObjectModel.Interfaces;
using Wiseman.PJC.Service.GroupSettings.Http.Message.ResponseContents;
using Wiseman.PJC.Service.GroupSettings.RDB.Entities;

namespace Wiseman.PJC.Service.GroupSettings.WebApi.Logics
{
    public class ReplaceGroupEntity
    {
        /// <summary>
        /// リード用のEntityから返却用のEntityに変換
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<GroupCategoryGroupRequestContent> CreateList(IList<GroupCategoryGroupEntity> list)
        {

            if (list.Count == 0)
            {
                return null;
            }
            var returnlist = new List<GroupCategoryGroupRequestContent>();

            var result = new GroupCategoryGroupRequestContent();

            string headerid = "";
            string detailid = "";

            foreach (var item in list)
            {
                if (headerid != item.ID)
                {
                    if (!string.IsNullOrWhiteSpace(headerid))
                    {
                        returnlist.Add(result);
                    }

                    headerid = item.ID;
                    result = new GroupCategoryGroupRequestContent();
                    result.Id = item.ID;
                    result.LockVersion = item.LOCKVERSION;
                    result.AreaCorpId = item.FACILITY_ID;
                    result.FacilityGroupId = item.FACILITYGROUP_ID;
                    result.FacilityId = item.FACILITY_ID;
                    result.GroupCategoryCode = item.GROUPCATEGORY_CODE;
                    result.GroupCategoryName = item.GROUPCATEGORY_NAME;
                    result.GroupCategoryKana = item.GROUPCATEGORY_KANA;
                    result.GroupCategoryRyakusho = item.GROUPCATEGORY_RYAKUSHO;
                    result.GroupTani = item.GROUPTANI;
                    result.DisplayOrder = item.DISPLAY_ORDER;
                    result.IsDeleted = item.IS_DELETED;
                    result.UpdateAccountid = item.UPDATE_ACCOUNTID​;
                    result.UpdateLoginid = item.LASTUPDATER_ID;
                    result.UpdateFacilityid = item.UPDATE_FACILIT​YID;
                    result.UpdateTimestamp = item.UPDATE_TIMESTAMP;
                    result.PostID = item.POST_ID;
                    result.LastUpdaterName = item.LASTUPDATER_NAME;
                    result.LastUpdaterId = item.LASTUPDATER_ID;
                }

                if (detailid != item.GRP_ID)
                {
                    detailid = item.GRP_ID;
                    var detail = new GroupResponseContent();

                    detail.Id = item.GRP_ID;
                    detail.LockVersion = item.GRP_LOCKVERSION;
                    detail.AreaCorpId = item.GRP_AREACORP_ID;
                    detail.FacilityGroupId = item.GRP_FACILITYGROUP_ID;
                    detail.FacilityId = item.GRP_FACILITY_ID;
                    detail.GroupCode = item.GRP_GROUP_CODE;
                    detail.GroupCategoryId = item.GRP_GROUPCATEGORY_ID;
                    detail.GroupName = item.GRP_GROUP_NAME;
                    detail.GroupKana = item.GRP_GROUP_KANA;
                    detail.GroupRyakusho = item.GRP_GROUP_RYAKUSHO;
                    detail.ValidFlag = item.GRP_VALID_FLAG;
                    detail.Remarks = item.GRP_REMARKS;
                    detail.DisplayOrder = item.GRP_DISPLAY_ORDER;
                    detail.IsDeleted = item.GRP_IS_DELETED;
                    detail.UpdateAccountid = item.GRP_UPDATE_ACCOUNTID​;
                    detail.UpdateLoginid = item.GRP_UPDATE_LOGINID​;
                    detail.UpdateFacilityid = item.GRP_UPDATE_FACILIT​YID;
                    detail.UpdateTimestamp = item.GRP_UPDATE_TIMESTAMP;
                    detail.PostID = item.GRP_POST_ID;
                    detail.LastUpdaterName = item.GRP_LASTUPDATER_NAME;
                    detail.LastUpdaterId = item.GRP_LASTUPDATER_ID;          
                    detail.NumofMember = item.NUM_OF_MEMBER;

                    if (result.GroupResponseContent == null)
                    {
                        result.GroupResponseContent = new List<GroupResponseContent>();
                    }
                    result.GroupResponseContent.Add(detail);
                }
            }

            if (!string.IsNullOrWhiteSpace(headerid))
            {
                returnlist.Add(result);
            }

            return returnlist;
        }
    }
}
