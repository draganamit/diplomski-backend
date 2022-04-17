using diplomski_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace diplomski_backend.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
        public DbSet<User> User{get; set;}
        public DbSet<Category> Category{get; set;}
        public DbSet<Product> Product{get; set;}
    }
}