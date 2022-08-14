using System;

namespace Proj4Me.Domain.Colaboradores.Events
{
  public class ColaboradorRegistradoEvent : BaseColaboradorEvent
  {
    public ColaboradorRegistradoEvent(Guid id, string nome, string descricao)
    {
      Id = id;
      Nome = nome;
      Descricao = descricao;      

      AggregateId = id; // precisa informar o aggregateid para a mensagem e todo mundo é uma mensagem
    }
  }
}