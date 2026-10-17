namespace ProjetoChat.Dominio.Entidades;
public class Atendimento
{ public int ID { get; set; }

    public string NomeCliente { get; set; } = string.Empty;

    public string EmailCliente { get; set; } = string.Empty;

    public string? Celular { get; set; }

    public string Assunto { get; set; } = string.Empty;

    public int? FuncionarioID { get; set; }

    public string Status { get; set; } = "aguardando";

    public DateTime DataInicio { get; set; }

    public DateTime? DataFim { get; set; }

    public Atendimento()
    {
        Status = "aguardando";
        DataInicio = DateTime.Now;
    }

    public void IniciarAtendimento(int funcionarioID)
    {
        FuncionarioID = funcionarioID;
        Status = "em_atendimento";
    }

    public void FinalizarAtendimento()
    {
        Status = "finalizado";
        DataFim = DateTime.Now;
    }
}