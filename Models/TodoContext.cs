using Microsoft.EntityFrameworkCore;

namespace PatosApi.Models;

public class TodoContext : DbContext
{
    public TodoContext(DbContextOptions<TodoContext> options)
        : base(options)
    {
    }

    public DbSet<PatosItem> PatosItem { get; set; } = null!;
}