using Microsoft.EntityFrameworkCore;
using products.Domain.Interfaces;
using products.Domain.Models;
using products.Infrastructure.Data;

namespace products.Infrastructure.Repositories;

public class ProductoRepository : IProductosRepository
{
    private readonly AppDbContext _dbContext;
    
    public ProductoRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<Producto>> GetAllProductos()
    {
        return await _dbContext.Productos.ToListAsync();
    }

    public async Task<Producto> AddProducto(Producto producto)
    {
        _dbContext.Productos.Add(producto);
        await  _dbContext.SaveChangesAsync();
        return producto;
    }

    public async Task<Producto> GetProductoById(int id)
    {
        return await _dbContext.Productos.FindAsync(id);
    }

    public async Task<Producto> UpdateProducto(Producto producto)
    {
        _dbContext.Productos.Update(producto);
        _dbContext.SaveChanges();
        return producto;
    }

    public  async Task DeleteProducto(Producto producto)
    {
        _dbContext.Productos.Remove(producto);
            await _dbContext.SaveChangesAsync();
    }

    public async Task Create(Producto producto)
    {
        _dbContext.Productos.Add(producto);
            await _dbContext.SaveChangesAsync();
    }
}