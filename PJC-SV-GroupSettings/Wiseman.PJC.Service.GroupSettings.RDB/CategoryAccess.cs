using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using Wiseman.PJC.Gen2.ObjectModel;
using Wiseman.PJC.Gen2.RDB.Core;
using Wiseman.PJC.Gen2.RDB.Entities;
using Wiseman.PJC.Gen2.RDB.Interfaces;
using Wiseman.PJC.Gen2.Setting.Server;
using Wiseman.PJC.Service.GroupSettings.RDB.Entities;
using Wiseman.PJC.Service.GroupSettings.RDB.Interfaces;

namespace Wiseman.PJC.Service.GroupSettings.RDB
{
    public class CategoryAccess : ICategoryAccess
    {
        private readonly string _category_tb = "GRP_CATEGORY";

        private IDBAccess _access;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="access"></param>
        public CategoryAccess(IDBAccess access)
        {
            _access = access;
        }

        /// <summary>
        /// カテゴリー一覧取得
        /// </summary>
        /// <param name="categoryCode">カテゴリコード</param>
        /// <param name="limit">取得上限件数</param>
        /// <param name="offset">オフセット</param>
        public IList<CategoryEntity> GetAsync(string categoryCode = "", short limit = 1000, short offset = 0)
        {
            // パラメータの設定
            var parameters = new Parameters();

            // SQLの組み立て
            var sb = new StringBuilder();

            //SELECT
            sb.AppendLine("SELECT");
            sb.AppendLine(" ID");
            sb.AppendLine(" ,LOCKVERSION");
            sb.AppendLine(" ,AREACORP_ID");
            sb.AppendLine(" ,FACILITYGROUP_ID");
            sb.AppendLine(" ,FACILITY_ID");
            sb.AppendLine(" ,CATEGORY_CODE");
            sb.AppendLine(" ,CATEGORY_NAME");
            sb.AppendLine(" ,IS_DELETED");
            sb.AppendLine(" ,UPDATE_ACCOUNTID");
            sb.AppendLine(" ,UPDATE_LOGINID");
            sb.AppendLine(" ,UPDATE_FACILITYID");
            sb.AppendLine(" ,UPDATE_TIMESTAMP");
            sb.AppendLine(" ,LASTUPDATER_NAME");
            sb.AppendLine(" ,LASTUPDATER_ID");

            //FROM
            sb.AppendLine("FROM");
            sb.AppendLine(_category_tb);

            //WHERE
            sb.AppendLine("WHERE ");
            sb.AppendLine(" IS_DELETED = '0' ");

            if (!String.IsNullOrWhiteSpace(categoryCode))
            {
                parameters.Add(":CATEGORY_CODE", DbType.Char, categoryCode);
                sb.AppendLine(" AND CATEGORY_CODE = :CATEGORY_CODE ");
            }

            //ORDER
            sb.AppendLine(" ORDER BY CATEGORY_CODE");

            //OFFSET
            sb.AppendLine(" OFFSET");
            parameters.Add(":OFFSET", DbType.Int32, offset);
            parameters.Add(":LIMIT", DbType.Int32, limit);
            sb.AppendLine(":OFFSET ROWS FETCH FIRST :LIMIT +1 ROWS ONLY");

            var accountTrans = _access.SelectSql<CategoryEntity>(CallerInfo.Create(Shared.ToSharedContext()),
                                                    sb.ToString(),
                                                    parameters);
            return accountTrans;
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
