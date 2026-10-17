using ProjetoChat.Dominio.Entidades;
using System.Threading.Tasks;
namespace ProjetoChat.Aplicacao
{

    public class UsuarioAplicacao : IUsuarioAplicacao
    {
        readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioAplicacao(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public async Task<int> Criar(Usuario usuario)
        {
            if (usuario == null)
                throw new Exception("Usuario Nao pode Ser Vazio");
            if (string.IsNullOrEmpty(usuario.Senha))
                throw new Exception("Nome Nao pode Ser Vazio");


            ValidarInformacoesUsuario(usuario);

            return await _usuarioRepositorio.Salvar(usuario);

        }

        public async Task Atualizar(Usuario usuario)
        {
            var usuarioDominio = _usuarioRepositorio.Obter(usuario.ID);

            if (usuarioDominio == null)
                throw new Exception("Usuario Nao pode Ser Vazio");

            ValidarInformacoesUsuario(usuario);

            usuarioDominio.Nome = usuario.Nome;
            usuarioDominio.Email = usuario.Email;

            _usuarioRepositorio.Atualizar(usuarioDominio);
        }
        
        public async Task AlterarSenha(Usuario usuario, string senhaAntiga)
        {
            var usuarioDominio = _usuarioRepositorio.Obter(usuario.ID);

            if (usuarioDominio == null)
                throw new Exception("Usuario Nao pode Ser Vazio");
                
            if (usuarioDominio.Senha != senhaAntiga)
                throw new Exception("Senha Invalida");

            usuarioDominio.Senha = usuario.Senha;

            _usuarioRepositorio.Atualizar(usuarioDominio);
        }

        public Usuario Obter(int usuarioId)
        {
            var usuarioDominio = _usuarioRepositorio.Obter(usuarioId);
            if (usuarioDominio == null)
                throw new Exception("Usuario nao encontrado");
            return usuarioDominio;
        }
        public Usuario ObterPorEmail(string email)
        {
            var usuarioDominio = _usuarioRepositorio.ObterPorEmail(email);
            if (usuarioDominio == null)
                throw new Exception("Usuario nao encontrado");
            return usuarioDominio;
        }

        public async Task Deletar (int usuarioId)
        {
            var usuarioDominio = _usuarioRepositorio.Obter(usuarioId);
            if (usuarioDominio == null)
                throw new Exception("Usuario nao encontrado");
            
            usuarioDominio.Deletar();
            _usuarioRepositorio.Atualizar(usuarioDominio);
        }

         public async Task Restaurar (int usuarioId)
        {
            var usuarioDominio = _usuarioRepositorio.Obter(usuarioId);
            if (usuarioDominio == null)
                throw new Exception("Usuario nao encontrado");
            
            usuarioDominio.Restaurar();
            _usuarioRepositorio.Atualizar(usuarioDominio);
        }

        public IEnumerable<Usuario> Listar(bool ativo)
        {
            return _usuarioRepositorio.Listar(ativo);
        }



        #region Util

        private static void ValidarInformacoesUsuario(Usuario usuario)
        {
            if (string.IsNullOrEmpty(usuario.Email))
                throw new Exception("Email Nao pode Ser Vazio");
            if (string.IsNullOrEmpty(usuario.Nome))
                throw new Exception("Nome Nao pode Ser Vazio");

        }
        #endregion


    }
}