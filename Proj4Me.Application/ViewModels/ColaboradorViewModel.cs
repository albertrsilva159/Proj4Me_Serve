using System;
using System.ComponentModel.DataAnnotations;

namespace Proj4Me.Application.ViewModels
{
  public class ColaboradorViewModel
  {

    public ColaboradorViewModel()
    {
      Id = Guid.NewGuid();
      //Cliente = new ClienteViewModel();
    }

    [Key]
    public Guid Id { get; set; }


    [Required(ErrorMessage = "Nome é requerido")]
    [MinLength(2, ErrorMessage = "Tamanho minimo requerido 2")]
    [MaxLength(150, ErrorMessage = "Tamanho maximo 150 caracteres")]
    [Display(Name = "Nome do Colaborador")]
    public string Nome { get; set; }

    [MaxLength(150, ErrorMessage = "Tamanho maximo 150 caracteres")]
    [Display(Name = "Email")]
    public string Email { get; set; }
  }
}