using System;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Models.DTOs
{
    public class FriendListDto
    {
        [Required]
        public string Friend { get; set; }

        [Required]
        public DateTime ConnectionDate { get; set; }
    }
}