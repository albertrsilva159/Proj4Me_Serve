using FluentValidation;
using Proj4Me.Domain.Core.Models;
using System;

namespace Proj4Me.Domain.ProjetosAreaServicos
{
  public class Cliente //: Entity<Cliente>
  {
    //// Construtor para o EF
    //protected Cliente() { }
    //public string Nome { get;  set; }
    //public Guid? ProjetoAreaServicoId { get; private set; }

    //// EF Propriedade de Navegação
    //public virtual ProjetoAreaServico ProjetoAreaServico { get; private set; }

    //public Cliente(Guid id, string nome, Guid? projetoAreaServicoId)
    //{
    //  Id = id;
    //  Nome = nome;
    //  ProjetoAreaServicoId = projetoAreaServicoId;
    //}
    //public override bool EhValido()
    //{
    //  RuleFor(c => c.Nome)
    //            .NotEmpty().WithMessage("O nome do cliente precisa ser fornecido")
    //            .Length(2, 150).WithMessage("O Logradouro precisa ter entre 2 e 150 caracteres");

    //  ValidationResult = Validate(this);

    //  return ValidationResult.IsValid;
    //}


  }
}