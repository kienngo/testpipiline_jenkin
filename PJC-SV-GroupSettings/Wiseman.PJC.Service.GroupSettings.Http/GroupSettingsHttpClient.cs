using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using Wiseman.PJC.Gen2.Http;
using Wiseman.PJC.Gen2.Http.Interfaces;
using Wiseman.PJC.Gen2.ObjectModel;
using Wiseman.PJC.Service.GroupSettings.Http.Interfaces;
using Wiseman.PJC.Service.GroupSettings.Http.Message.RequestContents;
using Wiseman.PJC.Service.GroupSettings.Http.Message.ResponseContents;

namespace Wiseman.PJC.Service.GroupSettings.Http
{
    /// <summary>
    /// HttpClientクラス
    /// </summary>
    public class GroupSettingsHttpClient : IGroupSettingsHttpClient
    {
        /// <summary>HTTPクライアント</summary>
        private readonly IHttpClient _httpClient;
        private static readonly string SERVICE = "GroupSettings";
        private static readonly string VERSION = "v1.0";
        private static readonly string RESOURCE = "GroupCategory";
        private static readonly string RESOURCE2 = "Group";
        private static readonly string RESOURCE3 = "Category";
        private static readonly string RESOURCE4 = "GroupManagement";
        private static readonly string RESOURCE5 = "GroupPatient";
        private static readonly string RESOURCE6 = "UnregistGroup";

        public GroupSettingsHttpClient() : this(new Wiseman.PJC.Gen2.Http.HttpClient())
        {

        }

