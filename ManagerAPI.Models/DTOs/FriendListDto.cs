using System;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Models.DTOs
{
    public class FriendListDto
    {
        public string FriendId { get; set; }
        public string Friend { get; set; }
        public string FriendFullName { get; set; }
        public string FriendImageTitle { get; set; }
        public byte[] FriendImageData { get; set; }
        public DateTime ConnectionDate { get; set; }
    }
}