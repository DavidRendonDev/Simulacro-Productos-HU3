using products.Domain.Models;

namespace products.Domain.Interfaces;

public interface IUsuariosRepository
{
    Task<IEnumerable<Usuario>> GetAllUsuarios();
    
    Task<Usuario?> GetById(int id);
    
    Task<Usuario?> GetByEmail(string email);
    
    Task Create(Usuario usuario);
    
    Task Update(Usuario usuario);
    
    Task Delete(int id);
}