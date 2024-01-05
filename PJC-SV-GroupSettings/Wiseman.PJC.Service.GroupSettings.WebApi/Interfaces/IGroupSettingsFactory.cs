using Wiseman.PJC.Gen2.RDB.Interfaces;
using Wiseman.PJC.Service.GroupSettings.RDB.Interfaces;

namespace Wiseman.PJC.Service.GroupSettings.WebApi.Interfaces
{
    public interface IGroupSettingsFactory : IDisposable
    {
        IGroupCategoryAccess CreateGroupCategoryAccess(IDBAccess dbAccess);
        IGroupCategoryJnlAccess CreateGroupCategoryJnlAccess(IDBAccess dbAccess);
        ICategorySelectedAccess CreateCategorySelectedAccess(IDBAccess dbAccess);
        ICategorySelectedJnlAccess CreateCategorySelectedJnlAccess(IDBAccess dbAccess);
        IGroupAccess CreateGroupAccess(IDBAccess dbAccess);
        IGroupJnlAccess CreateGroupJnlAccess(IDBAccess dbAccess);
        ICategoryAccess CreateCategoryAccess(IDBAccess dbAccess);
        IGroupManagementAccess CreateGroupManagementAccess(IDBAccess dbAccess);
        IGroupManagementJnlAccess CreateGroupManagementJnlAccess(IDBAccess dbAccess);
        IGroupPatientAccess CreateGroupPatientAccess(IDBAccess dbAccess);
        IGroupPatientJnlAccess CreateGroupPatientJnlAccess(IDBAccess dbAccess);
        IGroupStaffAccess CreateGroupStaffAccess(IDBAccess dbAccess);
        IGroupStaffJnlAccess CreateGroupStaffJnlAccess(IDBAccess dbAccess);
    }
}
