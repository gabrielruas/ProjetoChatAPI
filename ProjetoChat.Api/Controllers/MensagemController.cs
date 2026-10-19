using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

    public MensagemController(IMensagemAplicacao mensagemAplicacao)
    {
        _mensagemAplicacao = mensagemAplicacao;
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

    private static MensagemResposta Mapear(Mensagem mensagem) => new()
    {
        ID = mensagem.ID,
        AtendimentoID = mensagem.AtendimentoID,
        Remetente = mensagem.Remetente,
        Texto = mensagem.Texto,
        DataEnvio = mensagem.DataEnvio
    };
}
