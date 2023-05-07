using PairSoftAPI.Models;

namespace PairSoftAPI.ServiceRepository
{
    public interface IToDoListRepository
    {
        Task<List<ToDoList>> GetToDoList();
        Task<ToDoList> DeleteList(int id);
        Task<ToDoList> UpdateList(ToDoList toDoList);
        Task<ToDoList> InsertList(ToDoList toDoList);
        Task<ToDoList> FindById(int id);
    }
}
