using ProjetoChat.Dominio.Entidades;

namespace ProjetoChat.Aplicacao;

public interface IMensagemAplicacao
{
    Task<int> Criar(Mensagem mensagem);
    Task Atualizar(Mensagem mensagem);
    Mensagem Obter(int mensagemId);
    IEnumerable<Mensagem> ListarPorAtendimento(int atendimentoId);
}
