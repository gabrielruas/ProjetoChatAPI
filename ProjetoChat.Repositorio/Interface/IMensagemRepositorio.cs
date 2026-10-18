using ProjetoChat.Dominio.Entidades;

public interface IMensagemRepositorio
{
    Task<int> Salvar(Mensagem mensagem);
    Task Atualizar(Mensagem mensagem);
    Mensagem? Obter(int mensagemId);
    IEnumerable<Mensagem> ListarPorAtendimento(int atendimentoId);
}
