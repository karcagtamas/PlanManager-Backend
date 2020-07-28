using System;
using ManagerAPI.Domain.Entities.MC;
using ManagerAPI.Services.Common;
using ManagerAPI.Services.Services;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.DTOs.MC;
using ManagerAPI.Shared.Models;
using ManagerAPI.Shared.Models.MC;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieCorner.Services.Services.Interfaces;

namespace ManagerAPI.Backend.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class BookController : MyController<Book, BookModel, BookListDto, BookDto>
    {
        protected readonly IBookService BookService;
        public BookController(IBookService bookService, ILoggerService loggerService) : base(loggerService, bookService)
        {
            this.BookService = bookService;
        }

        [HttpGet("my")]
        public IActionResult GetMyBooks()
        {
            try
            {
                return Ok(this.BookService.GetMyList());
            }
            catch (MessageException me)
            {
                return BadRequest(this.Logger.ExceptionToResponse(me));
            }
            catch (Exception e)
            {
                return BadRequest(this.Logger.ExceptionToResponse(new Exception(FatalError), e));
            }
        }

        [HttpPut("map")]
        public IActionResult UpdateMyBooks([FromBody] MyBookModel model)
        {
            try
            {
                this.BookService.UpdateMyBooks(model.Ids);
                return Ok();
            }
            catch (MessageException me)
            {
                return BadRequest(this.Logger.ExceptionToResponse(me));
            }
            catch (Exception e)
            {
                return BadRequest(this.Logger.ExceptionToResponse(new Exception(FatalError), e));
            }
        }

        [HttpPut("map/status/{id}")]
        public IActionResult UpdateReadStatus(int id, [FromBody] BookReadStatusModel model)
        {
            try
            {
                this.BookService.UpdateReadStatus(id, model.Read);
                return Ok();
            }
            catch (MessageException me)
            {
                return BadRequest(this.Logger.ExceptionToResponse(me));
            }
            catch (Exception e)
            {
                return BadRequest(this.Logger.ExceptionToResponse(new Exception(FatalError), e));
            }
        }
    }
}
