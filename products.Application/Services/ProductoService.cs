using products.Domain.Interfaces;
using products.Domain.Models;

namespace products.Application.Services;

public class ProductoService
{
    private readonly IProductosRepository _productosRepository;

    public ProductoService(IProductosRepository productosRepository)
    {
        _productosRepository = productosRepository;
    }

    public async Task<IEnumerable<Producto>> GetAllProductos()
    {
        return await _productosRepository.GetAllProductos();
    }
    
    public async Task<Producto?> GetByIdProducto(int id)
    {
        return await _productosRepository.GetProductoById(id);
    }

    public async Task<Producto?> Create(Producto producto)
    {
        if (string.IsNullOrWhiteSpace(producto.Name) || producto.Price <= 0)
            return null;
        
        await _productosRepository.Create(producto);
        return producto;
    }

    public async Task<Producto?> Update(Producto producto)
    {
        var existe = await _productosRepository.GetProductoById(producto.Id);
        if (existe == null) return null;
        
        existe.Name = producto.Name;
        existe.Description = producto.Description;
        existe.Price = producto.Price;
        existe.Stock = producto.Stock;
        
        await _productosRepository.UpdateProducto(existe);
        return existe;
    }

    public async Task<Producto?> Delete(int id)
    {
        var producto = await _productosRepository.GetProductoById(id);
        if (producto ==  null)  return null;
        await _productosRepository.DeleteProducto(producto);
        return producto;
    }
}