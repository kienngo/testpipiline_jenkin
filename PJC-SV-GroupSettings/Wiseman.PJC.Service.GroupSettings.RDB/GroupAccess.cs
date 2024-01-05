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
using Wiseman.PJC.Service.GroupSettings.Http.Message.RequestContents;
using Wiseman.PJC.Gen2.Http;

namespace Wiseman.PJC.GroupSettings.RDB
{
    /// <summary>
    /// サンプルRDBアクセスクラス
    /// </summary>
    public class GroupAccess : IGroupAccess
    {
        private readonly string _group_tb = "GRP_GROUP";
        private readonly string _group_management_tb = "GRP_GROUPMANAGEMENT";
        private readonly string _group_patient_tb = "GRP_GROUPPATIENT";
        private readonly string _group_staff_tb = "GRP_GROUPSTAFF";

        private IDBAccess _access;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="access"></param>
        public GroupAccess(IDBAccess access)
        {
            _access = access;
        }

        /// <summary>
        /// グループ一覧取得
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="searchFlag"></param>
        /// <param name="groupCategoryCode"></param>
        /// <param name="groupCode"></param>
        /// <param name="postId"></param>
        /// <param name="validFlag"></param>
        /// <param name="kijunbi"></param>
        /// <param name="kijunbiFlag"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public IList<GroupCategoryGroupEntity> GetGroupAsync(string searchString = "",
                                                bool searchFlag = false,
                                                string groupCategoryCode = "",
                                                string groupTani = "",
                                                string groupCode = "",
                                                string postId = "",
                                                string validFlag = "",
                                                int? kijunbi = 0,
                                                bool kijunbiFlag = false,
                                                int? limit = null,
                                                int? offset = null)
        {
            // パラメータの設定
            var parameters = new Parameters();

            // SQLの組み立て
            var sb = new StringBuilder();

            if (string.IsNullOrEmpty(postId))
            {    
                sb.Append("WITH MAIN AS(");
                sb.Append(CreateReadListSql());

                if (!string.IsNullOrEmpty(searchString))
                {
                    parameters.Add(":SEARCHWORD", DbType.Varchar2, searchString);
                    if (searchFlag)
                    {
                        sb.AppendLine($" AND (TA.GROUPCATEGORY_NAME LIKE  '%' || :SEARCHWORD || '%' ");
                        sb.AppendLine($" OR TA.GROUPCATEGORY_KANA LIKE  '%' || :SEARCHWORD || '%' ");
                        sb.AppendLine($" OR TA.GROUPCATEGORY_Ryakusho LIKE  '%' || :SEARCHWORD || '%' ");
                        sb.AppendLine($" OR TB.GROUP_NAME LIKE  '%' || :SEARCHWORD || '%' ");
                        sb.AppendLine($" OR TB.GROUP_KANA LIKE  '%' || :SEARCHWORD || '%' ");
                        sb.AppendLine($" OR TB.GROUP_RYAKUSHO LIKE  '%' || :SEARCHWORD || '%'  )");
                    }
                    else
                    {
                        sb.AppendLine($" AND (TA.GROUPCATEGORY_NAME LIKE :SEARCHWORD || '%'");
                        sb.AppendLine($" OR TA.GROUPCATEGORY_KANA LIKE :SEARCHWORD || '%'");
                        sb.AppendLine($" OR TA.GROUPCATEGORY_Ryakusho LIKE :SEARCHWORD || '%'");
                        sb.AppendLine($" OR TB.GROUP_NAME LIKE :SEARCHWORD || '%'");
                        sb.AppendLine($" OR TB.GROUP_KANA LIKE :SEARCHWORD || '%'");
                        sb.AppendLine($" OR TB.GROUP_RYAKUSHO LIKE :SEARCHWORD || '%' )");
                    }
                }

                if (!string.IsNullOrEmpty(groupCategoryCode))
                {
                    parameters.Add(":GROUPCATEGORYCODE", DbType.Char, groupCategoryCode);
                    sb.AppendLine(" AND TA.GROUPCATEGORY_CODE = :GROUPCATEGORYCODE ");
                }

                if (!string.IsNullOrEmpty(groupTani))
                {
                    parameters.Add(":GRPTANI", DbType.Char, groupTani);
                    sb.AppendLine(" AND TA.GROUPTANI = :GRPTANI ");
                }

                if (!string.IsNullOrEmpty(groupCode))
                {
                    parameters.Add(":GROUPCODE", DbType.Varchar2, groupCode);
                    sb.AppendLine(" AND TB.GROUP_CODE = :GROUPCODE ");
                }

                if (!string.IsNullOrEmpty(validFlag))
                {
                    parameters.Add(":VALIDFLAG", DbType.Varchar2, validFlag);
                    sb.AppendLine(" AND TB.VALID_FLAG = :VALIDFLAG");
                }

                sb.AppendLine(")");

                if (kijunbi > 0)
                {
                    parameters.Add(":KIJUNBI", DbType.Long, kijunbi);
                    sb.AppendLine("SELECT");
                    sb.AppendLine("	MAIN.*");
                    sb.AppendLine("	,(NVL(TF.PAT_NUM,0) + NVL(TF.STAFF_NUM,0)) NUM_OF_MEMBER");
                    sb.AppendLine("	FROM");
                    sb.AppendLine("		MAIN");
                    sb.AppendLine($"	INNER JOIN {_group_management_tb} TC");
                    sb.AppendLine("		ON TA.GRP_ID = TB.GROUP_ID");

                    sb.AppendLine("LEFT JOIN");
                    sb.AppendLine("    ( SELECT COUNT(GROUPMANAGEMENT_ID) PAT_NUM, GROUPMANAGEMENT_ID");
                    sb.AppendLine($"    	FROM {_group_patient_tb}");
                    sb.AppendLine("    	GROUP BY GROUPMANAGEMENT_ID ) TD ON TD.GROUPMANAGEMENT_ID = TC.ID");
                    sb.AppendLine("LEFT JOIN");
                    sb.AppendLine("	(SELECT COUNT(GROUPMANAGEMENT_ID) STAFF_NUM, GROUPMANAGEMENT_ID");
                    sb.AppendLine($"    	FROM {_group_staff_tb}");
                    sb.AppendLine("    	GROUP BY GROUPMANAGEMENT_ID) TE	ON TE.GROUPMANAGEMENT_ID = TC.ID");

                    sb.AppendLine("	WHERE");
                    sb.AppendLine("    TC.STARTDATE <= :KIJUNBI ");

                    if (kijunbiFlag == false)
                    {
                        sb.AppendLine($"    AND TC.ENDDATE >= :KIJUNBI ");
                    }
                }
                else
                {
                    sb.AppendLine("SELECT");
                    sb.AppendLine("	MAIN.*");
                    sb.AppendLine("	,(NVL(TD.PAT_NUM,0) + NVL(TE.STAFF_NUM,0)) NUM_OF_MEMBER");
                    sb.AppendLine("	FROM");
                    sb.AppendLine("		MAIN");
                    sb.AppendLine($"	LEFT JOIN {_group_management_tb} TC");
                    sb.AppendLine("		ON MAIN.GRP_ID = TC.GROUP_ID");

                    sb.AppendLine("LEFT JOIN");
                    sb.AppendLine("    (SELECT COUNT(GROUPMANAGEMENT_ID) PAT_NUM, GROUPMANAGEMENT_ID");
                    sb.AppendLine($"    	FROM {_group_patient_tb}");
                    sb.AppendLine("    	GROUP BY GROUPMANAGEMENT_ID ) TD ON TD.GROUPMANAGEMENT_ID = TC.ID");
                    sb.AppendLine("LEFT JOIN");
                    sb.AppendLine("	   (SELECT COUNT(GROUPMANAGEMENT_ID) STAFF_NUM, GROUPMANAGEMENT_ID");
                    sb.AppendLine($"    	FROM {_group_staff_tb}");
                    sb.AppendLine("    	GROUP BY GROUPMANAGEMENT_ID) TE	ON TE.GROUPMANAGEMENT_ID = TC.ID");
                }
                sb.AppendLine("ORDER BY MAIN.GROUPCATEGORY_CODE, MAIN.GRP_GROUP_CODE");
                sb.AppendLine(" OFFSET");
                parameters.Add(":OFFSET", DbType.Int32, offset);
                parameters.Add(":LIMIT", DbType.Int32, limit);
                sb.AppendLine(":OFFSET ROWS FETCH FIRST :LIMIT +1 ROWS ONLY");
            }
            else
            {
                sb.AppendLine("SELECT");
                sb.AppendLine("	TA.*");
                sb.AppendLine("	,(NVL(TD.PAT_NUM,0) + NVL(TE.STAFF_NUM,0)) NUM_OF_MEMBER");
                sb.AppendLine("FROM");
                sb.AppendLine($"	{_group_tb} TA");

                sb.AppendLine($"	LEFT JOIN {_group_management_tb} TC");
                sb.AppendLine("		ON TA.ID = TC.GROUP_ID");
                sb.AppendLine("LEFT JOIN");
                sb.AppendLine("    (SELECT COUNT(GROUPMANAGEMENT_ID) PAT_NUM, GROUPMANAGEMENT_ID");
                sb.AppendLine($"    	FROM {_group_patient_tb}");
                sb.AppendLine("    	GROUP BY GROUPMANAGEMENT_ID ) TD ON TD.GROUPMANAGEMENT_ID = TC.ID");
                sb.AppendLine("LEFT JOIN");
                sb.AppendLine("	   (SELECT COUNT(GROUPMANAGEMENT_ID) STAFF_NUM, GROUPMANAGEMENT_ID");
                sb.AppendLine($"    	FROM {_group_staff_tb}");
                sb.AppendLine("    	GROUP BY GROUPMANAGEMENT_ID) TE	ON TE.GROUPMANAGEMENT_ID = TC.ID");
                // Where
                sb.AppendLine("WHERE");
                parameters.Add(":ID", DbType.Varchar2, postId);
                sb.AppendLine($"  POSTID = :ID");
                sb.AppendLine("ORDER BY TA.GROUP_CODE");

            }

            var result = _access.SelectSql<GroupCategoryGroupEntity>(CallerInfo.Create(Shared.ToSharedContext()),
                                                    sb.ToString(),
                                                    parameters);

            return result;
        }
        /// <summary>
        /// IDグループ一覧取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<GroupCategoryGroupEntity> GetGroupByIdAsync(string id)
        {
            // パラメータの設定
            var parameters = new Parameters();

            // SQLの組み立て
            var sb = new StringBuilder();

            if (!string.IsNullOrEmpty(id))
            {
                sb.Append(CreateReadListSql());

                parameters.Add(":ID", DbType.Varchar2, id);
                sb.AppendLine("AND TB.ID = :ID ");
                sb.AppendLine("ORDER BY TA.GROUPCATEGORY_CODE, TB.GROUP_CODE");
            }

            var result = _access.SelectSql<GroupCategoryGroupEntity>(CallerInfo.Create(Shared.ToSharedContext()),
                                                    sb.ToString(),
                                                    parameters);
            return result;
        }

