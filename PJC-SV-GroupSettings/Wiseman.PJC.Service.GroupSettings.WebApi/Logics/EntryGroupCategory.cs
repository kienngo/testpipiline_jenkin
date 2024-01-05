using Wiseman.PJC.Gen2.Http.Message.Responses;
using Wiseman.PJC.Gen2.RDB.Interfaces;
using Wiseman.PJC.Service.GroupSettings.Http.Message.RequestContents;
using Wiseman.PJC.Service.GroupSettings.Http.Message.ResponseContents;
using Wiseman.PJC.Service.GroupSettings.RDB;
using Wiseman.PJC.Service.GroupSettings.RDB.Entities;
using Wiseman.PJC.Service.GroupSettings.RDB.Interfaces;
using Wiseman.PJC.Service.GroupSettings.WebApi.Interfaces;

namespace Wiseman.PJC.Service.GroupSettings.WebApi.Logics
{
    public class EntryGroupCategory
    {
        public EntryGroupCategory() { }

        /// <summary>
        /// グループ分類登録
        /// </summary>
        /// <param name="accessor"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public Gen2.RDB.Entities.Result<GroupCategoryEntity> Post(IDBAccess accessor, IGroupSettingsFactory factory, GroupCategoryRequestContent content)
        {

            var groupCategory = new GroupCategoryEntity()
            {
                LockVersion = 0,
                AREACORP_ID = content.AreaCorpId,
                FACILITYGROUP_ID = content.FacilityGroupId,
                FACILITY_ID = content.FacilityId,
                GROUPCATEGORY_CODE = content.GroupCategoryCode,
                GROUPCATEGORY_NAME = content.GroupCategoryName,
                GROUPCATEGORY_KANA = content.GroupCategoryKana,
                GROUPCATEGORY_RYAKUSHO = content.GroupCategoryRyakusho,
                GROUPTANI = content.GroupTani,
                DISPLAY_ORDER = content.DisplayOrder,
                IS_DELETED = "0",
                POST_ID = content.PostID,
                LASTUPDATER_NAME = content.LastUpdaterName,
                LASTUPDATER_ID = content.LastUpdaterId
            };

            var GroupCategoryAccess = factory.CreateGroupCategoryAccess(accessor);
            var returnKohoValue = GroupCategoryAccess.CreateGroupCategory(groupCategory);
            if (returnKohoValue.Count > 0)
            {
                //重症度・看護必要度評価マスター候補ジャーナル
                var returnJnlValue = CreateGroupCategoryJurnalLogic(accessor, factory, returnKohoValue.Entity, "1");

                if (returnJnlValue == 0)
                {
                    return new Gen2.RDB.Entities.Result<GroupCategoryEntity>();
                }

                return returnKohoValue;
            }

            return new Gen2.RDB.Entities.Result<GroupCategoryEntity>();
        }

        public Gen2.RDB.Entities.Result<GroupCategoryEntity> Put(IDBAccess accessor, IGroupSettingsFactory factory, GroupCategoryRequestContent content)
        {
            // よく使う項目マスター
            var obj = new GroupCategoryEntity()
            {
                Id = content.Id,
                LockVersion = content.LockVersion,
                AREACORP_ID = content.AreaCorpId,
                FACILITYGROUP_ID = content.FacilityGroupId,
                FACILITY_ID = content.FacilityId,
                GROUPCATEGORY_CODE = content.GroupCategoryCode,
                GROUPCATEGORY_NAME = content.GroupCategoryName,
                GROUPCATEGORY_KANA = content.GroupCategoryKana,
                GROUPCATEGORY_RYAKUSHO = content.GroupCategoryRyakusho,
                GROUPTANI = content.GroupTani,
                DISPLAY_ORDER = content.DisplayOrder,
                IS_DELETED = "0",
                POST_ID = content.PostID,
                LASTUPDATER_NAME = content.LastUpdaterName,
                LASTUPDATER_ID = content.LastUpdaterId,
                Update_Timestamp = DateTime.Now
            };

            var access = factory.CreateGroupCategoryAccess(accessor);
            var returnValue = access.UpdateGroupCategory(obj);
            if (returnValue.Count > 0)
            {
                var returnJurnalValue = CreateGroupCategoryJurnalLogic(accessor, factory, returnValue.Entity, "2");
                if (returnJurnalValue == 0)
                {
                    return new Gen2.RDB.Entities.Result<GroupCategoryEntity>();
                }
                return returnValue;
            }

            return new Gen2.RDB.Entities.Result<GroupCategoryEntity>();
        }

