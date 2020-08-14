using System;
using System.Collections.Generic;
using ManagerAPI.Domain.Entities.MC;
using ManagerAPI.Domain.Enums.CM;
using ManagerAPI.Services.Common;
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
    public class BookController : MyController<Book, BookModel, BookListDto, BookDto, MovieCornerNotificationType>
    {
        protected readonly IBookService BookService;
        public BookController(IBookService bookService, ILoggerService loggerService) : base(loggerService, bookService)
        {
            this.BookService = bookService;
        }

        [HttpGet("my")]
        public IActionResult GetMyList()
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
        
        [HttpGet("my/{id}")]
        public IActionResult GetMy(int id)
        {
            try
            {
                return Ok(this.BookService.GetMy(id));
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

        [HttpGet("selector")]
        public IActionResult GetMySelectorList([FromQuery] bool onlyMine)
        {
            try
            {
                return Ok(this.BookService.GetMySelectorList(onlyMine));
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

        [HttpPut("map/status")]
        public IActionResult UpdateReadStatus([FromBody] List<BookReadStatusModel> models)
        {
            try
            {
                foreach (var model in models)
                {
                    this.BookService.UpdateReadStatus(model.Id, model.Read);
                }
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

        [HttpPost("map/{id}")]
        public IActionResult AddBookToMyBooks(int id) {
            try
            {
                this.BookService.AddBookToMyBooks(id);
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

        [HttpDelete("map/{id}")]
        public IActionResult RemoveBookFromMyBooks(int id) {
            try
            {
                this.BookService.RemoveBookFromMyBooks(id);
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
