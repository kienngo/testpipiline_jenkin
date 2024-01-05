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
    /// Categoryコントローラクラス
    /// </summary>
    public class CategoryController : ApiControllerBase
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
        public CategoryController() : this(new GroupSettingsLogic())
        {
        }

        /// <summary>
        /// テスト用コンストラクタ
        /// </summary>
        internal CategoryController(IGroupSettingsLogic logic)
        {
            this._groupSettingsLogic = logic;
        }

        #endregion

        #region 方法
        /// <summary>
        /// カテゴリー一覧取得
        /// </summary>
        /// <param name="CategoryCode">カテゴリコード</param>
        /// <param name="limit">取得上限件数</param>
        /// <param name="offset">オフセット</param>
        [HttpGet()]
        public IActionResult GetAsync([FromQuery] string CategoryCode = "",
                                [FromQuery] short limit = 1000,
                                [FromQuery] short offset = 0)
        {

            var resultcontent = new List<CategoryResponseContent>();

            var result = _groupSettingsLogic.GetListCategorySearch(CategoryCode, limit, offset);

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
        #endregion

    }
}