
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

    public AtendimentoController(
        IAtendimentoAplicacao atendimentoAplicacao)
    {
        _atendimentoAplicacao = atendimentoAplicacao;
    }

    // Criar atendimento
    [HttpPost("Criar")]
    public async Task<ActionResult<AtendimentoResposta>> Criar(
        [FromBody] AtendimentoCriar dto)
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

            var atendimentoCriado = _atendimentoAplicacao.Obter(id);

            return CreatedAtAction(
                nameof(Obter),
                new { atendimentoId = id },
                Mapear(atendimentoCriado));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // Obter atendimento pelo ID
    [HttpGet("Obter/{atendimentoId:int}")]
    public ActionResult<AtendimentoResposta> Obter(
        [FromRoute] int atendimentoId)
    {
        try
        {
            var atendimento = _atendimentoAplicacao.Obter(atendimentoId);

            return Ok(Mapear(atendimento));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }

    // Atualizar dados do atendimento
    [HttpPut("Atualizar")]
    public async Task<IActionResult> Atualizar(
        [FromBody] AtendimentoAtualizar dto)
    {
        try
        {
            var atendimento = new Atendimento
            {
                ID = dto.ID,
                NomeCliente = dto.NomeCliente,
                EmailCliente = dto.EmailCliente,
                Celular = dto.Celular,
                Assunto = dto.Assunto
            };

            await _atendimentoAplicacao.Atualizar(atendimento);

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }

    // Listar todos os atendimentos
    [HttpGet("Listar")]
    public IActionResult Listar()
    {
        var atendimentos = _atendimentoAplicacao
            .Listar()
            .Select(Mapear)
            .ToList();

        return Ok(atendimentos);
    }

    // Listar atendimentos por status
    [HttpGet("ListarPorStatus")]
    public IActionResult ListarPorStatus(
        [FromQuery] string status)
    {
        try
        {
            var atendimentos = _atendimentoAplicacao
                .ListarPorStatus(status)
                .Select(Mapear)
                .ToList();

            return Ok(atendimentos);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // Listar atendimentos por funcionário
    [HttpGet("ListarPorFuncionario/{funcionarioId:int}")]
    public IActionResult ListarPorFuncionario(
        [FromRoute] int funcionarioId)
    {
        try
        {
            var atendimentos = _atendimentoAplicacao
                .ListarPorFuncionario(funcionarioId)
                .Select(Mapear)
                .ToList();

            return Ok(atendimentos);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // Funcionário assume o atendimento
    [HttpPut("Iniciar/{atendimentoId:int}")]
    public async Task<IActionResult> Iniciar(
        [FromRoute] int atendimentoId,
        [FromBody] AtendimentoIniciar dto)
    {
        try
        {
            await _atendimentoAplicacao.IniciarAtendimento(
                atendimentoId,
                dto.FuncionarioID);

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }

    // Finalizar atendimento
    [HttpPut("Finalizar/{atendimentoId:int}")]
    public async Task<IActionResult> Finalizar(
        [FromRoute] int atendimentoId)
    {
        try
        {
            await _atendimentoAplicacao.FinalizarAtendimento(
                atendimentoId);

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }

    // Converter entidade em resposta da API
    private static AtendimentoResposta Mapear(
        Atendimento atendimento)
    {
        return new AtendimentoResposta
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
}
