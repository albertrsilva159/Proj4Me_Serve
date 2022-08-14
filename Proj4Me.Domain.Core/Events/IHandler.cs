using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj4Me.Domain.Core.Events
{
  // O "in" que dizer que vai aceitar nao só a classe message mas tambem qualquer outra que derive dela
  public interface IHandler<in T> where T : Message
  {

    void Handle(T message);

  }
}
