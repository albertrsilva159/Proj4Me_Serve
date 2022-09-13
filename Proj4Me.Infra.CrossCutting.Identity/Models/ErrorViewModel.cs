using System;
using System.Collections.Generic;
using System.Text;

namespace Proj4Me.Infra.CrossCutting.Identity.Models
{
  public class ErrorViewModel
  {
    public string RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
  }
}
