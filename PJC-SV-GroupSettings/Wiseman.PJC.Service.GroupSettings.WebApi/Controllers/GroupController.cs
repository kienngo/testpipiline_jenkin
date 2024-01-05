using Microsoft.AspNetCore.Mvc;
using Wiseman.PJC.Gen2.Http.Message.ErrorTypes;
using Wiseman.PJC.Gen2.Utility;
using Wiseman.PJC.Gen2.WebApi;
using Wiseman.PJC.Service.GroupSettings.Http.Message.Enums;
using Wiseman.PJC.Service.GroupSettings.Http.Message.ErrorContent;
using Wiseman.PJC.Service.GroupSettings.Http.Message.RequestContents;
using Wiseman.PJC.Service.GroupSettings.Http.Message.ResponseContents;
using Wiseman.PJC.Service.GroupSettings.WebApi.Entities;
using Wiseman.PJC.Service.GroupSettings.WebApi.Interfaces;
using Wiseman.PJC.Service.GroupSettings.WebApi.Logics;
using Wiseman.PJC.Service.GroupSettings.WebApi.Properties;

namespace Wiseman.PJC.Service.GroupSettings.WebApi.Controllers
{
    /// <summary>
    /// Groupコントローラクラス
    /// </summary>
    public class GroupController : ApiControllerBase
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
        public GroupController() : this(new GroupSettingsLogic())
        {
        }

        /// <summary>
        /// テスト用コンストラクタ
        /// </summary>
        internal GroupController(IGroupSettingsLogic logic)
        {
            this._groupSettingsLogic = logic;
        }

        #endregion

        #region 方法 
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
        [HttpGet()]
        public IActionResult GetAsync([FromQuery] string searchString = "",
                                          [FromQuery] bool searchFlag = false,
                                          [FromQuery] string groupCategoryCode = "",
                                          [FromQuery] string groupTani = "",
                                          [FromQuery] string groupCode = "",
                                          [FromQuery] string postId = "",
                                          [FromQuery] string validFlag = "",
                                          [FromQuery] int? kijunbi = 0,
                                          [FromQuery] bool kijunbiFlag = false,
                                          [FromQuery] int? limit = 1000,
                                          [FromQuery] int? offset = 0)
        {

            var resultcontent = new List<GroupCategoryGroupRequestContent>();

            var result = _groupSettingsLogic.GetGroupAsync(searchString, searchFlag, groupCategoryCode, groupTani, groupCode, postId, validFlag, kijunbi, kijunbiFlag, limit, offset);

            if (result?.Content?.Count > 0)
            {
                resultcontent = result.Content;
            }
            else
            {
                return NotFound();
            }

            return Ok(resultcontent);
        }

        /// <summary>
        /// IDグループ一覧取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet()]
        [Route("{id}")]
        public IActionResult GetById([FromRoute] string id)
        {
            var result = _groupSettingsLogic.GetGroupByIdAsync(id);

            if (result?.Content?.Count > 0)
            {
                return Ok(result.Content[0]);
            }
            else
            {
                return NotFound();
            }
        }
        /// <summary>
        /// グループ登録		
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        [HttpPost()]
        public IActionResult Post([FromBody] GroupRequestContent content)
        {
            var errors = this.GetValidationError<GroupBadRequestErrorContent>();

            if (this.HasValidationError())
            {
                return BadRequest(errors);
            }

            var updatemode = false;

            if (string.IsNullOrWhiteSpace(content.GroupCode))
            {

                var newcode = _groupSettingsLogic.ReadNewCodeGroup(content.GroupCategoryId);
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

                content.GroupCode = newcode.PadLeft(4, '0');
            }
            else
            {
                var codecheck = _groupSettingsLogic.ReadByCodeGroup( content.GroupCode, content.FacilityId, content.FacilityGroupId, content.AreaCorpId);
                if (codecheck?.Content != null && codecheck.Content.IS_DELETED.Equals("1"))
                {
                    updatemode = true;
                    content.Id = codecheck.Content.ID;
                    content.LockVersion = codecheck.Content.LOCKVERSION;
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

            var result = new Result<GroupCategoryGroupRequestContent>();
            if (!updatemode)
            {
                result = _groupSettingsLogic.CreateGroupAsync(content);

                if (result?.State == State.Conflict)
                {
                    return Conflict();
                }
            }
            else
            {
                result = _groupSettingsLogic.CreateAndHeaderUpdateGroupAsync(content);

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
        /// グループ更新		
        /// </summary>
        /// <param name="id"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        [HttpPut()]
        [Route("{id}")]
        public IActionResult Put([FromRoute] string id, [FromBody] GroupRequestContent content)
        {
            var errors = this.GetValidationError<GroupBadRequestErrorContent>();
            content.Id = id;

            // Validation結果判定
            if (this.HasValidationError())
            {
                return BadRequest(errors);
            }

            var returnValue = _groupSettingsLogic.UpdateGroupAsync(content);

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
                var badRequestResponse = new GroupBadRequestErrorContent();
                if (returnValue?.ErrorJson != null) badRequestResponse.ErrorCode1002 = JsonSerializer.Deserialize<BasicError>(returnValue.ErrorJson);
                return BadRequest(badRequestResponse);
            }
            return Ok(returnValue.Content);
        }
        /// <summary>
        /// グループ削除		
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] string id)
        {
            var returnValue = _groupSettingsLogic.DeleteGroupAsync(id);

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