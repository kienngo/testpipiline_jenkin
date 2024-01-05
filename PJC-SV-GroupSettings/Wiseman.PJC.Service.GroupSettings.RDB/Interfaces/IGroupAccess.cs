using System;
using System.Collections.Generic;
using Wiseman.PJC.Gen2.RDB.Entities;
using Wiseman.PJC.Service.GroupSettings.RDB.Entities;

namespace Wiseman.PJC.Service.GroupSettings.RDB.Interfaces
{
    public interface IGroupAccess : IDisposable
    {
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
        IList<GroupCategoryGroupEntity> GetGroupAsync(string searchString = "",
                                                       bool searchFlag = false,
                                                       string groupCategoryCode = "",
                                                       string groupTani = "",
                                                       string groupCode = "",
                                                       string postId = "",
                                                       string validFlag = "",
                                                       int? kijunbi = 0,
                                                       bool kijunbiFlag = false,
                                                       int? limit = null,
                                                       int? offset = null);
        /// <summary>
        /// IDグループ一覧取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IList<GroupCategoryGroupEntity> GetGroupByIdAsync(string id);

       /// <summary>
       /// 
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
        string ReadGroupCodeById(string id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        Result<GroupEntity> Update(GroupEntity content);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        Result<GroupEntity> Create(GroupEntity content);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        Result<GroupDeleteEntity> UpdateForDelete(GroupDeleteEntity content);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="facilityId"></param>
        /// <param name="shijiboSetCode"></param>
        /// <returns></returns>
        bool Exists(string id, string facilityId, string groupCode, string groupcategoryId);

        /// <summary>
        /// 自動採番はグループカテゴリコードの最大値です
        /// </summary>
        /// <param name="groupcategory">医療機関・施設ID</param>
        string ReadNewCodeGroup(string groupcategory);

        /// <summary>
        /// Codeに該当するレコードを取得する
        /// </summary>
        /// <param name="groupcode"></param>
        /// <param name="facilityid"></param>
        /// <param name="facilitygroupid"></param>
        /// <returns></returns>
        GroupReadByCodeEntity ReadByCodeGroup( string groupcode, string facilityid, string facilitygroupid, string areacorpid );

    }
}
