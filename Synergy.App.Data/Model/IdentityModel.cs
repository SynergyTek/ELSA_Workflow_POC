using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Synergy.App.Data.Model;

[DisplayColumn("Email")]
public class User : IdentityUser<Guid>;
public class Role : IdentityRole<Guid>;
