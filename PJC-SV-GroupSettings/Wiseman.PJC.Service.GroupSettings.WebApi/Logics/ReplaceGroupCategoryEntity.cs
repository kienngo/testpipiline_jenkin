using System.Collections.Generic;
using Wiseman.PJC.Service.GroupSettings.Http.Message.ResponseContents;
using Wiseman.PJC.Service.GroupSettings.RDB.Entities;

namespace Wiseman.PJC.Service.GroupSettings.WebApi.Logics
{
    public class ReplaceGroupCategoryEntity
    {
        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public static List<GroupCategoryResponseContent> CreateList(IList<GroupCategoryResultEntity> list)
        {
            if (list.Count == 0)
            {
                return null;
            }
            var returnlist = new List<GroupCategoryResponseContent>();

            var result = new GroupCategoryResponseContent();

            string headerid = "";
            string categoryselectedid = "";

            foreach (var item in list)
            {
                if (headerid != item.Id)
                {
                    if (!string.IsNullOrWhiteSpace(headerid))
                    {
                        returnlist.Add(result);
                    }

                    headerid = item.Id;
                    result = new GroupCategoryResponseContent();
                    result.Id = item.Id;
                    result.LockVersion = item.LockVersion;
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
                    result.NumOfGroup = item.NUM_OF_GROUP;
                }

                if (categoryselectedid != item.CAT_ID)
                {
                    categoryselectedid = item.CAT_ID;
                    var categoryselected = new CategorySelectedResponseContent();

                    categoryselected.Id = item.CAT_ID;
                    categoryselected.LockVersion = item.CAT_LOCKVERSION;
                    categoryselected.CategoryselectedCode = item.CAT_CATEGORYSELECTED_CODE;
                    categoryselected.CategoryselectedName = item.CAT_CATEGORYSELECTED_NAME;
                    categoryselected.GroupcategoryId = item.CAT_GROUPCATEGORY_ID;
                    categoryselected.CategoryId = item.CAT_CATEGORY_ID;
                    categoryselected.UpdateAccountid = item.CAT_UPDATE_ACCOUNTID​;
                    categoryselected.UpdateLoginid = item.CAT_UPDATE_LOGINID​;
                    categoryselected.UpdateFacilityid = item.CAT_UPDATE_FACILIT​YID;
                    categoryselected.UpdateTimestamp = item.CAT_UPDATE_TIMESTAMP;
                    categoryselected.PostID = item.CAT_POST_ID;
                    categoryselected.LastUpdaterName = item.CAT_LASTUPDATER_NAME;
                    categoryselected.LastUpdaterId = item.CAT_LASTUPDATER_ID;

                    if (result.CategorySelectedResponseContent == null)
                    {
                        result.CategorySelectedResponseContent = new List<CategorySelectedResponseContent>();
                    }
                    result.CategorySelectedResponseContent.Add(categoryselected);
                }
            }

            if (!string.IsNullOrWhiteSpace(headerid))
            {
                returnlist.Add(result);
            }

            return returnlist;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static GroupCategoryResponseContent Create(GroupCategoryEntity entity)
        {
            if (entity == null)
            {
                return null;
            }

            var result = new GroupCategoryResponseContent();

            result.Id = entity.Id;
            result.LockVersion = entity.LockVersion;
            result.AreaCorpId = entity.FACILITY_ID;
            result.FacilityGroupId = entity.FACILITYGROUP_ID;
            result.FacilityId = entity.FACILITY_ID;
            result.GroupCategoryCode = entity.GROUPCATEGORY_CODE;
            result.GroupCategoryName = entity.GROUPCATEGORY_NAME;
            result.GroupCategoryKana = entity.GROUPCATEGORY_KANA;
            result.GroupCategoryRyakusho = entity.GROUPCATEGORY_RYAKUSHO;
            result.GroupTani = entity.GROUPTANI;
            result.DisplayOrder = entity.DISPLAY_ORDER;
            result.IsDeleted = entity.IS_DELETED;
            result.UpdateAccountid = entity.Update_AccountId;
            result.UpdateLoginid = entity.LASTUPDATER_ID;
            result.UpdateFacilityid = entity.Update_FacilityId;
            result.UpdateTimestamp = entity.Update_Timestamp;
            result.PostID = entity.POST_ID;
            result.LastUpdaterName = entity.LASTUPDATER_NAME;
            result.LastUpdaterId = entity.LASTUPDATER_ID;

            return result;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public static List<GroupCategoryResponseContent> CreateUnregistList(IList<GroupCategoryGroupEntity> list)
        {
            if (list.Count == 0)
            {
                return null;
            }
            var returnlist = new List<GroupCategoryResponseContent>();
            var result = new GroupCategoryResponseContent();

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
                    result = new GroupCategoryResponseContent();
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
        #endregion

        #region Dispose
        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: マネージド状態を破棄します (マネージド オブジェクト)
                }

                // TODO: アンマネージド リソース (アンマネージド オブジェクト) を解放し、ファイナライザーをオーバーライドします
                // TODO: 大きなフィールドを null に設定します
                disposedValue = true;
            }
        }

        // // TODO: 'Dispose(bool disposing)' にアンマネージド リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします
        // ~xxxHttpClient()
        // {
        //     // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
