using ManagerAPI.Shared.DTOs.SL;
using System;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Shared.Models.SL
{
    /// <summary>
    /// Book create or update model
    /// </summary>
    public class BookModel
    {
        /// <summary>
        /// Init empty model
        /// </summary>
        public BookModel()
        {
        }

        /// <summary>
        /// Model from data object
        /// </summary>
        /// <param name="book">Book data object</param>
        public BookModel(BookDto book)
        {
            Name = book.Name;
            Author = book.Author;
            Description = book.Description;
            Publish = book.Publish;
        }

        [Required(ErrorMessage = "Field is required")]
        [MaxLength(150, ErrorMessage = "Maximum length is 150")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Field is required")]
        [MaxLength(150, ErrorMessage = "Max length is 150")]
        public string Author { get; set; }

        public string Description { get; set; }

        public DateTime? Publish { get; set; }
    }
}