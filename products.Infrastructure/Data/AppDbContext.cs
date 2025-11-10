using Microsoft.EntityFrameworkCore;
using products.Domain.Models;

namespace products.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options){ }
    
    public DbSet<Usuario>  Usuarios { get; set; }
    public DbSet<Producto> Productos { get; set; }
}