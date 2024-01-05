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
    public class GroupPatientAccess : IGroupPatientAccess
    {
        private readonly string _tb_grp_groupPatient = "GRP_GROUPPATIENT";
        private readonly string _tb_grp_groupcategory_tbl = "GRP_GROUPCATEGORY";
        private readonly string _tb_grp_group_tbl = "GRP_GROUP";
        private readonly string _tb_grp_groupmanagement_tbl = "GRP_GROUPMANAGEMENT";

        /// <summary>Oracleアクセサ</summary>
        private IDBAccess _access;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="access"></param>
        public GroupPatientAccess(IDBAccess access)
        {
            _access = access;
        }

        /// <summary>
        /// 検索メソッド
        /// </summary>
        /// <param name="groupCategoryCode"></param>
        /// <param name="groupTani"></param>
        /// <param name="areaCorpId"></param>
        /// <param name="facilityGroupId"></param>
        /// <param name="facilityId"></param>
        /// <param name="groupCode"></param>
        /// <param name="validFlag"></param>
        /// <param name="groupManagementCode"></param>
        /// <param name="kijunbi"></param>
        /// <param name="kijunbiFlag"></param>
        /// <param name="patientId"></param>
        /// <param name="postId"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public IList<GroupPatientResultEntity> Get(string groupCategoryCode = "",
                                                    string groupTani = "",
                                                    string areaCorpId = "",
                                                    string facilityGroupId = "",
                                                    string facilityId = "",
                                                    string groupCode = "",
                                                    string validFlag = "",
                                                    string groupManagementCode = "",
                                                    int? kijunbi = 0,
                                                    bool kijunbiFlag = false,
                                                    string patientId = "",
                                                    string postId = "",
                                                    int? limit = 1000,
                                                    int? offset = 0)
        {
            // パラメータの設定
            var parameters = new Parameters();

            // SQLの組み立て
            var sb = new StringBuilder();

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
            sb.AppendLine("		,TB.LOCKVERSION AS DETAIL_LOCKVERSION");
            sb.AppendLine("		,TB.AREACORP_ID AS DETAIL_AREACORP_ID");
            sb.AppendLine("		,TB.FACILITYGROUP_ID AS DETAIL_FACILITYGROUP_ID");
            sb.AppendLine("		,TB.FACILITY_ID AS DETAIL_FACILITY_ID");
            sb.AppendLine("		,TB.GROUP_CODE AS DETAIL_GROUP_CODE");
            sb.AppendLine("		,TB.GROUPCATEGORY_ID AS DETAIL_GROUPCATEGORY_ID");
            sb.AppendLine("		,TB.GROUP_NAME AS DETAIL_GROUP_NAME");
            sb.AppendLine("		,TB.GROUP_KANA AS DETAIL_GROUP_KANA");
            sb.AppendLine("		,TB.GROUP_RYAKUSHO AS DETAIL_GROUP_RYAKUSHO");
            sb.AppendLine("		,TB.VALID_FLAG AS DETAIL_VALID_FLAG");
            sb.AppendLine("		,TB.REMARKS AS DETAIL_REMARKS");
            sb.AppendLine("		,TB.DISPLAY_ORDER AS DETAIL_DISPLAY_ORDER");
            sb.AppendLine("		,TB.IS_DELETED AS DETAIL_IS_DELETED");
            sb.AppendLine("		,TB.UPDATE_ACCOUNTID AS DETAIL_UPDATE_ACCOUNTID");
            sb.AppendLine("		,TB.UPDATE_LOGINID AS DETAIL_UPDATE_LOGINID");
            sb.AppendLine("		,TB.UPDATE_FACILITYID AS DETAIL_UPDATE_FACILITYID");
            sb.AppendLine("		,TB.UPDATE_TIMESTAMP AS DETAIL_UPDATE_TIMESTAMP");
            sb.AppendLine("		,TB.POST_ID AS DETAIL_POST_ID");
            sb.AppendLine("		,TB.LASTUPDATER_NAME AS DETAIL_LASTUPDATER_NAME");
            sb.AppendLine("		,TB.LASTUPDATER_ID AS DETAIL_LASTUPDATER_ID");
            sb.AppendLine("	FROM");
            sb.AppendLine($"	{_tb_grp_groupcategory_tbl} TA");
            sb.AppendLine($"		INNER JOIN {_tb_grp_group_tbl} TB");
            sb.AppendLine("		ON TA.ID = TB.GROUPCATEGORY_ID");
            sb.AppendLine("	WHERE");
            sb.AppendLine("		TA.IS_DELETED = '0'");
            sb.AppendLine("	AND TA.GROUPTANI = '2'");
            sb.AppendLine("	AND TB.IS_DELETED = '0'");

            parameters.Add(":FACILITYID", DbType.Varchar2, facilityId);
            parameters.Add(":FACILITYGROUPID", DbType.Varchar2, facilityGroupId);
            parameters.Add(":AREACORPID", DbType.Varchar2, areaCorpId);
            sb.AppendLine("	AND TA.FACILITY_ID = :FACILITYID");
            sb.AppendLine("	AND TA.FACILITYGROUP_ID = :FACILITYGROUPID");
            sb.AppendLine("	AND TA.AREACORP_ID = :AREACORPID");

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

            if (validFlag == "0")
            {
                sb.AppendLine("	 AND TB.VALID_FLAG = '1'");
            }

            //ORDER
            sb.AppendLine("ORDER BY TA.GROUPCATEGORY_CODE, TB.GROUP_CODE");

            //OFFSET
            parameters.Add(":OFFSET", DbType.Int32, offset);
            parameters.Add(":LIMIT", DbType.Int32, limit);
            sb.AppendLine("OFFSET :OFFSET ROWS FETCH FIRST :LIMIT +1 ROWS ONLY");

            sb.AppendLine(")");

            sb.AppendLine(",MAIN_L1 AS(");
            sb.AppendLine("SELECT");
            sb.AppendLine("	MAIN.*");
            sb.AppendLine("	,TC.ID AS MNG_ID");
            sb.AppendLine("	,TC.LOCKVERSION AS MNG_LOCKVERSION");
            sb.AppendLine("	,TC.AREACORP_ID AS MNG_AREACORP_ID");
            sb.AppendLine("	,TC.FACILITYGROUP_ID AS MNG_FACILITYGROUP_ID");
            sb.AppendLine("	,TC.FACILITY_ID AS MNG_FACILITY_ID");
            sb.AppendLine("	,TC.GROUPMANAGEMENT_CODE AS MNG_GROUPMANAGEMENT_CODE");
            sb.AppendLine("	,TC.GROUP_ID AS MNG_GROUP_ID");
            sb.AppendLine("	,TC.STARTDATE AS MNG_STARTDATE");
            sb.AppendLine("	,TC.ENDDATE AS MNG_ENDDATE");
            sb.AppendLine("	,TC.IS_DELETED AS MNG_IS_DELETED");
            sb.AppendLine("	,TC.UPDATE_ACCOUNTID AS MNG_UPDATE_ACCOUNTID");
            sb.AppendLine("	,TC.UPDATE_LOGINID AS MNG_UPDATE_LOGINID");
            sb.AppendLine("	,TC.UPDATE_FACILITYID AS MNG_UPDATE_FACILITYID");
            sb.AppendLine("	,TC.UPDATE_TIMESTAMP AS MNG_UPDATE_TIMESTAMP");
            sb.AppendLine("	,TC.POST_ID AS MNG_POST_ID");
            sb.AppendLine("	,TC.LASTUPDATER_NAME AS MNG_LASTUPDATER_NAME");
            sb.AppendLine("	,TC.LASTUPDATER_ID AS MNG_LASTUPDATER_ID");

            sb.AppendLine("FROM");
            sb.AppendLine("	MAIN");
            sb.AppendLine($"	INNER JOIN {_tb_grp_groupmanagement_tbl} TC");
            sb.AppendLine("		ON MAIN.DETAIL_ID = TC.GROUP_ID");

            sb.AppendLine("	WHERE");
            sb.AppendLine("	TC.IS_DELETED ='0'");
            sb.AppendLine("	AND TC.STARTDATE =0 ");
            sb.AppendLine("	AND TC.ENDDATE = 99999999");

            sb.AppendLine(")");

            sb.AppendLine(",MAIN_L2 AS(");
            sb.AppendLine("SELECT");
            sb.AppendLine("	MAIN_L1.*");
            sb.AppendLine("	,TD.ID AS PAT_ID");
            sb.AppendLine("	,TD.LOCKVERSION AS PAT_LOCKVERSION");
            sb.AppendLine("	,TD.AREACORP_ID AS PAT_AREACORP_ID");
            sb.AppendLine("	,TD.FACILITYGROUP_ID AS PAT_FACILITYGROUP_ID");
            sb.AppendLine("	,TD.FACILITY_ID AS PAT_FACILITY_ID");
            sb.AppendLine("	,TD.GROUPPATIENT_CODE AS PAT_GROUPPATIENT_CODE");
            sb.AppendLine("	,TD.GROUPMANAGEMENT_ID AS PAT_GROUPMANAGEMENT_ID");
            sb.AppendLine("	,TD.PATIENT_ID AS PAT_PATIENT_ID");
            sb.AppendLine("	,TD.STARTDATE AS PAT_STARTDATE");
            sb.AppendLine("	,TD.ENDDATE AS PAT_ENDDATE");
            sb.AppendLine("	,TD.DISPLAY_ORDER AS PAT_DISPLAY_ORDER");
            sb.AppendLine("	,TD.UPDATE_ACCOUNTID AS PAT_UPDATE_ACCOUNTID");
            sb.AppendLine("	,TD.UPDATE_LOGINID AS PAT_UPDATE_LOGINID");
            sb.AppendLine("	,TD.UPDATE_FACILITYID AS PAT_UPDATE_FACILITYID");
            sb.AppendLine("	,TD.UPDATE_TIMESTAMP AS PAT_UPDATE_TIMESTAMP");
            sb.AppendLine("	,TD.POST_ID AS PAT_POST_ID");
            sb.AppendLine("	,TD.LASTUPDATER_NAME AS PAT_LASTUPDATER_NAME");
            sb.AppendLine("	,TD.LASTUPDATER_ID AS PAT_LASTUPDATER_ID");

            sb.AppendLine("FROM");
            sb.AppendLine("	MAIN_L1");
            sb.AppendLine($"	INNER JOIN {_tb_grp_groupPatient} TD");
            sb.AppendLine("		ON MAIN_L1.MNG_ID = TD.GROUPMANAGEMENT_ID");

            parameters.Add(":KIJUNBI", DbType.Int32, kijunbi);

            if (kijunbi != null && kijunbi != 0)
            {
                sb.AppendLine("	WHERE");
                sb.AppendLine("TD.STARTDATE <= :KIJUNBI");

                if (kijunbiFlag == false)
                {
                    sb.AppendLine("	AND :KIJUNBI <= TD.ENDDATE");
                }

                if (!string.IsNullOrEmpty(patientId))
                {
                    parameters.Add(":PATIENTID", DbType.Varchar2, patientId);
                    sb.AppendLine("	 AND TD.PATIENT_ID = :PATIENTID");
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(patientId))
                {
                    sb.AppendLine("	WHERE");
                    parameters.Add(":PATIENTID", DbType.Varchar2, patientId);
                    sb.AppendLine("	TD.PATIENT_ID = :PATIENTID");
                }
            }

            sb.AppendLine(")");

            sb.AppendLine("SELECT * FROM MAIN_L2");
            sb.AppendLine("ORDER BY MAIN_L2.GROUPCATEGORY_CODE, MAIN_L2.DETAIL_GROUP_CODE, MAIN_L2.PAT_GROUPPATIENT_CODE");

            var accountTrans = _access.SelectSql<GroupPatientResultEntity>(CallerInfo.Create(Shared.ToSharedContext()),
                                                    sb.ToString(),
                                                    parameters);

            return accountTrans;
        }

        /// <summary>
        /// Idに該当するリソースを取得する
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<GroupPatientEntity> GetById(string id)
        {
            // パラメータの設定
            var parameters = new Parameters();
            parameters.Add(":ID", DbType.Varchar2, id);

            // SQLの組み立て
            var sb = new StringBuilder();

            sb.AppendLine("	SELECT");
            sb.AppendLine("	TA.ID");
            sb.AppendLine("	,TA.LOCKVERSION");
            sb.AppendLine("	,TA.AREACORP_ID");
            sb.AppendLine("	,TA.FACILITYGROUP_ID");
            sb.AppendLine("	,TA.FACILITY_ID");
            sb.AppendLine("	,TA.GROUPPATIENT_CODE");
            sb.AppendLine("	,TA.GROUPMANAGEMENT_ID");
            sb.AppendLine("	,TA.PATIENT_ID");
            sb.AppendLine("	,TA.STARTDATE");
            sb.AppendLine("	,TA.ENDDATE");
            sb.AppendLine("	,TA.DISPLAY_ORDER");
            sb.AppendLine("	,TA.UPDATE_ACCOUNTID");
            sb.AppendLine("	,TA.UPDATE_LOGINID");
            sb.AppendLine("	,TA.UPDATE_FACILITYID");
            sb.AppendLine("	,TA.UPDATE_TIMESTAMP");
            sb.AppendLine("	,TA.POST_ID");
            sb.AppendLine("	,TA.LASTUPDATER_NAME");
            sb.AppendLine("	,TA.LASTUPDATER_ID");

            sb.AppendLine("FROM ");
            sb.AppendLine($" {_tb_grp_groupPatient} TA");

            sb.AppendLine("WHERE");
            sb.AppendLine(" TA.ID = :ID");
            sb.AppendLine("ORDER BY TA.GROUPPATIENT_CODE");

            var accountTrans = _access.SelectSql<GroupPatientEntity>(CallerInfo.Create(Shared.ToSharedContext()),
                                                    sb.ToString(),
                                                    parameters);

            return accountTrans;
        }
        /// <summary>
        /// Idに該当するリソースを取得する
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public IList<GroupPatientEntity> GetByIds(string ids)
        {
            // SQLの組み立て
            var sb = new StringBuilder();

            sb.AppendLine("	SELECT");
            sb.AppendLine("	TA.ID");
            sb.AppendLine("	,TA.LOCKVERSION");
            sb.AppendLine("	,TA.AREACORP_ID");
            sb.AppendLine("	,TA.FACILITYGROUP_ID");
            sb.AppendLine("	,TA.FACILITY_ID");
            sb.AppendLine("	,TA.GROUPPATIENT_CODE");
            sb.AppendLine("	,TA.GROUPMANAGEMENT_ID");
            sb.AppendLine("	,TA.PATIENT_ID");
            sb.AppendLine("	,TA.STARTDATE");
            sb.AppendLine("	,TA.ENDDATE");
            sb.AppendLine("	,TA.DISPLAY_ORDER");
            sb.AppendLine("	,TA.UPDATE_ACCOUNTID");
            sb.AppendLine("	,TA.UPDATE_LOGINID");
            sb.AppendLine("	,TA.UPDATE_FACILITYID");
            sb.AppendLine("	,TA.UPDATE_TIMESTAMP");
            sb.AppendLine("	,TA.POST_ID");
            sb.AppendLine("	,TA.LASTUPDATER_NAME");
            sb.AppendLine("	,TA.LASTUPDATER_ID");

            sb.AppendLine("FROM ");
            sb.AppendLine($" {_tb_grp_groupPatient} TA");

            sb.AppendLine("WHERE");
            sb.AppendLine($" TA.ID IN ({ids})");
            sb.AppendLine("ORDER BY TA.GROUPPATIENT_CODE");

            var accountTrans = _access.SelectSql<GroupPatientEntity>(CallerInfo.Create(Shared.ToSharedContext()),
                                                    sb.ToString());

            return accountTrans;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupPatientCode"></param>
        /// <returns></returns>
        public IList<GroupPatientEntity> GetByGroupPatientCode(string groupPatientCode, string patientID)
        {
            // パラメータの設定
            var parameters = new Parameters();
            parameters.Add(":GROUPPATIENTCODE", DbType.Varchar2, groupPatientCode);
            parameters.Add(":PATIENTID", DbType.Varchar2, patientID);

            // SQLの組み立て
            var sb = new StringBuilder();

            sb.AppendLine("	SELECT");
            sb.AppendLine("	TA.ID");
            sb.AppendLine("	,TA.LOCKVERSION");
            sb.AppendLine("	,TA.AREACORP_ID");
            sb.AppendLine("	,TA.FACILITYGROUP_ID");
            sb.AppendLine("	,TA.FACILITY_ID");
            sb.AppendLine("	,TA.GROUPPATIENT_CODE");
            sb.AppendLine("	,TA.GROUPMANAGEMENT_ID");
            sb.AppendLine("	,TA.PATIENT_ID");
            sb.AppendLine("	,TA.STARTDATE");
            sb.AppendLine("	,TA.ENDDATE");
            sb.AppendLine("	,TA.DISPLAY_ORDER");
            sb.AppendLine("	,TA.UPDATE_ACCOUNTID");
            sb.AppendLine("	,TA.UPDATE_LOGINID");
            sb.AppendLine("	,TA.UPDATE_FACILITYID");
            sb.AppendLine("	,TA.UPDATE_TIMESTAMP");
            sb.AppendLine("	,TA.POST_ID");
            sb.AppendLine("	,TA.LASTUPDATER_NAME");
            sb.AppendLine("	,TA.LASTUPDATER_ID");

            sb.AppendLine("FROM ");
            sb.AppendLine($" {_tb_grp_groupPatient} TA");

            sb.AppendLine("WHERE");
            sb.AppendLine(" TA.GROUPPATIENT_CODE = :GROUPPATIENTCODE ");
            sb.AppendLine(" AND TA.PATIENT_ID = :PATIENTID ");
            sb.AppendLine("ORDER BY TA.GROUPPATIENT_CODE");

            var accountTrans = _access.SelectSql<GroupPatientEntity>(CallerInfo.Create(Shared.ToSharedContext()),
                                                    sb.ToString(),
                                                    parameters);

            return accountTrans;
        }

        /// <summary>
        /// 投稿IDによるグループ患者リストの取得
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public IList<GroupPatientEntity> GetByPostId(string postId = "")
        {
            // パラメータの設定
            var parameters = new Parameters();

            // SQLの組み立て
            var sb = new StringBuilder();

            sb.AppendLine("SELECT");
            sb.AppendLine(" TA.*");
            sb.AppendLine("FROM ");
            sb.AppendLine($" {_tb_grp_groupPatient} TA");
            sb.AppendLine("WHERE");

            parameters.Add(":POSTID", DbType.Varchar2, postId);
            sb.AppendLine("TA.POST_ID = :POSTID");
            sb.AppendLine("ORDER BY TA.GROUPPATIENT_CODE");

            var accountTrans = _access.SelectSql<GroupPatientEntity>(CallerInfo.Create(Shared.ToSharedContext()),
                                                    sb.ToString(),
                                                    parameters);

            return accountTrans;
        }

        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public Result<GroupPatientEntity> Create(GroupPatientEntity content)
        {
            // データ更新
            return _access.InsertReturning(CallerInfo.Create(Shared.ToSharedContext()), content, _tb_grp_groupPatient);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public Result<GroupPatientEntity> Update(GroupPatientEntity content)
        {
            // データ更新
            return _access.UpdateReturning(CallerInfo.Create(Shared.ToSharedContext()), content, _tb_grp_groupPatient);
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

            return _access.Delete(CallerInfo.Create(Shared.ToSharedContext()), _tb_grp_groupPatient, ":ID = ID", parameters) > 0;
        }

        /// <summary>
        /// 未登録グループ一覧取得
        /// </summary>
        /// <param name="searchWord"></param>
        /// <param name="searchFlag"></param>
        /// <param name="areaCorpId"></param>
        /// <param name="facilityGroupId"></param>
        /// <param name="facilityId"></param>
        /// <param name="kijunbi"></param>
        /// <param name="patientId"></param>
        /// <param name="postId"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public IList<GroupCategoryGroupEntity> GetUnregistGroup(string searchWord = "",
                                                      bool searchFlag = false,
                                                      string areaCorpId = "",
                                                      string facilityGroupId = "",
                                                      string facilityId = "",
                                                      int? kijunbi = 0,
                                                      string patientId = "",
                                                      string postId = "",
                                                      int? limit = 1000,
                                                      int? offset = 0)
        {
            //// パラメータの設定
            var parameters = new Parameters();

            //// SQLの組み立て
            var sb = new StringBuilder();

            sb.AppendLine("SELECT");
            sb.AppendLine("	TA.ID");
            sb.AppendLine(" ,TA.LOCKVERSION");
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
            sb.AppendLine($"	{_tb_grp_groupcategory_tbl} TA");
            sb.AppendLine($"	INNER JOIN {_tb_grp_group_tbl} TB");
            sb.AppendLine("	ON TA.ID = TB.GROUPCATEGORY_ID");
            sb.AppendLine("WHERE");
            sb.AppendLine("TA.IS_DELETED = '0'");

            if (string.IsNullOrEmpty(postId))
            {

                parameters.Add(":FACILITYID", DbType.Varchar2, facilityId);
                parameters.Add(":FACILITYGROUPID", DbType.Varchar2, facilityGroupId);
                parameters.Add(":AREACORPID", DbType.Varchar2, areaCorpId);
                sb.AppendLine("AND TA.GROUPTANI = '2'");
                sb.AppendLine("AND TA.FACILITY_ID = :FACILITYID");
                sb.AppendLine("AND TA.FACILITYGROUP_ID = :FACILITYGROUPID");
                sb.AppendLine("AND TA.AREACORP_ID = :AREACORPID");

                if (!string.IsNullOrEmpty(searchWord))
                {
                    parameters.Add(":SEARCHWORD", DbType.Varchar2, searchWord);
                    if (searchFlag)
                    {
                        sb.AppendLine("AND (TA.GROUPCATEGORY_NAME LIKE '%' || :SEARCHWORD || '%'");
                        sb.AppendLine("    OR TB.GROUP_NAME LIKE '%' || :SEARCHWORD || '%'  )");
                    }
                    else
                    {
                        sb.AppendLine("AND (TA.GROUPCATEGORY_NAME LIKE :SEARCHWORD || '%'");
                        sb.AppendLine("		OR TB.GROUP_NAME LIKE :SEARCHWORD || '%'  )");
                    }
                }

                sb.AppendLine("AND TB.ID NOT IN(");
                sb.AppendLine("	SELECT");
                sb.AppendLine("		TC.GROUP_ID ");
                sb.AppendLine("	FROM");
                sb.AppendLine($"{_tb_grp_groupmanagement_tbl} TC ");
                sb.AppendLine($"LEFT JOIN {_tb_grp_groupPatient} TD ");
                sb.AppendLine("		ON TD.GROUPMANAGEMENT_ID = TC.ID ");
                sb.AppendLine("	WHERE");
                sb.AppendLine("		TC.IS_DELETED = '0'");

                if (kijunbi > 0)
                {
                    parameters.Add(":KIJUNBI", DbType.Int32, kijunbi);
                    sb.AppendLine("	AND TD.STARTDATE <= :KIJUNBI");
                    sb.AppendLine(" AND :KIJUNBI <= TD.ENDDATE");
                }

                if (!string.IsNullOrEmpty(patientId))
                {
                    parameters.Add(":PATIENTID", DbType.Varchar2, patientId);
                    sb.AppendLine(" AND TD.PATIENT_ID = :PATIENTID");
                }
                sb.AppendLine("	)");
            }
            else
            {
                parameters.Add(":POSTID", DbType.Int32, postId);
                sb.AppendLine("	TA.POST_ID = :POSTID");
            }

            parameters.Add(":OFFSET", DbType.Int32, offset);
            parameters.Add(":LIMIT", DbType.Int32, limit);
            sb.AppendLine("ORDER BY TA.DISPLAY_ORDER, TB.DISPLAY_ORDER");
            sb.AppendLine("OFFSET :OFFSET ROWS FETCH FIRST :LIMIT+1 ROWS ONLY");

            var accountTrans = _access.SelectSql<GroupCategoryGroupEntity>(CallerInfo.Create(Shared.ToSharedContext()),
                                                    sb.ToString(),
                                                    parameters);

            return accountTrans;
        }

        /// <summary>
        /// グループ管理IDの取得
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public string GetGroupManagementForCreateGroupPatient(string groupId)
        {
            var result = "";

            // パラメータの設定
            var parameters = new Parameters();
            parameters.Add(":GROUPID", DbType.Varchar2, groupId);

            // SQLの組み立て
            var sb = new StringBuilder();

            sb.AppendLine("SELECT");
            sb.AppendLine(" TA.ID");

            sb.AppendLine("FROM ");
            sb.AppendLine($" {_tb_grp_groupmanagement_tbl} TA");

            sb.AppendLine("WHERE");

            sb.AppendLine("TA.IS_DELETED ='0'");
            sb.AppendLine("AND TA.STARTDATE =0 ");
            sb.AppendLine("AND TA.ENDDATE = 99999999");
            sb.AppendLine("AND TA.GROUP_ID = :GROUPID");

            var accountTrans = _access.SelectSql<GroupPatientEntity>(CallerInfo.Create(Shared.ToSharedContext()),
                                                    sb.ToString(),
                                                    parameters);

            if (accountTrans != null && accountTrans.Count > 0)
            {
                result = accountTrans[0].Id;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="facilityid"></param>
        /// <param name="facilitygroupid"></param>
        /// <returns></returns>
        public string ReadNewCodeGroupPatient(string facilityid, string facilitygroupid, string patientId)
        {
            // パラメータの設定
            var parameters = new Parameters();
            {
                parameters.Add(":FACILITYID", DbType.Varchar2, facilityid);
                parameters.Add(":FACILITYGROUPID", DbType.Varchar2, facilitygroupid);
                parameters.Add(":PATIENTID", DbType.Varchar2, patientId);
            }

            // SQLの組み立て
            var sb = new StringBuilder();
            sb.AppendLine("SELECT");
            sb.AppendLine("	MAX(TA.GROUPPATIENT_CODE)+1 AS GROUPPATIENT_CODE");
            sb.AppendLine("FROM");
            sb.AppendLine($" {_tb_grp_groupPatient} TA");
            sb.AppendLine("WHERE");
            sb.AppendLine("	TA.FACILITY_ID = :FACILITYID");
            sb.AppendLine(" AND TA.FACILITYGROUP_ID = :FACILITYGROUPID");
            sb.AppendLine(" AND TA.PATIENT_ID = :PATIENTID");

            var results = _access.SelectSql<GroupPatientEntity>(CallerInfo.Create(Shared.ToSharedContext()), sb.ToString(), parameters);

            var result = "0001";
            if (results?.Count > 0 && results[0].GROUPPATIENT_CODE != null)
            {
                result = results[0]?.GROUPPATIENT_CODE.PadLeft(4, '0');
            }

            // 実行
            return result;
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
