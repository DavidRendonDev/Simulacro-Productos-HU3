using Microsoft.EntityFrameworkCore;
using products.Domain.Interfaces;
using products.Domain.Models;
using products.Infrastructure.Data;

namespace products.Infrastructure.Repositories;

public class UsuarioRepository : IUsuariosRepository
{
    private readonly AppDbContext _dbContext;
    
    public UsuarioRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<Usuario>> GetAllUsuarios()
    {
        return await _dbContext.Usuarios.ToListAsync();
    }

    public async Task<Usuario?> GetById(int id)
    {
        return await _dbContext.Usuarios.FindAsync(id);
    }

    public async Task<Usuario?> GetByEmail(string email)
    {
        return await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
    }

    public  async Task Create(Usuario usuario)
    {
        _dbContext.Usuarios.Add(usuario);
        await _dbContext.SaveChangesAsync();
        
    }

    public async Task Update(Usuario usuario)
    {
        _dbContext.Usuarios.Update(usuario);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var usuario = await _dbContext.Usuarios.FindAsync(id);
        _dbContext.Usuarios.Remove(usuario);
        await _dbContext.SaveChangesAsync();
    }
}