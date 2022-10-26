using Proj4Me.Domain.Core.Models;
using System;
using FluentValidation;
using Proj4Me.Domain.Colaboradores;
using Proj4Me.Domain.Perfis;
using System.Collections.Generic;
using System.Linq;
using Proj4Me.Domain.Clientes;

namespace Proj4Me.Domain.ProjetosAreaServicos
{
  public class ProjetoAreaServico : Entity<ProjetoAreaServico>
  {
   
    public ProjetoAreaServico(string nome, int index)
    {
      Id = Guid.NewGuid();
      Nome = nome;
      //Descricao = descricao;
      Index = index;
      //DataInicio = dataInicio;
     
    }

    // por ele ser privado entao so a classe interna NovoProjetoAreaServicoCompleto tem acesso a esse contrutor
    private ProjetoAreaServico() { }

    public long Index { get; private set; }
    public string Nome { get; private set; }
    //public string Descricao { get; private set; }

    //public DateTime Registro { get; private set; }

    public Guid? PerfilId { get; private set; }
    public Guid? ColaboradorId { get; private set; }
    public Guid? ClienteId { get; private set; }
    public DateTime? DataInicio { get; private set; }
    public int IndexProjetoProj4Me { get; private set; }
    public List<Tarefa> ListaTarefas { get; set; } 


    // public virtual List<Tarefa> ListaTarefasProjeto { get; private set; }

    //talvez trocar para nao aceitar nulo
    // EF propriedades de navegacao
    public virtual Perfil Perfil { get; private set; }
    public virtual Colaborador Colaborador { get; private set; }
    public virtual Cliente Cliente { get; private set; }
    public ICollection<Tarefa> Tarefas { get; set; }


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
      public static ProjetoAreaServico NovoProjetoAreaServicoCompleto(Guid id, string nome, string descricao, string cliente, Guid? colaboradorId, Guid? perfilId, Guid? clienteId, List<Tarefa>? tarefas)
      {
        var projetoAreaServico = new ProjetoAreaServico()
        {
          Id = id,
          Nome = nome,
         /// Descricao = descricao,
          ClienteId = clienteId,
          ///DataInicio = dataInicio,
          PerfilId = perfilId,
          ColaboradorId = colaboradorId
        };

        //if (tarefas.Count > 0)
        //{
        //  Tarefa.Add(tarefas);
        //}


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

      public static ProjetoAreaServico NovoProjetoAreaServicoBasico(string nome, long index, List<Tarefa> tarefas)
      {
        var projetoAreaServico = new ProjetoAreaServico()
        {
          Index = index,
          Nome = nome,
          //Cliente = cliente == null ? null : cliente,
          Tarefas = tarefas != null ? tarefas.Select(x => x).ToList() : null

        };
        //if (tarefas != null && tarefas.Count > 0)
        //{
        //  projetoAreaServico.ListaTarefas = tarefas.Select(c => new Tarefa
        //  {
        //    Index = c.Index,
        //    NomeTarefa = c.NomeTarefa,
        //    DataCriacao = c.DataCriacao,
        //    DataInicio = c.DataInicio
        //  }).ToList();
        //}






        return projetoAreaServico;
      }

      public static ProjetoAreaServico NovoProjetoAreaServico(int IndexProjetoProj4Me, string nome, Guid idCliente, Guid idColaborador)
      {

        var projetoAreaServico = new ProjetoAreaServico()
        {
          IndexProjetoProj4Me = IndexProjetoProj4Me,
          Nome = nome,
          ClienteId = idCliente,
          ColaboradorId = idColaborador
          //Cliente = cliente == null ? null : cliente,


        };
        //if (tarefas != null && tarefas.Count > 0)
        //{
        //  projetoAreaServico.ListaTarefas = tarefas.Select(c => new Tarefa
        //  {
        //    Index = c.Index,
        //    NomeTarefa = c.NomeTarefa,
        //    DataCriacao = c.DataCriacao,
        //    DataInicio = c.DataInicio
        //  }).ToList();
        //}

        return projetoAreaServico;
      }

    }


  }
}
