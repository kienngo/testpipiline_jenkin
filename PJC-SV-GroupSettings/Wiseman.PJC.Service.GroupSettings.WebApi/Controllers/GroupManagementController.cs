using Microsoft.AspNetCore.Mvc;
using Wiseman.PJC.Gen2.WebApi;
using Wiseman.PJC.Service.GroupSettings.Http.Message.RequestContents;
using Wiseman.PJC.Service.GroupSettings.Http.Message.ResponseContents;
using Wiseman.PJC.Service.GroupSettings.Http.Message.ErrorContents;
using Wiseman.PJC.Service.GroupSettings.WebApi.Interfaces;
using Wiseman.PJC.Service.GroupSettings.WebApi.Logics;
using Wiseman.PJC.Service.GroupSettings.WebApi.Properties;
using Wiseman.PJC.Gen2.Http.Message.ErrorTypes;
using Wiseman.PJC.Gen2.Utility;
using Wiseman.PJC.Service.GroupSettings.Http.Message.Enums;
using Wiseman.PJC.Service.GroupSettings.Http.Message.ErrorContent;
using Wiseman.PJC.Service.GroupSettings.WebApi.Entities;

namespace Wiseman.PJC.Service.GroupSettings.WebApi.Controllers
{
    /// <summary>
    /// GroupManagementコントローラクラス
    /// </summary>
    public class GroupManagementController : ApiControllerBase
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
        public GroupManagementController() : this(new GroupSettingsLogic())
        {
        }

        /// <summary>
        /// テスト用コンストラクタ
        /// </summary>
        internal GroupManagementController(IGroupSettingsLogic logic)
        {
            this._groupSettingsLogic = logic;
        }
        #endregion

        #region 方法
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
        [HttpGet()]
        public IActionResult GetAsync([FromQuery] string groupCategoryCode = "",
                    [FromQuery] string groupTani = "",
                    [FromQuery] string areaCorpId = "",
                    [FromQuery] string facilityGroupId = "",
                    [FromQuery] string facilityId = "",
                    [FromQuery] string groupCode = "",
                    [FromQuery] string validFlag = "",
                    [FromQuery] int? kijunbi = 0,
                    [FromQuery] bool kijunbiFlag = false,
                    [FromQuery] string groupManagementCode = "",
                    [FromQuery] string postId = "",
                    [FromQuery] int? limit = 1000,
                    [FromQuery] int? offset = 0)
        {
            var resultContent = new List<GroupCategoryResponseContent>();

            var result = _groupSettingsLogic.GetGroupManagementAsync(groupCategoryCode, groupTani, areaCorpId, facilityGroupId,
                                        facilityId, groupCode, validFlag, kijunbi, kijunbiFlag, groupManagementCode, postId, limit, offset);

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

        /// <summary>
        /// グループ管理1件取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet()]
        [Route("{id}")]
        public IActionResult GetById([FromRoute] string id)
        {
            // 取得処理実行
            var results = _groupSettingsLogic.GetGroupManagementById(id: id);
            if (!(results?.Content?.Count > 0))
            {
                // 該当IDのレコードが存在しない場合
                return NotFound();
            }

            return Ok(results.Content[0]);
        }

        /// <summary>
        /// グループ管理登録
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        [HttpPost()]
        public IActionResult Post([FromBody] GroupManagementPostContent content)
        {
            var errors = this.GetValidationError<GroupBadRequestErrorContent>();

            if (this.HasValidationError())
            {
                return BadRequest(errors);
            }

            var updatemode = false;

            if (string.IsNullOrWhiteSpace(content.GroupManagementCode))
            {

                var newcode = _groupSettingsLogic.ReadNewCodeGroupManagement(content.FacilityId, content.FacilityGroupId);
                if (int.Parse(newcode) > 9999)
                {
                    errors.ErrorCode1002 = new BasicError()
                    {
                        Message = BadRequestMessage.ErrorCode1002,
                    };

                    return BadRequest(errors);
                }

                if (string.IsNullOrWhiteSpace(newcode) || int.Parse(newcode) > 9999)
                {
                    newcode = "0001";
                }

                content.GroupManagementCode = newcode.PadLeft(4, '0');
            }
            else
            {
                var codecheck = _groupSettingsLogic.ReadByCodeGroupManagement(content.GroupManagementCode, content.FacilityId, content.FacilityGroupId);
                if (codecheck?.Content != null && codecheck.Content.IS_DELETED.Equals("1"))
                {
                    updatemode = true;
                    content.Id = codecheck.Content.Id;
                    content.LockVersion = codecheck.Content.LockVersion;
                }
                else if (codecheck?.Content != null && codecheck.Content.IS_DELETED.Equals("0"))
                {
                    errors.ErrorCode1001 = new BasicError()
                    {
                        Message = BadRequestMessage.ErrorCode1001,
                    };

                    return BadRequest(errors);
                }
            }

            var result = new Result<GroupManagementResponseContent>();
            if (!updatemode)
            {
                result = _groupSettingsLogic.CreateGroupManagementAsync(content);

                if (result?.State == State.Conflict)
                {
                    return Conflict();
                }
            }
            else
            {
                result = _groupSettingsLogic.CreateHeaderGroupManagementAsync(content);

                if (result?.State == State.CodeUsed)
                {
                    errors.ErrorCode1001 = new BasicError()
                    {
                        Message = BadRequestMessage.ErrorCode1001,
                    };
                    return BadRequest(errors);
                }
            }

            return Ok(result?.Content);
        }

        /// <summary>
        /// グループ管理更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] string id, [FromBody] GroupManagementPostContent content)
        {
            var errors = this.GetValidationError<GroupManagementBadRequestErrorContent>();
            content.Id = id;

            // Validation結果判定
            if (this.HasValidationError())
            {
                return BadRequest(errors);
            }

            var returnValue = _groupSettingsLogic.UpdateGroupManagementAsync(content);

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
                return Conflict();
            }

            if (returnValue.State != State.Success)
            {
                var badRequestResponse = new GroupManagementBadRequestErrorContent();
                if (returnValue?.ErrorJson != null) badRequestResponse.ErrorCode1002 = JsonSerializer.Deserialize<BasicError>(returnValue.ErrorJson);
                return BadRequest(badRequestResponse);
            }
            return Ok(returnValue.Content);
        }

        /// <summary>
        /// グループ管理削除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] string id)
        {
            var returnValue = _groupSettingsLogic.DeleteGroupManagementAsync(id);

            // エラー確認
            if (returnValue.State == State.NotFound)
            {
                // id 存在チェック
                return NotFound();
            }
            return Ok(returnValue.Content);
        }
        #endregion
    }
}