        /// <summary>
        /// テスト用コンストラクタ
        /// </summary>
        /// <param name="httpClient"></param>
        internal GroupSettingsHttpClient(IHttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        #region GroupCategory
        /// <summary>
        /// グループ分類一覧取得
        /// </summary>
        /// <param name="caller"></param>
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
        public async Task<IResult> GetAsync(CallerInfo caller,
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
            int? offset = 0)
        {
            var uri = $"{SERVICE}/{VERSION}/{RESOURCE}/";
            NameValueCollection queryParams = new NameValueCollection();
            if (!string.IsNullOrWhiteSpace(groupTani))
            {
                queryParams.Add("groupTani", groupTani);
            }
            if (!string.IsNullOrWhiteSpace(groupCategoryCode))
            {
                queryParams.Add("groupCategoryCode", groupCategoryCode);
            }
            if (!string.IsNullOrWhiteSpace(searchWord))
            {
                queryParams.Add("searchWord", searchWord);
            }
            if (searchFlag)
            {
                queryParams.Add("searchFlag", searchFlag.ToString());
            }
            if (!string.IsNullOrWhiteSpace(areaCorpId))
            {
                queryParams.Add("areaCorpId", areaCorpId);
            }
            if (!string.IsNullOrWhiteSpace(facilityGroupId))
            {
                queryParams.Add("facilityGroupId", facilityGroupId);
            }
            if (!string.IsNullOrWhiteSpace(facilityId))
            {
                queryParams.Add("facilityId", facilityId);
            }
            if (!string.IsNullOrWhiteSpace(postId))
            {
                queryParams.Add("postId", postId);
            }
            if (!string.IsNullOrWhiteSpace(categoryCode))
            {
                queryParams.Add("categoryCode", categoryCode);
            }
            if (limit != null)
            {
                queryParams.Add("limit", limit.ToString());
            }
            if (offset != null)
            {
                queryParams.Add("offset", offset.ToString());
            }

            uri += Gen2.Utility.UriEncode.BuildQueryString(queryParams);

            var response = await _httpClient.GetAsync(caller: caller, uri: uri);
            return response.ToResult();
        }

        /// <summary>
        /// グループ分類1件取得
        /// <param name="caller"></param>>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IResult> GetByidAsync(CallerInfo caller, string id)
        {
            var uri = $"{SERVICE}/{VERSION}/{RESOURCE}/{id}";

            var response = await _httpClient.GetAsync(caller: caller, uri: uri);
            return response.ToResult();
        }

        /// <summary>
        /// グループ分類登録
        /// </summary>
        /// <param name="caller"></param>
        /// <param name="content"></param>
        /// <returns>Okコンテンツは<see cref="GroupCategoryResponseContent"/></returns>
        public async Task<IResult> PostAsync(CallerInfo caller, GroupCategoryRequestContent content)
        {
            var uri = $"{SERVICE}/{VERSION}/{RESOURCE}/";
            var apiResult = await _httpClient.PostAsync(caller, uri, content);
            return apiResult.ToResult();
        }

        /// <summary>
        /// グループ分類更新	
        /// </summary>
        /// <param name="caller"></param>
        /// <param name="id"></param>
        /// <param name="content"></param>
        /// <returns>Okコンテンツは<see cref="GroupCategoryResponseContent"/></returns>
        public async Task<IResult> PutAsync(CallerInfo caller, string id, long? lockversion, GroupCategoryRequestContent content)
        {
            var uri = $"{SERVICE}/{VERSION}/{RESOURCE}/{id}";

            var apiResult = await _httpClient.PutAsync(caller, uri, content, lockversion);
            return apiResult.ToResult();
        }

        /// <summary>
        /// グループ分類削除
        /// </summary>
        /// <param name="caller"></param>
        /// <param name="id"></param>
        /// <returns>Okコンテンツは<see cref="GroupCategoryResponseContent"/></returns>
        public async Task<IResult> DeleteAsync(CallerInfo caller, string id)
        {
            var uri = $"{SERVICE}/{VERSION}/{RESOURCE}/{id}";
            var apiResult = await _httpClient.DeleteAsync(caller, uri);
            return apiResult.ToResult();
        }
        #endregion

        #region Group
        /// <summary>
        /// グループ一覧取得	
        /// </summary>
        /// <param name="caller"></param>
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
        public async Task<IResult> GetGroupAsync(CallerInfo caller,
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
            int? offset = 0)
        {
            var uri = $"{SERVICE}/{VERSION}/{RESOURCE2}/";
            NameValueCollection queryParams = new NameValueCollection();
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                queryParams.Add("searchString", searchString);
            }
            if (searchFlag)
            {
                queryParams.Add("searchFlag", searchFlag.ToString());
            }
            if (!string.IsNullOrWhiteSpace(groupCategoryCode))
            {
                queryParams.Add("groupCategoryCode", groupCategoryCode);
            }
            if (!string.IsNullOrWhiteSpace(groupTani))
            {
                queryParams.Add("groupTani", groupTani);
            }
            if (!string.IsNullOrWhiteSpace(groupCode))
            {
                queryParams.Add("groupCode", groupCode);
            }
            if (!string.IsNullOrWhiteSpace(postId))
            {
                queryParams.Add("postId", postId);
            }
            if (!string.IsNullOrWhiteSpace(validFlag))
            {
                queryParams.Add("validFlag", validFlag);
            }
            if (kijunbi != null)
            {
                queryParams.Add("kijunbi", kijunbi.ToString());
            }
            if (kijunbiFlag)
            {
                queryParams.Add("kijunbiFlag", kijunbiFlag.ToString());
            }
            if (limit != null)
            {
                queryParams.Add("limit", limit.ToString());
            }
            if (offset != null)
            {
                queryParams.Add("offset", offset.ToString());
            }

            uri += Gen2.Utility.UriEncode.BuildQueryString(queryParams);

            var response = await _httpClient.GetAsync(caller: caller, uri: uri);
            return response.ToResult();
        }

        /// <summary>
        /// グループ1件取得
        /// <param name="caller"></param>>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IResult> GetGroupByidAsync(CallerInfo caller, string id)
        {
            var uri = $"{SERVICE}/{VERSION}/{RESOURCE2}/{id}";

            var response = await _httpClient.GetAsync(caller: caller, uri: uri);
            return response.ToResult();
        }

        /// <summary>
        /// グループ登録
        /// </summary>
        /// <param name="caller"></param>
        /// <param name="content"></param>
        /// <returns>Okコンテンツは<see cref="GroupResponseContent"/></returns>
        public async Task<IResult> PostGroupAsync(CallerInfo caller, GroupRequestContent content)
        {
            var uri = $"{SERVICE}/{VERSION}/{RESOURCE2}/";
            var apiResult = await _httpClient.PostAsync(caller, uri, content);
            return apiResult.ToResult();
        }

