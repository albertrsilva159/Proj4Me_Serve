using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Proj4Me.Domain.Perfis;
using Proj4Me.Infra.Data.Extensions;

namespace Proj4Me.Infra.Data.Mappings
{
  public class PerfilMapping : EntityTypeConfiguration<Perfil>
  {
    public override void Map(EntityTypeBuilder<Perfil> builder)
    {
      builder.Property(e => e.Nome)
               .IsRequired()
               .HasMaxLength(150)
               .HasColumnType("varchar(150)");

      builder.Ignore(e => e.CascadeMode);
      builder.Ignore(e => e.ClassLevelCascadeMode);
      builder.Ignore(e => e.RuleLevelCascadeMode);
      builder.Ignore(e => e.ValidationResult);

      //nomeando tabela
      builder.ToTable("Perfil");
    }
  }
}