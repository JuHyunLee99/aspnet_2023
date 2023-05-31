using Microsoft.EntityFrameworkCore;

// 1. DB 연동
namespace TodoApiServer.Models
{
    public class ApplicationDbContext : DbContext
    {
        // 생성자 마법사로 만들것
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<TodoItem> TodoItems { get; set; }
    }

}
