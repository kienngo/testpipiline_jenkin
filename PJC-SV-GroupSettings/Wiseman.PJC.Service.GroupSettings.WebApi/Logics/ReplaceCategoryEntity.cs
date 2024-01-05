using Wiseman.PJC.Service.GroupSettings.Http.Message.ResponseContents;
using Wiseman.PJC.Service.GroupSettings.RDB.Entities;

namespace Wiseman.PJC.Service.GroupSettings.WebApi.Logics
{
    public class ReplaceCategoryEntity
    {
        /// <summary>
        /// リード用のEntityから返却用のEntityに変換
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<CategoryResponseContent> CreateList(IList<CategoryEntity> list)
        {

            if (list.Count == 0)
            {
                return null;
            }
            var returnlist = new List<CategoryResponseContent>();

            var result = new CategoryResponseContent();

            string headerid = "";

            foreach (var item in list)
            {
                if (headerid != item.Id)
                {
                    if (!string.IsNullOrWhiteSpace(headerid))
                    {
                        returnlist.Add(result);
                    }

                    headerid = item.Id;
                    result = new CategoryResponseContent();
                    result.Id = item.Id;
                    result.LockVersion = item.LockVersion;
                    result.AreaCorpId = item.AREACORP_ID;
                    result.FacilityGroupId = item.FACILITYGROUP_ID;
                    result.FacilityId = item.FACILITY_ID;
                    result.CategoryCode = item.CATEGORY_CODE;
                    result.CategoryName = item.CATEGORY_NAME;
                    result.IsDeleted = item.IS_DELETED;
                    result.UpdateAccountid = item.Update_AccountId;
                    result.UpdateLoginid = item.Update_LoginId;
                    result.UpdateFacilitYid = item.Update_FacilityId;
                    result.UpdateTimestamp = item.Update_Timestamp;
                    result.PostId = item.POST_ID;
                    result.LastupdaterName = item.LASTUPDATER_NAME;
                    result.LastupdaterId = item.LASTUPDATER_ID;
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
