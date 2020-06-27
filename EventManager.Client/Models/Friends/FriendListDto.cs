using System;
using System.ComponentModel.DataAnnotations;

namespace EventManager.Client.Models.Friends
{
    public class FriendListDto
    {
        public string FriendId { get; set; }
        public string Friend { get; set; }
        public string FriendFullName { get; set; }
        public string FriendImageTitle { get; set; }
        public byte[] FriendImageData { get; set; }
        public DateTime ConnectionDate { get; set; }

        public string ImageURL(string defaultImage)
        {
            if (FriendImageData.Length != 0)
            {
                var base64 = Convert.ToBase64String(FriendImageData);
                return $"data:image/gif;base64,{base64}";
            }
            return defaultImage;
        }
    }
}