using Microsoft.EntityFrameworkCore;
using ThirdAPI.Models;

namespace ThirdAPI.Datas
{
    public class DataContext : DbContext
    {
    public DataContext(DbContextOptions<DataContext> options) : base(options) {} 
    public DbSet<Book> Book { get; set;}
    }

    
}