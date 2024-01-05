using Wiseman.PJC.Service.GroupSettings.Http.Message.RequestContents;
using Wiseman.PJC.Service.GroupSettings.RDB.Entities;
using Wiseman.PJC.Service.GroupSettings.RDB.Interfaces;

namespace Wiseman.PJC.Service.GroupSettings.WebApi.Logics
{
    public class EntryGroupPatient
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public EntryGroupPatient()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="access"></param>
        /// <param name="jnlAccess"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public Gen2.RDB.Entities.Result<GroupPatientEntity> Post(IGroupPatientAccess access,
                                IGroupPatientJnlAccess jnlAccess, GroupPatientEntity content)
        {
            var returnValue = access.Create(content);
            if (returnValue.Count > 0)
            {
                var returnJurnalValue = CreateGroupPatientJurnalLogic(jnlAccess, returnValue.Entity, "1");
                if (returnJurnalValue == 0)
                {
                    return new Gen2.RDB.Entities.Result<GroupPatientEntity>();
                }
                return returnValue;
            }

            return new Gen2.RDB.Entities.Result<GroupPatientEntity>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="access"></param>
        /// <param name="jnlAccess"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public Gen2.RDB.Entities.Result<GroupPatientEntity> Put(IGroupPatientAccess access,
                                IGroupPatientJnlAccess jnlAccess, GroupPatientEntity content)
        {
            var returnValue = access.Update(content);
            if (returnValue.Count > 0)
            {
                var returnJurnalValue = CreateGroupPatientJurnalLogic(jnlAccess, returnValue.Entity, "2");
                if (returnJurnalValue == 0)
                {
                    return new Gen2.RDB.Entities.Result<GroupPatientEntity>();
                }
                return returnValue;
            }

            return new Gen2.RDB.Entities.Result<GroupPatientEntity>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="access"></param>
        /// <param name="jnlAccess"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public bool Delete(IGroupPatientAccess access,
                                IGroupPatientJnlAccess jnlAccess, GroupPatientEntity content)
        {

            var returnValue = access.Delete(content.Id);
            if (returnValue)
            {
                return CreateGroupPatientJurnalLogic(jnlAccess, content, "3") > 0;
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
        private int CreateGroupPatientJurnalLogic(IGroupPatientJnlAccess jnlAccess, GroupPatientEntity content, string operation)
        {
            return jnlAccess.Create(ReplaceGroupPatientEntity.ConvertToGroupPatientJnl(content, operation));
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
