using System;

namespace Proj4Me.Domain.Perfis.Events
{
  public class PerfilAtualizadoEvent : BasePerfilEvent
  {
    public PerfilAtualizadoEvent(Guid id, string nome)
    {
      Id = id;
      Nome = nome;

      AggregateId = id; // precisa informar o aggregateid para a mensagem e todo mundo é uma mensagem
    }
  }
}