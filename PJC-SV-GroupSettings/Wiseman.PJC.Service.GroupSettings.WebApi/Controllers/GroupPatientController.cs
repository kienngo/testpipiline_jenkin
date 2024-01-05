using Microsoft.AspNetCore.Mvc;
using Wiseman.PJC.Gen2.WebApi;
using Wiseman.PJC.Service.GroupSettings.Http.Message.RequestContents;
using Wiseman.PJC.Service.GroupSettings.Http.Message.ResponseContents;
using Wiseman.PJC.Service.GroupSettings.WebApi.Interfaces;
using Wiseman.PJC.Service.GroupSettings.WebApi.Logics;
using Wiseman.PJC.Service.GroupSettings.WebApi.Properties;
using Wiseman.PJC.Gen2.Http.Message.ErrorTypes;
using Wiseman.PJC.Gen2.Utility;
using Wiseman.PJC.Service.GroupSettings.Http.Message.Enums;
using Wiseman.PJC.Service.GroupSettings.Http.Message.ErrorContent;

namespace Wiseman.PJC.Service.GroupSettings.WebApi.Controllers
{
    /// <summary>
    /// GroupPatientコントローラクラス
    /// </summary>
    public class GroupPatientController : ApiControllerBase
    {
        #region 【定義部】変数
        /// <summary>
        /// 入院基本情報 ビジネスロジッククラス インスタンス
        /// </summary>
        private IGroupSettingsLogic _groupSettingsLogic;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public GroupPatientController() : this(new GroupSettingsLogic())
        {
        }

        /// <summary>
        /// テスト用コンストラクタ
        /// </summary>
        internal GroupPatientController(IGroupSettingsLogic logic)
        {
            this._groupSettingsLogic = logic;
        }
        #endregion

        #region 方法
        /// <summary>
        /// グループ患者リストを取得する
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
        [HttpGet()]
        public IActionResult GetAsync([FromQuery] string groupCategoryCode = "",
                    [FromQuery] string groupTani = "",
                    [FromQuery] string areaCorpId = "",
                    [FromQuery] string facilityGroupId = "",
                    [FromQuery] string facilityId = "",
                    [FromQuery] string groupCode = "",
                    [FromQuery] string validFlag = "",
                    [FromQuery] string groupManagementCode = "",
                    [FromQuery] int? kijunbi = 0,
                    [FromQuery] bool kijunbiFlag = false,
                    [FromQuery] string patientId = "",
                    [FromQuery] string postId = "",
                    [FromQuery] int? limit = 1000,
                    [FromQuery] int? offset = 0)
        {

            var resultContent = new List<GroupCategoryResponseContent>();

            if (String.IsNullOrWhiteSpace(postId))
            {

                var result = _groupSettingsLogic.GetGroupPatientAsync(groupCategoryCode, groupTani, areaCorpId, facilityGroupId,
                            facilityId, groupCode, validFlag, groupManagementCode, kijunbi, kijunbiFlag, patientId, postId, limit, offset);
                if (result?.Content?.Count > 0)
                {
                    resultContent = result.Content;
                }
                else
                {
                    return NotFound();
                }
                return Ok(resultContent);
            }
            else
            {
                resultContent[0].GroupResponseContent = new List<GroupResponseContent>();

                resultContent[0].GroupResponseContent[0].GroupManagementResponseContent = new List<GroupManagementResponseContent>();

                resultContent[0].GroupResponseContent[0].GroupManagementResponseContent[0].GroupPatientResponseContent = new List<GroupPatientResponseContent>();

                var result = _groupSettingsLogic.GetGroupPatientByPostId(postId);

                if (result?.Content != null)
                {
                    resultContent[0].GroupResponseContent[0].GroupManagementResponseContent[0].GroupPatientResponseContent = result.Content;
                }
                else
                {
                    return NotFound();
                }
                return Ok(resultContent);
            }
        }

        /// <summary>
        /// グループ患者アイテムを 1 つ獲得しました
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpGet()]
        [Route("{id}")]
        public IActionResult GetById([FromRoute] string id)
        {
            // 取得処理実行
            var results = _groupSettingsLogic.GetGroupPatientById(id: id);

            if (!(results?.Content?.Count > 0))
            {
                // 該当IDのレコードが存在しない場合
                return NotFound();
            }

            return Ok(results.Content[0]);
        }

