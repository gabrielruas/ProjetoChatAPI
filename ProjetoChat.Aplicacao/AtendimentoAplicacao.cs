
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjetoChat.Dominio.Entidades;

namespace ProjetoChat.Aplicacao
{
    public class AtendimentoAplicacao : IAtendimentoAplicacao
    {
        private readonly IAtendimentoRepositorio _atendimentoRepositorio;
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public AtendimentoAplicacao(
            IAtendimentoRepositorio atendimentoRepositorio,
            IUsuarioRepositorio usuarioRepositorio)
        {
            _atendimentoRepositorio = atendimentoRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
        }

        // Criar atendimento
        public async Task<int> Criar(Atendimento atendimento)
        {
            ValidarInformacoes(atendimento);

            atendimento.Status = "aguardando";
            atendimento.FuncionarioID = null;
            atendimento.DataInicio = DateTime.Now;
            atendimento.DataFim = null;

            return await _atendimentoRepositorio.Salvar(atendimento);
        }

        // Atualizar dados do cliente
        public async Task Atualizar(Atendimento atendimento)
        {
            ValidarInformacoes(atendimento);
            ValidarId(atendimento.ID, "Atendimento");

            // Confirma que o atendimento existe
            Obter(atendimento.ID);

            await _atendimentoRepositorio.Atualizar(atendimento);
        }

        // Obter atendimento pelo ID
        public Atendimento Obter(int atendimentoId)
        {
            ValidarId(atendimentoId, "Atendimento");

            return _atendimentoRepositorio.Obter(atendimentoId)
                ?? throw new InvalidOperationException(
                    "Atendimento não encontrado.");
        }

        // Listar todos os atendimentos
        public IEnumerable<Atendimento> Listar()
        {
            return _atendimentoRepositorio.Listar();
        }

        // Listar atendimentos por status
        public IEnumerable<Atendimento> ListarPorStatus(string status)
        {
            if (status != "aguardando" &&
                status != "em_atendimento" &&
                status != "finalizado")
            {
                throw new ArgumentException(
                    "Status de atendimento inválido.",
                    nameof(status));
            }

            return _atendimentoRepositorio.ListarPorStatus(status);
        }

        // Listar atendimentos por funcionário
        public IEnumerable<Atendimento> ListarPorFuncionario(
            int funcionarioId)
        {
            ValidarId(funcionarioId, "Funcionário");

            return _atendimentoRepositorio
                .ListarPorFuncionario(funcionarioId);
        }

        // Funcionário assume um atendimento
        public async Task IniciarAtendimento(
            int atendimentoId,
            int funcionarioId)
        {
            ValidarId(atendimentoId, "Atendimento");
            ValidarId(funcionarioId, "Funcionário");

            var funcionario = _usuarioRepositorio.Obter(funcionarioId);

            if (funcionario == null || !funcionario.Ativo)
            {
                throw new InvalidOperationException(
                    "Funcionário ativo não encontrado.");
            }

            await _atendimentoRepositorio.IniciarAtendimento(
                atendimentoId,
                funcionarioId);
        }

        // Finalizar atendimento
        public async Task FinalizarAtendimento(int atendimentoId)
        {
            ValidarId(atendimentoId, "Atendimento");

            await _atendimentoRepositorio.FinalizarAtendimento(
                atendimentoId);
        }

        // Validar dados obrigatórios
        private static void ValidarInformacoes(Atendimento atendimento)
        {
            ArgumentNullException.ThrowIfNull(atendimento);

            if (string.IsNullOrWhiteSpace(atendimento.NomeCliente))
            {
                throw new ArgumentException(
                    "Nome do cliente não pode ser vazio.");
            }

            if (string.IsNullOrWhiteSpace(atendimento.EmailCliente))
            {
                throw new ArgumentException(
                    "Email do cliente não pode ser vazio.");
            }

            if (string.IsNullOrWhiteSpace(atendimento.Assunto))
            {
                throw new ArgumentException(
                    "Assunto não pode ser vazio.");
            }
        }

        // Validar ID
        private static void ValidarId(int id, string entidade)
        {
            if (id <= 0)
            {
                throw new ArgumentException(
                    $"{entidade} deve ter um ID maior que zero.");
            }
        }
    }
}
