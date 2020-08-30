using System.Collections.Generic;
using ManagerAPI.Domain.Entities.SL;
using ManagerAPI.Services.Common;
using ManagerAPI.Shared.DTOs.SL;
using ManagerAPI.Shared.Models.SL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieCorner.Services.Services.Interfaces;

namespace ManagerAPI.Backend.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrator,Status Library User,Status Library Moderator,Status Library Administrator,Root")]
    [ApiController]
    public class BookController : MyController<Book, BookModel, BookListDto, BookDto>
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService) : base(bookService)
        {
            this._bookService = bookService;
        }

        [HttpGet("my")]
        public IActionResult GetMyList()
        {
            return Ok(this._bookService.GetMyList());
        }

        [HttpGet("my/{id}")]
        public IActionResult GetMy(int id)
        {
            return Ok(this._bookService.GetMy(id));
        }

        [HttpGet("selector")]
        public IActionResult GetMySelectorList([FromQuery] bool onlyMine)
        {
            return Ok(this._bookService.GetMySelectorList(onlyMine));
        }

        [HttpPut("map")]
        public IActionResult UpdateMyBooks([FromBody] MyBookModel model)
        {
            this._bookService.UpdateMyBooks(model.Ids);
            return Ok();
        }

        [HttpPut("map/status")]
        public IActionResult UpdateReadStatus([FromBody] List<BookReadStatusModel> models)
        {
            foreach (var model in models)
            {
                this._bookService.UpdateReadStatus(model.Id, model.Read);
            }

            return Ok();
        }

        [HttpPost("map/{id}")]
        public IActionResult AddBookToMyBooks(int id)
        {
            this._bookService.AddBookToMyBooks(id);
            return Ok();
        }

        [HttpDelete("map/{id}")]
        public IActionResult RemoveBookFromMyBooks(int id)
        {
            this._bookService.RemoveBookFromMyBooks(id);
            return Ok();
        }
        
        [HttpPost]
        [Authorize(Roles = "Administrator,Root,Moderator,Status Library Moderator,Status Library Administrator")]
        public override IActionResult Create([FromBody] BookModel model)
        {
            this._bookService.Add<BookModel>(model);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator,Root,Status Library Administrator")]
        public override IActionResult Delete(int id)
        {
            this._bookService.Remove(id);
            return Ok();
        }
        
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator,Root,Moderator,Status Library Moderator,Status Library Administrator")]
        public override IActionResult Update(int id, BookModel model)
        {
            this._bookService.Update<BookModel>(id, model);
            return Ok();
        }
    }
}