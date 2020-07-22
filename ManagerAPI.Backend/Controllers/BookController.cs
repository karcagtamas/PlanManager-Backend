using System;
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
    public class BookController : ControllerBase
    {
        private const string FATAL_ERROR = "Something bad happened. Try again later";
        private readonly ILoggerService _loggerService;
        private readonly IBookService _bookService;
        public BookController(IBookService bookService, ILoggerService loggerService)
        {
            _bookService = bookService;
            _loggerService = loggerService;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            try
            {
                return Ok(_bookService.GetAll());
            }
            catch (MessageException me)
            {
                return BadRequest(_loggerService.ExceptionToResponse(me));
            }
            catch (Exception)
            {
                return BadRequest(_loggerService.ExceptionToResponse(new Exception(FATAL_ERROR)));
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetBook(int id)
        {
            try
            {
                return Ok(_bookService.Get<BookListDto>(id));
            }
            catch (MessageException me)
            {
                return BadRequest(_loggerService.ExceptionToResponse(me));
            }
            catch (Exception)
            {
                return BadRequest(_loggerService.ExceptionToResponse(new Exception(FATAL_ERROR)));
            }
        }

        [HttpGet("my")]
        public IActionResult GetMyBooks()
        {
            try
            {
                return Ok(_bookService.GetMyList());
            }
            catch (MessageException me)
            {
                return BadRequest(_loggerService.ExceptionToResponse(me));
            }
            catch (Exception)
            {
                return BadRequest(_loggerService.ExceptionToResponse(new Exception(FATAL_ERROR)));
            }
        }

        [HttpPost]
        public IActionResult CreateBook([FromBody] BookModel model)
        {
            try
            {
                _bookService.Add<BookModel>(model);
                return Ok();
            }
            catch (MessageException me)
            {
                return BadRequest(_loggerService.ExceptionToResponse(me));
            }
            catch (Exception)
            {
                return BadRequest(_loggerService.ExceptionToResponse(new Exception(FATAL_ERROR)));
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] BookModel model)
        {
            try
            {
                _bookService.Update<BookModel>(id, model);
                return Ok();
            }
            catch (MessageException me)
            {
                return BadRequest(_loggerService.ExceptionToResponse(me));
            }
            catch (Exception)
            {
                return BadRequest(_loggerService.ExceptionToResponse(new Exception(FATAL_ERROR)));
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                _bookService.Remove(id);
                return Ok();
            }
            catch (MessageException me)
            {
                return BadRequest(_loggerService.ExceptionToResponse(me));
            }
            catch (Exception)
            {
                return BadRequest(_loggerService.ExceptionToResponse(new Exception(FATAL_ERROR)));
            }
        }

        [HttpPut("map")]
        public IActionResult UpdateMyBooks([FromBody] MyBookModel model)
        {
            try
            {
                _bookService.UpdateMyBooks(model.Ids);
                return Ok();
            }
            catch (MessageException me)
            {
                return BadRequest(_loggerService.ExceptionToResponse(me));
            }
            catch (Exception)
            {
                return BadRequest(_loggerService.ExceptionToResponse(new Exception(FATAL_ERROR)));
            }
        }

        [HttpPut("map/status/{id}")]
        public IActionResult UpdateReadStatus(int id, [FromBody] BookReadStatusModel model)
        {
            try
            {
                _bookService.UpdateReadStatus(id, model.Read);
                return Ok();
            }
            catch (MessageException me)
            {
                return BadRequest(_loggerService.ExceptionToResponse(me));
            }
            catch (Exception)
            {
                return BadRequest(_loggerService.ExceptionToResponse(new Exception(FATAL_ERROR)));
            }
        }
    }
}
