using Wiseman.PJC.Gen2.ObjectModel;
using Wiseman.PJC.Gen2.RDB;
using Wiseman.PJC.Gen2.RDB.Interfaces;
using Wiseman.PJC.Service.GroupSettings.Http.Message.RequestContent;
using Wiseman.PJC.Service.GroupSettings.Http.Message.RequestContents;
using Wiseman.PJC.Service.GroupSettings.Http.Message.ResponseContents;
using Wiseman.PJC.Service.GroupSettings.RDB;
using Wiseman.PJC.Service.GroupSettings.RDB.Entities;
using Wiseman.PJC.Service.GroupSettings.RDB.Interfaces;
using Wiseman.PJC.Service.GroupSettings.WebApi.Interfaces;

namespace Wiseman.PJC.Service.GroupSettings.WebApi.Logics
{
    public class EntryCategorySelected
    {
        public EntryCategorySelected() { }

        /// <summary>
        /// グループ分類登録
        /// </summary>
        /// <param name="accessor"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public Gen2.RDB.Entities.Result<CategorySelectedEntity> Post(ICategorySelectedAccess access,
                                ICategorySelectedJnlAccess jnlAccess, CategorySelectedRequestContent content)
        {

            var categorySelected = new CategorySelectedEntity()
            {
                
                LockVersion = 0,

                CATEGORYSELECTED_CODE = content.CategoryselectedCode,

                CATEGORY_ID = content.CategoryId,

                GROUPCATEGORY_ID = content.GroupcategoryId,

                POST_ID = content.PostID,

                LASTUPDATER_NAME = content.LastUpdaterName,

                LASTUPDATER_ID = content.LastUpdaterId
            };

            var returnCategorSelectedyValue = access.Create(categorySelected);

            if (returnCategorSelectedyValue.Count > 0)
            {

                var returnJnlValue = CreateCategorySelectedJnlLogic(jnlAccess, returnCategorSelectedyValue.Entity, "1");

                if (returnJnlValue == 0)
                {
                    return new Gen2.RDB.Entities.Result<CategorySelectedEntity>();
                }

                return returnCategorSelectedyValue;
            }

            return new Gen2.RDB.Entities.Result<CategorySelectedEntity>();
        }

        public bool Delete(ICategorySelectedAccess access,
                                ICategorySelectedJnlAccess jnlAccess, CategorySelectedResponseContent content)
        {

            var returnValue = access.Delete(content.Id);

            if (returnValue)
            {
                var Jurnalobj = new CategorySelectedEntity()
                {
                    Id = content.Id,
                    CATEGORYSELECTED_CODE = content.CategoryselectedCode,
                    CATEGORY_ID = content.CategoryId,
                    GROUPCATEGORY_ID = content.GroupcategoryId,
                    LASTUPDATER_ID = content.LastUpdaterId,
                    LASTUPDATER_NAME = content.LastUpdaterName,
                    LockVersion = content.LockVersion,
                    POST_ID = content.PostID,
                    Update_AccountId = content.UpdateAccountid,
                    Update_FacilityId = content.UpdateFacilityid,
                    Update_LoginId = content.UpdateLoginid,
                    Update_Timestamp = content.UpdateTimestamp
                };

                return CreateCategorySelectedJnlLogic(jnlAccess, Jurnalobj, "3") > 0;
            }

            return false;
        }

        /// <summary>
        ///  グループ分類仕訳登録
        /// </summary>
        /// <param name="accessor"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        private int CreateCategorySelectedJnlLogic(ICategorySelectedJnlAccess jnlAccess, CategorySelectedEntity content, string operation)
        {
            // グループ カテゴリ ジャーナル エンティティ
            var Jnlobj = new CategorySelectedJnlEntity()
            {
                Id = content.Id,
                LockVersion = content.LockVersion,
                CATEGORYSELECTED_CODE = content.CATEGORYSELECTED_CODE,
                GROUPCATEGORY_ID = content.GROUPCATEGORY_ID,
                LASTUPDATER_ID = content.LASTUPDATER_ID,
                LASTUPDATER_NAME = content.LASTUPDATER_NAME,
                OPERATION = operation,
                POST_ID = content.POST_ID,
                CATEGORY_ID = content.CATEGORY_ID,
                Update_AccountId = content.Update_AccountId,
                Update_FacilityId = content.Update_FacilityId,
                Update_LoginId = content.Update_LoginId,
                Update_Timestamp = content.Update_Timestamp,
                REC_ID = content.Id
            };

            return jnlAccess.Create(Jnlobj);
        }

        #region IDisposable Support
        private bool disposedValue = false; // 重複する呼び出しを検出するには

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: マネージ状態を破棄します (マネージ オブジェクト)。                              
                }

                // TODO: アンマネージ リソース (アンマネージ オブジェクト) を解放し、下のファイナライザーをオーバーライドします。
                // TODO: 大きなフィールドを null に設定します。

                disposedValue = true;
            }
        }

        // TODO: 上の Dispose(bool disposing) にアンマネージ リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします。
        // ~ProfileModel() {
        //   // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
        //   Dispose(false);
        // }

        // このコードは、破棄可能なパターンを正しく実装できるように追加されました。
        public void Dispose()
        {
            // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
            Dispose(true);
            // TODO: 上のファイナライザーがオーバーライドされる場合は、次の行のコメントを解除してください。
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
