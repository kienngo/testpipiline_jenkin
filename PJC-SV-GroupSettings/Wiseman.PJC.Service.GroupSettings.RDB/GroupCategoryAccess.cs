using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiseman.PJC.Gen2.ObjectModel;
using Wiseman.PJC.Gen2.RDB;
using Wiseman.PJC.Gen2.RDB.Core;
using Wiseman.PJC.Gen2.RDB.Entities;
using Wiseman.PJC.Gen2.RDB.Interfaces;
using Wiseman.PJC.Gen2.Setting.Server;
using Wiseman.PJC.Service.GroupSettings.RDB.Entities;
using Wiseman.PJC.Service.GroupSettings.RDB.Interfaces;
using IId_Type = System.String;

namespace Wiseman.PJC.Service.GroupSettings.RDB
{
    /// <summary>
    /// サンプルRDBアクセスクラス
    /// </summary>
    public class GroupCategoryAccess : IGroupCategoryAccess
    {
        private readonly string _tb_grp_groupcategory = "GRP_GROUPCATEGORY";
        private readonly string _tb_grp_group = "GRP_GROUP";
        private readonly string _tb_grp_categoryselected = "GRP_CATEGORYSELECTED";

        /// <summary>Oracleアクセサ</summary>
        private IDBAccess _access;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="access"></param>
        public GroupCategoryAccess(IDBAccess access)
        {
            _access = access;
        }

        /// <summary>
        /// 検索メソッド
        /// </summary>
        /// <param name="groupTani"></param>
        /// <param name="groupCategoryCode"></param>
        /// <param name="searchWord"></param>
        /// <param name="searchFlag"></param>
        /// <param name="areaCorpId"></param>
        /// <param name="facilityGroupId"></param>
        /// <param name="facilityId"></param>
        /// <param name="postId"></param>
        /// <param name="categoryCode"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public IList<GroupCategoryResultEntity> GetGroupCategory(string groupTani = "",
                                                            string groupCategoryCode = "",
                                                            string searchWord = "",
                                                            bool searchFlag = false,
                                                            string areaCorpId = "",
                                                            string facilityGroupId = "",
                                                            string facilityId = "",
                                                            string postId = "",
                                                            string categoryCode = "",
                                                            short? limit = 1000,
                                                            short? offset = 0)
        {
            // パラメータの設定
            var parameters = new Parameters();

            // SQLの組み立て
            var sb = new StringBuilder();

            sb.Append(CreateReadListSql());

            parameters.Add(":FACILITYID", DbType.Varchar2, facilityId);
            parameters.Add(":FACILITYGROUPID", DbType.Varchar2, facilityGroupId);
            parameters.Add(":AREACORPID", DbType.Varchar2, areaCorpId);
            sb.AppendLine("  AND TA.FACILITY_ID = :FACILITYID ");
            sb.AppendLine("  AND TA.FACILITYGROUP_ID = :FACILITYGROUPID ");
            sb.AppendLine("  AND TA.AREACORP_ID = :AREACORPID ");

            if (!String.IsNullOrWhiteSpace(groupTani))
            {
                parameters.Add(":GROUPTANI", DbType.Varchar2, groupTani);
                sb.AppendLine(" AND TA.GROUPTANI = :GROUPTANI ");
            }

            if (!String.IsNullOrWhiteSpace(searchWord))
            {
                parameters.Add(":SEARCHWORD", DbType.Varchar2, searchWord);
                if (searchFlag)
                {
                    sb.AppendLine("  AND (TA.GROUPCATEGORY_NAME LIKE  '%' || :SEARCHWORD || '%' ");
                    sb.AppendLine("  OR TA.GROUPCATEGORY_KANA LIKE  '%' || :SEARCHWORD || '%'   ");
                    sb.AppendLine("  OR TA.GROUPCATEGORY_RYAKUSHO LIKE  '%' || :SEARCHWORD || '%') ");
                }
                else
                {
                    sb.AppendLine("  AND (TA.GROUPCATEGORY_NAME LIKE :SEARCHWORD || '%' ");
                    sb.AppendLine("  OR TA.GROUPCATEGORY_KANA LIKE :SEARCHWORD || '%'   ");
                    sb.AppendLine("  OR TA.GROUPCATEGORY_RYAKUSHO LIKE :SEARCHWORD || '%')");
                }
            }

            if(!String.IsNullOrWhiteSpace(groupCategoryCode))
            {
                parameters.Add(":GROUPCATEGORYCODE", DbType.Char, groupCategoryCode);
                sb.AppendLine("  AND TA.GROUPCATEGORY_CODE = :GROUPCATEGORYCODE");
            }

            if(!String.IsNullOrWhiteSpace(categoryCode)){
                parameters.Add(":CATEGORYCODE", DbType.Varchar2, categoryCode);
                sb.AppendLine("  AND TB.CATEGORYSELECTED_CODE = :CATEGORYCODE");
            }

            //ORDER
            sb.AppendLine(" ORDER BY");
            sb.AppendLine(" TA.GROUPCATEGORY_CODE, TB.CATEGORYSELECTED_CODE");

            //OFFSET
            sb.AppendLine(" OFFSET");
            parameters.Add(":OFFSET", DbType.Int32, offset);
            parameters.Add(":LIMIT", DbType.Int32, limit);
            sb.AppendLine(":OFFSET ROWS FETCH FIRST :LIMIT +1 ROWS ONLY");

            var accountTrans = _access.SelectSql<GroupCategoryResultEntity>(CallerInfo.Create(Shared.ToSharedContext()),
                                                    sb.ToString(),
                                                    parameters);
            return accountTrans;
        }

