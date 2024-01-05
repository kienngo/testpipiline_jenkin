using Wiseman.PJC.Gen2.Http.Message.Responses;
using Wiseman.PJC.Gen2.RDB.Interfaces;
using Wiseman.PJC.Service.GroupSettings.Http.Message.RequestContents;
using Wiseman.PJC.Service.GroupSettings.Http.Message.ResponseContents;
using Wiseman.PJC.Service.GroupSettings.RDB.Entities;
using Wiseman.PJC.Service.GroupSettings.RDB.Interfaces;
using Wiseman.PJC.Service.GroupSettings.WebApi.Interfaces;

namespace Wiseman.PJC.Service.GroupSettings.WebApi.Logics
{
    public class EntryGroup
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public EntryGroup()
        {

        }

        public Gen2.RDB.Entities.Result<GroupEntity> Post(IDBAccess accessor, IGroupSettingsFactory factory, GroupRequestContent content)
        {
            // よく使う項目マスター
            var obj = new GroupEntity()
            {
                Id = content.Id,
                LockVersion = 0,
                FACILITY_ID = content.FacilityId,
                AREACORP_ID = content.AreaCorpId,
                POST_ID = content.PostID,
                IS_DELETED = "0",
                DISPLAY_ORDER = content.DisplayOrder,
                FACILITYGROUP_ID = content.FacilityGroupId,
                GROUPCATEGORY_ID = content.GroupCategoryId,
                GROUP_CODE = content.GroupCode,
                GROUP_KANA = content.GroupKana,
                GROUP_NAME = content.GroupName,
                GROUP_RYAKUSHO = content.GroupRyakusho,
                REMARKS = content.Remarks,
                VALID_FLAG = content.ValidFlag,
                LASTUPDATER_NAME = content.LastUpdaterName,
                LASTUPDATER_ID = content.LastUpdaterId
            };

            var access = factory.CreateGroupAccess(accessor);
            var returnValue = access.Create(obj);
            if (returnValue.Count > 0)
            {
                var returnJurnalValue = CreateGroupJurnalLogic(accessor, factory, returnValue.Entity, "1");
                if (returnJurnalValue == 0)
                {
                    return new Gen2.RDB.Entities.Result<GroupEntity>();
                }
                return returnValue;
            }

            return new Gen2.RDB.Entities.Result<GroupEntity>();
        }

        public Gen2.RDB.Entities.Result<GroupEntity> Put(IDBAccess accessor, IGroupSettingsFactory factory, GroupRequestContent content)
        {
            // よく使う項目マスター
            var obj = new GroupEntity()
            {
                Id = content.Id,
                LockVersion = content.LockVersion,
                FACILITY_ID = content.FacilityId,
                AREACORP_ID = content.AreaCorpId,
                POST_ID = content.PostID,
                IS_DELETED = "0",
                DISPLAY_ORDER = content.DisplayOrder,
                FACILITYGROUP_ID = content.FacilityGroupId,
                GROUPCATEGORY_ID = content.GroupCategoryId,
                GROUP_CODE = content.GroupCode,
                GROUP_KANA = content.GroupKana,
                GROUP_NAME = content.GroupName,
                GROUP_RYAKUSHO = content.GroupRyakusho,
                REMARKS = content.Remarks,
                VALID_FLAG = content.ValidFlag,
                LASTUPDATER_NAME = content.LastUpdaterName,
                LASTUPDATER_ID = content.LastUpdaterId
            };

            var access = factory.CreateGroupAccess(accessor);
            var returnValue = access.Update(obj);
            if (returnValue.Count > 0)
            {
                var returnJurnalValue = CreateGroupJurnalLogic(accessor, factory, returnValue.Entity, "2");
                if (returnJurnalValue == 0)
                {
                    return new Gen2.RDB.Entities.Result<GroupEntity>();
                }
                return returnValue;
            }

            return new Gen2.RDB.Entities.Result<GroupEntity>();
        }

        public Gen2.RDB.Entities.Result<GroupDeleteEntity> Delete(IDBAccess accessor, IGroupSettingsFactory factory, GroupResponseContent content)
        {
            var obj = new GroupDeleteEntity()
            {
                Id = content.Id,
                LockVersion = content.LockVersion,
                Is_Deleted = "1",
            };

            var access = factory.CreateGroupAccess(accessor);
            var returnValue = access.UpdateForDelete(obj);
            if (returnValue.Count > 0)
            {
                var Jurnalobj = new GroupEntity()
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
                    GROUPCATEGORY_ID = content.GroupCategoryId,
                    GROUP_CODE = content.GroupCode,
                    GROUP_KANA = content.GroupKana,
                    GROUP_NAME = content.GroupName,
                    GROUP_RYAKUSHO = content.GroupRyakusho,
                    REMARKS = content.Remarks,
                    Update_Timestamp = content.UpdateTimestamp,
                    VALID_FLAG = content.ValidFlag
                };
                var returnJurnalValue = CreateGroupJurnalLogic(accessor, factory, Jurnalobj, "3");
                if (returnJurnalValue == 0)
                {
                    return new Gen2.RDB.Entities.Result<GroupDeleteEntity>();
                }

                return returnValue;
            }

            return new Gen2.RDB.Entities.Result<GroupDeleteEntity>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accessor"></param>
        /// <param name="factory"></param>
        /// <param name="content"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        private int CreateGroupJurnalLogic(IDBAccess accessor, IGroupSettingsFactory factory, GroupEntity content, string operation)
        {
            var Jurnalobj = new GroupJnlEntity()
            {
                LockVersion = content.LockVersion,
                OPERATION = operation,
                REC_ID = content.Id,
                FACILITY_ID = content.FACILITY_ID,
                GROUP_CODE = content.GROUP_CODE,
                GROUPCATEGORY_ID = content.GROUPCATEGORY_ID,
                AREACORP_ID = content.AREACORP_ID,
                DISPLAY_ORDER = content.DISPLAY_ORDER,
                FACILITYGROUP_ID = content.FACILITYGROUP_ID,
                GROUP_KANA = content.GROUP_KANA,
                GROUP_NAME = content.GROUP_NAME,
                GROUP_RYAKUSHO = content.GROUP_RYAKUSHO,
                REMARKS = content.REMARKS,
                VALID_FLAG = content.VALID_FLAG,
                IS_DELETED = content.IS_DELETED,
                POST_ID = content.POST_ID,
                Update_AccountId = content.Update_AccountId,
                Update_LoginId = content.Update_LoginId,
                Update_FacilityId = content.Update_FacilityId,
                Update_Timestamp = content.Update_Timestamp,
                LASTUPDATER_NAME = content.LASTUPDATER_NAME,
                LASTUPDATER_ID = content.LASTUPDATER_ID
            };

            using var jnlAccess = factory.CreateGroupJnlAccess(accessor);
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
