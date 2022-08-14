using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Proj4Me.Domain.ProjetosAreaServicos;
using Proj4Me.Infra.Data.Extensions;

namespace Proj4Me.Infra.Data.Mappings
{
  //public class ClienteMapping : EntityTypeConfiguration<Cliente>
  //{
  //  public override void Map(EntityTypeBuilder<Cliente> builder)
  //  {
  //    //builder.HasOne(c => c.ProjetoAreaServico)
  //    //.WithOne(c => c.Cliente)
  //    //.HasForeignKey<Cliente>(c => c.ProjetoAreaServicoId)
  //    //.IsRequired(false);

  //    //builder.Property(e => e.Nome)
  //    //         .IsRequired()
  //    //         .HasMaxLength(100)
  //    //         .HasColumnType("varchar(150)");

  //    //builder.Ignore(e => e.ValidationResult);

  //    //builder.Ignore(e => e.CascadeMode);

  //    ////nomeando tabela
  //    //builder.ToTable("Cliente");
  //  }
  //}
}
