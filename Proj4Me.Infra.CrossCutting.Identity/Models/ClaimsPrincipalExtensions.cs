using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Proj4Me.Infra.CrossCutting.Identity.Models
{
  public static class ClaimsPrincipalExtensions
  {
    public static string GetUserId(this ClaimsPrincipal principal)// claimsPrincipal é uma classe que representa o usuario conectado na aplicação, o identity usa ele porem ele é uma classe do asp.net
    {
      if (principal == null)
      {
        throw new ArgumentException(nameof(principal));
      }

      var claim = principal.FindFirst(ClaimTypes.NameIdentifier);
      return claim?.Value;
    }
  }
}
