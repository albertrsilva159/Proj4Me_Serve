using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Proj4Me.Domain.Clientes;
using Proj4Me.Domain.Colaboradores;
using Proj4Me.Domain.Perfis;
using Proj4Me.Domain.ProjetosAreaServicos;
using Proj4Me.Infra.Data.Extensions;
using Proj4Me.Infra.Data.Mappings;
using System.IO;

namespace Proj4Me.Infra.Data.Context
{
  public class ProjetoAreaServicoContext : DbContext
  {
    public DbSet<ProjetoAreaServico> ProjetoAreaServico { get; set; }
    //public DbSet<Cliente> Cliente { get; set; }
    public DbSet<Colaborador> Colaborador { get; set; }
    public DbSet<ProjetoAreaServicoColaborador> ProjetosAreaServicoColaboradores { get; set; }
    public DbSet<Perfil> Perfil { get; set; }
    public DbSet<Cliente> Cliente { get; set; }
    public DbSet<Tarefa> Tarefa { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      #region FLuentAPI
      // o AddConfiguration foi o metodo da extensao model builder 
      modelBuilder.AddConfiguration(new ProjetoAreaServicoMapping());
      modelBuilder.AddConfiguration(new ClienteMapping());
      modelBuilder.AddConfiguration(new ColaboradorMapping());
      modelBuilder.AddConfiguration(new PerfilMapping());
      modelBuilder.AddConfiguration(new ProjetoAreaServicoColaboradorMapping());
      modelBuilder.AddConfiguration(new TarefaMapping());

      modelBuilder.Entity<ProjetoAreaServicoColaborador>().HasKey(x => new { x.ColaboradorId, x.ProjetoAreaServicoId });

      //modelBuilder.Entity<ProjetoAreaServicoColaborador>()
      //   .HasOne(bc => bc.Colaborador)
      //    .WithMany(b => b.ProjetosAreaServicoColaboradores)
      //     .HasForeignKey(bc => bc.ColaboradorId);

      //modelBuilder.Entity<ProjetoAreaServicoColaborador>()
      //    .HasOne(bc => bc.ProjetoAreaServico)
      //    .WithMany(c => c.ProjetosAreaServicoColaboradores)
      //     .HasForeignKey(bc => bc.ProjetoAreaServicoId);

      modelBuilder.Entity<ProjetoAreaServico>().Ignore(c => c.CascadeMode);
      modelBuilder.Entity<ProjetoAreaServico>().Ignore(c => c.ClassLevelCascadeMode);
      modelBuilder.Entity<ProjetoAreaServico>().Ignore(c => c.RuleLevelCascadeMode);
      modelBuilder.Entity<ProjetoAreaServico>().Ignore(c => c.ValidationResult);

      modelBuilder.Entity<Colaborador>().Ignore(c => c.CascadeMode);
      modelBuilder.Entity<Colaborador>().Ignore(c => c.ClassLevelCascadeMode);
      modelBuilder.Entity<Colaborador>().Ignore(c => c.RuleLevelCascadeMode);
      modelBuilder.Entity<Colaborador>().Ignore(c => c.ValidationResult);          

      modelBuilder.Entity<Perfil>().Ignore(c => c.CascadeMode);
      modelBuilder.Entity<Perfil>().Ignore(c => c.ClassLevelCascadeMode);
      modelBuilder.Entity<Perfil>().Ignore(c => c.RuleLevelCascadeMode);
      modelBuilder.Entity<Perfil>().Ignore(c => c.ValidationResult);

      modelBuilder.Entity<Cliente>().Ignore(c => c.CascadeMode);
      modelBuilder.Entity<Cliente>().Ignore(c => c.ClassLevelCascadeMode);
      modelBuilder.Entity<Cliente>().Ignore(c => c.RuleLevelCascadeMode);
      modelBuilder.Entity<Cliente>().Ignore(c => c.ValidationResult);

      modelBuilder.Entity<Tarefa>().Ignore(c => c.CascadeMode);
      modelBuilder.Entity<Tarefa>().Ignore(c => c.ClassLevelCascadeMode);
      modelBuilder.Entity<Tarefa>().Ignore(c => c.RuleLevelCascadeMode);
      modelBuilder.Entity<Tarefa>().Ignore(c => c.ValidationResult);


      #endregion

      base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      ///ConfigurationBuilder vai montar uma configuração que é uma dependencia do 
      ///BasePath é o diretorio raiz, onde o proprio projeto esta
      ///addJsonFile é uma especie de webconfig porem em json
      var config = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json")
          .Build();

      ///Definir qual vai ser a connection string onde informa que como opção vá usar o sqlserver para pegar a connection string do config com nome DefaultConnection
      ///TODO: No aquivo appsetting.json ir na propriedade dele e alterar a opção "Copy to Output Directory" para opção "Copy always" pra ele colocar esse arquivo na pasta bin
      optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
    }

  }
}
