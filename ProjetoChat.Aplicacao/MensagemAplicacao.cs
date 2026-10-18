using ProjetoChat.Dominio.Entidades;

namespace ProjetoChat.Aplicacao;

public class MensagemAplicacao : IMensagemAplicacao
{
    private readonly IMensagemRepositorio _mensagemRepositorio;
    private readonly IAtendimentoRepositorio _atendimentoRepositorio;

    public MensagemAplicacao(
        IMensagemRepositorio mensagemRepositorio,
        IAtendimentoRepositorio atendimentoRepositorio)
    {
        _mensagemRepositorio = mensagemRepositorio;
        _atendimentoRepositorio = atendimentoRepositorio;
    }

    public async Task<int> Criar(Mensagem mensagem)
    {
        ValidarInformacoes(mensagem);
        ValidarAtendimento(mensagem.AtendimentoID);
        mensagem.DataEnvio = DateTime.Now;
        return await _mensagemRepositorio.Salvar(mensagem);
    }

    public async Task Atualizar(Mensagem mensagem)
    {
        ArgumentNullException.ThrowIfNull(mensagem);
        ValidarTexto(mensagem.Texto);
        var mensagemDominio = Obter(mensagem.ID);
        mensagemDominio.Texto = mensagem.Texto;
        await _mensagemRepositorio.Atualizar(mensagemDominio);
    }

    public Mensagem Obter(int mensagemId)
    {
        ValidarId(mensagemId, "Mensagem");
        return _mensagemRepositorio.Obter(mensagemId)
            ?? throw new InvalidOperationException("Mensagem não encontrada.");
    }

    public IEnumerable<Mensagem> ListarPorAtendimento(int atendimentoId)
    {
        ValidarAtendimento(atendimentoId);
        return _mensagemRepositorio.ListarPorAtendimento(atendimentoId);
    }

    private void ValidarAtendimento(int atendimentoId)
    {
        ValidarId(atendimentoId, "Atendimento");
        if (_atendimentoRepositorio.Obter(atendimentoId) == null)
            throw new InvalidOperationException("Atendimento não encontrado.");
    }

    private static void ValidarInformacoes(Mensagem mensagem)
    {
        ArgumentNullException.ThrowIfNull(mensagem);
        if (string.IsNullOrWhiteSpace(mensagem.Remetente))
            throw new ArgumentException("Remetente não pode ser vazio.");
        ValidarTexto(mensagem.Texto);
    }

    private static void ValidarTexto(string texto)
    {
        if (string.IsNullOrWhiteSpace(texto))
            throw new ArgumentException("Texto da mensagem não pode ser vazio.");
    }

    private static void ValidarId(int id, string entidade)
    {
        if (id <= 0)
            throw new ArgumentException($"{entidade} deve ter um ID maior que zero.");
    }
}