        public Gen2.RDB.Entities.Result<GroupCategoryDeleteEntity> Delete(IDBAccess accessor, IGroupSettingsFactory factory, GroupCategoryResponseContent content)
        {
            var obj = new GroupCategoryDeleteEntity()
            {
                Id = content.Id,
                LockVersion = content.LockVersion,
                Is_Deleted = "1",
            };

            var access = factory.CreateGroupCategoryAccess(accessor);
            var returnValue = access.UpdateForDelete(obj);
            if (returnValue.Count > 0)
            {
                var Jurnalobj = new GroupCategoryEntity()
                {
                    Id = content.Id,
                    LockVersion = content.LockVersion,
                    FACILITY_ID = content.FacilityId,
                    AREACORP_ID = content.AreaCorpId,
                    IS_DELETED = "1",
                    POST_ID = content.PostID,
                    Update_AccountId = returnValue.Entity.Update_AccountId,
                    Update_LoginId = returnValue.Entity.Update_LoginId,
                    Update_FacilityId = returnValue.Entity.Update_FacilityId,
                    LASTUPDATER_NAME = content.LastUpdaterName,
                    LASTUPDATER_ID = content.LastUpdaterId,
                    DISPLAY_ORDER = content.DisplayOrder,
                    FACILITYGROUP_ID = content.FacilityGroupId,
                    Update_Timestamp = content.UpdateTimestamp,
                    GROUPCATEGORY_CODE = content.GroupCategoryCode,
                    GROUPCATEGORY_KANA = content.GroupCategoryKana,
                    GROUPCATEGORY_NAME = content.GroupCategoryName,
                    GROUPCATEGORY_RYAKUSHO = content.GroupCategoryRyakusho,
                    GROUPTANI = content.GroupTani
                };
                var returnJurnalValue = CreateGroupCategoryJurnalLogic(accessor, factory, Jurnalobj, "3");
                if (returnJurnalValue == 0)
                {
                    return new Gen2.RDB.Entities.Result<GroupCategoryDeleteEntity>();
                }

                return returnValue;
            }

            return new Gen2.RDB.Entities.Result<GroupCategoryDeleteEntity>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accessor"></param>
        /// <param name="factory"></param>
        /// <param name="content"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        private int CreateGroupCategoryJurnalLogic(IDBAccess accessor, IGroupSettingsFactory factory, GroupCategoryEntity content, string operation)
        {
            var Jurnalobj = new GroupCategoryJnlEntity()
            {
                Id = content.Id,
                LockVersion = content.LockVersion,
                FACILITY_ID = content.FACILITY_ID,
                AREACORP_ID = content.AREACORP_ID,
                IS_DELETED = content.IS_DELETED,
                POST_ID = content.POST_ID,
                Update_AccountId = content.Update_AccountId,
                Update_LoginId = content.Update_LoginId,
                Update_FacilityId = content.Update_FacilityId,
                LASTUPDATER_NAME = content.LASTUPDATER_NAME,
                LASTUPDATER_ID = content.LASTUPDATER_ID,
                DISPLAY_ORDER = content.DISPLAY_ORDER,
                FACILITYGROUP_ID = content.FACILITYGROUP_ID,
                Update_Timestamp = content.Update_Timestamp,
                GROUPCATEGORY_CODE = content.GROUPCATEGORY_CODE,
                GROUPCATEGORY_KANA = content.GROUPCATEGORY_KANA,
                GROUPCATEGORY_NAME = content.GROUPCATEGORY_NAME,
                GROUPCATEGORY_RYAKUSHO = content.GROUPCATEGORY_RYAKUSHO,
                GROUPTANI = content.GROUPTANI,
                OPERATION = operation,
                REC_ID = content.Id
            };

            using var jnlAccess = factory.CreateGroupCategoryJnlAccess(accessor);
            return jnlAccess.Create(Jurnalobj);
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
