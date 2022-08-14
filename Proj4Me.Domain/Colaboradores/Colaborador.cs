using FluentValidation;
using Proj4Me.Domain.Core.Models;
using Proj4Me.Domain.ProjetosAreaServicos;
using System;
using System.Collections.Generic;

namespace Proj4Me.Domain.Colaboradores
{
  public class Colaborador : Entity<Colaborador>
  {
    public Colaborador (Guid id, string nome, string email) 
    {
      Id = id;
      Nome = nome; 
      Email = email; 
    }

    //EF Construtor
    public Colaborador() { }

    //EF Propriedade de navegação onde um colaborador pode ter varios projetos por isso a coleção
    public virtual ICollection<ProjetoAreaServico> ProjetoAreaServico { get; set; } 

    public string Nome { get;  private set; }
    public string Email { get; private set; }
    public Guid? ColaboradorId { get; }

    public override bool EhValido()
    {
      RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O nome do colaborador precisa ser fornecido")
                .Length(2, 150).WithMessage("O Logradouro precisa ter entre 2 e 100 caracteres");

      ValidationResult = Validate(this);

      return ValidationResult.IsValid;
    }

  }
}