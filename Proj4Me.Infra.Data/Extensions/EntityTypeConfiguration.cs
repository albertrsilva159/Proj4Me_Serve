using Microsoft.EntityFrameworkCore.Metadata.Builders;

/// <summary>
/// Essa classe permite criar os maps fora do contexto.
/// Ela recebe uma entidade onde a entidade e uma classe
/// </summary>
namespace Proj4Me.Infra.Data.Extensions
{
  public abstract class EntityTypeConfiguration<TEntity> where TEntity : class
  {
    public abstract void Map(EntityTypeBuilder<TEntity> builder);
  }
}
