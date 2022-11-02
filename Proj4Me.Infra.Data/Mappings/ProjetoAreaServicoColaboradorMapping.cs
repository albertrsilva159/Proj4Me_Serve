using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Proj4Me.Domain.ProjetosAreaServicos;
using Proj4Me.Infra.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace Proj4Me.Infra.Data.Mappings
{
  internal class ProjetoAreaServicoColaboradorMapping : EntityTypeConfiguration<ProjetoAreaServicoColaborador>
  {
    public override void Map(EntityTypeBuilder<ProjetoAreaServicoColaborador> builder)
    {
      builder.ToTable("ProjetoAreaServicoColaborador");


      builder.HasOne(bc => bc.Colaborador)
       .WithMany(b => b.ProjetosAreaServicoColaboradores)
        .HasForeignKey(bc => bc.ColaboradorId);

      builder.HasOne(bc => bc.ProjetoAreaServico)
      .WithMany(c => c.ProjetosAreaServicoColaboradores)
       .HasForeignKey(bc => bc.ProjetoAreaServicoId);
    }
  }
}
