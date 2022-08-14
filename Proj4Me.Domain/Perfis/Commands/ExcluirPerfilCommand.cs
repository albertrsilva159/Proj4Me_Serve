using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj4Me.Domain.Perfis.Commands
{
  public class ExcluirPerfilCommand : BasePerfilCommand
  {
    public ExcluirPerfilCommand(Guid id)
    {
      Id = id;
      AggregateId = Id; // pra identificar o agregado, caso usar a funcionalidade event source que é salvar todos os comandos disparados é preciso saber qual é o agregado
    }
  }
}