        public string ReadGroupCodeById(string id)
        {
            // パラメータの設定
            var parameters = new Parameters();
            {
                parameters.Add(":ID", DbType.Varchar2, id);
            }

            // SQLの組み立て
            var sb = new StringBuilder();

            //SELECT
            sb.AppendLine("SELECT");
            sb.AppendLine("	GROUP_CODE");
            sb.AppendLine($"FROM {_group_tb}");
            sb.AppendLine("WHERE ");
            sb.AppendLine("  ID = :ID");

            var results = _access.SelectSql<GroupEntity>(CallerInfo.Create(Shared.ToSharedContext()),
                                                    sb.ToString(),
                                                    parameters);
            
            if (results != null && results?.Count > 0)
            {
                return results[0].GROUP_CODE;
            }

            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupcategoryid"></param>
        /// <returns></returns>
        public string ReadNewCodeGroup(string groupcategoryid)
        {
            // パラメータの設定
            var parameters = new Parameters();
            {
                parameters.Add(":GROUPCATEGORYID", DbType.Varchar2, groupcategoryid);
            }

            // SQLの組み立て
            var sb = new StringBuilder();
            sb.AppendLine("SELECT");
            sb.AppendLine("	MAX(TA.GROUP_CODE)+1 AS GROUP_CODE");
            sb.AppendLine("FROM");
            sb.AppendLine($" {_group_tb} TA");
            sb.AppendLine("WHERE");
            sb.AppendLine("	TA.GROUPCATEGORY_ID = :GROUPCATEGORYID");

            var results = _access.SelectSql<GroupEntity>(CallerInfo.Create(Shared.ToSharedContext()), sb.ToString(), parameters);

            var result = "0001";
            if (results?.Count > 0 && results[0].GROUP_CODE != null)
            {
                result = results[0]?.GROUP_CODE.PadLeft(4, '0');
            }

            // 実行
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupcode"></param>
        /// <param name="facilityid"></param>
        /// <param name="facilitygroupid"></param>
        /// <returns></returns>
        public GroupReadByCodeEntity ReadByCodeGroup(string groupcode, string facilityid, string facilitygroupid, string areacorpid)
        {
            // パラメータの設定
            var parameters = new Parameters();
            {
                // 医療機関・施設ID
                parameters.Add(":GROUPCODE", DbType.Char, groupcode);
                parameters.Add(":FACILITYID", DbType.Varchar2, facilityid);
                parameters.Add(":FACILITYGROUPID", DbType.Varchar2, facilitygroupid);
                parameters.Add(":AREACORPID", DbType.Varchar2, areacorpid);
            }

            // SQLの組み立て
            var sb = new StringBuilder();

            //SELECT
            sb.AppendLine("SELECT");
            sb.AppendLine("	TA.ID");
            sb.AppendLine("	,TA.LOCKVERSION");
            sb.AppendLine("	,TA.IS_DELETED");
            sb.AppendLine("FROM");
            sb.AppendLine($" {_group_tb} TA");
            sb.AppendLine("WHERE ");
            sb.AppendLine("  TA.GROUP_CODE = :GROUPCODE");
            sb.AppendLine("  AND TA.FACILITY_ID = :FACILITYID");
            sb.AppendLine("  AND TA.FACILITYGROUP_ID = :FACILITYGROUPID");
            sb.AppendLine("  AND TA.AREACORP_ID = :AREACORPID");

            var results = _access.SelectSql<GroupReadByCodeEntity>(CallerInfo.Create(Shared.ToSharedContext()),
                                                    sb.ToString(),
                                                    parameters);

            // 実行
            return results.FirstOrDefault();
        }

        private StringBuilder CreateReadListSql()
        {
            // SQLの組み立て
            var sb = new StringBuilder();

            sb.AppendLine("SELECT");
            sb.AppendLine("	TA.ID");
            sb.AppendLine("	,TA.LOCKVERSION");
            sb.AppendLine("	,TA.AREACORP_ID");
            sb.AppendLine("	,TA.FACILITYGROUP_ID");
            sb.AppendLine("	,TA.FACILITY_ID");
            sb.AppendLine("	,TA.GROUPCATEGORY_CODE");
            sb.AppendLine("	,TA.GROUPCATEGORY_NAME");
            sb.AppendLine("	,TA.GROUPCATEGORY_KANA");
            sb.AppendLine("	,TA.GROUPCATEGORY_RYAKUSHO");
            sb.AppendLine("	,TA.GROUPTANI");
            sb.AppendLine("	,TA.DISPLAY_ORDER");
            sb.AppendLine("	,TA.IS_DELETED");
            sb.AppendLine("	,TA.UPDATE_ACCOUNTID");
            sb.AppendLine("	,TA.UPDATE_LOGINID");
            sb.AppendLine("	,TA.UPDATE_FACILITYID");
            sb.AppendLine("	,TA.UPDATE_TIMESTAMP");
            sb.AppendLine("	,TA.POST_ID");
            sb.AppendLine("	,TA.LASTUPDATER_NAME");
            sb.AppendLine("	,TA.LASTUPDATER_ID");
            sb.AppendLine("	,TB.ID GRP_ID");
            sb.AppendLine("	,TB.LOCKVERSION GRP_LOCKVERSION");
            sb.AppendLine("	,TB.AREACORP_ID GRP_AREACORP_ID");
            sb.AppendLine("	,TB.FACILITYGROUP_ID GRP_FACILITYGROUP_ID");
            sb.AppendLine("	,TB.FACILITY_ID GRP_FACILITY_ID");
            sb.AppendLine("	,TB.GROUP_CODE GRP_GROUP_CODE");
            sb.AppendLine("	,TB.GROUPCATEGORY_ID GRP_GROUPCATEGORY_ID");
            sb.AppendLine("	,TB.GROUP_NAME GRP_GROUP_NAME");
            sb.AppendLine("	,TB.GROUP_KANA GRP_GROUP_KANA");
            sb.AppendLine("	,TB.GROUP_RYAKUSHO GRP_GROUP_RYAKUSHO");
            sb.AppendLine("	,TB.VALID_FLAG GRP_VALID_FLAG");
            sb.AppendLine("	,TB.REMARKS GRP_REMARKS");
            sb.AppendLine("	,TB.DISPLAY_ORDER GRP_DISPLAY_ORDER");
            sb.AppendLine("	,TB.IS_DELETED GRP_IS_DELETED");
            sb.AppendLine("	,TB.UPDATE_ACCOUNTID GRP_UPDATE_ACCOUNTID");
            sb.AppendLine("	,TB.UPDATE_LOGINID GRP_UPDATE_LOGINID");
            sb.AppendLine("	,TB.UPDATE_FACILITYID GRP_UPDATE_FACILITYID");
            sb.AppendLine("	,TB.UPDATE_TIMESTAMP GRP_UPDATE_TIMESTAMP");
            sb.AppendLine("	,TB.POST_ID GRP_POST_ID");
            sb.AppendLine("	,TB.LASTUPDATER_NAME GRP_LASTUPDATER_NAME");
            sb.AppendLine("	,TB.LASTUPDATER_ID GRP_LASTUPDATER_ID");
            sb.AppendLine("FROM");
            sb.AppendLine("	GRP_GROUPCATEGORY_TBL TA");
            sb.AppendLine("	INNER JOIN GRP_GROUP_TBL TB");
            sb.AppendLine("	ON TA.ID = TB.GROUPCATEGORY_ID");
            sb.AppendLine("WHERE");
            sb.AppendLine("	TA.IS_DELETED = '0'");
            sb.AppendLine("	AND TB.IS_DELETED = '0'");

            return sb;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public Result<GroupEntity> Create(GroupEntity content)
        {
            // データ更新
            return _access.InsertReturning(CallerInfo.Create(Shared.ToSharedContext()), content, _group_tb);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public Result<GroupEntity> Update(GroupEntity content)
        {
            // データ更新
            return _access.UpdateReturning(CallerInfo.Create(Shared.ToSharedContext()), content, _group_tb);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public Result<GroupDeleteEntity> UpdateForDelete(GroupDeleteEntity content)
        {
            // データ更新
            return _access.UpdateReturning(CallerInfo.Create(Shared.ToSharedContext()), content, _group_tb);
        }

        public bool Exists(string id, string facilityId, string groupCode, string groupcategoryId)
        {
            var parameters = new Parameters();
            {
                parameters.Add(":ID", DbType.Char, id);
                parameters.Add(":FACILITY_ID", DbType.Varchar2, facilityId);
                parameters.Add(":GROUPCATEGORY_ID", DbType.Varchar2, groupcategoryId);
                parameters.Add(":GROUP_CODE", DbType.Varchar2, groupCode);
            }

            var where = ":ID != ID AND GROUPCATEGORY_ID = :GROUPCATEGORY_ID AND GROUP_CODE = :GROUP_CODE AND FACILITY_ID = :FACILITY_ID";

            if (_access.Count(CallerInfo.Create(Shared.ToSharedContext()), _group_tb, where, parameters) == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
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
