using FluentValidation;
using Proj4Me.Domain.Core.Models;
using Proj4Me.Domain.ProjetosAreaServicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj4Me.Domain.Perfis
{
  public  class Perfil : Entity<Perfil>
  {
    public Perfil (Guid id, string nome)
    {
      Id = id;
      Nome = nome;
    }

    //EF Construtor
    public Perfil() { }

    //EF Propriedade de navegação onde um perfil pode ter varios projetos por isso a coleção
    public virtual ICollection<ProjetoAreaServico> ProjetoAreaServico { get; set; }

    public string Nome { get; private set; }
    public Guid? ProjetoAreaServicoId { get; }

    public override bool EhValido()
    {
      RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O nome do perfil precisa ser fornecido")
                .Length(2, 150).WithMessage("O Logradouro precisa ter entre 2 e 100 caracteres");

      ValidationResult = Validate(this);

      return ValidationResult.IsValid;
    }
  }
}
