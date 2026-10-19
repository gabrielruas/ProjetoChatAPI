using System.ComponentModel.DataAnnotations;

namespace ProjetoChat.Api.Model.Requisicao;

public class AtendimentoAtualizar : AtendimentoCriar
{
    [Range(1, int.MaxValue,
        ErrorMessage = "O ID deve ser maior que zero.")]
    public int ID { get; set; }
}