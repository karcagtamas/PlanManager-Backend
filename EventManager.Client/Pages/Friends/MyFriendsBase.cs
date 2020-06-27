using EventManager.Client.Models.Friends;
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
        private IFriendService FriendService { get; set; }

        [Inject]
        protected IHelperService HelperService { get; set; }

        [Inject]
        private IMatToaster Toaster { get; set; }

        protected List<FriendListDto> Friends { get; set; } = null;
        protected List<FriendRequestListDto> FriendRequests { get; set; } = null;

        protected override async Task OnInitializedAsync()
        {
            await GetFriends();
            await GetFriendRequests();
        }

        protected async Task GetFriendRequests()
        {
            this.FriendRequests = await FriendService.GetMyFriendRequests();
        }

        protected async Task GetFriends()
        {
            this.Friends = await FriendService.GetMyFriends();
            for (int i = 0; i < this.Friends.Count; i++)
            {

            }
        }
    }
}
