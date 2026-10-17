using ProjetoChat.Dominio.Entidades;

namespace ProjetoChat.Aplicacao;

public class AtendimentoAplicacao : IAtendimentoAplicacao
{
    private readonly IAtendimentoRepositorio _atendimentoRepositorio;
    private readonly IUsuarioRepositorio _usuarioRepositorio;

    public AtendimentoAplicacao(
        IAtendimentoRepositorio atendimentoRepositorio,
        IUsuarioRepositorio usuarioRepositorio)
    {
        _atendimentoRepositorio = atendimentoRepositorio;
        _usuarioRepositorio = usuarioRepositorio;
    }

    public async Task<int> Criar(Atendimento atendimento)
    {
        ValidarInformacoes(atendimento);
        atendimento.Status = "aguardando";
        atendimento.FuncionarioID = null;
        atendimento.DataInicio = DateTime.Now;
        atendimento.DataFim = null;
        return await _atendimentoRepositorio.Salvar(atendimento);
    }

    public async Task Atualizar(Atendimento atendimento)
    {
        ValidarInformacoes(atendimento);
        var atendimentoDominio = Obter(atendimento.ID);
        atendimentoDominio.NomeCliente = atendimento.NomeCliente;
        atendimentoDominio.EmailCliente = atendimento.EmailCliente;
        atendimentoDominio.Celular = atendimento.Celular;
        atendimentoDominio.Assunto = atendimento.Assunto;
        await _atendimentoRepositorio.Atualizar(atendimentoDominio);
    }

    public Atendimento Obter(int atendimentoId)
    {
        ValidarId(atendimentoId, "Atendimento");
        return _atendimentoRepositorio.Obter(atendimentoId)
            ?? throw new InvalidOperationException("Atendimento não encontrado.");
    }

    public IEnumerable<Atendimento> Listar()
    {
        return _atendimentoRepositorio.Listar();
    }

    public IEnumerable<Atendimento> ListarPorStatus(string status)
    {
        if (status != "aguardando" && status != "em_atendimento" && status != "finalizado")
            throw new ArgumentException("Status de atendimento inválido.", nameof(status));

        return _atendimentoRepositorio.ListarPorStatus(status);
    }

    public IEnumerable<Atendimento> ListarPorFuncionario(int funcionarioId)
    {
        ValidarId(funcionarioId, "Funcionário");
        return _atendimentoRepositorio.ListarPorFuncionario(funcionarioId);
    }

    public async Task IniciarAtendimento(int atendimentoId, int funcionarioId)
    {
        ValidarId(funcionarioId, "Funcionário");
        var atendimento = Obter(atendimentoId);
        if (atendimento.Status != "aguardando")
            throw new InvalidOperationException("Somente atendimentos aguardando podem ser iniciados.");

        var funcionario = _usuarioRepositorio.Obter(funcionarioId);
        if (funcionario == null || !funcionario.Ativo)
            throw new InvalidOperationException("Funcionário ativo não encontrado.");

        atendimento.IniciarAtendimento(funcionarioId);
        await _atendimentoRepositorio.Atualizar(atendimento);
    }

    public async Task FinalizarAtendimento(int atendimentoId)
    {
        var atendimento = Obter(atendimentoId);
        if (atendimento.Status != "em_atendimento")
            throw new InvalidOperationException("Somente atendimentos em andamento podem ser finalizados.");

        atendimento.FinalizarAtendimento();
        await _atendimentoRepositorio.Atualizar(atendimento);
    }

    private static void ValidarInformacoes(Atendimento atendimento)
    {
        ArgumentNullException.ThrowIfNull(atendimento);
        if (string.IsNullOrWhiteSpace(atendimento.NomeCliente))
            throw new ArgumentException("Nome do cliente não pode ser vazio.");
        if (string.IsNullOrWhiteSpace(atendimento.EmailCliente))
            throw new ArgumentException("Email do cliente não pode ser vazio.");
        if (string.IsNullOrWhiteSpace(atendimento.Assunto))
            throw new ArgumentException("Assunto não pode ser vazio.");
    }

    private static void ValidarId(int id, string entidade)
    {
        if (id <= 0)
            throw new ArgumentException($"{entidade} deve ter um ID maior que zero.");
    }
}