        /// <summary>
        /// 投稿IDによるグループカテゴリリストの取得
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public IList<GroupCategoryResultEntity> GetGroupCategoryByPostId(string postId)
        {
            // パラメータの設定
            var parameters = new Parameters();
            parameters.Add(":POSTID", DbType.Varchar2, postId);

            // SQLの組み立て
            var sb = new StringBuilder();

            //SELECT
            sb.AppendLine(" SELECT");

            sb.AppendLine("  TA.*");

            //FROM
            sb.AppendLine(" FROM ");
            sb.AppendLine($"  {_tb_grp_groupcategory} TA ");

            //WHERE
            sb.AppendLine(" WHERE ");
            sb.AppendLine("  TA.POST_ID = :POSTID ");
            sb.AppendLine(" ORDER BY");
            sb.AppendLine("  TA.GROUPCATEGORY_CODE");

            var accountTrans = _access.SelectSql<GroupCategoryResultEntity>(CallerInfo.Create(Shared.ToSharedContext()),
                                                    sb.ToString(),
                                                    parameters);
            return accountTrans;
        }

        /// <summary>
        /// Idに該当するリソースを取得する
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public IList<GroupCategoryResultEntity> GetGroupCategoryById(string id)
        {
            // パラメータの設定
            var parameters = new Parameters();
            parameters.Add(":ID", DbType.Varchar2, id);

            // SQLの組み立て
            var sb = new StringBuilder();

            sb.Append(CreateReadListSql());

            sb.AppendLine(" AND TA.ID = :ID ");
            sb.AppendLine(" ORDER BY TA.GROUPCATEGORY_CODE, TB.CATEGORYSELECTED_CODE");

            var accountTrans = _access.SelectSql<GroupCategoryResultEntity>(CallerInfo.Create(Shared.ToSharedContext()),
                                                    sb.ToString(),
                                                    parameters);

            return accountTrans;
        }

        /// <summary>
        /// レコードを新規作成する
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public Result<GroupCategoryEntity> CreateGroupCategory(GroupCategoryEntity content)
        {
            // データ作成
            var result = _access.InsertReturning(CallerInfo.Create(Shared.ToSharedContext()),
                                                            content,
                                                            _tb_grp_groupcategory);
            return result;
        }

        /// <summary>
        /// 記録を更新
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public Result<GroupCategoryEntity> UpdateGroupCategory(GroupCategoryEntity content)
        {
            // データ作成
            var result = _access.UpdateReturning(CallerInfo.Create(Shared.ToSharedContext()),
                                                            content,
                                                            _tb_grp_groupcategory);
            return result;
        }

        /// <summary>
        /// グループカテゴリ(ID指定)を1つ取得しました
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<GroupCategoryResultEntity> ReadListGroupCategoryById(string id = "")
        {
            // パラメータ
            var parameters = new Parameters();

            parameters.Add(":ID", DbType.Varchar2, id);
            // SQLの組み立て(where)
            var sb = new StringBuilder();

            sb.Append(CreateReadListSql());

            sb.AppendLine(" AND TA.ID = :ID ");

            // データ取得
            return _access.SelectSql<GroupCategoryResultEntity>(CallerInfo.Create(Shared.ToSharedContext())
                , sb.ToString()
                , parameters);
        }

