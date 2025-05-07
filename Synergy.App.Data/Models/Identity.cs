using Microsoft.AspNetCore.Identity;

namespace Synergy.App.Data.Models;

public class User : IdentityUser<Guid>;
public class Role : IdentityRole<Guid>;