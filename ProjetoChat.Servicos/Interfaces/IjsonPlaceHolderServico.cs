using ProjetoChat.Dominio.Entidades;
namespace ProjetoChat.Servicos.interfaces
{
    public interface IJsonPlaceHolderServico
    {
        Task<List<Tarefa>> ListarTarefas();
    }
}