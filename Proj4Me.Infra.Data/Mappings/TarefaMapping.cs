using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Proj4Me.Domain.ProjetosAreaServicos;
using Proj4Me.Infra.Data.Extensions;


namespace Proj4Me.Infra.Data.Mappings
{
  public class TarefaMapping : EntityTypeConfiguration<Tarefa>
  {

    public override void Map(EntityTypeBuilder<Tarefa> builder)
    {
      builder.Property(e => e.NomeTarefa)
               .IsRequired()
               .HasMaxLength(300)
               .HasColumnType("varchar(300)");

      builder.Property(e => e.IndexTarefaProj4Me)
               .IsRequired()
               .HasColumnType("int");

      builder.Property(e => e.DataEsforco)
           .IsRequired()
           .HasColumnType("DateTime");

      builder.Property(e => e.NomeColaborador)
            .HasColumnType("varchar(300)");

      builder.Property(e => e.Comentario)
            .IsRequired()
            .HasColumnType("varchar(5000)");

      builder.Property(e => e.TotalTempoGasto)
            .IsRequired()
            .HasColumnType("varchar(100)");

      builder.Property(e => e.TempoGastoDetalhado)
            .IsRequired()
            .HasColumnType("varchar(100)");

      builder.Property(e => e.IndexProjetoProj4Me)
            .IsRequired()
            .HasColumnType("int");


      builder.Ignore(e => e.CascadeMode);
      builder.Ignore(e => e.ClassLevelCascadeMode);
      builder.Ignore(e => e.RuleLevelCascadeMode);
      builder.Ignore(e => e.ValidationResult);

      //nomeando tabela
      builder.ToTable("Tarefa");
    }
  }
}
