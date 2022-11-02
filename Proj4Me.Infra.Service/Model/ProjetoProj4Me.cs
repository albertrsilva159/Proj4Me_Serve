using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
//using static Proj4Me.Infra.Service.Model.ProjetoProj4Me;

namespace Proj4Me.Infra.Service.Model
{
  public partial class ProjetoProj4Me
  {
    [JsonProperty("index")]
    public int Index { get; set; }

    [JsonProperty("title")]
    public string NomeProjeto { get; set; }

    [JsonProperty("restricted")]
    public bool Restricted { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("leader")]
    public Leader Leader { get; set; }

    [JsonProperty("client")]
    public Client Client { get; set; }

    [JsonProperty("startDate")]
    public DateTime? DataInicio { get; set; }

    [JsonProperty("dueDate")]
    public object DueDate { get; set; }

    [JsonProperty("tags")]
    public List<object> Tags { get; set; }

    [JsonProperty("teamMembers")]
    public List<TeamMember> TeamMembers { get; set; }

    [JsonProperty("customFields")]
    public List<CustomField> CustomFields { get; set; }
  }

  public partial class Client
  {
    [JsonProperty("id")]
    public int IndexClienteProj4Me { get; set; }

    [JsonProperty("name")]
    public string Nome { get; set; }
  }

  public partial class CustomField
  {
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("customFieldID")]
    public int CustomFieldId { get; set; }

    [JsonProperty("value")]
    public object Value { get; set; }

    [JsonProperty("label")]
    public string Label { get; set; }
  }

  public partial class Leader
  {
    [JsonProperty("id")]
    public int IndexColaboradorProj4Me { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("businessEmail")]
    public string BusinessEmail { get; set; }
  }

  public partial class TeamMember
  {
    [JsonProperty("collaborator")]
    public Leader Collaborator { get; set; }
  }
}
