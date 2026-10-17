using ProjetoChat.Dominio.Entidades;

namespace ProjetoChat.Aplicacao;

public interface IAtendimentoAplicacao
{
    Task<int> Criar(Atendimento atendimento);
    Task Atualizar(Atendimento atendimento);
    Atendimento Obter(int atendimentoId);
    IEnumerable<Atendimento> Listar();
    IEnumerable<Atendimento> ListarPorStatus(string status);
    IEnumerable<Atendimento> ListarPorFuncionario(int funcionarioId);
    Task IniciarAtendimento(int atendimentoId, int funcionarioId);
    Task FinalizarAtendimento(int atendimentoId);
}
