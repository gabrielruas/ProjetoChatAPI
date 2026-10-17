using ProjetoChat.Dominio.Entidades;

public interface IAtendimentoRepositorio
{
    Task<int> Salvar(Atendimento atendimento);
    Task Atualizar(Atendimento atendimento);
    Atendimento? Obter(int atendimentoId);
    IEnumerable<Atendimento> Listar();
    IEnumerable<Atendimento> ListarPorStatus(string status);
    IEnumerable<Atendimento> ListarPorFuncionario(int funcionarioId);
}
