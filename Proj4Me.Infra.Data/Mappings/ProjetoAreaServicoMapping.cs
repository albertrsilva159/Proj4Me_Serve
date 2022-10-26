using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Proj4Me.Domain.ProjetosAreaServicos;
using Proj4Me.Infra.Data.Extensions;
using System.Numerics;
using System.Reflection.Emit;

namespace Proj4Me.Infra.Data.Mappings
{
  public class ProjetoAreaServicoMapping : EntityTypeConfiguration<ProjetoAreaServico>
  {
    public override void Map(EntityTypeBuilder<ProjetoAreaServico> builder)
    {
      builder.Property(e => e.Nome)
        .HasColumnType("varchar(150)")
        .IsRequired();

      builder.Property(e => e.IndexProjetoProj4Me)
       .HasColumnType("int")
       .IsRequired();


      builder.Ignore(e => e.CascadeMode);
      builder.Ignore(e => e.ClassLevelCascadeMode);
      builder.Ignore(e => e.RuleLevelCascadeMode);
      builder.Ignore(e => e.ValidationResult);

      builder.ToTable("ProjetoAreaServico");

      //relacionamento um projeto pode ter um colaborador, mas um colaborador pode ter varios projetos
      builder.HasOne(c => c.Colaborador)
        .WithMany(p => p.ProjetoAreaServico)
        .HasForeignKey(c => c.ColaboradorId);

      builder.HasOne(c => c.Perfil)
        .WithMany(p => p.ProjetoAreaServico)
        .HasForeignKey(c => c.PerfilId);

      builder.HasOne(c => c.Cliente)
        .WithMany(p => p.ProjetoAreaServico)
        .HasForeignKey(c => c.ClienteId);

      builder.HasMany(p => p.Tarefas)
        .WithOne(c => c.ProjetoAreaServico);

    }
  }
}
