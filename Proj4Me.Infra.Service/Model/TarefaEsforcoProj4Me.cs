using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj4Me.Infra.Service.Model
{
  public class TarefaEsforcoProj4Me
  {
    public int id { get; set; }
    public Worker worker { get; set; }
    public int effort { get; set; }
    //public string effortDate { get; set; }

    private DateTime _effortDate;
    public string effortDate
    {
      get { return _effortDate.ToString(); }
      set
      {
        if (value != null)
          _effortDate = DateTime.Parse(value);
      }
    }

    public string? comment { get; set; }
  }

  public class Worker
  {
    public int id { get; set; }
    public string name { get; set; }
    public string businessEmail { get; set; }
  }
}
