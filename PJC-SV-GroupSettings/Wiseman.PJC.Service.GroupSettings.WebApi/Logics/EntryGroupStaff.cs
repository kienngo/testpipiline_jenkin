using Wiseman.PJC.Service.GroupSettings.Http.Message.RequestContents;
using Wiseman.PJC.Service.GroupSettings.RDB.Entities;
using Wiseman.PJC.Service.GroupSettings.RDB.Interfaces;

namespace Wiseman.PJC.Service.GroupSettings.WebApi.Logics
{
    public class EntryGroupStaff
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public EntryGroupStaff()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="access"></param>
        /// <param name="jnlAccess"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public Gen2.RDB.Entities.Result<GroupStaffEntity> Post(IGroupStaffAccess access,
                                IGroupStaffJnlAccess jnlAccess, GroupStaffPostContent content)
        {
            var returnValue = access.Create(ReplaceGroupStaffEntity.ConvertResquestToEntity(content));
            if (returnValue.Count > 0)
            {
                var returnJurnalValue = CreateGroupStaffJurnalLogic(jnlAccess, returnValue.Entity, "1");
                if (returnJurnalValue == 0)
                {
                    return new Gen2.RDB.Entities.Result<GroupStaffEntity>();
                }
                return returnValue;
            }

            return new Gen2.RDB.Entities.Result<GroupStaffEntity>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="access"></param>
        /// <param name="jnlAccess"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public Gen2.RDB.Entities.Result<GroupStaffEntity> Put(IGroupStaffAccess access,
                                IGroupStaffJnlAccess jnlAccess, GroupStaffPostContent content)
        {
            var returnValue = access.Update(ReplaceGroupStaffEntity.ConvertResquestToEntity(content));
            if (returnValue.Count > 0)
            {
                var returnJurnalValue = CreateGroupStaffJurnalLogic(jnlAccess, returnValue.Entity, "2");
                if (returnJurnalValue == 0)
                {
                    return new Gen2.RDB.Entities.Result<GroupStaffEntity>();
                }
                return returnValue;
            }

            return new Gen2.RDB.Entities.Result<GroupStaffEntity>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="access"></param>
        /// <param name="jnlAccess"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public bool Delete(IGroupStaffAccess access,
                                IGroupStaffJnlAccess jnlAccess, GroupStaffEntity content)
        {
            var returnValue = access.Delete(content.Id);
            if (returnValue)
            {
                return CreateGroupStaffJurnalLogic(jnlAccess, content, "3") > 0;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jnlAccess"></param>
        /// <param name="content"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        private int CreateGroupStaffJurnalLogic(IGroupStaffJnlAccess jnlAccess, GroupStaffEntity content, string operation)
        {
            return jnlAccess.Create(ReplaceGroupStaffEntity.ConvertToGroupStaffJnl(content, operation));
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
