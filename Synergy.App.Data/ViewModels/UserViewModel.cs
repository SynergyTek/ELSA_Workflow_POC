using Synergy.App.Data.Models;

namespace Synergy.App.Data.ViewModels;

public class UserViewModel:User
{
    public IList<string> Roles { get; set; } = null!;
}