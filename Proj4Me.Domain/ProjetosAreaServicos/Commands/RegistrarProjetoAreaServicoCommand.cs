using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj4Me.Domain.ProjetosAreaServicos.Commands
{

  /// <summary>
  /// A boa pratica fala que é preciso validar qualquer informação, porem as informações abaixo ja estao sendo validadas nas entidades e como o command automaticamente vai se transformar em entidade entao nao faz necessario
  /// </summary>
  public class RegistrarProjetoAreaServicoCommand : BaseProjetoAreaServicoCommand
  {

    /// <summary>
    /// o Commad é uma DTO praticamente e nao deve fazer referencia a nenhuma entidade
    /// </summary>
    /// <param name="nome"></param>
    /// <param name="descricao"></param>
    /// <param name="descricaoLonga"></param>
    /// <param name="cliente"></param>
    /// <param name="colaboradorId"></param>
    public RegistrarProjetoAreaServicoCommand(string nome, string descricao, Guid colaboradorId, Guid perfilId, Guid clienteId)
    {
      Nome = nome;
      Descricao = descricao;
      ClienteId = clienteId;
      PerfilId = perfilId;
      ColaboradorId = colaboradorId;
      //Cliente = cliente;

    }

    //public IncluirClienteProjetoAreaServicoCommand Cliente { get; private set; }  

  }
}
