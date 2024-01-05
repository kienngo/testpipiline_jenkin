using Wiseman.PJC.Gen2.RDB;
using Wiseman.PJC.Gen2.RDB.Interfaces;
using Wiseman.PJC.Service.GroupSettings.Http.Message.ResponseContents;
using Wiseman.PJC.Service.GroupSettings.WebApi.Entities;
using Wiseman.PJC.Service.GroupSettings.Http.Message.Enums;
using Wiseman.PJC.Service.GroupSettings.RDB.Entities;
using Wiseman.PJC.Service.GroupSettings.WebApi.Interfaces;
using Wiseman.PJC.Gen2.ObjectModel;
using Wiseman.PJC.Gen2.Setting.Server;
using Wiseman.PJC.Service.GroupSettings.RDB.Interfaces;
using Wiseman.PJC.Service.GroupSettings.Http.Message.RequestContents;
using Wiseman.PJC.Gen2.Http.Message.Responses;
using Wiseman.PJC.Service.GroupSettings.WebApi.Properties;
using Wiseman.PJC.Gen2.Http.Message.Enums;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Wiseman.PJC.GroupSettings.RDB;
using Wiseman.PJC.Service.GroupSettings.RDB;
using Wiseman.PJC.Gen2.Utility;
using Wiseman.PJC.Service.GroupSettings.WebApi.Extension;

namespace Wiseman.PJC.Service.GroupSettings.WebApi.Logics
{
    public class GroupSettingsLogic : IGroupSettingsLogic
    {
        private IDBAccessFactory _dbAccessFactory;
        // ジャーナル
        private IGroupSettingsFactory _GroupSettingsFactory;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public GroupSettingsLogic() : this(new GroupSettingsFactory(), new DBAccessFactory())
        {

        }

        /// <summary>
        /// コンストラクタ（テスト用）
        /// </summary>
        /// <param name="shijiboSetMaserFactory"></param>
        /// <param name="dbAccessFactory"></param>
        public GroupSettingsLogic(GroupSettingsFactory shijiboSetMaserFactory, IDBAccessFactory dbAccessFactory)
        {
            this._dbAccessFactory = dbAccessFactory;
            this._GroupSettingsFactory = shijiboSetMaserFactory;
        }

        #region GroupCategory

        /// <summary>
        /// グループカテゴリリストの取得
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
        public Result<List<GroupCategoryResponseContent>> GetGroupCategory(string groupTani = "",
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
            var results = new Result<List<GroupCategoryResponseContent>>();

            using var accessor = _dbAccessFactory.Create();

            accessor.CreateConnection(CallerInfo.Create(Shared.ToSharedContext()));

            using var groupCategoryAccess = _GroupSettingsFactory.CreateGroupCategoryAccess(accessor);

            var returnValues = groupCategoryAccess.GetGroupCategory(groupTani, groupCategoryCode, searchWord, searchFlag, areaCorpId, facilityGroupId, facilityId, postId, categoryCode, limit, offset);

            if (returnValues.Count == 0)
            {
                results.State = State.NotFound;

                accessor.DisposeConnection();

                return results;
            }

            results.Content = ReplaceGroupCategoryEntity.CreateList(returnValues);

            results.State = State.Success;

            accessor.DisposeConnection();

            return results;
        }

        /// <summary>
        /// 投稿IDによるグループカテゴリリストの取得
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public Result<List<GroupCategoryResponseContent>> GetGroupCategoryByPostId(string postId)
        {
            var results = new Result<List<GroupCategoryResponseContent>>();

            using var accessor = _dbAccessFactory.Create();

            accessor.CreateConnection(CallerInfo.Create(Shared.ToSharedContext()));

            using var groupCategoryAccess = _GroupSettingsFactory.CreateGroupCategoryAccess(accessor);

            var returnValues = groupCategoryAccess.GetGroupCategoryByPostId(postId);

            if (returnValues.Count == 0)
            {
                results.State = State.NotFound;

                accessor.DisposeConnection();

                return results;
            }

            results.Content = ReplaceGroupCategoryEntity.CreateList(returnValues);

            results.State = State.Success;

            accessor.DisposeConnection();

            return results;
        }

        /// <summary>
        /// Idに該当するリソースを取得する
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Result<List<GroupCategoryResponseContent>> GetGroupCategoryById(string id = "")
        {
            var results = new Result<List<GroupCategoryResponseContent>>();

            using var accessor = _dbAccessFactory.Create();

            accessor.CreateConnection(CallerInfo.Create(Shared.ToSharedContext()));

            // データ取得
            using var groupCategoryAccess = _GroupSettingsFactory.CreateGroupCategoryAccess(accessor);

            var returnValues = groupCategoryAccess.GetGroupCategoryById(id: id);

            results.Content = ReplaceGroupCategoryEntity.CreateList(returnValues);

            accessor.DisposeConnection();

            return results;
        }

        /// <summary>
        /// グループ分類登録
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public Result<GroupCategoryResponseContent> CreateGroupCategory(GroupCategoryRequestContent content)
        {
            var result = new Result<GroupCategoryResponseContent>();

            using var accessor = _dbAccessFactory.Create();
            accessor.CreateConnectionWithTransaction(CallerInfo.Create(Shared.ToSharedContext()));
            using var groupCategoryAccess = _GroupSettingsFactory.CreateGroupCategoryAccess(accessor);
            using var categorySelectedAccess = _GroupSettingsFactory.CreateCategorySelectedAccess(accessor);
            using var categorySelectedJnlAccess = _GroupSettingsFactory.CreateCategorySelectedJnlAccess(accessor);

            var returnValue = new EntryGroupCategory().Post(accessor, _GroupSettingsFactory, content);

            if (returnValue.Count == 0)
            {
                accessor.DisposeConnection();
                result.State = State.Conflict;
                return result;
            }

            if (content.CategorySelectedRequestContent.Count > 0)
            {
                foreach (var item in content.CategorySelectedRequestContent)
                {
                    item.GroupcategoryId = returnValue.Entity.Id;

                    var returnCategorySelectedValue = new EntryCategorySelected().Post(categorySelectedAccess, categorySelectedJnlAccess, item);

                    if (returnCategorySelectedValue.Count == 0)
                    {
                        accessor.DisposeConnection();
                        result.State = State.Conflict;
                        return result;
                    }
                }
            }

            // コミット
            accessor.CommitTrans();
            accessor.DisposeConnection();

            result.State = State.Success;
            result.Content = GetGroupCategoryById(returnValue.Entity.Id)?.Content?[0];
            return result;
        }

        /// <summary>
        /// 重症度・看護必要度評価マスター登録(ヘッダのみ復活更新)
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public Result<GroupCategoryResponseContent> CreateAndHeaderUpdateGroupCategory(GroupCategoryRequestContent content)
        {
            var result = new Result<GroupCategoryResponseContent>();

            using var accessor = _dbAccessFactory.Create();
            accessor.CreateConnectionWithTransaction(CallerInfo.Create(Shared.ToSharedContext()));
            using var KangoHitsuyodoHyokaAccess = _GroupSettingsFactory.CreateGroupCategoryAccess(accessor);
            using var categorySelectedAccess = _GroupSettingsFactory.CreateCategorySelectedAccess(accessor);
            using var categorySelectedJnlAccess = _GroupSettingsFactory.CreateCategorySelectedJnlAccess(accessor);

            // 重症度・看護必要度評価マスター
            var returnValue = new EntryGroupCategory().Put(accessor, _GroupSettingsFactory, content);

            if (returnValue.Count == 0)
            {
                accessor.DisposeConnection();
                result.State = State.Conflict;
                return result;
            }

            if (content.CategorySelectedRequestContent.Count > 0)
            {
                foreach (var item in content.CategorySelectedRequestContent)
                //foreach (var item in content.CategorySelectedRequestContent.Select(x => x.ActionFlag == ActionFlag.Insert).ToList())
                {
                    item.GroupcategoryId = returnValue.Entity.Id;

                    var returnCategorySelectedValue = new EntryCategorySelected().Post(categorySelectedAccess, categorySelectedJnlAccess, item);

                    if (returnCategorySelectedValue.Count == 0)
                    {
                        accessor.DisposeConnection();
                        result.State = State.Conflict;
                        return result;
                    }
                }
            }

            // コミット
            accessor.CommitTrans();
            accessor.DisposeConnection();

            result.State = State.Success;
            result.Content = GetGroupCategoryById(returnValue.Entity.Id)?.Content?[0];
            return result;
        }

