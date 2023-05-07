using Microsoft.EntityFrameworkCore;
using PairSoftAPI.Models;

namespace PairSoftAPI.ServiceRepository
{
    public class ToDoListRepository:IToDoListRepository
    {
        private readonly PairSoftDbContext _DbContext;
        public ToDoListRepository(PairSoftDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public async Task<List<ToDoList>> GetToDoList()
        {
            try
            {
                return await _DbContext.ToDoLists.ToListAsync();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ToDoList> DeleteList(int id)
        {
            try
            {
                var deleteentity = await _DbContext.ToDoLists.FindAsync(id);
                if (deleteentity != null)
                {
                    _DbContext.ToDoLists.Remove(deleteentity);
                    int status = _DbContext.SaveChanges();
                    if (status == 1)
                        return deleteentity;
                    else
                        return null;

                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ToDoList> UpdateList(ToDoList toDoList)
        {
            try
            {
                var existentity = await _DbContext.ToDoLists.FindAsync(toDoList.Id);
                if (existentity != null)
                {
                    existentity.Title = toDoList.Title;
                    existentity.Description = toDoList.Description;
                    existentity.DueDate = toDoList.DueDate;
                    existentity.IsCompleted = toDoList.IsCompleted;
                    int status = await _DbContext.SaveChangesAsync();
                    if (status == 1)
                        return existentity;

                    return null;

                }
                return null; ;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ToDoList> InsertList(ToDoList toDoList)
        {
            try
            {
                await _DbContext.ToDoLists.AddAsync(toDoList);
                int status = _DbContext.SaveChanges();
                if (status == 1)
                    return toDoList;
                else
                    return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ToDoList> FindById(int id)
        {
            try
            {
                var existentity = await _DbContext.ToDoLists.FindAsync(id);
                return existentity;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
