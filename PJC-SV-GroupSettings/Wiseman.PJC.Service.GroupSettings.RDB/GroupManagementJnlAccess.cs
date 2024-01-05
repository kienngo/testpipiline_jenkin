using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiseman.PJC.Gen2.ObjectModel;
using Wiseman.PJC.Gen2.RDB.Core;
using Wiseman.PJC.Gen2.RDB.Interfaces;
using Wiseman.PJC.Gen2.Setting.Server;
using Wiseman.PJC.Service.GroupSettings.RDB.Entities;
using Wiseman.PJC.Service.GroupSettings.RDB.Interfaces;

namespace Wiseman.PJC.Service.GroupSettings.RDB
{
    public class GroupManagementJnlAccess : IGroupManagementJnlAccess
    {
        private IDBAccess _access;

        private readonly string _groupManagementJnl_tb = "GRP_GROUPMANAGEMENTJNL";

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public GroupManagementJnlAccess(IDBAccess access)
        {
            _access = access;
        }

        public int Create(GroupManagementJnlEntity content)
        {
            var parameters = new Parameters();

            var sb = new StringBuilder();
            sb.AppendLine("INSERT INTO");
            sb.AppendLine($" {_groupManagementJnl_tb}");
            sb.AppendLine("(");
            sb.AppendLine("  HISTORY_SEQ");
            sb.AppendLine(", LOCKVERSION");
            sb.AppendLine(", OPERATION");
            sb.AppendLine(", REC_ID");
            sb.AppendLine(", AREACORP_ID");
            sb.AppendLine(", FACILITYGROUP_ID");
            sb.AppendLine(", FACILITY_ID");
            sb.AppendLine(", GROUPMANAGEMENT_CODE");
            sb.AppendLine(", GROUP_ID");
            sb.AppendLine(", STARTDATE");
            sb.AppendLine(", ENDDATE");
            sb.AppendLine(", IS_DELETED");
            sb.AppendLine(", POST_ID");
            sb.AppendLine(", UPDATE_ACCOUNTID");
            sb.AppendLine(", UPDATE_LOGINID");
            sb.AppendLine(", UPDATE_FACILITYID");
            sb.AppendLine(", UPDATE_TIMESTAMP");
            sb.AppendLine(", LASTUPDATER_NAME");
            sb.AppendLine(", LASTUPDATER_ID");
            sb.AppendLine(")");
            sb.AppendLine(" VALUES (");

            sb.Append(" (");
            sb.AppendLine("SELECT ");
            sb.AppendLine("  CASE WHEN MAX(HISTORY_SEQ) IS NULL THEN 0 ELSE MAX(HISTORY_SEQ) + 1 END");
            sb.AppendLine("  FROM");
            sb.AppendLine($" {_groupManagementJnl_tb}");
            parameters.Add(":REC_ID", DbType.Varchar2, content.REC_ID);
            sb.AppendLine("  WHERE REC_ID = :REC_ID");
            sb.AppendLine(" )");
            sb.Append(',');

            sb.Append(SQLFunction.Number(content.LockVersion));
            sb.Append(',');

            sb.Append(SQLFunction.Char(content.OPERATION));
            sb.Append(',');

            sb.Append(SQLFunction.Char(content.REC_ID));
            sb.Append(',');

            sb.Append(SQLFunction.Char(content.AREACORP_ID));
            sb.Append(',');

            sb.Append(SQLFunction.Char(content.FACILITYGROUP_ID));
            sb.Append(',');

            sb.Append(SQLFunction.Char(content.FACILITY_ID));
            sb.Append(',');

            sb.Append(SQLFunction.Char(content.GROUPMANAGEMENT_CODE));
            sb.Append(',');

            sb.Append(SQLFunction.Char(content.GROUP_ID));
            sb.Append(',');

            sb.Append(SQLFunction.Number(content.STARTDATE));
            sb.Append(',');

            sb.Append(SQLFunction.Number(content.ENDDATE));
            sb.Append(',');

            sb.Append(SQLFunction.Char(content.IS_DELETED));
            sb.Append(',');

            sb.Append(SQLFunction.Char(content.POST_ID));
            sb.Append(',');

            sb.Append(SQLFunction.Char(content.Update_AccountId));
            sb.Append(',');

            sb.Append(SQLFunction.Char(content.Update_LoginId));
            sb.Append(',');

            sb.Append(SQLFunction.Char(content.Update_FacilityId));
            sb.Append(',');

            sb.Append("SYSDATE");
            sb.Append(',');

            sb.Append(SQLFunction.Char(content.LASTUPDATER_NAME));
            sb.Append(',');

            sb.Append(SQLFunction.Char(content.LASTUPDATER_ID));

            sb.AppendLine(")");

            return _access.ExecuteSql(CallerInfo.Create(Shared.ToSharedContext()), sb.ToString(), parameters);
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
                    _access = null;
                }

                // TODO: アンマネージ リソース (アンマネージ オブジェクト) を解放し、下のファイナライザーをオーバーライドします。
                // TODO: 大きなフィールドを null に設定します。

                disposedValue = true;
            }
        }

        // TODO: 上の Dispose(bool disposing) にアンマネージ リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします。
        // ~ProfileAccess() {
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