        /// <summary>
        /// グループ分類更新
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public Result<GroupCategoryResponseContent> UpdateGroupCategory(GroupCategoryRequestContent content)
        {
            var result = new Result<GroupCategoryResponseContent>();
            result.Content = new GroupCategoryResponseContent();

            using var accessor = _dbAccessFactory.Create();
            accessor.CreateConnectionWithTransaction(CallerInfo.Create(Shared.ToSharedContext()));
            using var groupCategoryAccess = _GroupSettingsFactory.CreateGroupCategoryAccess(accessor);
            using var categorySelectedAccess = _GroupSettingsFactory.CreateCategorySelectedAccess(accessor);
            using var categorySelectedJnlAccess = _GroupSettingsFactory.CreateCategorySelectedJnlAccess(accessor);

            //更新対象データ取得
            var beforedata = ReadListGroupCategoryById(content.Id);

            if (beforedata.Content == null || beforedata.Content?.Count == 0)
            {
                result.State = State.NotFound;
                accessor.DisposeConnection();
                return result;
            }

            // コードの重複チェック 
            if (groupCategoryAccess.DuplicateCheckGroupCategory(content.FacilityId, content.FacilityGroupId, content.GroupCategoryCode, content.Id, content.AreaCorpId))
            {
                result.State = State.CodeUsed;
                accessor.DisposeConnection();
                return result;
            }

            content.LockVersion = beforedata?.Content?[0].LockVersion;
            var returnValue = new EntryGroupCategory().Put(accessor, _GroupSettingsFactory, content);

            if (returnValue.Count == 0)
            {
                // 楽観排他エラー更新後確認
                result.State = State.Conflict;
                accessor.DisposeConnection();
                return result;
            }

            if (returnValue.Count > 0)
            {
                if (content.CategorySelectedRequestContent.Count > 0)
                {
                    foreach (var item in content.CategorySelectedRequestContent)
                    {
                        item.GroupcategoryId = returnValue.Entity.Id;
                        if (item.ActionFlag == ActionFlag.Insert)
                        {
                            var returnCategorySelectedValue = new EntryCategorySelected().Post(categorySelectedAccess, categorySelectedJnlAccess, item);

                            if (returnCategorySelectedValue.Count == 0)
                            {
                                accessor.DisposeConnection();
                                result.State = State.Conflict;
                                return result;
                            }
                        }
                        else if (item.ActionFlag == ActionFlag.Delete)
                        {
                            //var itemDel = ReadListCategorySelectedByGroupCategoryIdAndCategoryId(item.GroupcategoryId, item.CategoryId);
                            var itemDel = beforedata?.Content?[0].CategorySelectedResponseContent.FirstOrDefault(x => x.GroupcategoryId == item.GroupcategoryId && x.CategoryId == item.CategoryId);

                            if (itemDel == null)
                            {
                                accessor.DisposeConnection();
                                result.State = State.NotFound;
                                return result;
                            }

                            var returnCategorySelectedValue = new EntryCategorySelected().Delete(categorySelectedAccess, categorySelectedJnlAccess, itemDel);

                            if (!returnCategorySelectedValue)
                            {
                                accessor.DisposeConnection();
                                result.State = State.Conflict;
                                return result;
                            }
                        }
                    }
                }
            }

            // コミット
            accessor.CommitTrans();
            accessor.DisposeConnection();

            result.State = State.Success;
            result.Content = GetGroupCategoryById(returnValue.Entity.Id)?.Content?[0];
            return result;

        }

        /// <summary>
        /// 自動採番はグループカテゴリコードの最大値です
        /// </summary>
        /// <param name="facilityid">医療機関・施設ID</param>
        /// <returns></returns>
        public string ReadNewCodeGroupCategory(String facilityid)
        {
            using var accessor = _dbAccessFactory.Create();
            accessor.CreateConnection(CallerInfo.Create(Shared.ToSharedContext()));
            // データ取得
            using var groupCategoryAccess = _GroupSettingsFactory.CreateGroupCategoryAccess(accessor);
            var code = groupCategoryAccess.ReadNewCodeGroupCategory(facilityid);
            accessor.DisposeConnection();
            return code;
        }

        /// <summary>
        /// 重症度・看護必要度評価マスターコード取得
        /// </summary>
        /// <returns></returns>
        public Result<GroupCategoryResultEntity> ReadByCodeGroupCategory(string facilityid, string groupcategorycode, string facilitygroupid, string areacorpid)
        {
            var result = new Result<GroupCategoryResultEntity>();

            using var accessor = _dbAccessFactory.Create();
            accessor.CreateConnection(CallerInfo.Create(Shared.ToSharedContext()));
            // データ取得
            using var groupCategoryAccess = _GroupSettingsFactory.CreateGroupCategoryAccess(accessor);
            result.Content = groupCategoryAccess.ReadByCodeGroupCategory(facilityid, groupcategorycode, facilitygroupid, areacorpid);

            accessor.DisposeConnection();

            return result;

        }

        /// <summary>
        /// グループカテゴリ(ID指定)を1つ取得しました
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Result<List<GroupCategoryResponseContent>> ReadListGroupCategoryById(string id = "")
        {
            var results = new Result<List<GroupCategoryResponseContent>>();

            using var accessor = _dbAccessFactory.Create();

            accessor.CreateConnection(CallerInfo.Create(Shared.ToSharedContext()));

            // データ取得
            using var groupCategoryAccess = _GroupSettingsFactory.CreateGroupCategoryAccess(accessor);

            var returnValues = groupCategoryAccess.ReadListGroupCategoryById(id: id);

            results.Content = ReplaceGroupCategoryEntity.CreateList(returnValues);

            accessor.DisposeConnection();

            return results;
        }

        /// <summary>
        /// Delete category group
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Result<CountResponseContent> DeleteGroupCategoryAsync(string id)
        {
            var result = new Result<CountResponseContent>();

            using var accessor = _dbAccessFactory.Create();
            accessor.CreateConnectionWithTransaction(CallerInfo.Create(Shared.ToSharedContext()));

            var dellist = GetGroupCategoryById(id);

            if ((dellist == null) || (dellist.State == State.NotFound) || (dellist.Content == null))
            {
                result.State = State.NotFound;
                accessor.DisposeConnection();
                return result;
            }

            var entry = new EntryGroupCategory();
            using var categorySelectedAccess = _GroupSettingsFactory.CreateCategorySelectedAccess(accessor);
            using var categorySelectedJnlAccess = _GroupSettingsFactory.CreateCategorySelectedJnlAccess(accessor);

            var count = 0;
            var returnValue = entry.Delete(accessor, _GroupSettingsFactory, dellist.Content[0]);
            if (returnValue.Count > 0)
            {
                count += 1;
                foreach (var item in dellist.Content[0].CategorySelectedResponseContent)
                {
                    if (new EntryCategorySelected().Delete(categorySelectedAccess, categorySelectedJnlAccess, item))
                    {
                        count += 1;
                    }
                }
            }

            result.State = State.Success;
            result.Content = new CountResponseContent()
            {
                Count = count
            };

            // コミット
            accessor.CommitTrans();
            accessor.DisposeConnection();

            return result;

        }
        #endregion

        #region CategorySelected
        /// <summary>
        /// グループカテゴリ(ID指定)を1つ取得しました
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Result<List<CategorySelectedResponseContent>> ReadListCategorySelectedById(string id = "")
        {
            var results = new Result<List<CategorySelectedResponseContent>>();

            using var accessor = _dbAccessFactory.Create();

            accessor.CreateConnection(CallerInfo.Create(Shared.ToSharedContext()));

            // データ取得
            using var groupCategoryAccess = _GroupSettingsFactory.CreateCategorySelectedAccess(accessor);

            var returnValues = groupCategoryAccess.GetById(id: id);

            results.Content = ReplaceCategorySelectedEntity.CreateList(returnValues);

            accessor.DisposeConnection();

            return results;
        }
        #endregion

