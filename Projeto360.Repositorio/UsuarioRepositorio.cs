using Projeto360.Dominio.Entidades;
using Projeto360.Repositorio.Configuracoes;

namespace DataAccess.Repositorio
{
    public class UsuarioRepositorio : BaseRepositorio, IUsuarioRepositorio
    {

        public UsuarioRepositorio(Projeto360Contexto contexto) : base(contexto)
        {

        }
         
        public async Task<int> Salvar(Usuario usuario)
        {
            _contexto.Usuarios.Add(usuario);
            _contexto.SaveChanges();

            return usuario.ID;
        }

    
        public async Task Atualizar(Usuario usuario)
        {
            _contexto.Usuarios.Update(usuario);
            _contexto.SaveChanges();
        }
    
    
        public  Usuario Obter(int usuarioId)
        {
            return _contexto.Usuarios
                .Where(u => u.ID == usuarioId)
                .Where(u => u.Ativo)
                .FirstOrDefault();
               
        }

        public Usuario ObterPorEmail(string Email)
        {
            return _contexto.Usuarios
                .Where(u => u.Ativo)
                .FirstOrDefault(c => c.Email == Email);
        }
        public IEnumerable<Usuario> Listar(bool ativo)
        {
            return _contexto.Usuarios
                    .Where(u => u.Ativo == ativo)
                    .ToList();
        }
    }
}

