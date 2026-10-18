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
public class AtendimentoController : ControllerBase
{
    private readonly IAtendimentoAplicacao _atendimentoAplicacao;

    public AtendimentoController(IAtendimentoAplicacao atendimentoAplicacao)
    {
        _atendimentoAplicacao = atendimentoAplicacao;
    }

    [HttpPost("Criar")]
    public async Task<ActionResult> Criar([FromBody] AtendimentoCriar dto)
    {
        try
        {
            var atendimento = new Atendimento
            {
                NomeCliente = dto.NomeCliente,
                EmailCliente = dto.EmailCliente,
                Celular = dto.Celular,
                Assunto = dto.Assunto
            };
            var id = await _atendimentoAplicacao.Criar(atendimento);
            return CreatedAtAction(nameof(Obter), new { atendimentoId = id }, Mapear(atendimento));
        }
        catch (ArgumentException ex) { return BadRequest(ex.Message); }
    }

    [HttpGet("Obter/{atendimentoId:int}")]
    public ActionResult<AtendimentoResposta> Obter([FromRoute] int atendimentoId)
    {
        try { return Ok(Mapear(_atendimentoAplicacao.Obter(atendimentoId))); }
        catch (ArgumentException ex) { return BadRequest(ex.Message); }
        catch (InvalidOperationException ex) { return NotFound(ex.Message); }
    }

    [HttpPut("Atualizar")]
    public async Task<ActionResult> Atualizar([FromBody] AtendimentoAtualizar dto)
    {
        try
        {
            await _atendimentoAplicacao.Atualizar(new Atendimento
            {
                ID = dto.ID,
                NomeCliente = dto.NomeCliente,
                EmailCliente = dto.EmailCliente,
                Celular = dto.Celular,
                Assunto = dto.Assunto
            });
            return NoContent();
        }
        catch (ArgumentException ex) { return BadRequest(ex.Message); }
        catch (InvalidOperationException ex) { return NotFound(ex.Message); }
    }

    [HttpGet("Listar")]
    public ActionResult Listar()
    {
        return Ok(_atendimentoAplicacao.Listar().Select(Mapear).ToList());
    }

    [HttpGet("ListarPorStatus")]
    public ActionResult ListarPorStatus([FromQuery] string status)
    {
        try { return Ok(_atendimentoAplicacao.ListarPorStatus(status).Select(Mapear).ToList()); }
        catch (ArgumentException ex) { return BadRequest(ex.Message); }
    }

    [HttpGet("ListarPorFuncionario/{funcionarioId:int}")]
    public ActionResult ListarPorFuncionario([FromRoute] int funcionarioId)
    {
        try { return Ok(_atendimentoAplicacao.ListarPorFuncionario(funcionarioId).Select(Mapear).ToList()); }
        catch (ArgumentException ex) { return BadRequest(ex.Message); }
    }

    [HttpPut("Iniciar/{atendimentoId:int}")]
    public async Task<ActionResult> Iniciar([FromRoute] int atendimentoId, [FromBody] AtendimentoIniciar dto)
    {
        try
        {
            await _atendimentoAplicacao.IniciarAtendimento(atendimentoId, dto.FuncionarioID);
            return NoContent();
        }
        catch (ArgumentException ex) { return BadRequest(ex.Message); }
        catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
    }

    [HttpPut("Finalizar/{atendimentoId:int}")]
    public async Task<ActionResult> Finalizar([FromRoute] int atendimentoId)
    {
        try
        {
            await _atendimentoAplicacao.FinalizarAtendimento(atendimentoId);
            return NoContent();
        }
        catch (ArgumentException ex) { return BadRequest(ex.Message); }
        catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
    }

    private static AtendimentoResposta Mapear(Atendimento atendimento) => new()
    {
        ID = atendimento.ID,
        NomeCliente = atendimento.NomeCliente,
        EmailCliente = atendimento.EmailCliente,
        Celular = atendimento.Celular,
        Assunto = atendimento.Assunto,
        FuncionarioID = atendimento.FuncionarioID,
        Status = atendimento.Status,
        DataInicio = atendimento.DataInicio,
        DataFim = atendimento.DataFim
    };
}
