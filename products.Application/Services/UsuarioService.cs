using products.Domain.Interfaces;
using products.Domain.Models;

namespace products.Application.Services;

public class UsuarioService
{
    private readonly IUsuariosRepository _usuariosRepository;
    
    public UsuarioService(IUsuariosRepository usuariosRepository)
    {
        _usuariosRepository = usuariosRepository;
    }

    public async Task<IEnumerable<Usuario>> GetAll()
    {
        return await _usuariosRepository.GetAllUsuarios();
    }

    public async Task<Usuario> GetById(int id)
    {
        return await _usuariosRepository.GetById(id);
    }

    public async Task<Usuario?> Update(Usuario usuario)
    {
        var existe = await _usuariosRepository.GetById(usuario.Id);
        if (existe == null) return null;

        existe.Username = usuario.Username;
        existe.Email = usuario.Email;
        existe.Role = usuario.Role;
        
        await _usuariosRepository.Update(existe);
        return existe;
    }

    public async Task<Usuario?> Delete(int id)
    {
        var usuario = await _usuariosRepository.GetById(id);
        if (usuario == null) return null;

        await _usuariosRepository.Delete(id);
        return usuario;
    }
    
    
}