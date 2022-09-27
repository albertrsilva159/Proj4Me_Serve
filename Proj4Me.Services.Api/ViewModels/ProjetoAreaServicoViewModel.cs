using System;
using System.ComponentModel.DataAnnotations;

namespace Proj4Me.Services.Api.ViewModels
{
  public class ProjetoAreaServicoViewModel
  {
    public ProjetoAreaServicoViewModel()
    {
      Id = Guid.NewGuid();
      //Colaborador = new ColaboradorViewModel();
      //Cliente = new ClienteViewModel();
    }

    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Nome é requerido")]
    [MinLength(2, ErrorMessage = "Tamanho minimo requerido 2")]
    [MaxLength(150, ErrorMessage = "Tamanho maximo 150 caracteres")]
    [Display(Name = "Nome do Projeto")]
    public string Nome { get; set; }

    [Display(Name = "Descricao do projeto")]
    public string Descricao { get; set; }

    ///public ClienteViewModel Cliente { get; set; }

    public PerfilViewModel Perfil { get; set; }

    public ColaboradorViewModel Colaborador { get; set; }

    //public Guid ClienteId { get; set; }
    [Required(ErrorMessage = "Perfil deve ser informado")]
    public Guid PerfilId { get; set; }
    [Required(ErrorMessage = "Colaborador deve ser informado")]
    public Guid ColaboradorId { get; set; }












  }
}
