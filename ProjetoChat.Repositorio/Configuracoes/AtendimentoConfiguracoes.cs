using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoChat.Dominio.Entidades;

namespace ProjetoChat.Repositorio.Configuracoes;

public class AtendimentoConfiguracoes : IEntityTypeConfiguration<Atendimento>
{
    public void Configure(EntityTypeBuilder<Atendimento> builder)
    {
        builder.ToTable("atendimentos").HasKey(x => x.ID);
        builder.Property(x => x.ID).HasColumnName("AtendimentoId");
        builder.Property(x => x.NomeCliente).IsRequired();
        builder.Property(x => x.EmailCliente).IsRequired();
        builder.Property(x => x.Celular).IsRequired(false);
        builder.Property(x => x.Assunto).IsRequired();
        builder.Property(x => x.Status).IsRequired();
        builder.Property(x => x.DataInicio).IsRequired();
        builder.HasOne<Usuario>().WithMany().HasForeignKey(x => x.FuncionarioID)
            .OnDelete(DeleteBehavior.Restrict);
    }
}