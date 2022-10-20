namespace Proj4Me.Services.Api.ViewModels
{
  public class DTO_ListaProjetos
  {
    public DTO_ListaProjetos(string valor, string chave )
    {
      Valor = valor;
      Chave = chave;
    }
    public DTO_ListaProjetos()
    {    
    }

    public string Valor { get; set; }
    public string Chave { get; set; }
    
  }
}
