
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjetoChat.Dominio.Entidades;

namespace DataAccess.Repositorio
{
    public class AtendimentoRepositorio : BaseRepositorio, IAtendimentoRepositorio
    {
        public AtendimentoRepositorio(ProjetoChatContexto contexto)
            : base(contexto)
        {
        }

        // Salvar novo atendimento
        public async Task<int> Salvar(Atendimento atendimento)
        {
            await _contexto.Atendimentos.AddAsync(atendimento);
            await _contexto.SaveChangesAsync();

            return atendimento.ID;
        }

        // Atualizar dados do cliente
        public async Task Atualizar(Atendimento atendimento)
        {
            var registro = await _contexto.Atendimentos
                .FindAsync(atendimento.ID);

            if (registro == null)
            {
                throw new InvalidOperationException(
                    "Atendimento não encontrado.");
            }

            registro.NomeCliente = atendimento.NomeCliente;
            registro.EmailCliente = atendimento.EmailCliente;
            registro.Celular = atendimento.Celular;
            registro.Assunto = atendimento.Assunto;

            await _contexto.SaveChangesAsync();
        }

        // Buscar atendimento pelo ID
        public Atendimento? Obter(int atendimentoId)
        {
            return _contexto.Atendimentos
                .AsNoTracking()
                .FirstOrDefault(x => x.ID == atendimentoId);
        }

        // Listar todos os atendimentos
        public IEnumerable<Atendimento> Listar()
        {
            return _contexto.Atendimentos
                .AsNoTracking()
                .OrderBy(x => x.DataInicio)
                .ToList();
        }

        // Listar atendimentos por status
        public IEnumerable<Atendimento> ListarPorStatus(string status)
        {
            return _contexto.Atendimentos
                .AsNoTracking()
                .Where(x => x.Status == status)
                .OrderBy(x => x.DataInicio)
                .ToList();
        }

        // Listar atendimentos de um funcionário
        public IEnumerable<Atendimento> ListarPorFuncionario(
            int funcionarioId)
        {
            return _contexto.Atendimentos
                .AsNoTracking()
                .Where(x => x.FuncionarioID == funcionarioId)
                .OrderBy(x => x.DataInicio)
                .ToList();
        }

        // Funcionário assume um atendimento
        public async Task IniciarAtendimento(
            int atendimentoId,
            int funcionarioId)
        {
            if (funcionarioId <= 0)
            {
                throw new ArgumentException(
                    "Funcionário inválido.");
            }

            var atendimento = await _contexto.Atendimentos
                .FindAsync(atendimentoId);

            if (atendimento == null)
            {
                throw new InvalidOperationException(
                    "Atendimento não encontrado.");
            }

            if (atendimento.Status != "aguardando")
            {
                throw new InvalidOperationException(
                    "Este atendimento não está aguardando.");
            }

            atendimento.IniciarAtendimento(funcionarioId);

            await _contexto.SaveChangesAsync();
        }

        // Finalizar atendimento
        public async Task FinalizarAtendimento(int atendimentoId)
        {
            var atendimento = await _contexto.Atendimentos
                .FindAsync(atendimentoId);

            if (atendimento == null)
            {
                throw new InvalidOperationException(
                    "Atendimento não encontrado.");
            }

            if (atendimento.Status != "em_atendimento")
            {
                throw new InvalidOperationException(
                    "Este atendimento não está em andamento.");
            }

            atendimento.FinalizarAtendimento();

            await _contexto.SaveChangesAsync();
        }
    }
}
