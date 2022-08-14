using Proj4Me.Domain.Core.Models;
using System;
using FluentValidation;
using Proj4Me.Domain.Colaboradores;
using Proj4Me.Domain.Perfis;

namespace Proj4Me.Domain.ProjetosAreaServicos
{
  public class ProjetoAreaServico : Entity<ProjetoAreaServico>
  {
    public ProjetoAreaServico(string nome, string descricao, int index)
    {
      Id = Guid.NewGuid();
      Nome = nome;
      Descricao = descricao;
      Index = index;
    }

    // por ele ser privado entao so a classe interna NovoProjetoAreaServicoCompleto tem acesso a esse contrutor
    private ProjetoAreaServico() { }

    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public int Index { get; private set; }
    public DateTime Registro { get; private set; }

    public Guid? PerfilId { get; private set; }
    public Guid? ColaboradorId { get; private set; }


    //talvez trocar para nao aceitar nulo
    // EF propriedades de navegacao
    public virtual Perfil Perfil { get; private set; }
    public virtual Colaborador Colaborador { get; private set; }

    public override bool EhValido()
    {
      Validar();
      return ValidationResult.IsValid;
    }

    //public void AtribuirCliente(Cliente cliente)
    //{
    //  if (!cliente.EhValido()) return;
    //  Cliente = cliente;
    //}

    //Fazend dessa forma caso precise alterar o colaborador do projeto
    public void AtribuirColaborador(Colaborador colaborador)
    {
      if (!colaborador.EhValido()) return;
      Colaborador = colaborador;
    }

    public void AtribuirPerfil(Perfil perfil)
    {
      if (!perfil.EhValido()) return;
      Perfil = perfil;
    }

    #region Validações

    private void Validar()
    {
      ValidarNome();
      ValidationResult = Validate(this); // aqui a entidade esta se auto validando, onde o validationResult esta na classe Entity

      // Validacoes adicionais 
      //ValidarCliente();
    }

    private void ValidarNome()
    {
      RuleFor(c => c.Nome)
          .NotEmpty().WithMessage("O nome do evento precisa ser fornecido")
          .Length(2, 150).WithMessage("O nome do projeto precisa ter entre 2 e 150 caracteres");
    }

    //private void ValidarCliente()
    //{
    //  //if (ClienteId) return;
    //  if (Cliente.EhValido()) return;

    //  foreach (var error in Cliente.ValidationResult.Errors)
    //  {
    //    ValidationResult.Errors.Add(error);
    //  }
    //}
    #endregion

    public static class ProjetoAreaServicoFactory
    {
      public static ProjetoAreaServico NovoProjetoAreaServicoCompleto(Guid id, string nome, string descricao, Guid? colaboradorId, Guid? perfilId)
      {
        var projetoAreaServico = new ProjetoAreaServico()
        {
          Id = id,
          Nome = nome,
          Descricao = descricao,
          PerfilId = perfilId,
          ColaboradorId = colaboradorId
        };

        if (colaboradorId.HasValue)
        {
          projetoAreaServico.ColaboradorId = colaboradorId.Value;
        }

        if (perfilId.HasValue)
        {
          projetoAreaServico.PerfilId = perfilId.Value;
        }


        return projetoAreaServico;
      }
    }
  }
}
