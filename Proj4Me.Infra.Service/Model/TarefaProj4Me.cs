using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj4Me.Infra.Service.Model
{
  // Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
  public class Owner
  {
    public int id { get; set; }
    public string name { get; set; }
    public string businessEmail { get; set; }
  }

  public class TarefaProj4Me
  {
    public string title { get; set; }
    public int index { get; set; }
    public string longIndex { get; set; }
    public object description { get; set; }
    public object priority { get; set; }
    public int estimatedEffort { get; set; }
    public int? progress { get; set; }
    public object parentTaskIndex { get; set; }
    public Status status { get; set; }
    public DateTime creationDate { get; set; }

    private DateTime _startDate;
    public string startDate
    {
      get { return _startDate.ToString(); }
      set { 
        if(value != null)
          _startDate = DateTime.Parse(value); 
      }
    }   

    private DateTime _doneDate;
    public string doneDate
    {
      get { return _doneDate.ToString(); }
      set
      {
        if (value != null)
          _doneDate = DateTime.Parse(value);
      }
    }

    ////public string dueDate { get; set; }
    public Owner owner { get; set; } 
  }

  public class Status
  {
    public int id { get; set; }
    public string name { get; set; }
  }

  

}
//[{"id":252950,"worker":{"id":66567,"name":"FABIO GIMONSKI","businessEmail":"fabio.gimonski@simply.com.br"},"effort":924,"effortDate":"2022-03-04","comment":null}]