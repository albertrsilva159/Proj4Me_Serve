using FluentValidation;
using Proj4Me.Domain.Core.Models;
using Proj4Me.Domain.ProjetosAreaServicos;
using System;
using System.Collections.Generic;

namespace Proj4Me.Domain.Clientes
{
  public class Cliente : Entity<Cliente>
  {
    public Cliente (Guid id, string nome, int indexClienteProj4Me) 
    {
      Id = id;
      Nome = nome;
      IndexClienteProj4Me = indexClienteProj4Me; 
    }

    //EF Construtor
    public Cliente() { }

    //EF Propriedade de navegação onde um cliente pode ter varios projetos por isso a coleção
    public virtual ICollection<ProjetoAreaServico> ProjetoAreaServico { get; set; } 

    public string Nome { get;  set; }
    public int IndexClienteProj4Me { get;  set; }
    public Guid? ClienteId { get; }

    public override bool EhValido()
    {
      RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O nome do cliente precisa ser fornecido")
                .Length(2, 150).WithMessage("O nome precisa ter entre 2 e 100 caracteres");

      ValidationResult = Validate(this);

      return ValidationResult.IsValid;
    }

  }
}