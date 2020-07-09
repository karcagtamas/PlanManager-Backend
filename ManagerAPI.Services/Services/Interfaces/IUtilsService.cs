using System;
using ManagerAPI.Models.Entities;
using ManagerAPI.Models.Models;

namespace ManagerAPI.Services.Services.Interfaces 
{
    public interface IUtilsService 
    {
        User GetCurrentUser ();
        string GetCurrentUserId ();
        string UserDisplay(User user);
    }
}