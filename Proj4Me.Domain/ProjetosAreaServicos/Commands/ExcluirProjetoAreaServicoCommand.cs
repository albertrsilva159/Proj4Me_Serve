using Proj4Me.Domain.Core.Commands;
using System;


namespace Proj4Me.Domain.ProjetosAreaServicos.Commands
{
  public class ExcluirProjetoAreaServicoCommand : BaseProjetoAreaServicoCommand
  {
    public ExcluirProjetoAreaServicoCommand(Guid id)
    {
      Id = id;
      AggregateId = Id; // pra identificar o agregado, caso usar a funcionalidade event source que é salvar todos os comandos disparados é preciso saber qual é o agregado
    }
  }
}
