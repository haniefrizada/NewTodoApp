using Microsoft.EntityFrameworkCore;
using NewTodoApp.Models;

namespace NewTodoApp.Data
{
    public class NewTodoDbContext : DbContext
    {
        public NewTodoDbContext(DbContextOptions options) : base(options)
        { 
        }

        DbSet<TodoItem> TodoItems { get; set; }
    }
}
