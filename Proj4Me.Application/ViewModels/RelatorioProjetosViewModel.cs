using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj4Me.Application.ViewModels
{
  public class RelatorioProjetosViewModel
  {

    //public string NomeProjeto { get; set; }
    [Display(Name = "Data Inicio")]
    [Required(ErrorMessage = "A data é requerida")]
    [DataType(DataType.Date)]
    public DateTime? DataInicio { get; set; }

    [Display(Name = "Data Fim")]
    [Required(ErrorMessage = "A data é requerida")]
    [DataType(DataType.Date)]
    public DateTime? DataFim { get; set; }
    public Guid ColaboradorId { get; set; }
    public string NomeColaborador { get; set; }
    public Guid PerfilId { get; set; }
    public string NomePerfil { get; set; }
    //public ProjetoAreaServicoViewModel projeto { get; set; }
    ///public List<Tarefa> ListaTarefas { get; set; }
    public List<ProjetoAreaServicoViewModel> ListaProjetos { get; set; }
  }
}
