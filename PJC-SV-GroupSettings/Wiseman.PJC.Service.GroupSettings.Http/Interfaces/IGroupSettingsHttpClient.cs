using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiseman.PJC.Gen2.Http.Interfaces;
using Wiseman.PJC.Gen2.ObjectModel;
using Wiseman.PJC.Service.GroupSettings.Http.Message.RequestContents;

namespace Wiseman.PJC.Service.GroupSettings.Http.Interfaces
{
    /// <summary>
    /// サンプルHttpClientインタフェース
    /// </summary>
    public interface IGroupSettingsHttpClient : IDisposable
    {
        #region GroupCategory
        // グループ分類一覧取得
        Task<IResult> GetAsync(CallerInfo caller,
            string groupTani = "",
            string groupCategoryCode = "",
            string searchWord = "",
            bool searchFlag = false,
            string areaCorpId = "",
            string facilityGroupId = "",
            string facilityId = "",
            string postId = "",
            string categoryCode = "",
            int? limit = 1000,
            int? offset = 0);

        // グループ分類1件取得
        Task<IResult> GetByidAsync(CallerInfo caller, string id);

        // グループ分類登録
        Task<IResult> PostAsync(CallerInfo caller, GroupCategoryRequestContent content);

        // グループ分類更新	
        Task<IResult> PutAsync(CallerInfo caller, string id, long? lockversion, GroupCategoryRequestContent content);

        // グループ分類削除
        Task<IResult> DeleteAsync(CallerInfo caller, string id);
        #endregion

        #region Group
        // グループ一覧取得	
        Task<IResult> GetGroupAsync(CallerInfo caller,
            string searchString = "",
            bool searchFlag = false,
            string groupCategoryCode = "",
            string groupTani = "",
            string groupCode = "",
            string postId = "",
            string validFlag = "",
            int? kijunbi = 0,
            bool kijunbiFlag = false,
            int? limit = 1000,
            int? offset = 0);

        // グループ1件取得
        Task<IResult> GetGroupByidAsync(CallerInfo caller, string id);

        // グループ登録
        Task<IResult> PostGroupAsync(CallerInfo caller, GroupRequestContent content);

        // グループ更新	
        Task<IResult> PutGroupAsync(CallerInfo caller, string id, long? lockversion, GroupRequestContent content);

        // グループ削除
        Task<IResult> DeleteGroupAsync(CallerInfo caller, string id);
        #endregion

        #region Category
        // カテゴリー一覧取得
        Task<IResult> GetCategoryAsync(CallerInfo caller,
            string categoryCode = "",
            int? limit = 1000,
            int? offset = 0);
        #endregion

        #region GroupManagement
        // グループ管理一覧取得	
        Task<IResult> GetGroupManagementAsync(CallerInfo caller,
            string groupCategoryCode = "",
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

        // グループ1件取得
        Task<IResult> GetGroupManagementByidAsync(CallerInfo caller, string id);

        // グループ管理登録
        Task<IResult> PostGroupManagementAsync(CallerInfo caller, GroupManagementPostContent content);

        // グループ管理更新	
        Task<IResult> PutGroupManagementAsync(CallerInfo caller, string id, long? lockversion, GroupManagementPostContent content);

        // グループ管理削除
        Task<IResult> DeleteGroupManagementAsync(CallerInfo caller, string id);
        #endregion

        #region Group Patient
        // グループ患者リストを取得する
        Task<IResult> GetGroupPatientAsync(CallerInfo caller,
                        string groupCategoryCode = "",
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

        //グループ患者アイテムを 1 つ獲得しました
        Task<IResult> GetGroupPatientByidAsync(CallerInfo caller, string id);

        //グループ患者登録
        Task<IResult> PostGroupPatientAsync(CallerInfo caller, List<GroupPatientPostContent> content);

        //グループ患者の最新情報
        Task<IResult> PutGroupPatientAsync(CallerInfo caller, List<GroupPatientPostContent> content);

        //グループ患者の削除
        Task<IResult> DeleteGroupPatientAsync(CallerInfo caller, List<string> idList);
        #endregion

        #region UnregistGroup
        //未登録グループのリストを取得する
        Task<IResult> GetUnregistGroupAsync(CallerInfo caller,
                        string searchWord = "",
                        bool searchFlag = false,
                        string areaCorpId = "",
                        string facilityGroupId = "",
                        string facilityId = "",
                        int? kijunbi = 0,
                        string patientId = "",
                        string postId = "",
                        int? limit = 1000,
                        int? offset = 0);
        #endregion
    }
}
