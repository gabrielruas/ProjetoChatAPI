
using Microsoft.AspNetCore.Mvc;
using ProjetoChat.Dominio.Entidades;
using ProjetoChat.Api.Model.Requisicao;
using ProjetoChat.Api.Model.Resposta;
using ProjetoChat.Aplicacao;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
namespace ProjetoChat.Api
{

    [ApiController]
    [Route("[Controller]")]
    public class UsuarioController : ControllerBase

    {
        private readonly IUsuarioAplicacao _usuarioAplicacao;

        public UsuarioController(IUsuarioAplicacao usuarioAplicacao)
        {
            _usuarioAplicacao = usuarioAplicacao;
        }

        [HttpPost]
        [Route("Criar")]
        public async Task<ActionResult> Criar([FromBody] UsuarioCriar usuarioCriar)
        {

            try
            {
                var usuarioDominio = new Usuario()
                {
                    Nome = usuarioCriar.Nome,
                    Email = usuarioCriar.Email,
                    Senha = usuarioCriar.Senha,
                    Online = usuarioCriar.Online
                };

                var usuarioID = _usuarioAplicacao.Criar(usuarioDominio);
                return Ok(usuarioID);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message ?? ex.Message);
            }

        }
        [HttpGet]
        [Route("Obter/{usuarioId}")]
        public async Task<ActionResult> Obter([FromRoute] int usuarioId)
        {
            try
            {
                var usuarioDominio = _usuarioAplicacao.Obter(usuarioId);
                var usuarioResposta = new UsuarioResposta()
                {
                    Id = usuarioDominio.ID,
                    Nome = usuarioDominio.Nome,
                    Email = usuarioDominio.Email,
                    Online = usuarioDominio.Online
                };
                return Ok(usuarioResposta);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPut]
        [Route("Atualizar")]
        public async Task<ActionResult> Atualizar([FromBody] UsuarioAtualizar dto)
        {
            try
            {
                var usuario = new Usuario()
                {
                    ID = dto.ID,
                    Nome = dto.Nome,
                    Email = dto.Email,
                    Online = dto.Online
                };
                _usuarioAplicacao.Atualizar(usuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        [Route("AlterarSenha")]
        public async Task<ActionResult> AlterarSenha([FromBody] UsuarioAlterarSenha usuario)
        {
            try
            {
                var usuarioDominio = new Usuario()
                {
                    ID = usuario.ID,
                    Senha = usuario.Senha
                };
                _usuarioAplicacao.AlterarSenha(usuarioDominio, usuario.SenhaAntiga);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpDelete]
        [Route("Deletar/{usuarioId}")]
        public async Task<ActionResult> Deletar([FromBody] int usuarioId)
        {
            try
            {
                _usuarioAplicacao.Deletar(usuarioId);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPut]
        [Route("Restaurar/{usuarioId}")]
        public async Task<ActionResult> Restaurar([FromBody] int usuarioId)
        {
            try
            {
                _usuarioAplicacao.Restaurar(usuarioId);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet]
        [Route("Listar")]
        public async Task<ActionResult> List([FromQuery] bool ativos)
        {
            try
            {
                var usuariosDominio = _usuarioAplicacao.Listar(ativos);

                var usuarios = usuariosDominio.Select(x => new UsuarioResposta()
                {
                    Id = x.ID,
                    Nome = x.Nome,
                    Email = x.Email,
                    Online = x.Online
                }).ToList();

                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("ListarTiposUsuario")]
        public async Task<IActionResult> ListarTiposUsuario()
        {
            try
            {
                var tiposUsuario = new[]
                {
            new
            {
                Id = 1,
                Nome = "Administrador",
                Descricao = "O Administrador tem acesso a todos os campos do sistema"
                
            },
            new
            {
                Id = 2,
                Nome = "Gestor",
                Descricao = "O Gestor tem acesso a alguns campos do sistema"
            },
            new
            {
                Id = 3,
                Nome = "Desenvolvedor",
                Descricao = "O Desenvolvedor tem acesso total durante o desenvolvimento e depois apenas ao seu campo"
            }
        };

                return Ok(tiposUsuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }





    }




}