        /// <summary>
        /// 患者グループを登録する
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        [HttpPost()]
        public IActionResult PostGroupPatient([FromBody] List<GroupPatientPostContent> content)
        {
            var errors = this.GetValidationError<GroupPatientBadRequestErrorContent>();

            // Validation結果判定
            if (this.HasValidationError())
            {
                return BadRequest(errors);
            }

            // 登録処理実行
            var returnValue = _groupSettingsLogic.CreateGroupPatient(content);

            if (returnValue.State == State.NotFound)
            {
                return NotFound();
            }

            if (returnValue.State == State.CodeUsed)
            {
                errors.ErrorCode1001 = new BasicError()
                {
                    Message = BadRequestMessage.ErrorCode1001,
                };
                return BadRequest(errors);
            }

            if (returnValue.State == State.Conflict)
            {
                if (returnValue?.ErrorJson != null)
                {
                    errors.ErrorCode1003 = new BasicError()
                    {
                        Message = returnValue.ErrorJson,
                    };
                    return Conflict(errors);
                }
                return Conflict();         
            }

            if (returnValue.State != State.Success)
            {
                var badRequestResponse = new GroupPatientBadRequestErrorContent();
                if (returnValue?.ErrorJson != null) badRequestResponse.ErrorCode1002 = JsonSerializer.Deserialize<BasicError>(returnValue.ErrorJson);
                return BadRequest(badRequestResponse);
            }
            return Ok(returnValue?.Content);

        }

        /// <summary>
        /// Putアクションメソッド
        /// </summary>
        /// <returns></returns>
        [HttpPut()]
        public IActionResult Put([FromBody] List<GroupPatientPostContent> content)
        {
            var errors = this.GetValidationError<GroupPatientBadRequestErrorContent>();

            // Validation結果判定
            if (this.HasValidationError())
            {
                return BadRequest(errors);
            }

            var returnValue = _groupSettingsLogic.UpdateGroupPatient(content);

            // エラー確認
            if (returnValue.State == State.NotFound)
            {
                // id 存在チェック
                return NotFound();
            }

            if (returnValue.State == State.CodeUsed)
            {
                errors.ErrorCode1001 = new BasicError()
                {
                    Message = BadRequestMessage.ErrorCode1001,
                };
                //コード重複エラーの場合
                return BadRequest(errors);
            }

            // 楽観排他確認
            if (returnValue.State == State.Conflict)
            {
                if (returnValue?.ErrorJson != null)
                {
                    errors.ErrorCode1003 = new BasicError()
                    {
                        Message = returnValue.ErrorJson,
                    };
                    return Conflict(errors);
                }
                return Conflict();
            }

            if (returnValue.State != State.Success)
            {
                var badRequestResponse = new GroupPatientBadRequestErrorContent();
                if (returnValue?.ErrorJson != null) badRequestResponse.ErrorCode1002 = JsonSerializer.Deserialize<BasicError>(returnValue.ErrorJson);
                return BadRequest(badRequestResponse);
            }
            return Ok(returnValue.Content);
        }

        /// <summary>
        /// グループ患者削除
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        [HttpDelete()]
        public IActionResult Delete([FromQuery] List<string> idList)
        {
            var returnValue = _groupSettingsLogic.DeleteGroupPatientAsync(idList);

            // エラー確認
            if (returnValue.State == State.NotFound)
            {
                // id 存在チェック
                return NotFound();
            }
            return Ok(returnValue.Content);
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
        [HttpGet()]
        [Route("/UnregistGroup")]
        public IActionResult GetUnregistGroupAsync([FromQuery] string searchWord = "",
                                                   [FromQuery] bool searchFlag = false,
                                                   [FromQuery] string areaCorpId = "",
                                                   [FromQuery] string facilityGroupId = "",
                                                   [FromQuery] string facilityId = "",
                                                   [FromQuery] int? kijunbi = 0,
                                                   [FromQuery] string patientId = "",
                                                   [FromQuery] string postId = "",
                                                   [FromQuery] int? limit = 1000,
                                                   [FromQuery] int? offset = 0)
        {

            var resultContent = new List<GroupCategoryResponseContent>();

            var result = _groupSettingsLogic.GetUnregistGroupAsync(searchWord, searchFlag, areaCorpId, facilityGroupId, facilityId, kijunbi, patientId, postId, limit, offset);

            if (result?.Content?.Count > 0)
            {
                resultContent = result.Content;
            }
            else
            {
                return NotFound();
            }

            return Ok(resultContent);
        }
        #endregion
    }
}