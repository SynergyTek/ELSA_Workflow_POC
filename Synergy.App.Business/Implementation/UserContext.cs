using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Synergy.App.Business.Interface;
using Synergy.App.Data;
using Synergy.App.Data.Models;
using Synergy.App.Data.ViewModels;

namespace Synergy.App.Business.Implementation
{
    public class UserContext : IUserContext
    {
        public UserContext(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            if (httpContextAccessor.HttpContext.User.Identity?.IsAuthenticated == false)
            {
                return;
            }
            var userId = httpContextAccessor.HttpContext.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier)
                .Value;
            if (string.IsNullOrEmpty(userId))
            {
                return; // Or throw an exception if user is not authenticated
            }

            var u = userManager.FindByIdAsync(userId).Result;
            if (u == null) return;
            Id = u.Id;
            UserName = u?.UserName;
            Email = u?.Email;
        }

        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public Guid UserId { get; set; }
    }
}