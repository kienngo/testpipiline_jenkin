using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiseman.PJC.Gen2.RDB.Entities;
using Wiseman.PJC.Service.GroupSettings.RDB.Entities;

namespace Wiseman.PJC.Service.GroupSettings.RDB.Interfaces
{
    public interface IGroupManagementAccess : IDisposable
    {
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
        IList<GroupManagementListDetailEntity> Get(string groupCategoryCode = "",
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
                                                                int? offset = 0);

        /// <summary>
        /// グループ管理1件取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IList<GroupManagementDetailSettingsEntity> GetById(string id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="facilityid">医療機関・施設ID</param>
        string ReadNewCodeGroupManagement(string facilityid, string facilitygroupid);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupManagementCode"></param>
        /// <param name="facilityid"></param>
        /// <param name="facilitygroupid"></param>
        /// <returns></returns>
        GroupManagementEntity ReadByCodeGroupManagement(string groupManagementCode, string facilityid, string facilitygroupid);

        /// <summary>
        /// グループ管理登録
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        Result<GroupManagementEntity> Create(GroupManagementEntity content);

        /// <summary>
        /// グループ管理更新
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        Result<GroupManagementEntity> Update(GroupManagementEntity content);

        /// <summary>
        /// グループ管理削除
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        Result<GroupManagementDeleteEntity> UpdateForDelete(GroupManagementDeleteEntity content);
    }
}
