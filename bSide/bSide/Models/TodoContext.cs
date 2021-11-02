using Microsoft.EntityFrameworkCore;

namespace bSide.Models
{
   
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        public DbSet<bSideCard> TodoItems { get; set; }
    }
}