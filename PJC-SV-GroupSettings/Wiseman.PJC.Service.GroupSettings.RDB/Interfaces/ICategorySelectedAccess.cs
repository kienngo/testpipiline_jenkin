using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiseman.PJC.Gen2.RDB.Entities;
using Wiseman.PJC.Service.GroupSettings.RDB.Entities;

namespace Wiseman.PJC.Service.GroupSettings.RDB.Interfaces
{
    public interface ICategorySelectedAccess : IDisposable
    {

        /// <summary>
        /// Idに該当するリソースを取得する
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        IList<CategorySelectedEntity> GetById(string id);
        /// <summary>
        /// レコードを新規作成する
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        Result<CategorySelectedEntity> Create(CategorySelectedEntity content);
        /// <summary>
        /// 削除のための更新
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        bool Delete(string id);
    }
}
