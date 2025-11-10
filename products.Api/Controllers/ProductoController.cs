using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using products.Application.Services;
using products.Domain.Models;

namespace products.Api.Controllers;
[ApiController]
[Route("api/productos")]
[Authorize]
public class ProductoController:ControllerBase
{
    private readonly ProductoService _productoService;

    public ProductoController(ProductoService productoService)
    {
        _productoService = productoService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var productos = await _productoService.GetAllProductos();
        return Ok(productos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var productos = await _productoService.GetByIdProducto(id);
        if (productos == null) return NotFound();
        return Ok(productos);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(Producto producto)
    {
        var createdProducto = await _productoService.Create(producto);
        if(createdProducto == null) return BadRequest();
        return Ok(createdProducto);
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, Producto producto)
    {
        producto.Id = id;
        var updateProducto = await _productoService.Update(producto);
        if (updateProducto == null) return BadRequest();
        return Ok(updateProducto);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var deletedProducto = await _productoService.Delete(id);
        if (deletedProducto == null) return BadRequest();
        return Ok(deletedProducto);
    }
}