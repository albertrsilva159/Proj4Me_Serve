using FluentValidation;
using Proj4Me.Domain.Colaboradores;
using Proj4Me.Domain.Core.Events;
using Proj4Me.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Proj4Me.Domain.ProjetosAreaServicos
{
  public class Tarefa : Entity<Tarefa>
  {
    public Tarefa ( int indexTarefaProj4Me, 
                    string nomeTarefa, 
                    DateTime? dataEsforco,  
                    string nomeColaborador, 
                    string? comentario, 
                    string? tempoGastoDetalhado, 
                    string? totalTempoGasto
                    //int indexProjetoProj4Me
      )
    {
      Id = Guid.NewGuid();
      IndexTarefaProj4Me = indexTarefaProj4Me;
      NomeTarefa = nomeTarefa;
      DataEsforco = dataEsforco;
      NomeColaborador = nomeColaborador;
      Comentario = comentario;
      TempoGastoDetalhado = tempoGastoDetalhado;
      TotalTempoGasto = totalTempoGasto;
      //IndexProjetoProj4Me = indexProjetoProj4Me;
    }


    public Tarefa() { }

    public Guid Id { get; set; }
    public int IndexTarefaProj4Me { get; set; }
    public string NomeTarefa { get; set;}    
    public DateTime? DataEsforco { get; set; } 
    public string NomeColaborador { get; set; }
    public string? Comentario { get; set; }
    public string? TempoGastoDetalhado { get; set; }
    public string? TotalTempoGasto { get; set; }
    public int IndexProjetoProj4Me { get; set; }
    // public int IndexProjetoProj4Me { get;  set; }

    public virtual ProjetoAreaServico ProjetoAreaServico { get; set; }

    public override bool EhValido()
    {
      RuleFor(c => c.NomeTarefa)
               .NotEmpty().WithMessage("O nome da tarefa precisa ser preenchido")
               .Length(2, 150).WithMessage("O nome da tarefa precisa ter entre 2 e 150 caracteres");

      ValidationResult = Validate(this);

      return ValidationResult.IsValid;
    }

    //public TarefaDetalheEsforco TarefaDetalheEsforco { get; set; }
    //public Colaborador Colaborador { get; set; }
  }
}
