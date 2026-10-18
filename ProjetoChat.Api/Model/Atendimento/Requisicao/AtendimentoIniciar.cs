using System.ComponentModel.DataAnnotations;

namespace ProjetoChat.Api.Model.Requisicao;

public class AtendimentoIniciar
{
    [Range(1, int.MaxValue)]
    public int FuncionarioID { get; set; }
}
