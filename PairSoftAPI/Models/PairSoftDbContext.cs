using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PairSoftAPI.Models
{
    public class PairSoftDbContext:DbContext
    {
        public PairSoftDbContext(DbContextOptions<PairSoftDbContext> option) : base(option)
        {
        }
        public DbSet<ToDoList> ToDoLists { get; set; }
    }
}
