using System;

namespace ManagerAPI.Shared.DTOs
{
    /// <summary>
    /// Friend list DTO
    /// </summary>
    public class FriendListDto
    {
        public string FriendId { get; set; }
        public string Friend { get; set; }
        public string FriendFullName { get; set; }
        public string FriendImageTitle { get; set; }
        public byte[] FriendImageData { get; set; }
        public DateTime ConnectionDate { get; set; }

        /// <summary>
        /// Generate image url
        /// </summary>
        /// <param name="defaultImage">Default image path</param>
        /// <returns>Generated image path</returns>
        public string ImageUrl(string defaultImage)
        {
            if (FriendImageData.Length == 0)
            {
                return defaultImage;
            }

            string base64 = Convert.ToBase64String(FriendImageData);
            return $"data:image/gif;base64,{base64}";

        }
    }
}