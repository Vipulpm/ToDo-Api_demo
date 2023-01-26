using ToDo_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ToDo_Api.Data
{
    public class ApiDbContext : DbContext
    {
        public virtual DbSet<ItemData> Items { get; set; }

        public ApiDbContext(DbContextOptions <ApiDbContext> options) : base(options) 
        { }
    }
}
