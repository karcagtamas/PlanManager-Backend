using EventManager.Client.Services;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManager.Client.Pages.Friends
{
    public class MyFriendsBase : ComponentBase
    {
        [Inject]
        public IFriendService FriendService { get; set; }

        [Inject]
        public IMatToaster Toaster { get; set; }
    }
}
