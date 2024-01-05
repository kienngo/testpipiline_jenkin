using Wiseman.PJC.Service.GroupSettings.Http.Message.RequestContent;
using Wiseman.PJC.Service.GroupSettings.Http.Message.ResponseContents;
using Wiseman.PJC.Service.GroupSettings.RDB.Entities;

namespace Wiseman.PJC.Service.GroupSettings.WebApi.Logics
{
    public class ReplaceCategorySelectedEntity
    {
        /// <summary>
        /// リード用のEntityから返却用のEntityに変換
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<CategorySelectedResponseContent> CreateList(IList<CategorySelectedEntity> list)
        {

            if (list.Count == 0)
            {
                return null;
            }
            var returnlist = new List<CategorySelectedResponseContent>();

            var result = new CategorySelectedResponseContent();

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
                    result = new CategorySelectedResponseContent();
                    result.Id = item.Id;
                    result.LockVersion = item.LockVersion;
                    result.CategoryselectedCode = item.CATEGORYSELECTED_CODE;
                    //result.CategoryselectedName = item.CATEGORYSELECTED_NAME;
                    result.CategoryId = item.CATEGORY_ID;
                    result.GroupcategoryId = item.GROUPCATEGORY_ID;
                    result.UpdateAccountid = item.Update_AccountId;
                    result.UpdateLoginid = item.Update_LoginId;
                    result.UpdateFacilityid = item.Update_FacilityId;
                    result.UpdateTimestamp = item.Update_Timestamp;
                    result.PostID = item.POST_ID;
                    result.LastUpdaterName = item.LASTUPDATER_NAME;
                    result.LastUpdaterId = item.LASTUPDATER_ID;
                }
            }

            if (!string.IsNullOrWhiteSpace(headerid))
            {
                returnlist.Add(result);
            }

            return returnlist;
        }

        /// <summary>
        /// リクエスト内容からレスポンス内容への変換
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        public static CategorySelectedResponseContent ConvertRequestToResponse(CategorySelectedRequestContent rq)
        {
            if (rq == null)
            {
                return null;
            }

            var result = new CategorySelectedResponseContent();

            result.Id = rq.Id;
            result.LockVersion = rq.LockVersion;
            result.CategoryselectedCode = rq.CategoryselectedCode;
            result.CategoryId = rq.CategoryId;
            result.GroupcategoryId = rq.GroupcategoryId;
            result.UpdateAccountid = rq.UpdateAccountid;
            result.UpdateLoginid = rq.UpdateLoginid;
            result.UpdateFacilityid = rq.UpdateFacilityid;
            result.UpdateTimestamp = rq.UpdateTimestamp;
            result.PostID = rq.PostID;
            result.LastUpdaterName = rq.LastUpdaterName;
            result.LastUpdaterId = rq.LastUpdaterId;

            return result;
        }
    }
}
