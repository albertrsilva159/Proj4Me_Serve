using System;


namespace Proj4Me.Domain.ProjetosAreaServicos.Events
{
  public class ProjetoAreaServicoRegistradoEvent : BaseProjetoAreaServicoEvent
  {
    public ProjetoAreaServicoRegistradoEvent(Guid id, string nome)
    {
      Nome = nome;
      //Descricao = descricao;
      Id = id;

      AggregateId = id; // precisa informar o aggregateid para a mensagem e todo mundo é uma mensagem
    }
  }
}
