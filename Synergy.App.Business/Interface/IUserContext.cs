using Microsoft.AspNetCore.Http;
using Synergy.App.Data.Model;
using Synergy.App.Data.ViewModel;

namespace Synergy.App.Business.Interface
{
    public interface IUserContext
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public Guid UserId { get; }
        public User User { get; set; }

    }
}