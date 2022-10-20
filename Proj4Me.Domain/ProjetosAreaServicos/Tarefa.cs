using FluentValidation;
using Proj4Me.Domain.Colaboradores;
using Proj4Me.Domain.Core.Events;
using Proj4Me.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proj4Me.Domain.ProjetosAreaServicos
{
  public class Tarefa : Entity<Tarefa>
  {
    public Tarefa (long index, string nomeTarefa, DateTime? dataEsforco,  string nomeColaborador, string? comentario, string? tempoGastoDetalhado, string? totalTempoGasto)
    {
      Index = index;
      NomeTarefa = nomeTarefa;
      DataEsforco = dataEsforco;
      NomeColaborador = nomeColaborador;
      Comentario = comentario;
      TempoGastoDetalhado = tempoGastoDetalhado;
      TotalTempoGasto = totalTempoGasto;
    }


    public Tarefa() { }

    public long Index { get; set; }
    public string NomeTarefa { get; set;}    
    public DateTime? DataEsforco { get; set; } 
    public string NomeColaborador { get; set; }
    public string? Comentario { get; set; }
    public string? TempoGastoDetalhado { get; set; }
    public string? TotalTempoGasto { get; set; }
    
    public ProjetoAreaServico ProjetoAreaServico { get; set; }

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
