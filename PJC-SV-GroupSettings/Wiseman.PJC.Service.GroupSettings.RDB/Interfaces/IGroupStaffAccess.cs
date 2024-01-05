using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiseman.PJC.Gen2.RDB.Entities;
using Wiseman.PJC.Service.GroupSettings.RDB.Entities;

namespace Wiseman.PJC.Service.GroupSettings.RDB.Interfaces
{
    public interface IGroupStaffAccess : IDisposable
    {
        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        Result<GroupStaffEntity> Create(GroupStaffEntity content);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        Result<GroupStaffEntity> Update(GroupStaffEntity content);

        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(string id);
    }
}
