using PairSoftAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Test
{
    public class DummyDataDBInitializer
    {
        public DummyDataDBInitializer()
        {
        }

        public void Seed(PairSoftDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.ToDoLists.AddRange(
                new ToDoList() { Title = "Test Title 1", Description = "Test Description 1", DueDate = DateTime.Now, IsCompleted = true },
                new ToDoList() { Title = "Test Title 2", Description = "Test Description 2", DueDate = DateTime.Now, IsCompleted = false }

            );
            context.SaveChanges();
        }
    }
}
