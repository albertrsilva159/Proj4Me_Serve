using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Proj4Me.Domain.Colaboradores;
using Proj4Me.Infra.Data.Extensions;


namespace Proj4Me.Infra.Data.Mappings
{
  public class ColaboradorMapping : EntityTypeConfiguration<Colaborador>
  {

    public override void Map(EntityTypeBuilder<Colaborador> builder)
    {
      builder.Property(e => e.Nome)
               .IsRequired()
               .HasMaxLength(150)
               .HasColumnType("varchar(150)");

      builder.Property(e => e.Email)
               .IsRequired()
               .HasMaxLength(100)
               .HasColumnType("varchar(100)");

      builder.Ignore(e => e.CascadeMode);
      builder.Ignore(e => e.ClassLevelCascadeMode);
      builder.Ignore(e => e.RuleLevelCascadeMode);
      builder.Ignore(e => e.ValidationResult);

      //nomeando tabela
      builder.ToTable("Colaborador");
    }
  }
}
