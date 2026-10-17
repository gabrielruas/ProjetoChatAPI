using ProjetoChat.Dominio.Entidades;
using ProjetoChat.Servicos.interfaces;

namespace ProjetoChat.Aplicacao
{
    public class TarefaAplicacao : ITarefaAplicacao
    {   
        private readonly IJsonPlaceHolderServico _jsonPlaceHolderServico;

        public TarefaAplicacao(IJsonPlaceHolderServico jsonPlaceHolderServico)
        {
            _jsonPlaceHolderServico = jsonPlaceHolderServico;
        }

        public List<Tarefa> ListarTarefas()
        {
            return _jsonPlaceHolderServico.ListarTarefas().Result;
        }
    }
}