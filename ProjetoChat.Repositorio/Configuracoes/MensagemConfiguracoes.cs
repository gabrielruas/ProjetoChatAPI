using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoChat.Dominio.Entidades;

namespace ProjetoChat.Repositorio.Configuracoes;

public class MensagemConfiguracoes : IEntityTypeConfiguration<Mensagem>
{
    public void Configure(EntityTypeBuilder<Mensagem> builder)
    {
        builder.ToTable("mensagens").HasKey(x => x.ID);
        builder.Property(x => x.ID).HasColumnName("MensagemId");
        builder.Property(x => x.Remetente).IsRequired();
        builder.Property(x => x.Texto).IsRequired();
        builder.Property(x => x.DataEnvio).IsRequired();
        builder.HasOne<Atendimento>().WithMany().HasForeignKey(x => x.AtendimentoID)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(x => new { x.AtendimentoID, x.DataEnvio, x.ID });
    }
}
