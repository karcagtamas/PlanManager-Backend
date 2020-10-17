using ManagerAPI.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ManagerAPI.Services.Services.Interfaces
{
    public interface IUtilsService
    {
        User GetCurrentUser();
        string GetCurrentUserId();
        string UserDisplay(User user);
        string InjectString(string baseText, params string[] args);
        string ErrorsToString(IEnumerable<IdentityError> errors);
    }
}