        /// <summary>
        /// グループ更新	
        /// </summary>
        /// <param name="caller"></param>
        /// <param name="id"></param>
        /// <param name="content"></param>
        /// <returns>Okコンテンツは<see cref="GroupResponseContent"/></returns>
        public async Task<IResult> PutGroupAsync(CallerInfo caller, string id, long? lockversion, GroupRequestContent content)
        {
            var uri = $"{SERVICE}/{VERSION}/{RESOURCE2}/{id}";

            var apiResult = await _httpClient.PutAsync(caller, uri, content, lockversion);
            return apiResult.ToResult();
        }

        /// <summary>
        /// グループ削除
        /// </summary>
        /// <param name="caller"></param>
        /// <param name="id"></param>
        /// <returns>Okコンテンツは<see cref="GroupResponseContent"/></returns>
        public async Task<IResult> DeleteGroupAsync(CallerInfo caller, string id)
        {
            var uri = $"{SERVICE}/{VERSION}/{RESOURCE2}/{id}";
            var apiResult = await _httpClient.DeleteAsync(caller, uri);
            return apiResult.ToResult();
        }
        #endregion

        #region カテゴリー(Category)
        /// <summary>
        /// カテゴリー一覧取得
        /// </summary>
        /// <param name="caller"></param>
        /// <param name="categoryCode"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public async Task<IResult> GetCategoryAsync(CallerInfo caller,
            string categoryCode = "",
            int? limit = 1000,
            int? offset = 0)
        {
            var uri = $"{SERVICE}/{VERSION}/{RESOURCE3}/";
            NameValueCollection queryParams = new NameValueCollection();

            if (!string.IsNullOrWhiteSpace(categoryCode))
            {
                queryParams.Add("categoryCode", categoryCode);
            }
            if (limit != null)
            {
                queryParams.Add("limit", limit.ToString());
            }
            if (offset != null)
            {
                queryParams.Add("offset", offset.ToString());
            }

            uri += Gen2.Utility.UriEncode.BuildQueryString(queryParams);

            var response = await _httpClient.GetAsync(caller: caller, uri: uri);
            return response.ToResult();
        }
        #endregion

        #region グループ管理(GroupManagement)
        /// <summary>
        /// グループ管理一覧取得
        /// </summary>
        /// <param name="caller"></param>
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
        public async Task<IResult> GetGroupManagementAsync(CallerInfo caller, 
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
                        int? offset = 0)
        {
            var uri = $"{SERVICE}/{VERSION}/{RESOURCE4}/";
            NameValueCollection queryParams = new NameValueCollection();
            if (!string.IsNullOrWhiteSpace(groupCategoryCode))
            {
                queryParams.Add("groupCategoryCode", groupCategoryCode);
            }
            if (!string.IsNullOrWhiteSpace(groupTani))
            {
                queryParams.Add("groupTani", groupTani);
            }
            if (!string.IsNullOrWhiteSpace(areaCorpId))
            {
                queryParams.Add("areaCorpId", areaCorpId);
            }
            if (!string.IsNullOrWhiteSpace(facilityGroupId))
            {
                queryParams.Add("facilityGroupId", facilityGroupId);
            }
            if (!string.IsNullOrWhiteSpace(facilityId))
            {
                queryParams.Add("facilityId", facilityId);
            }
            if (!string.IsNullOrWhiteSpace(groupCode))
            {
                queryParams.Add("groupCode", groupCode);
            }
            if (!string.IsNullOrWhiteSpace(validFlag))
            {
                queryParams.Add("validFlag", validFlag);
            }
            if (kijunbi != null)
            {
                queryParams.Add("kijunbi", kijunbi.ToString());
            }
            if (kijunbiFlag)
            {
                queryParams.Add("kijunbiFlag", kijunbiFlag.ToString());
            }
            if (!string.IsNullOrWhiteSpace(groupManagementCode))
            {
                queryParams.Add("groupManagementCode", groupManagementCode);
            }
            if (!string.IsNullOrWhiteSpace(postId))
            {
                queryParams.Add("postId", postId);
            }
            if (limit != null)
            {
                queryParams.Add("limit", limit.ToString());
            }
            if (offset != null)
            {
                queryParams.Add("offset", offset.ToString());
            }

            uri += Gen2.Utility.UriEncode.BuildQueryString(queryParams);

            var response = await _httpClient.GetAsync(caller: caller, uri: uri);
            return response.ToResult();
        }

        /// <summary>
        /// グループ管理1件取得
        /// </summary>
        /// <param name="caller"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IResult> GetGroupManagementByidAsync(CallerInfo caller, string id)
        {
            var uri = $"{SERVICE}/{VERSION}/{RESOURCE4}/{id}";

            var response = await _httpClient.GetAsync(caller: caller, uri: uri);
            return response.ToResult();
        }

