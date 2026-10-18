using System.ComponentModel.DataAnnotations;

namespace ProjetoChat.Api.Model.Requisicao;

public class AtendimentoAtualizar : AtendimentoCriar
{
    [Range(1, int.MaxValue)]
    public int ID { get; set; }
}
