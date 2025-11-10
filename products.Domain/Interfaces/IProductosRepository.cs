using products.Domain.Models;

namespace products.Domain.Interfaces;

public interface IProductosRepository
{
    Task<IEnumerable<Producto>> GetAllProductos();
    
    Task<Producto> AddProducto(Producto producto);
    
    Task<Producto> GetProductoById(int id);
    
    Task<Producto> UpdateProducto(Producto producto);
    
    Task DeleteProducto(Producto producto);
    
    Task Create(Producto producto);
}