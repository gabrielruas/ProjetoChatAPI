using Projeto360.Dominio.Entidades;
namespace Projeto360.Servicos.interfaces
{
    public interface IJsonPlaceHolderServico
    {
        Task<List<Tarefa>> ListarTarefas();
    }
}