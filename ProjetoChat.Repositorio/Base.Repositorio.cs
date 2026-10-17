namespace DataAccess.Repositorio
{
    public abstract class BaseRepositorio
    {
        protected readonly ProjetoChatContexto _contexto;

        protected BaseRepositorio(ProjetoChatContexto contexto)
        {
            _contexto = contexto;
        }
    }
}
