using ProjetoChat.Dominio.Entidades;

namespace DataAccess.Repositorio;

public class MensagemRepositorio : BaseRepositorio, IMensagemRepositorio
{
    public MensagemRepositorio(ProjetoChatContexto contexto) : base(contexto)
    {
    }

    public async Task<int> Salvar(Mensagem mensagem)
    {
        _contexto.Mensagens.Add(mensagem);
        await _contexto.SaveChangesAsync();
        return mensagem.ID;
    }

    public async Task Atualizar(Mensagem mensagem)
    {
        _contexto.Mensagens.Update(mensagem);
        await _contexto.SaveChangesAsync();
    }

    public Mensagem? Obter(int mensagemId)
    {
        return _contexto.Mensagens.FirstOrDefault(x => x.ID == mensagemId);
    }

    public IEnumerable<Mensagem> ListarPorAtendimento(int atendimentoId)
    {
        return _contexto.Mensagens.Where(x => x.AtendimentoID == atendimentoId)
            .OrderBy(x => x.DataEnvio).ThenBy(x => x.ID).ToList();
    }
}
