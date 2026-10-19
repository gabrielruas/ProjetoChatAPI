using System.ComponentModel.DataAnnotations;

namespace ProjetoChat.Api.Model.Requisicao;

public class MensagemCriar
{
    [Range(1, int.MaxValue)]
    public int AtendimentoID { get; set; }
    [Required]
    public string Remetente { get; set; } = string.Empty;
    [Required]
    public string Texto { get; set; } = string.Empty;
}
