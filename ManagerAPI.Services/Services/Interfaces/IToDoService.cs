using System.Collections.Generic;
using ManagerAPI.Models.DTOs;

namespace ManagerAPI.Services.Services.Interfaces
{
    public interface IToDoService
    {
        List<ToDoDateDto> GetToDos(string userId, bool isSolved);

        ToDoDataDto GetToDo(int id);

        void DeleteToDo(int id);

        void UpdateToDo(ToDoDataDto toDoElement, string userId);

        int CreateToDo(ToDoCreateDto toDoElement, string userId);
    }
}
