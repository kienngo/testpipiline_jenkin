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
    public class GroupManagementAccess : IGroupManagementAccess
    {
        private readonly string _group_tb = "GRP_GROUP";
        private readonly string _groupCategory_tb = "GRP_GROUPCATEGORY";
        private readonly string _groupManagement_tb = "GRP_GROUPMANAGEMENT";
        private readonly string _groupPatient_tb = "GRP_GROUPPATIENT";
        private readonly string _groupStaff_tb = "GRP_GROUPSTAFF";

        private IDBAccess _access;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="access"></param>
        public GroupManagementAccess(IDBAccess access)
        {
            _access = access;
        }

        /// <summary>
        /// グループ管理一覧取得
        /// </summary>
        /// <param name="groupCategoryCode">グループ分類コード</param>
        /// <param name="groupTani">グループ管理単位</param>
        /// <param name="areaCorpId">地域法人グループ</param>
        /// <param name="facilityGroupId">医療機関・施設グループID</param>
        /// <param name="facilityId">医療機関・施設ID</param>
        /// <param name="groupCode">グループコード</param>
        /// <param name="validFlag">無効含む</param>
        /// <param name="kijunbi">基準日</param>
        /// <param name="kijunbiFlag">終了分を含む</param>
        /// <param name="groupManagementCode">グループ管理コード</param>
        /// <param name="postId">POST識別子</param>
        /// <param name="limit">取得上限件数</param>
        /// <param name="offset">オフセット</param>
        /// <returns></returns>
        public IList<GroupManagementListDetailEntity> Get(string groupCategoryCode = "",
                                                                string groupTani = "",
                                                                string areaCorpId = "",
                                                                string facilityGroupId = "",
                                                                string facilityId = "",
                                                                string groupCode = "",
                                                                string validFlag = "",
                                                                int? kijunbi = 0,
                                                                bool kijunbiFlag = false,
                                                                string groupManagementCode = "",
                                                                string postId = "",
                                                                int? limit = 1000,
                                                                int? offset = 0)
        {
            // パラメータの設定
            var parameters = new Parameters();

            // SQLの組み立て
            var sb = new StringBuilder();

            if (string.IsNullOrEmpty(postId))
            {
                sb.AppendLine("WITH MAIN AS (");
                sb.AppendLine("	SELECT");
                sb.AppendLine("		TA.ID");
                sb.AppendLine("		,TA.LOCKVERSION");
                sb.AppendLine("		,TA.AREACORP_ID");
                sb.AppendLine("		,TA.FACILITYGROUP_ID");
                sb.AppendLine("		,TA.FACILITY_ID");
                sb.AppendLine("		,TA.GROUPCATEGORY_CODE");
                sb.AppendLine("		,TA.GROUPCATEGORY_NAME");
                sb.AppendLine("		,TA.GROUPCATEGORY_KANA");
                sb.AppendLine("		,TA.GROUPCATEGORY_RYAKUSHO");
                sb.AppendLine("		,TA.GROUPTANI");
                sb.AppendLine("		,TA.DISPLAY_ORDER");
                sb.AppendLine("		,TA.IS_DELETED");
                sb.AppendLine("		,TA.UPDATE_ACCOUNTID");
                sb.AppendLine("		,TA.UPDATE_LOGINID");
                sb.AppendLine("		,TA.UPDATE_FACILITYID");
                sb.AppendLine("		,TA.UPDATE_TIMESTAMP");
                sb.AppendLine("		,TA.POST_ID");
                sb.AppendLine("		,TA.LASTUPDATER_NAME");
                sb.AppendLine("		,TA.LASTUPDATER_ID");
                sb.AppendLine("		,TB.ID DETAIL_ID");
                sb.AppendLine("		,TB.LOCKVERSION DETAIL_LOCKVERSION");
                sb.AppendLine("		,TB.AREACORP_ID DETAIL_AREACORP_ID");
                sb.AppendLine("		,TB.FACILITYGROUP_ID DETAIL_FACILITYGROUP_ID");
                sb.AppendLine("		,TB.FACILITY_ID DETAIL_FACILITY_ID");
                sb.AppendLine("		,TB.GROUP_CODE DETAIL_GROUP_CODE");
                sb.AppendLine("		,TB.GROUPCATEGORY_ID DETAIL_GROUPCATEGORY_ID");
                sb.AppendLine("		,TB.GROUP_NAME DETAIL_GROUP_NAME");
                sb.AppendLine("		,TB.GROUP_KANA DETAIL_GROUP_KANA");
                sb.AppendLine("		,TB.GROUP_RYAKUSHO DETAIL_GROUP_RYAKUSHO");
                sb.AppendLine("		,TB.VALID_FLAG DETAIL_VALID_FLAG");
                sb.AppendLine("		,TB.REMARKS DETAIL_REMARKS");
                sb.AppendLine("		,TB.DISPLAY_ORDER DETAIL_DISPLAY_ORDER");
                sb.AppendLine("		,TB.IS_DELETED DETAIL_IS_DELETED");
                sb.AppendLine("		,TB.UPDATE_ACCOUNTID DETAIL_UPDATE_ACCOUNTID");
                sb.AppendLine("		,TB.UPDATE_LOGINID DETAIL_UPDATE_LOGINID");
                sb.AppendLine("		,TB.UPDATE_FACILITYID DETAIL_UPDATE_FACILITYID");
                sb.AppendLine("		,TB.UPDATE_TIMESTAMP DETAIL_UPDATE_TIMESTAMP");
                sb.AppendLine("		,TB.POST_ID DETAIL_POST_ID");
                sb.AppendLine("		,TB.LASTUPDATER_NAME DETAIL_LASTUPDATER_NAME");
                sb.AppendLine("		,TB.LASTUPDATER_ID DETAIL_LASTUPDATER_ID");
                sb.AppendLine("	FROM");
                sb.AppendLine($"	{_groupCategory_tb} TA");
                sb.AppendLine($"		INNER JOIN {_group_tb} TB");
                sb.AppendLine("		ON TA.ID = TB.GROUPCATEGORY_ID");
                sb.AppendLine("	WHERE");
                sb.AppendLine("		TA.IS_DELETED = '0'");
                sb.AppendLine("	AND TB.IS_DELETED = '0'");
        
                if (!string.IsNullOrEmpty(facilityId))
                {
                    parameters.Add(":FACILITYID", DbType.Varchar2, facilityId);   
                    sb.AppendLine("	AND TA.FACILITY_ID = :FACILITYID");              
                }

                if (!string.IsNullOrEmpty(facilityGroupId))
                {
                    parameters.Add(":FACILITYGROUPID", DbType.Varchar2, facilityGroupId);
                    sb.AppendLine("	AND TA.FACILITYGROUP_ID = :FACILITYGROUPID");
                }

                parameters.Add(":AREACORPID", DbType.Varchar2, areaCorpId);    
                sb.AppendLine("	AND TA.AREACORP_ID = :AREACORPID");

                if (!string.IsNullOrEmpty(groupTani))
                {
                    parameters.Add(":GROUPTANI", DbType.Char, groupTani);
                    sb.AppendLine("	 AND TA.GROUPTANI = :GROUPTANI");
                }

                if (!string.IsNullOrEmpty(groupCategoryCode))
                {
                    parameters.Add(":GROUPCATEGORYCODE", DbType.Char, groupCategoryCode);
                    sb.AppendLine("	 AND TA.GROUPCATEGORY_CODE = :GROUPCATEGORYCODE");
                }

                if (!string.IsNullOrEmpty(groupCode))
                {
                    parameters.Add(":GROUPCODE", DbType.Char, groupCode);
                    sb.AppendLine("	 AND TB.GROUP_CODE = :GROUPCODE");
                }

                if (!string.IsNullOrEmpty(validFlag))
                {
                    parameters.Add(":VALIDFLAG", DbType.Char, validFlag);
                    sb.AppendLine("	 AND TB.VALID_FLAG = :VALIDFLAG");
                }

                parameters.Add(":OFFSET", DbType.Int32, offset);
                parameters.Add(":LIMIT", DbType.Int32, limit);
                sb.AppendLine("ORDER BY TA.GROUPCATEGORY_CODE, TB.GROUP_CODE");
                sb.AppendLine("OFFSET :OFFSET ROWS FETCH FIRST :LIMIT +1 ROWS ONLY");
                sb.AppendLine(")");
                sb.AppendLine("SELECT");
                sb.AppendLine("	MAIN.*");
                sb.AppendLine("	,TC.ID MNG_ID");
                sb.AppendLine("	,TC.LOCKVERSION MNG_LOCKVERSION");
                sb.AppendLine("	,TC.AREACORP_ID MNG_AREACORP_ID");
                sb.AppendLine("	,TC.FACILITYGROUP_ID MNG_FACILITYGROUP_ID");
                sb.AppendLine("	,TC.FACILITY_ID MNG_FACILITY_ID");
                sb.AppendLine("	,TC.GROUPMANAGEMENT_CODE MNG_GROUPMANAGEMENT_CODE");
                sb.AppendLine("	,TC.GROUP_ID MNG_GROUP_ID");
                sb.AppendLine("	,TC.STARTDATE MNG_STARTDATE");
                sb.AppendLine("	,TC.ENDDATE MNG_ENDDATE");
                sb.AppendLine("	,TC.IS_DELETED MNG_IS_DELETED");
                sb.AppendLine("	,TC.UPDATE_ACCOUNTID MNG_UPDATE_ACCOUNTID");
                sb.AppendLine("	,TC.UPDATE_LOGINID MNG_UPDATE_LOGINID");
                sb.AppendLine("	,TC.UPDATE_FACILITYID MNG_UPDATE_FACILITYID");
                sb.AppendLine("	,TC.UPDATE_TIMESTAMP MNG_UPDATE_TIMESTAMP");
                sb.AppendLine("	,TC.POST_ID MNG_POST_ID");
                sb.AppendLine("	,TC.LASTUPDATER_NAME MNG_LASTUPDATER_NAME");
                sb.AppendLine("	,TC.LASTUPDATER_ID MNG_LASTUPDATER_ID");
                sb.AppendLine("FROM");
                sb.AppendLine("	MAIN");
                sb.AppendLine($"	LEFT JOIN {_groupManagement_tb} TC");
                sb.AppendLine("		ON MAIN.DETAIL_ID = TC.GROUP_ID");
                sb.AppendLine("		AND TC.IS_DELETED = '0'");

                if (kijunbi > 0)
                {
                    parameters.Add(":KIJUNBI", DbType.Int32, kijunbi);
                    sb.AppendLine("		AND TC.STARTDATE <= :KIJUNBI");

                    if (kijunbiFlag == false)
                    {
                        sb.AppendLine("		AND :KIJUNBI <= TC.ENDDATE");
                    }
                }
                sb.AppendLine("ORDER BY MAIN.GROUPCATEGORY_CODE, MAIN.DETAIL_GROUP_CODE, TC.GROUPMANAGEMENT_CODE");

            }
            else
            {
                parameters.Add(":POSTID", DbType.Varchar2, postId);
                sb.AppendLine("SELECT");
                sb.AppendLine("	TA.*");
                sb.AppendLine("FROM");
                sb.AppendLine($" {_groupManagement_tb} TA");
                sb.AppendLine("WHERE");
                sb.AppendLine("    POST_ID = :POSTID");
                sb.AppendLine("ORDER BY TA.GROUPMANAGEMENT_CODE");

            }

            var accountTrans = _access.SelectSql<GroupManagementListDetailEntity>(CallerInfo.Create(Shared.ToSharedContext()),
                                        sb.ToString(),
                                        parameters);
            return accountTrans;
        }

        /// <summary>
        /// グループ管理1件取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<GroupManagementDetailSettingsEntity> GetById(string id)
        {
            // パラメータの設定
            var parameters = new Parameters();
            parameters.Add(":ID", DbType.Varchar2, id);

            // SQLの組み立て
            var sb = new StringBuilder();

            //SELECT
            sb.AppendLine("SELECT");
            sb.AppendLine("	TA.ID");
            sb.AppendLine("	,TA.LOCKVERSION");
            sb.AppendLine("	,TA.AREACORP_ID");
            sb.AppendLine("	,TA.FACILITYGROUP_ID");
            sb.AppendLine("	,TA.FACILITY_ID");
            sb.AppendLine("	,TA.GROUPMANAGEMENT_CODE");
            sb.AppendLine("	,TA.GROUP_ID");
            sb.AppendLine("	,TA.STARTDATE");
            sb.AppendLine("	,TA.ENDDATE");
            sb.AppendLine("	,TA.IS_DELETED");
            sb.AppendLine("	,TA.UPDATE_ACCOUNTID");
            sb.AppendLine("	,TA.UPDATE_LOGINID");
            sb.AppendLine("	,TA.UPDATE_FACILITYID");
            sb.AppendLine("	,TA.UPDATE_TIMESTAMP");
            sb.AppendLine("	,TA.POST_ID");
            sb.AppendLine("	,TA.LASTUPDATER_NAME");
            sb.AppendLine("	,TA.LASTUPDATER_ID");
            sb.AppendLine("	,TB.ID PAT_ID");
            sb.AppendLine("	,TB.LOCKVERSION PAT_LOCKVERSION");
            sb.AppendLine("	,TB.AREACORP_ID PAT_AREACORP_ID");
            sb.AppendLine("	,TB.FACILITYGROUP_ID PAT_FACILITYGROUP_ID");
            sb.AppendLine("	,TB.FACILITY_ID PAT_FACILITY_ID");
            sb.AppendLine("	,TB.GROUPPATIENT_CODE PAT_GROUPPATIENT_CODE");
            sb.AppendLine("	,TB.GROUPMANAGEMENT_ID PAT_GROUPMANAGEMENT_ID");
            sb.AppendLine("	,TB.PATIENT_ID PAT_PATIENT_ID");
            sb.AppendLine("	,TB.STARTDATE PAT_STARTDATE");
            sb.AppendLine("	,TB.ENDDATE PAT_ENDDATE");
            sb.AppendLine("	,TB.DISPLAY_ORDER PAT_DISPLAY_ORDER");
            sb.AppendLine("	,TB.UPDATE_ACCOUNTID PAT_UPDATE_ACCOUNTID");
            sb.AppendLine("	,TB.UPDATE_LOGINID PAT_UPDATE_LOGINID");
            sb.AppendLine("	,TB.UPDATE_FACILITYID PAT_UPDATE_FACILITYID");
            sb.AppendLine("	,TB.UPDATE_TIMESTAMP PAT_UPDATE_TIMESTAMP");
            sb.AppendLine("	,TB.POST_ID PAT_POST_ID");
            sb.AppendLine("	,TB.LASTUPDATER_NAME PAT_LASTUPDATER_NAME");
            sb.AppendLine("	,TB.LASTUPDATER_ID PAT_LASTUPDATER_ID");
            sb.AppendLine("	,NULL STAFF_ID");
            sb.AppendLine("	,NULL STAFF_LOCKVERSION");
            sb.AppendLine("	,NULL STAFF_AREACORP_ID");
            sb.AppendLine("	,NULL STAFF_FACILITYGROUP_ID");
            sb.AppendLine("	,NULL STAFF_FACILITY_ID");
            sb.AppendLine("	,NULL STAFF_GROUPSTAFF_CODE");
            sb.AppendLine("	,NULL STAFF_GROUPMANAGEMENT_ID");
            sb.AppendLine("	,NULL STAFF_STAFF_ID");
            sb.AppendLine("	,NULL STAFF_STARTDATE");
            sb.AppendLine("	,NULL STAFF_ENDDATE");
            sb.AppendLine("	,NULL STAFF_DISPLAY_ORDER");
            sb.AppendLine("	,NULL STAFF_UPDATE_ACCOUNTID");
            sb.AppendLine("	,NULL STAFF_UPDATE_LOGINID");
            sb.AppendLine("	,NULL STAFF_UPDATE_FACILITYID");
            sb.AppendLine("	,NULL STAFF_UPDATE_TIMESTAMP");
            sb.AppendLine("	,NULL STAFF_POST_ID");
            sb.AppendLine("	,NULL STAFF_LASTUPDATER_NAME");
            sb.AppendLine("	,NULL STAFF_LASTUPDATER_ID");
            sb.AppendLine("FROM");
            sb.AppendLine($" {_groupManagement_tb} TA");
            sb.AppendLine($" LEFT JOIN {_groupPatient_tb} TB");
            sb.AppendLine("	ON TA.ID = TB.GROUPMANAGEMENT_ID");

            //WHERE
            sb.AppendLine("WHERE");
            sb.AppendLine("	TA.IS_DELETED = '0'");
            sb.AppendLine("	AND TA.ID = :ID");

            //UNION
            sb.AppendLine("UNION");

            //SELECT
            sb.AppendLine("SELECT");
            sb.AppendLine("	TA.ID");
            sb.AppendLine("	,TA.LOCKVERSION");
            sb.AppendLine("	,TA.AREACORP_ID");
            sb.AppendLine("	,TA.FACILITYGROUP_ID");
            sb.AppendLine("	,TA.FACILITY_ID");
            sb.AppendLine("	,TA.GROUPMANAGEMENT_CODE");
            sb.AppendLine("	,TA.GROUP_ID");
            sb.AppendLine("	,TA.STARTDATE");
            sb.AppendLine("	,TA.ENDDATE");
            sb.AppendLine("	,TA.IS_DELETED");
            sb.AppendLine("	,TA.UPDATE_ACCOUNTID");
            sb.AppendLine("	,TA.UPDATE_LOGINID");
            sb.AppendLine("	,TA.UPDATE_FACILITYID");
            sb.AppendLine("	,TA.UPDATE_TIMESTAMP");
            sb.AppendLine("	,TA.POST_ID");
            sb.AppendLine("	,TA.LASTUPDATER_NAME");
            sb.AppendLine("	,TA.LASTUPDATER_ID");
            sb.AppendLine("	,NULL PAT_ID");
            sb.AppendLine("	,NULL PAT_LOCKVERSION");
            sb.AppendLine("	,NULL PAT_AREACORP_ID");
            sb.AppendLine("	,NULL PAT_FACILITYGROUP_ID");
            sb.AppendLine("	,NULL PAT_FACILITY_ID");
            sb.AppendLine("	,NULL PAT_GROUPPATIENT_CODE");
            sb.AppendLine("	,NULL PAT_GROUPMANAGEMENT_ID");
            sb.AppendLine("	,NULL PAT_PATIENT_ID");
            sb.AppendLine("	,NULL PAT_STARTDATE");
            sb.AppendLine("	,NULL PAT_ENDDATE");
            sb.AppendLine("	,NULL PAT_DISPLAY_ORDER");
            sb.AppendLine("	,NULL PAT_UPDATE_ACCOUNTID");
            sb.AppendLine("	,NULL PAT_UPDATE_LOGINID");
            sb.AppendLine("	,NULL PAT_UPDATE_FACILITYID");
            sb.AppendLine("	,NULL PAT_UPDATE_TIMESTAMP");
            sb.AppendLine("	,NULL PAT_POST_ID");
            sb.AppendLine("	,NULL PAT_LASTUPDATER_NAME");
            sb.AppendLine("	,NULL PAT_LASTUPDATER_ID");
            sb.AppendLine("	,TC.ID STAFF_ID");
            sb.AppendLine("	,TC.LOCKVERSION STAFF_LOCKVERSION");
            sb.AppendLine("	,TC.AREACORP_ID STAFF_AREACORP_ID");
            sb.AppendLine("	,TC.FACILITYGROUP_ID STAFF_FACILITYGROUP_ID");
            sb.AppendLine("	,TC.FACILITY_ID STAFF_FACILITY_ID");
            sb.AppendLine("	,TC.GROUPSTAFF_CODE STAFF_GROUPSTAFF_CODE");
            sb.AppendLine("	,TC.GROUPMANAGEMENT_ID STAFF_GROUPMANAGEMENT_ID");
            sb.AppendLine("	,TC.STAFF_ID STAFF_STAFF_ID");
            sb.AppendLine("	,TC.STARTDATE STAFF_STARTDATE");
            sb.AppendLine("	,TC.ENDDATE STAFF_ENDDATE");
            sb.AppendLine("	,TC.DISPLAY_ORDER STAFF_DISPLAY_ORDER");
            sb.AppendLine("	,TC.UPDATE_ACCOUNTID STAFF_UPDATE_ACCOUNTID");
            sb.AppendLine("	,TC.UPDATE_LOGINID STAFF_UPDATE_LOGINID");
            sb.AppendLine("	,TC.UPDATE_FACILITYID STAFF_UPDATE_FACILITYID");
            sb.AppendLine("	,TC.UPDATE_TIMESTAMP STAFF_UPDATE_TIMESTAMP");
            sb.AppendLine("	,TC.POST_ID STAFF_POST_ID");
            sb.AppendLine("	,TC.LASTUPDATER_NAME STAFF_LASTUPDATER_NAME");
            sb.AppendLine("	,TC.LASTUPDATER_ID STAFF_LASTUPDATER_ID");
            sb.AppendLine("FROM");
            sb.AppendLine($" {_groupManagement_tb} TA");
            sb.AppendLine($" LEFT JOIN {_groupStaff_tb} TC");
            sb.AppendLine("	ON TA.ID = TC.GROUPMANAGEMENT_ID");

            //WHERE
            sb.AppendLine("WHERE");
            sb.AppendLine("	TA.IS_DELETED = '0'");
            sb.AppendLine("	AND TA.ID = :ID");

            var accountTrans = _access.SelectSql<GroupManagementDetailSettingsEntity>(CallerInfo.Create(Shared.ToSharedContext()),
                                                    sb.ToString(),
                                                    parameters);
            return accountTrans;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="facilityid"></param>
        /// <param name="facilitygroupid"></param>
        /// <returns></returns>
        public string ReadNewCodeGroupManagement(string facilityid, string facilitygroupid)
        {
            // パラメータの設定
            var parameters = new Parameters();
            {
                parameters.Add(":FACILITYID", DbType.Varchar2, facilityid);
                parameters.Add(":FACILITYGROUPID", DbType.Varchar2, facilitygroupid);
            }

            // SQLの組み立て
            var sb = new StringBuilder();
            sb.AppendLine("SELECT");
            sb.AppendLine("	MAX(TA.GROUPMANAGEMENT_CODE)+1 AS GROUPMANAGEMENT_CODE");
            sb.AppendLine("FROM");
            sb.AppendLine($" {_groupManagement_tb} TA");
            sb.AppendLine("WHERE");
            sb.AppendLine("	TA.FACILITY_ID = :FACILITYID");
            sb.AppendLine(" AND TA.FACILITYGROUP_ID = :FACILITYGROUPID");

            var results = _access.SelectSql<GroupManagementEntity>(CallerInfo.Create(Shared.ToSharedContext()), sb.ToString(), parameters);

            var result = "0001";
            if (results?.Count > 0 && results[0].GROUPMANAGEMENT_CODE != null)
            {
                result = results[0]?.GROUPMANAGEMENT_CODE.PadLeft(4, '0');
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
        public GroupManagementEntity ReadByCodeGroupManagement(string groupManagementCode, string facilityid, string facilitygroupid)
        {
            // パラメータの設定
            var parameters = new Parameters();
            {
                // 医療機関・施設ID
                parameters.Add(":GROUPMANAGEMENT_CODE", DbType.Char, groupManagementCode);
                parameters.Add(":FACILITYID", DbType.Varchar2, facilityid);
                parameters.Add(":FACILITYGROUPID", DbType.Varchar2, facilitygroupid);
            }

            // SQLの組み立て
            var sb = new StringBuilder();

            //SELECT
            sb.AppendLine("SELECT");
            sb.AppendLine("	TA.ID");
            sb.AppendLine("	,TA.LOCKVERSION");
            sb.AppendLine("	,TA.IS_DELETED");
            sb.AppendLine("FROM");
            sb.AppendLine($" {_groupManagement_tb} TA");
            sb.AppendLine("WHERE ");
            sb.AppendLine("  TA.GROUPMANAGEMENT_CODE = :GROUPMANAGEMENT_CODE");
            sb.AppendLine("  AND TA.FACILITY_ID = :FACILITYID");
            sb.AppendLine("  AND TA.FACILITYGROUP_ID = :FACILITYGROUPID");

            var results = _access.SelectSql<GroupManagementEntity>(CallerInfo.Create(Shared.ToSharedContext()),
                                                    sb.ToString(),
                                                    parameters);

            // 実行
            return results.FirstOrDefault();
        }

        /// <summary>
        /// グループ管理登録
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public Result<GroupManagementEntity> Create(GroupManagementEntity content)
        {
            if (content.ENDDATE == 0)
            {
                content.ENDDATE = 99999999;
            }
            // データ更新
            return _access.InsertReturning(CallerInfo.Create(Shared.ToSharedContext()), content, _groupManagement_tb);
        }

        /// <summary>
        /// グループ管理更新
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public Result<GroupManagementEntity> Update(GroupManagementEntity content)
        {
            // データ更新
            return _access.UpdateReturning(CallerInfo.Create(Shared.ToSharedContext()), content, _groupManagement_tb);
        }


        /// <summary>
        /// グループ管理削除
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public Result<GroupManagementDeleteEntity> UpdateForDelete(GroupManagementDeleteEntity content)
        {
            // データ更新
            return _access.UpdateReturning(CallerInfo.Create(Shared.ToSharedContext()), content, _groupManagement_tb);
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
