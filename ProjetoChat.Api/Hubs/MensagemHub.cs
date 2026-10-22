using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using ProjetoChat.Api.Model.Resposta;
using ProjetoChat.Aplicacao;
using ProjetoChat.Dominio.Entidades;

namespace ProjetoChat.Api.Hubs;

public class MensagemHub : Hub
{
    private readonly IMensagemAplicacao _mensagemAplicacao;

    public MensagemHub(IMensagemAplicacao mensagemAplicacao)
    {
        _mensagemAplicacao = mensagemAplicacao;
    }

    public async Task EntrarAtendimento(int atendimentoId)
    {
        try
        {
            // Valida o atendimento antes de permitir a inscrição no grupo.
            _mensagemAplicacao.ListarPorAtendimento(atendimentoId);
            await Groups.AddToGroupAsync(Context.ConnectionId, Grupo(atendimentoId));
            await Clients.Caller.SendAsync("MensagensAtualizadas", atendimentoId,
                _mensagemAplicacao.ListarPorAtendimento(atendimentoId).Select(Mapear).ToList());
        }
        catch (ArgumentException ex) { throw new HubException(ex.Message); }
        catch (InvalidOperationException ex) { throw new HubException(ex.Message); }
    }

    public Task SairAtendimento(int atendimentoId) =>
        Groups.RemoveFromGroupAsync(Context.ConnectionId, Grupo(atendimentoId));

    public static string Grupo(int atendimentoId) => $"atendimento:{atendimentoId}";

    public static MensagemResposta Mapear(Mensagem mensagem) => new()
    {
        ID = mensagem.ID,
        AtendimentoID = mensagem.AtendimentoID,
        Remetente = mensagem.Remetente,
        Texto = mensagem.Texto,
        DataEnvio = mensagem.DataEnvio
    };
}
