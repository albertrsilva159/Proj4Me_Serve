using System;
using System.ComponentModel.DataAnnotations;

namespace Proj4Me.Services.Api.ViewModels
{
  public class ClienteViewModel
  {
    [Key]
    public Guid Id { get; set; }


    //[Required(ErrorMessage = "Nome é requerido")]
    //[MinLength(2, ErrorMessage = "Tamanho minimo requerido 2")]
    //[MaxLength(100, ErrorMessage = "Tamanho maximo 100 caracteres")]
    [Display(Name = "Nome do Cliente")]
    public string Nome { get; set; }
  }
}