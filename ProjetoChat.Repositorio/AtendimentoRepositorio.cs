using ProjetoChat.Dominio.Entidades;

namespace DataAccess.Repositorio
{
    public class AtendimentoRepositorio : BaseRepositorio, IAtendimentoRepositorio
    {
        public AtendimentoRepositorio(ProjetoChatContexto contexto) : base(contexto)
        {
        }

        public async Task<int> Salvar(Atendimento atendimento)
        {
            _contexto.Atendimentos.Add(atendimento);
            await _contexto.SaveChangesAsync();
            return atendimento.ID;
        }

        public async Task Atualizar(Atendimento atendimento)
        {
            _contexto.Atendimentos.Update(atendimento);
            await _contexto.SaveChangesAsync();
        }

        public Atendimento? Obter(int atendimentoId)
        {
            return _contexto.Atendimentos.FirstOrDefault(x => x.ID == atendimentoId);
        }

        public IEnumerable<Atendimento> Listar()
        {
            return _contexto.Atendimentos.OrderBy(x => x.DataInicio).ToList();
        }

        public IEnumerable<Atendimento> ListarPorStatus(string status)
        {
            return _contexto.Atendimentos.Where(x => x.Status == status)
                .OrderBy(x => x.DataInicio).ToList();
        }

        public IEnumerable<Atendimento> ListarPorFuncionario(int funcionarioId)
        {
            return _contexto.Atendimentos.Where(x => x.FuncionarioID == funcionarioId)
                .OrderBy(x => x.DataInicio).ToList();
        }
    }
}