        /// <summary>
        /// 指定されたIDを除く、Codeに該当するレコードの存在を確認する
        /// </summary>
        /// <param name="facilityid"></param>
        /// <param name="facilitygroupid"></param>
        /// <param name="groupcategorycode"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DuplicateCheckGroupCategory(string facilityid, string facilitygroupid, string groupcategorycode, string id, string areacorpid)
        {
            var parameters = new Parameters();
            {
                parameters.Add(":FACILITY_ID", DbType.Varchar2, facilityid);
                parameters.Add(":FACILITYGROUP_ID", DbType.Varchar2, facilitygroupid);
                parameters.Add(":AREACORP_ID", DbType.Varchar2, areacorpid);
                parameters.Add(":GROUPCATEGORY_CODE", DbType.Char, groupcategorycode);
                parameters.Add(":ID", DbType.Varchar2, id);

            }

            var where = "AREACORP_ID = :AREACORP_ID AND FACILITY_ID = :FACILITY_ID AND FACILITYGROUP_ID = :FACILITYGROUP_ID AND GROUPCATEGORY_CODE = :GROUPCATEGORY_CODE AND ID != :ID ";

            if (_access.Count(CallerInfo.Create(Shared.ToSharedContext()), _tb_grp_groupcategory, where, parameters) == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 自動採番はグループカテゴリコードの最大値です
        /// </summary>
        /// <param name="facilityid">医療機関・施設ID</param>
        public string ReadNewCodeGroupCategory(string facilityid)
        {
            // パラメータの設定
            var parameters = new Parameters();
            {
                // 医療機関・施設ID
                parameters.Add(":FACILITY_ID", DbType.Varchar2, facilityid);
            }

            // SQLの組み立て
            var sb = new StringBuilder();
            sb.AppendLine("SELECT ");
            sb.AppendLine("  MAX(TA.GROUPCATEGORY_CODE)+1 AS GROUPCATEGORY_CODE ");
            sb.AppendLine("FROM ");
            sb.AppendLine($"  {_tb_grp_groupcategory} TA");
            sb.AppendLine("WHERE ");
            sb.AppendLine("  TA.FACILITY_ID = :FACILITY_ID ");

            var results = _access.SelectSql<GroupCategoryResultEntity>(CallerInfo.Create(Shared.ToSharedContext()), sb.ToString(), parameters);

            var result = "0001";
            if (results?.Count > 0 && results[0].GROUPCATEGORY_CODE != null)
            {
                result = results[0]?.GROUPCATEGORY_CODE.PadLeft(4, '0');
            }

            // 実行
            return result;
        }

        /// <summary>
        /// Codeに該当するレコードを取得する
        /// </summary>
        /// <returns></returns>
        public GroupCategoryResultEntity ReadByCodeGroupCategory(string facilityid, string groupcategorycode, string facilitygroupid, string areacorpid)
        {
            // パラメータの設定
            var parameters = new Parameters();
            {
                // 医療機関・施設ID
                parameters.Add(parameterName: ":FACILITYID", dbtype: DbType.Varchar2, value: facilityid);
                // グループ分類コード
                parameters.Add(parameterName: ":GROUPCATEGORYCODE", dbtype: DbType.Char, value: groupcategorycode);
                parameters.Add(parameterName: ":FACILITYGROUPID", dbtype: DbType.Varchar2, value: facilitygroupid);
                parameters.Add(parameterName: ":AREACORPID", dbtype: DbType.Varchar2, value: areacorpid);
            }

            // SQLの組み立て
            var sb = new StringBuilder();

            //SELECT
            sb.AppendLine("SELECT ");
            sb.AppendLine("  TA.ID");
            sb.AppendLine("  ,TA.LOCKVERSION");
            sb.AppendLine("  ,TA.IS_DELETED");

            //FROM
            sb.AppendLine("FROM ");
            sb.AppendLine($"  {_tb_grp_groupcategory} TA");

            //WHERE 
            sb.AppendLine("WHERE ");
            sb.AppendLine("  TA.GROUPCATEGORY_CODE = :GROUPCATEGORYCODE ");
            sb.AppendLine("  AND TA.FACILITY_ID = :FACILITYID ");
            sb.AppendLine("  AND TA.FACILITYGROUP_ID = :FACILITYGROUPID");
            sb.AppendLine("  AND TA.AREACORP_ID = :AREACORPID");

            var results = _access.SelectSql<GroupCategoryResultEntity>(CallerInfo.Create(Shared.ToSharedContext()), sb.ToString(), parameters);

            // 実行
            return results.FirstOrDefault();
        }

        /// <summary>
        /// 削除のための更新
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public Result<GroupCategoryDeleteEntity> UpdateForDelete(GroupCategoryDeleteEntity content)
        {
            // データ更新
            return _access.UpdateReturning(CallerInfo.Create(Shared.ToSharedContext()), content, _tb_grp_groupcategory);
        }

        private StringBuilder CreateReadListSql()
        {
            // SQLの組み立て
            var sb = new StringBuilder();

            //SELECT
            sb.AppendLine(" SELECT");

            sb.AppendLine("  TA.ID");
            sb.AppendLine("  ,TA.LOCKVERSION");
            sb.AppendLine("  ,TA.AREACORP_ID");
            sb.AppendLine("  ,TA.FACILITYGROUP_ID");
            sb.AppendLine("  ,TA.FACILITY_ID");
            sb.AppendLine("  ,TA.GROUPCATEGORY_CODE");
            sb.AppendLine("  ,TA.GROUPCATEGORY_NAME");
            sb.AppendLine("  ,TA.GROUPCATEGORY_KANA");
            sb.AppendLine("  ,TA.GROUPCATEGORY_RYAKUSHO");
            sb.AppendLine("  ,TA.GROUPTANI");
            sb.AppendLine("  ,TA.DISPLAY_ORDER");
            sb.AppendLine("  ,TA.IS_DELETED");
            sb.AppendLine("  ,TA.UPDATE_ACCOUNTID");
            sb.AppendLine("  ,TA.UPDATE_LOGINID");
            sb.AppendLine("  ,TA.UPDATE_FACILITYID");
            sb.AppendLine("  ,TA.UPDATE_TIMESTAMP");
            sb.AppendLine("  ,TA.POST_ID");
            sb.AppendLine("  ,TA.LASTUPDATER_NAME");
            sb.AppendLine("  ,TA.LASTUPDATER_ID");

            sb.AppendLine("  ,TB.ID CAT_ID");
            sb.AppendLine("  ,TB.LOCKVERSION CAT_LOCKVERSION");
            sb.AppendLine("  ,TB.CATEGORYSELECTED_CODE CAT_CATEGORYSELECTED_CODE");
            sb.AppendLine("  ,TA.GROUPCATEGORY_NAME CAT_CATEGORYSELECTED_NAME");
            sb.AppendLine("  ,TB.GROUPCATEGORY_ID CAT_GROUPCATEGORY_ID");
            sb.AppendLine("  ,TB.CATEGORY_ID CAT_CATEGORY_ID");
            sb.AppendLine("  ,TB.UPDATE_ACCOUNTID CAT_UPDATE_ACCOUNTID");
            sb.AppendLine("  ,TB.UPDATE_LOGINID CAT_UPDATE_LOGINID");
            sb.AppendLine("  ,TB.UPDATE_FACILITYID CAT_UPDATE_FACILITYID");
            sb.AppendLine("  ,TB.UPDATE_TIMESTAMP CAT_UPDATE_TIMESTAMP");
            sb.AppendLine("  ,TB.POST_ID CAT_POST_ID");
            sb.AppendLine("  ,TB.LASTUPDATER_NAME CAT_LASTUPDATER_NAME");
            sb.AppendLine("  ,TB.LASTUPDATER_ID CAT_LASTUPDATER_ID");
            sb.AppendLine("  ,NVL(TC.NUM_OF_GROUP,0) NUM_OF_GROUP");

            //FROM
            sb.AppendLine(" FROM ");
            sb.AppendLine($"  {_tb_grp_groupcategory} TA ");
            sb.AppendLine($"  LEFT JOIN {_tb_grp_categoryselected} TB ");
            sb.AppendLine("  ON TA.ID = TB.GROUPCATEGORY_ID ");
            sb.AppendLine("  LEFT JOIN (SELECT");
            sb.AppendLine("            	GROUPCATEGORY_ID");
            sb.AppendLine("            	,COUNT(*) NUM_OF_GROUP");
            sb.AppendLine("            FROM");
            sb.AppendLine($"            	{_tb_grp_group}");
            sb.AppendLine("            WHERE");
            sb.AppendLine("            	IS_DELETED ='0' ");
            sb.AppendLine("            GROUP BY");
            sb.AppendLine("            	GROUPCATEGORY_ID) TC ON TA.ID = TC.GROUPCATEGORY_ID");

            //WHERE
            sb.AppendLine(" WHERE ");
            sb.AppendLine("  TA.IS_DELETED = '0' ");

            return sb;
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
