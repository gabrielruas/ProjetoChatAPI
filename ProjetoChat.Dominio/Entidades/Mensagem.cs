
namespace ProjetoChat.Dominio.Entidades;

public class Mensagem
{
    public int ID { get; set; }

    public int AtendimentoID { get; set; }

    public string Remetente { get; set; } = string.Empty;

    public string Texto { get; set; } = string.Empty;

    public DateTime DataEnvio { get; set; }

    public Mensagem()
    {
        DataEnvio = DateTime.Now;
    }
}
