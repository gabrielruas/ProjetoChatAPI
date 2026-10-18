using System;

namespace ProjetoChat.Api.Model.Resposta;

public class AtendimentoResposta
{
    public int ID { get; set; }
    public string NomeCliente { get; set; } = string.Empty;
    public string EmailCliente { get; set; } = string.Empty;
    public string? Celular { get; set; }
    public string Assunto { get; set; } = string.Empty;
    public int? FuncionarioID { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime DataInicio { get; set; }
    public DateTime? DataFim { get; set; }
}
