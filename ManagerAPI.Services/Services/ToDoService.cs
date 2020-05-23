using AutoMapper;
using ManagerAPI.DataAccess;
using ManagerAPI.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace ManagerAPI.Services.Services
{
    public class ToDoService
    {
        private readonly DatabaseContext _context;
        private UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger<ToDoService> _logger;
        private readonly IUserService _userService;
        public ToDoService(DatabaseContext context, UserManager<User> userManager, IMapper mapper, ILogger<ToDoService> logger, IUserService userService)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
            _userService = userService;
        }
        
        /*
        public List<ToDoDateDTO> GetToDos(string userId, bool isSolved)
        {
            UserDTO user = _userService.GetUserById(userId);
            _logger.LogInformation($"Get all ToDos for user {user.UserName} ({user.Id})");

            List<ToDo> toDoList = _context.ToDos.Where(x => x.OwnerId == userId && x.IsSolved == isSolved).ToList();
            List<ToDoDateDTO> list = new List<ToDoDateDTO>();
            foreach (var i in toDoList)
            {
                ToDoDateDTO el = list.FirstOrDefault(x => x.DueDate == i.DueDate);
                if (el != null)
                {
                    el.ToDoList.Add(_mapper.Map<ToDoDTO>(i));
                }
                else
                {
                    list.Add(new ToDoDateDTO() { DueDate = i.DueDate, ToDoList = new List<ToDoDTO>() { _mapper.Map<ToDoDTO>(i) } });
                }
            }
            return list;
        }

        public ToDoDataDTO GetToDo(int id)
        {
            _logger.LogInformation($"Get ToDo with Id {id}");
            ToDo todo = _context.ToDos.Find(id);
            if (todo == null)
            {
                string msg = $"ToDo does not exist with this Id ({id})";
                _logger.LogError(msg);
                throw new Exception(msg);
            }
            else
            {
                return _mapper.Map<ToDoDataDTO>(todo);
            }
        }

        public void DeleteToDo(int id)
        {
            _logger.LogInformation($"Delete ToDo with Id {id}");
            ToDo todo = _context.ToDos.Find(id);
            if (todo == null)
            {
                string msg = $"ToDo does not exist with this Id ({id})";
                _logger.LogError(msg);
                throw new Exception(msg);
            }
            try
            {
                _context.ToDos.Remove(todo);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw e;
            }
        }

        public int CreateToDo(ToDoCreateDTO toDoElement, string userId)
        {
            _logger.LogInformation("Create ToDo");
            try
            {
                User user = _context.ApplicationUsers.Find(userId);
                ToDo newToDo = new ToDo() { Title = toDoElement.Title, DueDate = toDoElement.DueDate, IsSolved = false, OwnerId = userId, Owner = user };
                _context.ToDos.Add(newToDo);
                _context.SaveChanges();
                return newToDo.Id;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw e;
            }
        }

        public void UpdateToDo(ToDoDataDTO toDoElement, string userId)
        {
            ToDo oldToDo = _context.ToDos.Find(toDoElement.Id);
            if (oldToDo == null)
            {
                string msg = $"ToDo does not exist with this Id ({toDoElement.Id})";
                _logger.LogError(msg);
                throw new Exception(msg);
            }
            if (oldToDo.OwnerId == userId)
            {
                UserDTO user = _userService.GetUserById(userId);
                string msg = $"Invalid request. Element's owner is {oldToDo.Owner.UserName} ({oldToDo.OwnerId}), the requester is {user.UserName} ({userId})";
                _logger.LogError(msg);
                throw new Exception(msg);
            }
            try
            {
                oldToDo.IsSolved = toDoElement.IsSolved;
                oldToDo.Title = toDoElement.Title;
                oldToDo.DueDate = toDoElement.DueDate;
                _context.ToDos.Update(oldToDo);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw e;
            }
        }
        
        
        */
    }
}
