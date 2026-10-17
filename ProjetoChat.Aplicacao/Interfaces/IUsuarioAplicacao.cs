
using ProjetoChat.Dominio.Entidades;

namespace ProjetoChat.Aplicacao
{
    public interface IUsuarioAplicacao
    {
        Task<int> Criar(Usuario usuario);
        Task AlterarSenha(Usuario usuarioDTO, string senhaAntiga);
        Task Atualizar(Usuario usuarioDTO);
        Task Deletar(int usuarioId);
        Task Restaurar(int usuarioId);
        IEnumerable<Usuario> Listar(bool ativo); 
        Usuario Obter(int usuarioId);
        Usuario ObterPorEmail(string email);
    }
}