using System.ComponentModel.DataAnnotations;

namespace ProjetoChat.Api.Model.Requisicao;

public class MensagemAtualizar
{
    [Range(1, int.MaxValue)]
    public int ID { get; set; }
    [Required]
    public string Texto { get; set; } = string.Empty;
}