        #region Group
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
        public Result<List<GroupCategoryGroupRequestContent>> GetGroupAsync(string searchString = "",
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
            var results = new Result<List<GroupCategoryGroupRequestContent>>();

            using var accessor = _dbAccessFactory.Create();

            accessor.CreateConnection(CallerInfo.Create(Shared.ToSharedContext()));

            using var categoryAccess = _GroupSettingsFactory.CreateGroupAccess(accessor);

            var returnValues = categoryAccess.GetGroupAsync(searchString, searchFlag, groupCategoryCode, groupTani, groupCode, postId, validFlag, kijunbi, kijunbiFlag, limit, offset);

            if (returnValues.Count == 0)
            {
                results.State = State.NotFound;

                accessor.DisposeConnection();

                return results;
            }

            results.Content = ReplaceGroupEntity.CreateList(returnValues);

            results.State = State.Success;

            accessor.DisposeConnection();

            return results;
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
        public Result<List<GroupCategoryGroupRequestContent>> GetGroupByIdAsync(string id)
        {
            var results = new Result<List<GroupCategoryGroupRequestContent>>();

            using var accessor = _dbAccessFactory.Create();

            accessor.CreateConnection(CallerInfo.Create(Shared.ToSharedContext()));

            using var categoryAccess = _GroupSettingsFactory.CreateGroupAccess(accessor);

            var returnValues = categoryAccess.GetGroupByIdAsync(id);

            if (returnValues.Count == 0)
            {
                results.State = State.NotFound;

                accessor.DisposeConnection();

                return results;
            }

            results.Content = ReplaceGroupEntity.CreateList(returnValues);

            results.State = State.Success;

            accessor.DisposeConnection();

            return results;
        }

        public Result<GroupCategoryGroupRequestContent> CreateGroupAsync(GroupRequestContent content)
        {
            var result = new Result<GroupCategoryGroupRequestContent>();

            using var accessor = _dbAccessFactory.Create();
            accessor.CreateConnectionWithTransaction(CallerInfo.Create(Shared.ToSharedContext()));
            using var groupCategoryAccess = _GroupSettingsFactory.CreateGroupCategoryAccess(accessor);

            var returnValue = new EntryGroup().Post(accessor, _GroupSettingsFactory, content);

            if (returnValue.Count == 0)
            {
                accessor.DisposeConnection();
                result.State = State.Conflict;
                return result;
            }
            // コミット
            accessor.CommitTrans();
            accessor.DisposeConnection();

            result.State = State.Success;
            result.Content = GetGroupByIdAsync(returnValue.Entity.Id)?.Content?[0];
            return result;
        }
        public Result<GroupCategoryGroupRequestContent> CreateAndHeaderUpdateGroupAsync(GroupRequestContent content)
        {
            var result = new Result<GroupCategoryGroupRequestContent>();

            using var accessor = _dbAccessFactory.Create();
            accessor.CreateConnectionWithTransaction(CallerInfo.Create(Shared.ToSharedContext()));
            using var KangoHitsuyodoHyokaAccess = _GroupSettingsFactory.CreateGroupCategoryAccess(accessor);

            // 重症度・看護必要度評価マスター
            var returnValue = new EntryGroup().Put(accessor, _GroupSettingsFactory, content);

            if (returnValue.Count == 0)
            {
                accessor.DisposeConnection();
                result.State = State.Conflict;
                return result;
            }

            // コミット
            accessor.CommitTrans();
            accessor.DisposeConnection();

            result.State = State.Success;
            result.Content = GetGroupByIdAsync(returnValue.Entity.Id)?.Content?[0];

            return result;
        }
        public Result<GroupCategoryGroupRequestContent> UpdateGroupAsync(GroupRequestContent content)
        {
            var result = new Result<GroupCategoryGroupRequestContent>();
            using var accessor = _dbAccessFactory.Create();
            accessor.CreateConnectionWithTransaction(CallerInfo.Create(Shared.ToSharedContext()));
            using var groupAccess = _GroupSettingsFactory.CreateGroupAccess(accessor);

            var beforeData = GetGroupByIdAsync(content.Id);

            if (beforeData.Content == null || beforeData.Content.Count == 0 || beforeData.Content[0].GroupResponseContent == null)
            {
                result.State = State.NotFound;
                accessor.DisposeConnection();
                return result;
            }

            if (groupAccess.Exists(content.Id, content.FacilityId, content.GroupCode, content.GroupCategoryId))
            {
                result.State = State.CodeUsed;
                accessor.DisposeConnection();
                return result;
            }

            var entityBefore = beforeData.Content[0].GroupResponseContent[0];
            content.LockVersion = entityBefore.LockVersion;

            var returnValue = new EntryGroup().Put(accessor, _GroupSettingsFactory, content);

            if (returnValue.Count == 0)
            {
                result.State = State.Conflict;
                accessor.DisposeConnection();
                return result;
            }

            // コミット
            accessor.CommitTrans();
            accessor.DisposeConnection();

            result.Content = GetGroupByIdAsync(content.Id)?.Content?[0];

            return result;
        }

        /// <summary>
        /// Delete group
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Result<CountResponseContent> DeleteGroupAsync(string id)
        {
            var result = new Result<CountResponseContent>();

            using var accessor = _dbAccessFactory.Create();
            accessor.CreateConnectionWithTransaction(CallerInfo.Create(Shared.ToSharedContext()));

            var dellist = GetGroupByIdAsync(id: id);

            if ((dellist == null) || (dellist.State == State.NotFound) || (dellist.Content == null) || (dellist.Content[0].GroupResponseContent == null))
            {
                result.State = State.NotFound;
                accessor.DisposeConnection();
                return result;
            }

            var delitem = dellist.Content[0];

            var count = 0;
            foreach (var item in delitem.GroupResponseContent)
            {
                var returnValue = new EntryGroup().Delete(accessor, _GroupSettingsFactory, item);
                if (returnValue != null && returnValue.Count > 0)
                {
                    count += 1;
                }
            }

            result.State = State.Success;
            result.Content = new CountResponseContent()
            {
                Count = count
            };

            // コミット
            accessor.CommitTrans();
            accessor.DisposeConnection();

            return result;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupcategoryid"></param>
        /// <returns></returns>
        public string ReadNewCodeGroup(string groupcategoryid)
        {
            using var accessor = _dbAccessFactory.Create();
            accessor.CreateConnection(CallerInfo.Create(Shared.ToSharedContext()));
            // データ取得
            using var groupAccess = _GroupSettingsFactory.CreateGroupAccess(accessor);
            var code = groupAccess.ReadNewCodeGroup(groupcategoryid);
            accessor.DisposeConnection();
            return code;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="facilityid"></param>
        /// <param name="facilitygroupid"></param>
        /// <returns></returns>
        public Result<GroupReadByCodeEntity> ReadByCodeGroup(string groupcode, string facilityid, string facilitygroupid, string areacorpid)
        {
            var result = new Result<GroupReadByCodeEntity>();

            using var accessor = _dbAccessFactory.Create();
            accessor.CreateConnection(CallerInfo.Create(Shared.ToSharedContext()));
            // データ取得
            using var groupAccess = _GroupSettingsFactory.CreateGroupAccess(accessor);
            result.Content = groupAccess.ReadByCodeGroup(groupcode, facilityid, facilitygroupid, areacorpid);

            accessor.DisposeConnection();

            return result;

        }
        #endregion

        #region カテゴリー(Category)
        /// <summary>
        /// カテゴリー一覧取得
        /// </summary>
        /// <param name="categoryCode">カテゴリコード</param>
        /// <param name="limit">取得上限件数</param>
        /// <param name="offset">オフセット</param>
        public Result<List<CategoryResponseContent>> GetListCategorySearch(string categoryCode = "",
                                short limit = 1000,
                                short offset = 0)
        {
            var results = new Result<List<CategoryResponseContent>>();

            using var accessor = _dbAccessFactory.Create();

            accessor.CreateConnection(CallerInfo.Create(Shared.ToSharedContext()));

            using var categoryAccess = _GroupSettingsFactory.CreateCategoryAccess(accessor);

            var returnValues = categoryAccess.GetAsync(categoryCode, limit, offset);

            if (returnValues.Count == 0)
            {
                results.State = State.NotFound;

                accessor.DisposeConnection();

                return results;
            }

            results.Content = ReplaceCategoryEntity.CreateList(returnValues);

            results.State = State.Success;

            accessor.DisposeConnection();

            return results;
        }
        #endregion

        #region GroupManagement
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
        public Result<List<GroupCategoryResponseContent>> GetGroupManagementAsync(string groupCategoryCode = "",
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
            var results = new Result<List<GroupCategoryResponseContent>>();

            using var accessor = _dbAccessFactory.Create();

            accessor.CreateConnection(CallerInfo.Create(Shared.ToSharedContext()));

            using var groupManagementAccess = _GroupSettingsFactory.CreateGroupManagementAccess(accessor);

            var returnValues = groupManagementAccess.Get(groupCategoryCode, groupTani, areaCorpId, facilityGroupId, facilityId, groupCode, validFlag, kijunbi, kijunbiFlag, groupManagementCode, postId, limit, offset);

            if (returnValues.Count == 0)
            {
                results.State = State.NotFound;

                accessor.DisposeConnection();

                return results;
            }

            results.Content = ReplaceGroupManagementEntity.CreateList(returnValues);

            results.State = State.Success;

            accessor.DisposeConnection();

            return results;
        }

        /// <summary>
        /// グループ管理1件取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Result<List<GroupManagementResponseContent>> GetGroupManagementById(string id = "")
        {
            var results = new Result<List<GroupManagementResponseContent>>();

            using var accessor = _dbAccessFactory.Create();

            accessor.CreateConnection(CallerInfo.Create(Shared.ToSharedContext()));

            // データ取得
            using var groupManagementAccess = _GroupSettingsFactory.CreateGroupManagementAccess(accessor);

            var returnValues = groupManagementAccess.GetById(id);

            results.Content = ReplaceGroupManagementEntity.CreateDetail(returnValues);

            accessor.DisposeConnection();

            return results;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="facilityid"></param>
        /// <param name="facilitygroupid"></param>
        /// <returns></returns>
        public string ReadNewCodeGroupManagement(String facilityid, string facilitygroupid)
        {
            using var accessor = _dbAccessFactory.Create();
            accessor.CreateConnection(CallerInfo.Create(Shared.ToSharedContext()));
            // データ取得
            using var groupAccess = _GroupSettingsFactory.CreateGroupManagementAccess(accessor);
            var code = groupAccess.ReadNewCodeGroupManagement(facilityid, facilitygroupid);
            accessor.DisposeConnection();
            return code;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="facilityId"></param>
        /// <param name="facilitygroupId"></param>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public string ReadNewCodeGroupPatient(String facilityId, string facilitygroupId, string patientId)
        {
            using var accessor = _dbAccessFactory.Create();
            accessor.CreateConnection(CallerInfo.Create(Shared.ToSharedContext()));
            // データ取得
            using var groupAccess = _GroupSettingsFactory.CreateGroupPatientAccess(accessor);
            var code = groupAccess.ReadNewCodeGroupPatient(facilityId, facilitygroupId, patientId);
            accessor.DisposeConnection();
            return code;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="facilityid"></param>
        /// <param name="facilitygroupid"></param>
        /// <returns></returns>
        public Result<GroupManagementEntity> ReadByCodeGroupManagement(string groupManagementCode, string facilityid, string facilitygroupid)
        {
            var result = new Result<GroupManagementEntity>();

            using var accessor = _dbAccessFactory.Create();
            accessor.CreateConnection(CallerInfo.Create(Shared.ToSharedContext()));
            // データ取得
            using var groupAccess = _GroupSettingsFactory.CreateGroupManagementAccess(accessor);
            result.Content = groupAccess.ReadByCodeGroupManagement(groupManagementCode, facilityid, facilitygroupid);

            accessor.DisposeConnection();

            return result;

        }

        public Result<GroupManagementResponseContent> CreateGroupManagementAsync(GroupManagementPostContent content)
        {
            var result = new Result<GroupManagementResponseContent>();

            using var accessor = _dbAccessFactory.Create();
            accessor.CreateConnectionWithTransaction(CallerInfo.Create(Shared.ToSharedContext()));

            var returnValue = new EntryGroupManagement().Post(accessor, _GroupSettingsFactory, ReplaceGroupManagementEntity.CreateEntityPost(content));

            if (returnValue.Count == 0)
            {
                accessor.DisposeConnection();
                result.State = State.Conflict;
                return result;
            }

            using var groupStaffAccess = _GroupSettingsFactory.CreateGroupStaffAccess(accessor);
            using var groupStaffJnlAccess = _GroupSettingsFactory.CreateGroupStaffJnlAccess(accessor);
            using var groupPatientAccess = _GroupSettingsFactory.CreateGroupPatientAccess(accessor);
            using var groupPatientJnlAccess = _GroupSettingsFactory.CreateGroupPatientJnlAccess(accessor);

            foreach (var item in content.GroupPatientPostContent)
            {
                item.GroupManagementId = returnValue.Entity.Id;
                if (new EntryGroupPatient().Post(groupPatientAccess, groupPatientJnlAccess, ReplaceGroupPatientEntity.ConvertResquestToEntity(item)).Count == 0)
                {
                    result.State = State.Conflict;
                    accessor.DisposeConnection();
                    return result;
                }
            }

            foreach (var item in content.GroupStaffPostContent)
            {
                item.GroupManagementId = returnValue.Entity.Id;
                if (new EntryGroupStaff().Post(groupStaffAccess, groupStaffJnlAccess, item).Count == 0)
                {
                    result.State = State.Conflict;
                    accessor.DisposeConnection();
                    return result;
                }
            }

            // コミット
            accessor.CommitTrans();
            accessor.DisposeConnection();

            result.State = State.Success;
            result.Content = GetGroupManagementById(returnValue.Entity.Id)?.Content?[0];
            return result;
        }

        public Result<GroupManagementResponseContent> CreateHeaderGroupManagementAsync(GroupManagementPostContent content)
        {
            var result = new Result<GroupManagementResponseContent>();

            using var accessor = _dbAccessFactory.Create();
            accessor.CreateConnectionWithTransaction(CallerInfo.Create(Shared.ToSharedContext()));

            var returnValue = new EntryGroupManagement().Put(accessor, _GroupSettingsFactory, ReplaceGroupManagementEntity.CreateEntityPut(content));

            if (returnValue.Count == 0)
            {
                accessor.DisposeConnection();
                result.State = State.Conflict;
                return result;
            }

            using var groupStaffAccess = _GroupSettingsFactory.CreateGroupStaffAccess(accessor);
            using var groupStaffJnlAccess = _GroupSettingsFactory.CreateGroupStaffJnlAccess(accessor);
            using var groupPatientAccess = _GroupSettingsFactory.CreateGroupPatientAccess(accessor);
            using var groupPatientJnlAccess = _GroupSettingsFactory.CreateGroupPatientJnlAccess(accessor);

            foreach (var item in content.GroupPatientPostContent)
            {
                item.GroupManagementId = returnValue.Entity.Id;
                if (new EntryGroupPatient().Post(groupPatientAccess, groupPatientJnlAccess, ReplaceGroupPatientEntity.ConvertResquestToEntity(item)).Count == 0)
                {
                    result.State = State.Conflict;
                    accessor.DisposeConnection();
                    return result;
                }
            }

            foreach (var item in content.GroupStaffPostContent)
            {
                item.GroupManagementId = returnValue.Entity.Id;
                if (new EntryGroupStaff().Post(groupStaffAccess, groupStaffJnlAccess, item).Count == 0)
                {
                    result.State = State.Conflict;
                    accessor.DisposeConnection();
                    return result;
                }
            }

            // コミット
            accessor.CommitTrans();
            accessor.DisposeConnection();

            result.State = State.Success;
            result.Content = GetGroupManagementById(returnValue.Entity.Id)?.Content?[0];

            return result;
        }

        public Result<GroupManagementResponseContent> UpdateGroupManagementAsync(GroupManagementPostContent content)
        {
            var result = new Result<GroupManagementResponseContent>();
            using var accessor = _dbAccessFactory.Create();
            accessor.CreateConnectionWithTransaction(CallerInfo.Create(Shared.ToSharedContext()));

            var beforeData = GetGroupManagementById(content.Id)?.Content?[0];

            if (beforeData == null)
            {
                result.State = State.NotFound;
                accessor.DisposeConnection();
                return result;
            }

            var returnValue = new Gen2.RDB.Entities.Result<GroupManagementEntity>();
            var flagInsert = false;
            using var groupAccess = _GroupSettingsFactory.CreateGroupAccess(accessor);
            var groupCode = groupAccess.ReadGroupCodeById(content.GroupId);
            var dataCheck = GetGroupManagementAsync(areaCorpId: content.AreaCorpId, facilityGroupId: content.FacilityGroupId, facilityId: content.FacilityId, groupCode: groupCode);

            if (dataCheck.Content != null && dataCheck.Content.Any(x => x.GroupResponseContent != null &&
                            x.GroupResponseContent.Any(y => y.GroupManagementResponseContent != null && y.GroupManagementResponseContent.Any())))
            {
                var liItem = dataCheck.Content.Where(x => x.GroupResponseContent.Any(x => x.GroupManagementResponseContent.Any(x => x.GroupId == content.GroupId))).FirstOrDefault();
                if (liItem != null)
                {
                    var item = liItem.GroupResponseContent?.First().GroupManagementResponseContent.Where(x => x.Id != content.Id && x.StartDate <= content.EndDate && x.EndDate >= content.StartDate)?.FirstOrDefault();
                    if (item == null)
                    {
                        if (content.StartDate >= (beforeData.StartDate + 1) && beforeData.EndDate == 99999999)
                        {
                            beforeData.EndDate = content.StartDate.ToDateTime().Value.AddDays(-1).ToInt().Value;
                            beforeData.LastUpdaterId = content.LastUpdaterId;
                            beforeData.LastUpdaterName = content.LastUpdaterName;
                            if (new EntryGroupManagement().Put(accessor, _GroupSettingsFactory, ReplaceGroupManagementEntity.ConvertResponseToEntity(beforeData)).Count > 0)
                            {
                                content.GroupManagementCode = ReadNewCodeGroupManagement(content.FacilityId, content.FacilityGroupId);
                                if (int.Parse(content.GroupManagementCode) > 9999)
                                {
                                    result.ErrorJson = BadRequestMessage.ErrorCode1002;
                                    return result;
                                }
                                returnValue = new EntryGroupManagement().Post(accessor, _GroupSettingsFactory, ReplaceGroupManagementEntity.CreateEntityPost(content));
                                flagInsert = true;
                            }
                        }
                        else
                        {
                            content.LockVersion = beforeData?.LockVersion;
                            returnValue = new EntryGroupManagement().Put(accessor, _GroupSettingsFactory, ReplaceGroupManagementEntity.CreateEntityPut(content));
                        }
                    }
                }
            }

            if (returnValue.Count == 0)
            {
                result.State = State.Conflict;
                accessor.DisposeConnection();
                return result;
            }

            using var groupStaffAccess = _GroupSettingsFactory.CreateGroupStaffAccess(accessor);
            using var groupStaffJnlAccess = _GroupSettingsFactory.CreateGroupStaffJnlAccess(accessor);
            using var groupPatientAccess = _GroupSettingsFactory.CreateGroupPatientAccess(accessor);
            using var groupPatientJnlAccess = _GroupSettingsFactory.CreateGroupPatientJnlAccess(accessor);

            var checkStateError = false;

            foreach (var item in content.GroupPatientPostContent)
            {
                item.GroupManagementId = returnValue.Entity.Id;

                if (flagInsert)
                {
                    if (item.ActionFlag == ActionFlag.Insert || item.ActionFlag == ActionFlag.Update)
                    {
                        checkStateError = new EntryGroupPatient().Post(groupPatientAccess, groupPatientJnlAccess, ReplaceGroupPatientEntity.ConvertResquestToEntity(item)).Count == 0;
                    }          
                }
                else
                {
                    switch (item.ActionFlag)
                    {
                        case ActionFlag.Delete:
                            checkStateError = !(new EntryGroupPatient().Delete(groupPatientAccess, groupPatientJnlAccess, ReplaceGroupPatientEntity.ConvertResquestToEntity(item)));
                            break;
                        case ActionFlag.Insert:
                            checkStateError = new EntryGroupPatient().Post(groupPatientAccess, groupPatientJnlAccess, ReplaceGroupPatientEntity.ConvertResquestToEntity(item)).Count == 0;
                            break;
                        case ActionFlag.Update:
                            checkStateError = new EntryGroupPatient().Put(groupPatientAccess, groupPatientJnlAccess, ReplaceGroupPatientEntity.ConvertResquestToEntity(item)).Count == 0;
                            break;
                        default:
                            break;
                    }
                }

                if (checkStateError)
                {
                    result.State = State.Conflict;
                    accessor.DisposeConnection();
                    return result;
                }
            }

            foreach (var item in content.GroupStaffPostContent)
            {
                item.GroupManagementId = returnValue.Entity.Id;

                if (flagInsert)
                {
                    if (item.ActionFlag == ActionFlag.Insert || item.ActionFlag == ActionFlag.Update )
                    {
                        checkStateError = new EntryGroupStaff().Post(groupStaffAccess, groupStaffJnlAccess, item).Count == 0;
                    }      
                }
                else
                {
                    switch (item.ActionFlag)
                    {
                        case ActionFlag.Delete:
                            checkStateError = !(new EntryGroupStaff().Delete(groupStaffAccess, groupStaffJnlAccess, ReplaceGroupStaffEntity.ConvertResquestToEntity(item)));
                            break;
                        case ActionFlag.Insert:
                            checkStateError = new EntryGroupStaff().Post(groupStaffAccess, groupStaffJnlAccess, item).Count == 0;
                            break;
                        case ActionFlag.Update:
                            checkStateError = new EntryGroupStaff().Put(groupStaffAccess, groupStaffJnlAccess, item).Count == 0;
                            break;
                        default:
                            break;
                    }
                }

                if (checkStateError)
                {
                    result.State = State.Conflict;
                    accessor.DisposeConnection();
                    return result;
                }
            }

            // コミット
            accessor.CommitTrans();
            accessor.DisposeConnection();

            result.Content = GetGroupManagementById(returnValue.Entity.Id)?.Content?[0];

            return result;
        }

        /// <summary>
        /// グループ管理削除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Result<CountResponseContent> DeleteGroupManagementAsync(string id)
        {
            var result = new Result<CountResponseContent>();

            using var accessor = _dbAccessFactory.Create();
            accessor.CreateConnectionWithTransaction(CallerInfo.Create(Shared.ToSharedContext()));

            var groupManagement = GetGroupManagementById(id);

            if ((groupManagement == null) || (groupManagement.State == State.NotFound) || (groupManagement.Content == null))
            {
                result.State = State.NotFound;
                accessor.DisposeConnection();
                return result;
            }

            var entry = new EntryGroupManagement();
            using var groupStaffAccess = _GroupSettingsFactory.CreateGroupStaffAccess(accessor);
            using var groupStaffJnlAccess = _GroupSettingsFactory.CreateGroupStaffJnlAccess(accessor);
            using var groupPatientAccess = _GroupSettingsFactory.CreateGroupPatientAccess(accessor);
            using var groupPatientJnlAccess = _GroupSettingsFactory.CreateGroupPatientJnlAccess(accessor);

            var count = 0;
            var returnValue = entry.Delete(accessor, _GroupSettingsFactory, groupManagement.Content[0]);
            if (returnValue.Count > 0)
            {
                count += 1;
                foreach (var item in groupManagement.Content[0].GroupPatientResponseContent)
                {
                    if (new EntryGroupPatient().Delete(groupPatientAccess, groupPatientJnlAccess, ReplaceGroupPatientEntity.ConvertResponseToEntity(item)))
                    {
                        count += 1;
                    }
                }
                foreach (var item in groupManagement.Content[0].GroupStaffResponseContent)
                {
                    if (new EntryGroupStaff().Delete(groupStaffAccess, groupStaffJnlAccess, ReplaceGroupStaffEntity.ConvertResponseToEntity(item)))
                    {
                        count += 1;
                    }
                }
            }

            result.State = State.Success;
            result.Content = new CountResponseContent()
            {
                Count = count
            };

            // コミット
            accessor.CommitTrans();
            accessor.DisposeConnection();

            return result;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="facilityid"></param>
        /// <param name="facilitygroupid"></param>
        /// <returns></returns>
        public Result<GroupManagementEntity> GetGroupManagementForCreateGroupPatient(string groupManagementCode, string facilityid, string facilitygroupid)
        {
            var result = new Result<GroupManagementEntity>();

            using var accessor = _dbAccessFactory.Create();
            accessor.CreateConnection(CallerInfo.Create(Shared.ToSharedContext()));
            // データ取得
            using var groupAccess = _GroupSettingsFactory.CreateGroupManagementAccess(accessor);

            result.Content = groupAccess.ReadByCodeGroupManagement(groupManagementCode, facilityid, facilitygroupid);

            accessor.DisposeConnection();

            return result;

        }

        #endregion

        #region GroupPatient
        /// <summary>
        /// グループ患者削除
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        public Result<CountResponseContent> DeleteGroupPatientAsync(List<string> idList)
        {
            var result = new Result<CountResponseContent>();

            using var accessor = _dbAccessFactory.Create();
            accessor.CreateConnectionWithTransaction(CallerInfo.Create(Shared.ToSharedContext()));

            using var groupPatientAccess = _GroupSettingsFactory.CreateGroupPatientAccess(accessor);
            using var groupPatientJnlAccess = _GroupSettingsFactory.CreateGroupPatientJnlAccess(accessor);

            var count = 0;

            foreach (var id in idList)
            {
                var groupManagement = GetGroupPatientById(id);

                if ((groupManagement == null) || (groupManagement.State == State.NotFound) || (groupManagement.Content == null))
                {
                    result.State = State.NotFound;
                    accessor.DisposeConnection();
                    return result;
                }

                if (new EntryGroupPatient().Delete(groupPatientAccess, groupPatientJnlAccess, ReplaceGroupPatientEntity.ConvertResponseToEntity(groupManagement.Content[0])))
                {
                    count += 1;
                }
            }

            result.State = State.Success;
            result.Content = new CountResponseContent()
            {
                Count = count
            };

            // コミット
            accessor.CommitTrans();
            accessor.DisposeConnection();

            return result;

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
        public Result<List<GroupCategoryResponseContent>> GetGroupPatientAsync(string groupCategoryCode = "",
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
            var results = new Result<List<GroupCategoryResponseContent>>();

            using var accessor = _dbAccessFactory.Create();

            accessor.CreateConnection(CallerInfo.Create(Shared.ToSharedContext()));

            using var groupPatientAccess = _GroupSettingsFactory.CreateGroupPatientAccess(accessor);

            var returnValues = groupPatientAccess.Get(groupCategoryCode, groupTani, areaCorpId, facilityGroupId, facilityId, groupCode, validFlag, groupManagementCode, kijunbi, kijunbiFlag, patientId, postId, limit, offset);

            if (returnValues.Count == 0)
            {
                results.State = State.NotFound;

                accessor.DisposeConnection();

                return results;
            }

            results.Content = ReplaceGroupPatientEntity.CreateListGroupCategory(returnValues);

            results.State = State.Success;

            accessor.DisposeConnection();

            return results;
        }

        /// <summary>
        /// 投稿IDによるグループ患者リストの取得
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public Result<List<GroupPatientResponseContent>> GetGroupPatientByPostId(string postId)
        {
            var results = new Result<List<GroupPatientResponseContent>>();

            using var accessor = _dbAccessFactory.Create();

            accessor.CreateConnection(CallerInfo.Create(Shared.ToSharedContext()));

            using var groupPatientAccess = _GroupSettingsFactory.CreateGroupPatientAccess(accessor);

            var returnValues = groupPatientAccess.GetByPostId(postId);

            if (returnValues.Count == 0)
            {
                results.State = State.NotFound;

                accessor.DisposeConnection();

                return results;
            }

            results.Content = ReplaceGroupPatientEntity.CreateListPatientResponse(returnValues);

            results.State = State.Success;

            accessor.DisposeConnection();

            return results;
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
        public Result<List<GroupCategoryResponseContent>> GetUnregistGroupAsync(string searchWord = "",
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
            var results = new Result<List<GroupCategoryResponseContent>>();

            using var accessor = _dbAccessFactory.Create();

            accessor.CreateConnection(CallerInfo.Create(Shared.ToSharedContext()));

            using var groupPatientAccess = _GroupSettingsFactory.CreateGroupPatientAccess(accessor);

            var returnValues = groupPatientAccess.GetUnregistGroup(searchWord, searchFlag, areaCorpId, facilityGroupId, facilityId, kijunbi, patientId, postId, limit, offset);

            if (returnValues.Count == 0)
            {
                results.State = State.NotFound;

                accessor.DisposeConnection();

                return results;
            }

            results.Content = ReplaceGroupCategoryEntity.CreateUnregistList(returnValues);

            results.State = State.Success;

            accessor.DisposeConnection();

            return results;
        }

        /// <summary>
        /// Idに該当するリソースを取得する
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Result<List<GroupPatientResponseContent>> GetGroupPatientById(string id = "")
        {
            var results = new Result<List<GroupPatientResponseContent>>();

            using var accessor = _dbAccessFactory.Create();

            accessor.CreateConnection(CallerInfo.Create(Shared.ToSharedContext()));

            // データ取得
            using var groupPatientAccess = _GroupSettingsFactory.CreateGroupPatientAccess(accessor);

            var returnValues = groupPatientAccess.GetById(id: id);

            results.Content = ReplaceGroupPatientEntity.CreateListPatientResponse(returnValues);

            accessor.DisposeConnection();

            return results;
        }

        /// <summary>
        /// Idに該当するリソースを取得する
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Result<List<GroupPatientResponseContent>> GetGroupPatientByIds(string ids = "")
        {
            var results = new Result<List<GroupPatientResponseContent>>();

            using var accessor = _dbAccessFactory.Create();

            accessor.CreateConnection(CallerInfo.Create(Shared.ToSharedContext()));

            // データ取得
            using var groupPatientAccess = _GroupSettingsFactory.CreateGroupPatientAccess(accessor);

            var returnValues = groupPatientAccess.GetByIds(ids);

            results.Content = ReplaceGroupPatientEntity.CreateListPatientResponse(returnValues);

            accessor.DisposeConnection();

            return results;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupPatientCode"></param>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public Result<List<GroupPatientResponseContent>> GetGroupPatientByGroupPatientCode(string groupPatientCode, string patientId)
        {
            var results = new Result<List<GroupPatientResponseContent>>();

            using var accessor = _dbAccessFactory.Create();

            accessor.CreateConnection(CallerInfo.Create(Shared.ToSharedContext()));

            // データ取得
            using var groupPatientAccess = _GroupSettingsFactory.CreateGroupPatientAccess(accessor);

            var returnValues = groupPatientAccess.GetByGroupPatientCode(groupPatientCode, patientId);

            results.Content = ReplaceGroupPatientEntity.CreateListPatientResponse(returnValues);

            accessor.DisposeConnection();

            return results;
        }

        /// <summary>
        /// グループ患者登録
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public Result<List<GroupPatientResponseContent>> CreateGroupPatient(List<GroupPatientPostContent> content)
        {
            var result = new Result<List<GroupPatientResponseContent>>();
            result.Content = new List<GroupPatientResponseContent>();

            if (content.Count == 0)
            {
                result.State = State.NotFound;
                return result;
            }

            string groupPatientCode = "";

            using var accessor = _dbAccessFactory.Create();
            accessor.CreateConnectionWithTransaction(CallerInfo.Create(Shared.ToSharedContext()));

            using var groupPatientAccess = _GroupSettingsFactory.CreateGroupPatientAccess(accessor);
            using var groupPatientJnlAccess = _GroupSettingsFactory.CreateGroupPatientJnlAccess(accessor);

            var dataCheck = GetGroupPatientAsync(areaCorpId: content.FirstOrDefault().AreaCorpId, facilityGroupId: content.FirstOrDefault()?.FacilityGroupId, facilityId: content.FirstOrDefault()?.FacilityId, patientId: content.FirstOrDefault()?.PatientId, limit: 1000, offset: 0);
            if (dataCheck.Content != null && dataCheck.Content.Any(x => x.GroupResponseContent != null &&
                x.GroupResponseContent.Any(x => x.GroupManagementResponseContent != null && x.GroupManagementResponseContent.Any(x => x.GroupPatientResponseContent != null))))
            {
                bool? item = dataCheck.Content?.Any(x => x.GroupResponseContent.Any(x => x.GroupManagementResponseContent.Any(x => content.Any(y => y.Group_Id == x.GroupId) && x.GroupPatientResponseContent.Any(x => x.StartDate <= content.FirstOrDefault().EndDate && x.EndDate >= content.FirstOrDefault().StartDate))));
                if (item == true)
                {
                    result.State = State.Conflict;
                    result.ErrorJson = BadRequestMessage.ErrorCode1003;
                    return result;
                }
            }

            foreach (var item in content)
            {
                var groupManagementCode = ReadNewCodeGroupManagement(item.FacilityId, item.FacilityGroupId);
                if (int.Parse(groupManagementCode) > 9999)
                {
                    result.State = State.Conflict;
                    return result;
                }

                var groupManagement = new EntryGroupManagement().Post(accessor, _GroupSettingsFactory, ReplaceGroupManagementEntity.ConvertPatientPostToManagementEntity(item, groupManagementCode));

                if (groupManagement.Count == 0)
                {
                    result.State = State.Conflict;
                    return result;
                }

                if (string.IsNullOrEmpty(groupPatientCode))
                {
                    groupPatientCode = ReadNewCodeGroupPatient(item.FacilityId, item.FacilityGroupId, item.PatientId);

                    if (int.Parse(groupPatientCode) > 9999)
                    {
                        result.State = State.Conflict;
                        return result;
                    }

                }

                item.GroupManagementId = groupManagement.Entity?.Id;
                item.GroupPatientCode = groupPatientCode;
                var returnValue = new EntryGroupPatient().Post(groupPatientAccess, groupPatientJnlAccess, ReplaceGroupPatientEntity.ConvertResquestToEntity(item));

                if (returnValue.Count > 0)
                {
                    result.State = State.Success;
                    result.Content.Add(ReplaceGroupPatientEntity.CreatePatientResponse(returnValue.Entity));
                }
                else
                {
                    result.State = State.Conflict;
                    return result;
                }
            }

            accessor.CommitTrans();
            accessor.DisposeConnection();

            return result;
        }

        /// <summary>
        /// グループ分類更新
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public Result<List<GroupPatientResponseContent>> UpdateGroupPatient(List<GroupPatientPostContent> content)
        {
            var returnValue = new Gen2.RDB.Entities.Result<GroupPatientEntity>();
            var result = new Result<List<GroupPatientResponseContent>>();
            result.Content = new List<GroupPatientResponseContent>();

            var liPatientContent = content.Where(x => x.ActionFlag != ActionFlag.Insert && x.StartDate > 0 && x.EndDate > 0).Select(x => new { x.AreaCorpId, x.FacilityGroupId, x.FacilityId, x.PatientId, x.GroupPatientCode, x.StartDate, x.EndDate }).Distinct().ToList();
            if (liPatientContent == null || liPatientContent.Count > 1)
            {
                result.State = State.Conflict;
                return result;
            }
            var objPatientContent = liPatientContent.First();

            var beforeData = GetGroupPatientByIds(string.Join(',', content.Where(x => !string.IsNullOrEmpty(x.Id)).Select(x => "'" + x.Id + "'").ToList())).Content;
            if (beforeData == null)
            {
                result.State = State.NotFound;
                return result;
            }

            var liPatientBeforeData = beforeData?.Where(x => x.StartDate > 0 && x.EndDate > 0).Select(x => new { x.PatientId, x.GroupPatientCode, x.StartDate, x.EndDate }).Distinct().ToList();
            if (liPatientBeforeData == null || liPatientBeforeData.Count > 1)
            {
                result.State = State.Conflict;
                return result;
            }
            var objPatientBeforeData = liPatientBeforeData.First();

            using var accessor = _dbAccessFactory.Create();

            accessor.CreateConnectionWithTransaction(CallerInfo.Create(Shared.ToSharedContext()));
            using var groupPatientAccess = _GroupSettingsFactory.CreateGroupPatientAccess(accessor);
            using var groupPatientJnlAccess = _GroupSettingsFactory.CreateGroupPatientJnlAccess(accessor);

            var dataCheck = GetGroupPatientAsync(areaCorpId: objPatientContent.AreaCorpId, facilityGroupId: objPatientContent.FacilityGroupId, facilityId: objPatientContent.FacilityId, patientId: objPatientContent.PatientId, limit: 1000, offset: 0);
            if (dataCheck.Content != null && dataCheck.Content.Any(x => x.GroupResponseContent != null &&
                x.GroupResponseContent.Any(x => x.GroupManagementResponseContent != null && x.GroupManagementResponseContent.Any(x => x.GroupPatientResponseContent != null))))
            {
               bool? item = dataCheck.Content?.Any(x => x.GroupResponseContent.Any(x => x.GroupManagementResponseContent.Any(x => x.GroupPatientResponseContent.Any(x => !content.Any(y => y.Id == x.Id) && x.StartDate <= objPatientContent.EndDate && x.EndDate >= objPatientContent.StartDate))));
                if (item == false)
                {
                    int maxStartDate = 0;
                    int maxEndDate = 0;
                    var dataGroupPatient = dataCheck.Content?.SelectMany(x => x.GroupResponseContent)
                                                    .SelectMany(x => x.GroupManagementResponseContent)
                                                    .SelectMany(x => x.GroupPatientResponseContent);

                    if (dataGroupPatient != null && dataGroupPatient.Any())
                    {
                        maxStartDate = dataGroupPatient.Max(x => x.StartDate);
                        maxEndDate = dataGroupPatient.Max(x => x.EndDate);

                        if (objPatientContent.StartDate >= (maxStartDate + 1) && maxEndDate == 99999999)
                        {
                            foreach (var itemGroupPatient in beforeData)
                            {
                                if (itemGroupPatient.EndDate >= objPatientContent.StartDate)
                                {
                                    itemGroupPatient.EndDate = objPatientContent.StartDate.ToDateTime().Value.AddDays(-1).ToInt().Value;
                                    itemGroupPatient.LastUpdaterId = content[0].LastUpdaterId;
                                    itemGroupPatient.LastUpdaterName = content[0].LastUpdaterName;

                                    returnValue = new EntryGroupPatient().Put(groupPatientAccess, groupPatientJnlAccess, ReplaceGroupPatientEntity.ConvertResponseToEntity(itemGroupPatient));

                                    if (returnValue.Count <= 0)
                                    {
                                        result.State = State.Conflict;
                                        accessor.DisposeConnection();
                                        return result;
                                    }
                                }
                            }

                            string groupPatientCode = "";

                            foreach (var itemGroupPatient in content)
                            {
                                if (itemGroupPatient.ActionFlag != ActionFlag.Delete)
                                {
                                    var groupManagementCode = ReadNewCodeGroupManagement(objPatientContent.FacilityId, objPatientContent.FacilityGroupId);
                                    if (int.Parse(groupManagementCode) > 9999)
                                    {
                                        result.State = State.Conflict;
                                        return result;
                                    }

                                    var groupManagement = new EntryGroupManagement().Post(accessor, _GroupSettingsFactory, ReplaceGroupManagementEntity.ConvertPatientPostToManagementEntity(itemGroupPatient, groupManagementCode));

                                    if (groupManagement.Count == 0)
                                    {
                                        result.State = State.Conflict;
                                        return result;
                                    }

                                    if (string.IsNullOrEmpty(groupPatientCode))
                                    {
                                        groupPatientCode = ReadNewCodeGroupPatient(objPatientContent.FacilityId, objPatientContent.FacilityGroupId, objPatientContent.PatientId);

                                        if (int.Parse(groupPatientCode) > 9999)
                                        {
                                            result.State = State.Conflict;
                                            return result;
                                        }
                                    }

                                    itemGroupPatient.GroupManagementId = groupManagement.Entity.Id;
                                    itemGroupPatient.GroupPatientCode = groupPatientCode;
                                    returnValue = new EntryGroupPatient().Post(groupPatientAccess, groupPatientJnlAccess, ReplaceGroupPatientEntity.ConvertResquestToEntity(itemGroupPatient));

                                    if (returnValue.Count > 0)
                                    {
                                        result.State = State.Success;
                                        result.Content.Add(ReplaceGroupPatientEntity.CreatePatientResponse(returnValue.Entity));
                                    }
                                    else
                                    {
                                        result.State = State.Conflict;
                                        return result;
                                    }
                                }
                            }
                        }
                        else
                        {
                            foreach (var itemGroupPatient in content)
                            {
                                switch (itemGroupPatient.ActionFlag)
                                {
                                    case ActionFlag.Delete:
                                        var groupManagement = dataCheck.Content?.SelectMany(x => x.GroupResponseContent)
                                                                                .SelectMany(x => x.GroupManagementResponseContent).Where(x => x.Id == itemGroupPatient.GroupManagementId).FirstOrDefault();
                                        if (groupManagement != null)
                                        {
                                            var returnGroupManagementValue = new EntryGroupManagement().Delete(accessor, _GroupSettingsFactory, groupManagement);
                                            if (returnGroupManagementValue.Count > 0)
                                            {
                                                if (new EntryGroupPatient().Delete(groupPatientAccess, groupPatientJnlAccess, ReplaceGroupPatientEntity.ConvertResquestToEntity(itemGroupPatient)))
                                                {
                                                    break;
                                                }
                                                else
                                                {
                                                    result.State = State.Conflict;
                                                    accessor.DisposeConnection();
                                                    return result;
                                                }
                                            }
                                            else
                                            {
                                                result.State = State.Conflict;
                                                accessor.DisposeConnection();
                                                return result;
                                            }
                                        }
                                        else
                                        {
                                            result.State = State.NotFound;
                                            accessor.DisposeConnection();
                                            return result;
                                        }
                                    case ActionFlag.Insert:
                                        var newcode = ReadNewCodeGroupManagement(objPatientContent.FacilityId, objPatientContent.FacilityGroupId);
                                        if (int.Parse(newcode) > 9999)
                                        {
                                            result.State = State.Conflict;
                                            accessor.DisposeConnection();
                                            return result;
                                        }

                                        var newGroupManagementCode = newcode.PadLeft(4, '0');
                                        var newGroupManagement = new EntryGroupManagement().Post(accessor, _GroupSettingsFactory, ReplaceGroupManagementEntity.ConvertPatientPostToManagementEntity(itemGroupPatient, newGroupManagementCode));
                                        if (newGroupManagement.Count == 0)
                                        {
                                            result.State = State.Conflict;
                                            accessor.DisposeConnection();
                                            return result;
                                        }

                                        itemGroupPatient.GroupManagementId = newGroupManagement.Entity.Id;
                                        itemGroupPatient.GroupPatientCode = objPatientContent.GroupPatientCode;
                                        returnValue = new EntryGroupPatient().Post(groupPatientAccess, groupPatientJnlAccess, ReplaceGroupPatientEntity.ConvertResquestToEntity(itemGroupPatient));

                                        if (returnValue.Count <= 0)
                                        {
                                            result.State = State.Conflict;
                                            accessor.DisposeConnection();
                                            return result;
                                        }

                                        result.Content.Add(ReplaceGroupPatientEntity.CreatePatientResponse(returnValue.Entity));
                                        break;
                                    case ActionFlag.Update:
                                        var groupPatient = dataCheck.Content?.SelectMany(x => x.GroupResponseContent)
                                                   .SelectMany(x => x.GroupManagementResponseContent)
                                                   .SelectMany(x => x.GroupPatientResponseContent).Where(x => x.Id == itemGroupPatient.Id).FirstOrDefault();
                                        if (groupPatient != null)
                                        {
                                            itemGroupPatient.LockVersion = groupPatient.LockVersion;
                                            returnValue = new EntryGroupPatient().Put(groupPatientAccess, groupPatientJnlAccess, ReplaceGroupPatientEntity.ConvertResquestToEntity(itemGroupPatient));

                                            if (returnValue.Count <= 0)
                                            {
                                                result.State = State.Conflict;
                                                accessor.DisposeConnection();
                                                return result;
                                            }

                                            result.Content.Add(ReplaceGroupPatientEntity.CreatePatientResponse(returnValue.Entity));
                                            break;
                                        }
                                        else
                                        {
                                            result.State = State.NotFound;
                                            accessor.DisposeConnection();
                                            return result;
                                        }
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    result.State = State.Conflict;
                    result.ErrorJson = BadRequestMessage.ErrorCode1003;
                    accessor.DisposeConnection();
                    return result;
                }
            }
            else
            {
                result.State = State.NotFound;
                accessor.DisposeConnection();
                return result;
            }

            result.State = State.Success;
            // コミット
            accessor.CommitTrans();
            accessor.DisposeConnection();

            return result;

        }

        /// <summary>
        /// Idに該当するリソースを取得する
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Result<List<GroupPatientResponseContent>> ReadGroupPatientById(string id = "")
        {
            var results = new Result<List<GroupPatientResponseContent>>();

            using var accessor = _dbAccessFactory.Create();

            accessor.CreateConnection(CallerInfo.Create(Shared.ToSharedContext()));

            // データ取得
            using var groupPatientAccess = _GroupSettingsFactory.CreateGroupPatientAccess(accessor);

            var returnValues = groupPatientAccess.GetById(id: id);

            results.Content = ReplaceGroupPatientEntity.CreateListPatientResponse(returnValues);

            accessor.DisposeConnection();

            return results;
        }

        /// <summary>
        /// グループ管理IDの取得
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public string GetGroupManagementForCreateGroupPatient(string groupId)
        {
            var returnValues = "";

            using var accessor = _dbAccessFactory.Create();

            accessor.CreateConnection(CallerInfo.Create(Shared.ToSharedContext()));

            // データ取得
            using var groupPatientAccess = _GroupSettingsFactory.CreateGroupPatientAccess(accessor);

            returnValues = groupPatientAccess.GetGroupManagementForCreateGroupPatient(groupId);

            accessor.DisposeConnection();

            return returnValues;
        }

        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // 重複する呼び出しを検出するには
        private object value;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: マネージ状態を破棄します (マネージ オブジェクト)。
                    _dbAccessFactory.Dispose();
                    _dbAccessFactory = null;
                    _GroupSettingsFactory?.Dispose();
                    _GroupSettingsFactory = null;
                }

                // TODO: アンマネージ リソース (アンマネージ オブジェクト) を解放し、下のファイナライザーをオーバーライドします。
                // TODO: 大きなフィールドを null に設定します。

                disposedValue = true;
            }
        }

        // TODO: 上の Dispose(bool disposing) にアンマネージ リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします。
        // ~ProfileModel() {
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