        /// <summary>
        /// グループ管理登録
        /// </summary>
        /// <param name="caller"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<IResult> PostGroupManagementAsync(CallerInfo caller, GroupManagementPostContent content)
        {
            var uri = $"{SERVICE}/{VERSION}/{RESOURCE4}/";
            var apiResult = await _httpClient.PostAsync(caller, uri, content);
            return apiResult.ToResult();
        }

        /// <summary>
        /// グループ管理更新
        /// </summary>
        /// <param name="caller"></param>
        /// <param name="id"></param>
        /// <param name="lockversion"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<IResult> PutGroupManagementAsync(CallerInfo caller, string id, long? lockversion, GroupManagementPostContent content)
        {
            var uri = $"{SERVICE}/{VERSION}/{RESOURCE4}/{id}";

            var apiResult = await _httpClient.PutAsync(caller, uri, content, lockversion);
            return apiResult.ToResult();
        }

        /// <summary>
        /// グループ管理削除
        /// </summary>
        /// <param name="caller"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IResult> DeleteGroupManagementAsync(CallerInfo caller, string id)
        {
            var uri = $"{SERVICE}/{VERSION}/{RESOURCE4}/{id}";
            var apiResult = await _httpClient.DeleteAsync(caller, uri);
            return apiResult.ToResult();
        }
        #endregion

        #region GroupPatient

        /// <summary>
        /// グループ患者リストを取得する
        /// </summary>
        /// <param name="caller"></param>
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
        /// <param name="patientId">患者ID</param>
        /// <param name="postId">POST識別子</param>
        /// <param name="limit">取得上限件数</param>
        /// <param name="offset">オフセット</param>
        /// <returns></returns>
        public async Task<IResult> GetGroupPatientAsync(CallerInfo caller,
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
                        int? offset = 0)
        {
            var uri = $"{SERVICE}/{VERSION}/{RESOURCE5}/";
            NameValueCollection queryParams = new NameValueCollection();
            if (!string.IsNullOrWhiteSpace(groupCategoryCode))
            {
                queryParams.Add("groupCategoryCode", groupCategoryCode);
            }
            if (!string.IsNullOrWhiteSpace(groupTani))
            {
                queryParams.Add("groupTani", groupTani);
            }
            if (!string.IsNullOrWhiteSpace(areaCorpId))
            {
                queryParams.Add("areaCorpId", areaCorpId);
            }
            if (!string.IsNullOrWhiteSpace(facilityGroupId))
            {
                queryParams.Add("facilityGroupId", facilityGroupId);
            }
            if (!string.IsNullOrWhiteSpace(facilityId))
            {
                queryParams.Add("facilityId", facilityId);
            }
            if (!string.IsNullOrWhiteSpace(groupCode))
            {
                queryParams.Add("groupCode", groupCode);
            }
            if (!string.IsNullOrWhiteSpace(validFlag))
            {
                queryParams.Add("validFlag", validFlag);
            }
            if (kijunbi != null)
            {
                queryParams.Add("kijunbi", kijunbi.ToString());
            }
            if (kijunbiFlag)
            {
                queryParams.Add("kijunbiFlag", kijunbiFlag.ToString());
            }
            if (!string.IsNullOrWhiteSpace(groupManagementCode))
            {
                queryParams.Add("groupManagementCode", groupManagementCode);
            }
            if (!string.IsNullOrWhiteSpace(patientId))
            {
                queryParams.Add("patientId", patientId);
            }
            if (!string.IsNullOrWhiteSpace(postId))
            {
                queryParams.Add("postId", postId);
            }
            if (limit != null)
            {
                queryParams.Add("limit", limit.ToString());
            }
            if (offset != null)
            {
                queryParams.Add("offset", offset.ToString());
            }

            uri += Gen2.Utility.UriEncode.BuildQueryString(queryParams);

            var response = await _httpClient.GetAsync(caller: caller, uri: uri);
            return response.ToResult();
        }

        /// <summary>
        /// グループ患者アイテムを 1 つ獲得しました
        /// </summary>
        /// <param name="caller"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IResult> GetGroupPatientByidAsync(CallerInfo caller, string id)
        {
            var uri = $"{SERVICE}/{VERSION}/{RESOURCE5}/{id}";

            var response = await _httpClient.GetAsync(caller: caller, uri: uri);

            return response.ToResult();
        }

