using Synergy.App.Data.Model;

namespace Synergy.App.Data.ViewModel;

public class UserViewModel:User
{
    public IList<string> Roles { get; set; } = null!;
}