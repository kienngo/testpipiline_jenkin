using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiseman.PJC.Gen2.RDB.Entities;
using Wiseman.PJC.Service.GroupSettings.RDB.Entities;

namespace Wiseman.PJC.Service.GroupSettings.RDB.Interfaces
{
    public interface IGroupPatientAccess : IDisposable
    {

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
        IList<GroupPatientResultEntity> Get(string groupCategoryCode = "",
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
                                                    int? offset = 0);

        /// <summary>
        /// Idに該当するリソースを取得する
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IList<GroupPatientEntity> GetById(string id);

        /// <summary>
        /// Idに該当するリソースを取得する
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        IList<GroupPatientEntity> GetByIds(string ids);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupPatientCode"></param>
        /// <param name="patientId"></param>
        /// <returns></returns>
        IList<GroupPatientEntity> GetByGroupPatientCode(string groupPatientCode, string patientId);

        /// <summary>
        /// 投稿IDによるグループ患者リストの取得
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        IList<GroupPatientEntity> GetByPostId(string postId = "");

        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        Result<GroupPatientEntity> Create(GroupPatientEntity content);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        Result<GroupPatientEntity> Update(GroupPatientEntity content);

        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(string id);

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
        IList<GroupCategoryGroupEntity> GetUnregistGroup(string searchWord = "",
                                                        bool searchFlag = false,
                                                        string areaCorpId = "",
                                                        string facilityGroupId = "",
                                                        string facilityId = "",
                                                        int? kijunbi = 0,
                                                        string patientId = "",
                                                        string postId = "",
                                                        int? limit = 1000,
                                                        int? offset = 0);       
        /// グループ管理IDの取得
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        string GetGroupManagementForCreateGroupPatient(string groupId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="facilityId"></param>
        /// <param name="facilitygroupId"></param>
        /// <param name="patientId"></param>
        /// <returns></returns>
        string ReadNewCodeGroupPatient(string facilityId, string facilitygroupId, string patientId);
    }
}
