using ProjetoChat.Dominio.Entidades;
public interface IUsuarioRepositorio
{
    Task<int> Salvar(Usuario usuario);
    Task Atualizar(Usuario usuario);
    Usuario Obter(int usuarioId);
    Usuario ObterPorEmail(string email);
    IEnumerable<Usuario> Listar(bool ativo);
}
