using Wiseman.PJC.Gen2.RDB.Interfaces;
using Wiseman.PJC.Service.GroupSettings.WebApi.Interfaces;
using Wiseman.PJC.GroupSettings.RDB;
using Wiseman.PJC.Service.GroupSettings.RDB.Interfaces;
using Wiseman.PJC.Service.GroupSettings.RDB;

namespace Wiseman.PJC.Service.GroupSettings.WebApi.Logics
{
    public class GroupSettingsFactory : IGroupSettingsFactory
    {
        public IGroupCategoryAccess CreateGroupCategoryAccess(IDBAccess dbAccess)
        {
            return new GroupCategoryAccess(dbAccess);
        }
        public IGroupCategoryJnlAccess CreateGroupCategoryJnlAccess(IDBAccess dbAccess)
        {
            return new GroupCategoryJnlAccess(dbAccess);
        }
        public ICategorySelectedAccess CreateCategorySelectedAccess(IDBAccess dbAccess)
        {
            return new CategorySelectedAccess(dbAccess);
        }
        public ICategorySelectedJnlAccess CreateCategorySelectedJnlAccess(IDBAccess dbAccess)
        {
            return new CategorySelectedJnlAccess(dbAccess);
        }
        public IGroupAccess CreateGroupAccess(IDBAccess dbAccess)
        {
            return new GroupAccess(dbAccess);
        }
        public IGroupJnlAccess CreateGroupJnlAccess(IDBAccess dbAccess)
        {
            return new GroupJnlAccess(dbAccess);
        }
        public ICategoryAccess CreateCategoryAccess(IDBAccess dbAccess)
        {
            return new CategoryAccess(dbAccess);
        }
        public IGroupManagementAccess CreateGroupManagementAccess(IDBAccess dbAccess)
        {
            return new GroupManagementAccess(dbAccess);
        }
        public IGroupManagementJnlAccess CreateGroupManagementJnlAccess(IDBAccess dbAccess)
        {
            return new GroupManagementJnlAccess(dbAccess);
        }

        public IGroupPatientAccess CreateGroupPatientAccess(IDBAccess dbAccess)
        {
            return new GroupPatientAccess(dbAccess);
        }

        public IGroupPatientJnlAccess CreateGroupPatientJnlAccess(IDBAccess dbAccess)
        {
            return new GroupPatientJnlAccess(dbAccess);
        }

        public IGroupStaffAccess CreateGroupStaffAccess(IDBAccess dbAccess)
        {
            return new GroupStaffAccess(dbAccess);
        }

        public IGroupStaffJnlAccess CreateGroupStaffJnlAccess(IDBAccess dbAccess)
        {
            return new GroupStaffJnlAccess(dbAccess);
        }

        #region IDisposable Support
        private bool disposedValue = false; // 重複する呼び出しを検出するには

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {

                }

                disposedValue = true;
            }
        }

        // TODO: 上の Dispose(bool disposing) にアンマネージ リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします。
        // ~PreviousValueDac() {
        //   // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
        //   Dispose(false);
        // }

        // このコードは、破棄可能なパターンを正しく実装できるように追加されました。
        /// <summary>
        /// 
        /// </summary>
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
