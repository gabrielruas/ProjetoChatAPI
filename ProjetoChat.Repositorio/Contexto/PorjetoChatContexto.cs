using Microsoft.EntityFrameworkCore;
using ProjetoChat.Dominio.Entidades;
using ProjetoChat.Repositorio.Configuracoes;


public class ProjetoChatContexto : DbContext
{
    private DbContextOptions _options;


    public DbSet<Usuario> Usuarios { get; set; }
    public ProjetoChatContexto()
    {
        
    }
   
 
    public ProjetoChatContexto(DbContextOptions options) : base(options)
    {
        _options = options;
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if(_options == null)
            optionsBuilder.UseSqlite(@"Filename=./projetoChat.sqlite;");

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UsuarioConfiguracoes());
    }
}