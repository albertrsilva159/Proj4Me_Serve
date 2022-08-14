using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Proj4Me.Domain.ProjetosAreaServicos;
using Proj4Me.Infra.Data.Extensions;

namespace Proj4Me.Infra.Data.Mappings
{
  public class ProjetoAreaServicoMapping : EntityTypeConfiguration<ProjetoAreaServico>
  {
    public override void Map(EntityTypeBuilder<ProjetoAreaServico> builder)
    {
      builder.Property(e => e.Nome)
        .HasColumnType("varchar(150)")
        .IsRequired();

      builder.Property(e => e.Descricao)
       .HasColumnType("varchar(150)");

      builder.Property(e => e.Registro)
       .HasColumnType("varchar");

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
    }
  }
}
