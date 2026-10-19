
using ProjetoChat.Dominio.Entidades;

public interface IAtendimentoRepositorio
{
    // Criar atendimento
    Task<int> Salvar(Atendimento atendimento);

    // Atualizar dados do atendimento
    Task Atualizar(Atendimento atendimento);

    // Buscar atendimento pelo ID
    Atendimento? Obter(int atendimentoId);

    // Listar todos os atendimentos
    IEnumerable<Atendimento> Listar();

    // Listar atendimentos por status
    IEnumerable<Atendimento> ListarPorStatus(string status);

    // Listar atendimentos por funcionário
    IEnumerable<Atendimento> ListarPorFuncionario(
        int funcionarioId);

    // Funcionário assume o atendimento
    Task IniciarAtendimento(
        int atendimentoId,
        int funcionarioId);

    // Finalizar atendimento
    Task FinalizarAtendimento(int atendimentoId);
}
