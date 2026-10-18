using System.ComponentModel.DataAnnotations;

namespace ProjetoChat.Api.Model.Requisicao;

public class AtendimentoCriar
{
    [Required]
    public string NomeCliente { get; set; } = string.Empty;
    [Required, EmailAddress]
    public string EmailCliente { get; set; } = string.Empty;
    public string? Celular { get; set; }
    [Required]
    public string Assunto { get; set; } = string.Empty;
}