        /// <summary>
        /// グループ患者登録
        /// </summary>
        /// <param name="caller"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<IResult> PostGroupPatientAsync(CallerInfo caller, List<GroupPatientPostContent> content)
        {
            var uri = $"{SERVICE}/{VERSION}/{RESOURCE5}/";
            var apiResult = await _httpClient.PostAsync(caller, uri, content);
            return apiResult.ToResult();
        }

        /// <summary>
        /// グループ患者の最新情報
        /// </summary>
        /// <param name="caller"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<IResult> PutGroupPatientAsync(CallerInfo caller, List<GroupPatientPostContent> content)
        {
            var uri = $"{SERVICE}/{VERSION}/{RESOURCE5}/";

            var apiResult = await _httpClient.PutAsync(caller, uri, content);
            return apiResult.ToResult();
        }

        /// <summary>
        /// グループ患者の削除
        /// </summary>
        /// <param name="caller"></param>
        /// <param name="idList"></param>
        /// <returns></returns>
        public async Task<IResult> DeleteGroupPatientAsync(CallerInfo caller, List<string> idList)
        {
            var uri = $"{SERVICE}/{VERSION}/{RESOURCE5}/";
            var queryParams = new NameValueCollection();
            idList.ForEach(Id => queryParams.Add("idList", Id));
            uri += Gen2.Utility.UriEncode.BuildQueryString(queryParams);
            var apiResult = await _httpClient.DeleteAsync(caller, uri);
            return apiResult.ToResult();
        }

        #endregion

        #region UnregistGroup

        /// <summary>
        /// 未登録グループのリストを取得する
        /// </summary>
        /// <param name="caller"></param>
        /// <param name="areaCorpId">地域法人グループ</param>
        /// <param name="facilityGroupId">医療機関・施設グループID</param>
        /// <param name="facilityId">医療機関・施設ID</param>
        /// <param name="kijunbi">基準日</param>
        /// <param name="patientId">患者ID</param>
        /// <param name="postId">POST識別子</param>
        /// <param name="limit">取得上限件数</param>
        /// <param name="offset">オフセット</param>
        /// <returns></returns>
        public async Task<IResult> GetUnregistGroupAsync(CallerInfo caller,
                        string searchWord = "",
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
            var uri = $"{SERVICE}/{VERSION}/{RESOURCE6}/";
            NameValueCollection queryParams = new NameValueCollection();
            if (!string.IsNullOrWhiteSpace(searchWord))
            {
                queryParams.Add("searchWord", searchWord);
            }
            if (searchFlag)
            {
                queryParams.Add("searchFlag", searchFlag.ToString());
            }
            if (!string.IsNullOrWhiteSpace(areaCorpId))
            {
                queryParams.Add("areaCorpId", areaCorpId);
            }
            if (!string.IsNullOrWhiteSpace(facilityGroupId))
            {
                queryParams.Add("facilityGroupId", facilityGroupId);
            }
            if (!string.IsNullOrWhiteSpace(facilityId))
            {
                queryParams.Add("facilityId", facilityId);
            }
            if (kijunbi != null)
            {
                queryParams.Add("kijunbi", kijunbi.ToString());
            }
            if (!string.IsNullOrWhiteSpace(patientId))
            {
                queryParams.Add("patientId", patientId);
            }
            if (!string.IsNullOrWhiteSpace(postId))
            {
                queryParams.Add("postId", postId);
            }
            if (limit != null)
            {
                queryParams.Add("limit", limit.ToString());
            }
            if (offset != null)
            {
                queryParams.Add("offset", offset.ToString());
            }

            uri += Gen2.Utility.UriEncode.BuildQueryString(queryParams);

            var response = await _httpClient.GetAsync(caller: caller, uri: uri);
            return response.ToResult();
        }

        #endregion

        #region Dispose
        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: マネージド状態を破棄します (マネージド オブジェクト)
                }

                // TODO: アンマネージド リソース (アンマネージド オブジェクト) を解放し、ファイナライザーをオーバーライドします
                // TODO: 大きなフィールドを null に設定します
                disposedValue = true;
            }
        }

        // // TODO: 'Dispose(bool disposing)' にアンマネージド リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします
        // ~xxxHttpClient()
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
