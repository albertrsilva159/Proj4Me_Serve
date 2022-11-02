using FluentValidation;
using Proj4Me.Domain.Core.Models;
using Proj4Me.Domain.ProjetosAreaServicos;
using System;
using System.Collections.Generic;
using System.Numerics;

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
    public virtual ICollection<ProjetoAreaServicoColaborador> ProjetosAreaServicoColaboradores { get; set; } 

    public string Nome { get;   set; }
    public string Email { get;  set; }
    public int IndexColaboradorProj4Me { get;  set; }
    public Guid? ColaboradorId { get; }

    public override bool EhValido()
    {
      RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O nome do colaborador precisa ser fornecido")
                .Length(2, 150).WithMessage("O nome precisa ter entre 2 e 100 caracteres");

      ValidationResult = Validate(this);

      return ValidationResult.IsValid;
    }

  }
}