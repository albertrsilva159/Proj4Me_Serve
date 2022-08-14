using System;

namespace Proj4Me.Domain.ProjetosAreaServicos.Events
{
  public class ProjetoAreaServicoAtualizadoEvent : BaseProjetoAreaServicoEvent
  {
    public ProjetoAreaServicoAtualizadoEvent(Guid id, string nome, string descricao)
    {
      Nome = nome;
      Descricao = descricao;
      Id = id;

      AggregateId = id; // precisa informar o aggregateid para a mensagem e todo mundo é uma mensagem
    }
  }
}
