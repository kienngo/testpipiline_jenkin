using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiseman.PJC.Gen2.ObjectModel;
using Wiseman.PJC.Gen2.RDB.Core;
using Wiseman.PJC.Gen2.RDB.Entities;
using Wiseman.PJC.Gen2.RDB.Interfaces;
using Wiseman.PJC.Gen2.Setting.Server;
using Wiseman.PJC.Service.GroupSettings.RDB.Entities;
using Wiseman.PJC.Service.GroupSettings.RDB.Interfaces;

namespace Wiseman.PJC.Service.GroupSettings.RDB
{
    public class GroupStaffAccess : IGroupStaffAccess
    {
        private readonly string _tb_grp_groupStaff = "GRP_GROUPSTAFF";

        /// <summary>Oracleアクセサ</summary>
        private IDBAccess _access;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="access"></param>
        public GroupStaffAccess(IDBAccess access)
        {
            _access = access;
        }

        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public Result<GroupStaffEntity> Create(GroupStaffEntity content)
        {
            // データ更新
            return _access.InsertReturning(CallerInfo.Create(Shared.ToSharedContext()), content, _tb_grp_groupStaff);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public Result<GroupStaffEntity> Update(GroupStaffEntity content)
        {
            // データ更新
            return _access.UpdateReturning(CallerInfo.Create(Shared.ToSharedContext()), content, _tb_grp_groupStaff);
        }

        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            var parameters = new Parameters();
            {
                parameters.Add(":ID", DbType.Char, id);
            }

            return _access.Delete(CallerInfo.Create(Shared.ToSharedContext()), _tb_grp_groupStaff, ":ID = ID", parameters) > 0;
        }

        #region Dispose
        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: マネージド状態を破棄します (マネージド オブジェクト)
                    _access?.Dispose();
                    _access = null;
                }

                // TODO: アンマネージド リソース (アンマネージド オブジェクト) を解放し、ファイナライザーをオーバーライドします
                // TODO: 大きなフィールドを null に設定します
                disposedValue = true;
            }
        }

        // // TODO: 'Dispose(bool disposing)' にアンマネージド リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします
        // ~xxxAccess()
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
