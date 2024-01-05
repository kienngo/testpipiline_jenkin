using Wiseman.PJC.Gen2.RDB.Interfaces;
using Wiseman.PJC.Service.GroupSettings.Http.Message.ResponseContents;
using Wiseman.PJC.Service.GroupSettings.RDB.Entities;
using Wiseman.PJC.Service.GroupSettings.WebApi.Interfaces;

namespace Wiseman.PJC.Service.GroupSettings.WebApi.Logics
{
    public class EntryGroupManagement
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public EntryGroupManagement()
        {

        }

        public Gen2.RDB.Entities.Result<GroupManagementEntity> Post(IDBAccess accessor, IGroupSettingsFactory factory, GroupManagementEntity content)
        {
            var access = factory.CreateGroupManagementAccess(accessor);
            var returnValue = access.Create(content);
            if (returnValue.Count > 0)
            {
                var returnJurnalValue = CreateGroupManagementJurnalLogic(accessor, factory, returnValue.Entity, "1");
                if (returnJurnalValue == 0)
                {
                    return new Gen2.RDB.Entities.Result<GroupManagementEntity>();
                }
                return returnValue;
            }

            return new Gen2.RDB.Entities.Result<GroupManagementEntity>();
        }

        public Gen2.RDB.Entities.Result<GroupManagementEntity> Put(IDBAccess accessor, IGroupSettingsFactory factory, GroupManagementEntity content)
        {
            var access = factory.CreateGroupManagementAccess(accessor);
            var returnValue = access.Update(content);
            if (returnValue.Count > 0)
            {
                var returnJurnalValue = CreateGroupManagementJurnalLogic(accessor, factory, returnValue.Entity, "2");
                if (returnJurnalValue == 0)
                {
                    return new Gen2.RDB.Entities.Result<GroupManagementEntity>();
                }
                return returnValue;
            }

            return new Gen2.RDB.Entities.Result<GroupManagementEntity>();
        }

        public Gen2.RDB.Entities.Result<GroupManagementDeleteEntity> Delete(IDBAccess accessor, IGroupSettingsFactory factory, GroupManagementResponseContent content)
        {
            var obj = new GroupManagementDeleteEntity()
            {
                Id = content.Id,
                LockVersion = content.LockVersion,
                Is_Deleted = "1",
            };

            var access = factory.CreateGroupManagementAccess(accessor);
            var returnValue = access.UpdateForDelete(obj);
            if (returnValue.Count > 0)
            {
                var Jurnalobj = new GroupManagementEntity()
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
                    FACILITYGROUP_ID = content.FacilityGroupId,
                    Update_Timestamp = content.UpdateTimestamp,
                    ENDDATE = content.EndDate,
                    GROUPMANAGEMENT_CODE = content.GroupManagementCode,
                    GROUP_ID = content.GroupId,
                    STARTDATE = content.StartDate,
                };
                var returnJurnalValue = CreateGroupManagementJurnalLogic(accessor, factory, Jurnalobj, "3");
                if (returnJurnalValue == 0)
                {
                    return new Gen2.RDB.Entities.Result<GroupManagementDeleteEntity>();
                }

                return returnValue;
            }

            return new Gen2.RDB.Entities.Result<GroupManagementDeleteEntity>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accessor"></param>
        /// <param name="factory"></param>
        /// <param name="content"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        private int CreateGroupManagementJurnalLogic(IDBAccess accessor, IGroupSettingsFactory factory, GroupManagementEntity content, string operation)
        {
            var Jurnalobj = new GroupManagementJnlEntity()
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
                FACILITYGROUP_ID = content.FACILITYGROUP_ID,
                Update_Timestamp = content.Update_Timestamp,
                OPERATION = operation,
                REC_ID = content.Id,
                ENDDATE = content.ENDDATE,
                GROUPMANAGEMENT_CODE = content.GROUPMANAGEMENT_CODE,
                GROUP_ID = content.GROUP_ID,
                STARTDATE = content.STARTDATE
            };

            using var jnlAccess = factory.CreateGroupManagementJnlAccess(accessor);
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
