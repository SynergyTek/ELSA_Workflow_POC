using Synergy.App.Business.Interface;
using Synergy.App.Data;
using Synergy.App.Data.Models;
using Synergy.App.Data.ViewModels;

namespace Synergy.App.Business.Implementation;

public class LeaveBusiness(IContextBase<LeaveViewModel, Leave> repo, ApplicationDbContext context)
    : BaseBusiness<LeaveViewModel, Leave>(repo), ILeaveBusiness
{
    
}