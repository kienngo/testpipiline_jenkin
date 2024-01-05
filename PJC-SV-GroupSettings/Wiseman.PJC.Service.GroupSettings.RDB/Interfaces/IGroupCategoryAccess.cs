using System;
using System.Collections.Generic;
using Wiseman.PJC.Gen2.RDB.Entities;
using Wiseman.PJC.Service.GroupSettings.RDB.Entities;

namespace Wiseman.PJC.Service.GroupSettings.RDB.Interfaces
{
    public interface IGroupCategoryAccess : IDisposable
    {
        /// <summary>
        /// ID指定取得
        /// </summary>
        /// <param name="id">サロゲートキー</param>
        /// <returns></returns>
        IList<GroupCategoryResultEntity> GetGroupCategory(string groupTani = "",
                                                    string groupCategoryCode = "",
                                                    string searchWord = "",
                                                    bool searchFlag = false,
                                                    string areaCorpId = "",
                                                    string facilityGroupId = "",
                                                    string facilityId = "",
                                                    string postId = "",
                                                    string categoryCode = "",
                                                    short? limit = 1000,
                                                    short? offset = 0);

        /// <summary>
        /// 投稿IDによるグループカテゴリリストの取得
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        IList<GroupCategoryResultEntity> GetGroupCategoryByPostId(string postId);

        /// <summary>
        /// Idに該当するリソースを取得する
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        IList<GroupCategoryResultEntity> GetGroupCategoryById(string id);

        /// <summary>
        /// レコードを新規作成する
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        Result<GroupCategoryEntity> CreateGroupCategory(GroupCategoryEntity content);

        /// <summary>
        /// 記録を更新
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        Result<GroupCategoryEntity> UpdateGroupCategory(GroupCategoryEntity content);

        /// <summary>
        /// グループカテゴリ(ID指定)を1つ取得しました
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IList<GroupCategoryResultEntity> ReadListGroupCategoryById(string id = "");

        /// <summary>
        /// 指定されたIDを除く、Codeに該当するレコードの存在を確認する
        /// </summary>
        /// <param name="facilityid"></param>
        /// <param name="facilitygroupid"></param>
        /// <param name="groupcategorycode"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DuplicateCheckGroupCategory(string facilityid, string facilitygroupid, string groupcategorycode, string id, string areacorpid);

        /// <summary>
        /// 自動採番はグループカテゴリコードの最大値です
        /// </summary>
        /// <param name="facilityid">医療機関・施設ID</param>
        string ReadNewCodeGroupCategory(string facilityid);

        /// <summary>
        /// Codeに該当するレコードを取得する
        /// </summary>
        /// <param name="facilityid"></param>
        /// <param name="groupcategorycode"></param>
        /// <returns></returns>
        GroupCategoryResultEntity ReadByCodeGroupCategory(string facilityid, string groupcategorycode, string facilitygroupid, string areacorpid);

        /// <summary>
        /// 削除のための更新
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        Result<GroupCategoryDeleteEntity> UpdateForDelete(GroupCategoryDeleteEntity content);
    }
}
