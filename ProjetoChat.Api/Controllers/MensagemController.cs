using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ProjetoChat.Api.Hubs;
using ProjetoChat.Api.Model.Requisicao;
using ProjetoChat.Api.Model.Resposta;
using ProjetoChat.Aplicacao;
using ProjetoChat.Dominio.Entidades;

namespace ProjetoChat.Api;

[ApiController]
[Route("[controller]")]
public class MensagemController : ControllerBase
{
    private readonly IMensagemAplicacao _mensagemAplicacao;
    private readonly IHubContext<MensagemHub> _hub;

    public MensagemController(IMensagemAplicacao mensagemAplicacao, IHubContext<MensagemHub> hub)
    {
        _mensagemAplicacao = mensagemAplicacao;
        _hub = hub;
    }

    [HttpPost("Criar")]
    public async Task<ActionResult> Criar([FromBody] MensagemCriar dto)
    {
        try
        {
            var mensagem = new Mensagem
            {
                AtendimentoID = dto.AtendimentoID,
                Remetente = dto.Remetente,
                Texto = dto.Texto
            };
            var id = await _mensagemAplicacao.Criar(mensagem);
            await NotificarAtendimento(mensagem.AtendimentoID);
            return CreatedAtAction(nameof(Obter), new { mensagemId = id }, Mapear(mensagem));
        }
        catch (ArgumentException ex) { return BadRequest(ex.Message); }
        catch (InvalidOperationException ex) { return NotFound(ex.Message); }
    }

    [HttpPut("Atualizar")]
    public async Task<ActionResult> Atualizar([FromBody] MensagemAtualizar dto)
    {
        try
        {
            await _mensagemAplicacao.Atualizar(new Mensagem { ID = dto.ID, Texto = dto.Texto });
            await NotificarAtendimento(_mensagemAplicacao.Obter(dto.ID).AtendimentoID);
            return NoContent();
        }
        catch (ArgumentException ex) { return BadRequest(ex.Message); }
        catch (InvalidOperationException ex) { return NotFound(ex.Message); }
    }

    [HttpGet("Obter/{mensagemId:int}")]
    public ActionResult<MensagemResposta> Obter([FromRoute] int mensagemId)
    {
        try { return Ok(Mapear(_mensagemAplicacao.Obter(mensagemId))); }
        catch (ArgumentException ex) { return BadRequest(ex.Message); }
        catch (InvalidOperationException ex) { return NotFound(ex.Message); }
    }

    [HttpGet("ListarPorAtendimento/{atendimentoId:int}")]
    public ActionResult ListarPorAtendimento([FromRoute] int atendimentoId)
    {
        try { return Ok(_mensagemAplicacao.ListarPorAtendimento(atendimentoId).Select(Mapear).ToList()); }
        catch (ArgumentException ex) { return BadRequest(ex.Message); }
        catch (InvalidOperationException ex) { return NotFound(ex.Message); }
    }

    private Task NotificarAtendimento(int atendimentoId) =>
        _hub.Clients.Group(MensagemHub.Grupo(atendimentoId)).SendAsync(
            "MensagensAtualizadas", atendimentoId,
            _mensagemAplicacao.ListarPorAtendimento(atendimentoId).Select(Mapear).ToList());

    private static MensagemResposta Mapear(Mensagem mensagem) => MensagemHub.Mapear(mensagem);
}
