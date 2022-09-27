using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Proj4Me.Infra.CrossCutting.Identity.Authorization
{
  public class SigningCredentialsConfiguration
  {
    private const string SecretKey = "eventosio@meuambienteToken";
    public static readonly SymmetricSecurityKey Key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
    public SigningCredentials SigningCredentials { get; }

    public SigningCredentialsConfiguration()
    {
      SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
    }
  }
}