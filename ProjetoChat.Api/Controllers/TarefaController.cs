using Microsoft.AspNetCore.Mvc;
using ProjetoChat.Aplicacao;
using System.Threading.Tasks;
using System;
using System.Linq;
using ProjetoChat.Api.Model.Resposta;



namespace ProjetoChat.Api
{
    [ApiController]
    [Route("[Controller]")]


    public class TarefaController : ControllerBase
{
    private readonly ITarefaAplicacao _tarefaAplicacao;

    public TarefaController(ITarefaAplicacao tarefaAplicacao)
    {
        _tarefaAplicacao = tarefaAplicacao;
    }

        
        [HttpGet]
        [Route("Listar")]
        public async Task<ActionResult> Get()
        {
            try
            {
                var tarefa= _tarefaAplicacao.ListarTarefas();

                var tarefaResposta = tarefa.Select(tarefa => new TarefaResposta
                {
                        ID = tarefa.ID,
                        Nome = tarefa.Nome,
                        Completa = tarefa.Completa
                }
                
                
                );

                return Ok(tarefaResposta);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        





    }




}