using Microsoft.AspNetCore.Mvc;
using Wiseman.PJC.Gen2.Http.Message.ErrorTypes;
using Wiseman.PJC.Gen2.RDB.Interfaces;
using Wiseman.PJC.Gen2.Utility;
using Wiseman.PJC.Gen2.WebApi;
using Wiseman.PJC.Gen2.WebApi.Enums;
using Wiseman.PJC.Gen2.WebApi.Filters;
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
    /// GroupCategoryコントローラクラス
    /// </summary>
    public class GroupCategoryController : ApiControllerBase
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
        public GroupCategoryController() : this(new GroupSettingsLogic())
        {
        }

        /// <summary>
        /// テスト用コンストラクタ
        /// </summary>
        internal GroupCategoryController(IGroupSettingsLogic logic)
        {
            this._groupSettingsLogic = logic;
        }

        #endregion

        #region 方法
        /// <summary>
        /// グループ分類一覧取得
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
        [HttpGet()]
        public IActionResult GetAsync([FromQuery] string groupTani,
                                                 [FromQuery] string groupCategoryCode,
                                                 [FromQuery] string searchWord,
                                                 [FromQuery] bool searchFlag,
                                                 [FromQuery] string areaCorpId,
                                                 [FromQuery] string facilityGroupId,
                                                 [FromQuery] string facilityId,
                                                 [FromQuery] string postId,
                                                 [FromQuery] string categoryCode,
                                                 [FromQuery] short? limit = 1000,
                                                 [FromQuery] short? offset = 0)
        {
            var resultContent = new List<GroupCategoryResponseContent>();

            if (String.IsNullOrWhiteSpace(postId))
            {
                var result = _groupSettingsLogic.GetGroupCategory(groupTani, groupCategoryCode, searchWord, searchFlag, areaCorpId, facilityGroupId, facilityId, postId, categoryCode, limit, offset);

                if (result?.Content?.Count > 0)
                {
                    resultContent = result.Content;
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                var result = _groupSettingsLogic.GetGroupCategoryByPostId(postId);
                if (result?.Content != null)
                {
                    resultContent = result.Content;
                }
                else
                {
                    return NotFound();
                }
            }

            return Ok(resultContent);
        }

        /// <summary>
        /// Idに該当するリソースを取得する
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet()]
        [Route("{id}")]
        public IActionResult GetById([FromRoute] string id)
        {
            // 取得処理実行
            var results = _groupSettingsLogic.GetGroupCategoryById(id: id);
            if (!(results?.Content?.Count > 0))
            {
                // 該当IDのレコードが存在しない場合
                return NotFound();
            }

            return Ok(results.Content[0]);
        }

        /// <summary>
        /// グループ登録
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        [HttpPost()]
        public IActionResult Post([FromBody] GroupCategoryRequestContent content)
        {
            var errors = this.GetValidationError<GroupCategoryBadRequestErrorContent>();

            // Validation結果判定
            if (this.HasValidationError())
            {
                return BadRequest(errors);
            }

            var updatemode = false;
            if (string.IsNullOrWhiteSpace(content.GroupCategoryCode))
            {
                // コード自動附番
                var newcode = _groupSettingsLogic.ReadNewCodeGroupCategory(content.FacilityId);
                if (int.Parse(newcode) > 9999)
                {
                    // 桁あふれはエラーにする
                    errors.ErrorCode1002 = new BasicError()
                    {
                        Message = BadRequestMessage.ErrorCode1002,
                    };

                    return BadRequest(errors);
                }

                if(string.IsNullOrEmpty(newcode) || int.Parse(newcode) == 0)
                {
                    newcode = "0001";
                }

                content.GroupCategoryCode = newcode.PadLeft(4, '0');
            }
            else
            {
                var codecheck = _groupSettingsLogic.ReadByCodeGroupCategory(content.FacilityId, content.GroupCategoryCode, content.FacilityGroupId, content.AreaCorpId);
                if (codecheck?.Content != null && "1".Equals(codecheck.Content.IS_DELETED))
                {
                    // 削除データが存在している場合は復活
                    updatemode = true;
                    content.IsDeleted = "0";
                    content.Id = codecheck.Content.Id;
                    content.LockVersion = codecheck.Content.LockVersion;
                }
                else if (codecheck?.Content != null && "0".Equals(codecheck.Content.IS_DELETED))
                {
                    // あるのに、有効データの場合は、コード重複
                    errors.ErrorCode1001 = new BasicError()
                    {
                        Message = BadRequestMessage.ErrorCode1001,
                    };
                    // コード重複エラーの場合
                    return BadRequest(errors);
                }
            }

            GroupCategoryResponseContent returnValue = null;

            if (!updatemode)
            {
                if (!string.IsNullOrWhiteSpace(content.AreaCorpId))
                {
                    // 登録処理実行
                    var result = _groupSettingsLogic.CreateGroupCategory(content);
                    returnValue = result.Content;
                }
            }
            else
            {
                // 更新処理実行
                var result = _groupSettingsLogic.CreateAndHeaderUpdateGroupCategory(content);

                // エラー確認
                if (result?.State == State.CodeUsed)
                {
                    errors.ErrorCode1001 = new BasicError()
                    {
                        Message = BadRequestMessage.ErrorCode1001,
                    };
                    // コード重複エラーの場合
                    return BadRequest(errors);
                }
                returnValue = result.Content;
            }

            return Ok(returnValue);

        }

        /// <summary>
        /// Putアクションメソッド
        /// </summary>
        /// <returns></returns>
        [HttpPut()]
        [Route("{id}")]
        //[IllegalCustomHeaderFilter(CustomHeaderType.LockVersion)]
        public IActionResult Put([FromRoute] string id, [FromBody] GroupCategoryRequestContent content)
        {

            var errors = this.GetValidationError<GroupCategoryBadRequestErrorContent>();
            content.Id = id;
            // Validation結果判定
            if (this.HasValidationError())
            {
                return BadRequest(errors);
            }

            var returnValue = _groupSettingsLogic.UpdateGroupCategory(content);

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
                return Conflict();
            }

            if (returnValue.State != State.Success)
            {
                var badRequestResponse = new GroupCategoryBadRequestErrorContent();
                if (returnValue?.ErrorJson != null) badRequestResponse.ErrorCode1002 = JsonSerializer.Deserialize<BasicError>(returnValue.ErrorJson);
                return BadRequest(badRequestResponse);
            }
            return Ok(returnValue.Content);
        }

        /// <summary>
        /// グループ削除	
        /// </summary>
        /// <param name="id">サロゲートキー</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] string id)
        {
            var returnValue = _groupSettingsLogic.DeleteGroupCategoryAsync(id);

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