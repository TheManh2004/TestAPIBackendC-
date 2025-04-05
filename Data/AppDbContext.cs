using Microsoft.EntityFrameworkCore;
using TestAPI.Models;

namespace TestAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UploadedFile> UploadedFiles { get; set; }
    }
}
