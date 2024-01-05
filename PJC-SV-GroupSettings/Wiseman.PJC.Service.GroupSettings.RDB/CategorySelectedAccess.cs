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
    public class CategorySelectedAccess : ICategorySelectedAccess
    {
        private readonly string _tb_grp_categoryselected = "GRP_CATEGORYSELECTED";

        /// <summary>Oracleアクセサ</summary>
        private IDBAccess _access;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="access"></param>
        public CategorySelectedAccess(IDBAccess access)
        {
            _access = access;
        }

        /// <summary>
        /// Idに該当するリソースを取得する
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public IList<CategorySelectedEntity> GetById(string id)
        {
            // パラメータの設定
            var parameters = new Parameters();
            parameters.Add(":ID", DbType.Varchar2, id);

            // SQLの組み立て
            var sb = new StringBuilder();

            sb.Append("SELECT");

            sb.Append(" TB.*");

            sb.Append(" FROM");

            sb.Append($" {_tb_grp_categoryselected} TB");

            //WHERE
            sb.AppendLine(" WHERE ");

            sb.AppendLine("  TB.ID = :ID ");

            var accountTrans = _access.SelectSql<CategorySelectedEntity>(CallerInfo.Create(Shared.ToSharedContext()),
                                                    sb.ToString(),
                                                    parameters);

            return accountTrans;
        }

        /// <summary>
        /// レコードを新規作成する
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public Result<CategorySelectedEntity> Create(CategorySelectedEntity content)
        {
            // データ作成
            var result = _access.InsertReturning(CallerInfo.Create(Shared.ToSharedContext()),
                                                            content,
                                                            _tb_grp_categoryselected);
            return result;
        }

        public bool Delete(string id)
        {
            var parameters = new Parameters();
            {
                parameters.Add(":ID", DbType.Char, id);
            }

            return _access.Delete(CallerInfo.Create(Shared.ToSharedContext()), _tb_grp_categoryselected, ":ID = ID", parameters) > 0;
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
