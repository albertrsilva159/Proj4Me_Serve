//using Proj4Me.Domain.Core.Notifications;
using Proj4Me.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Proj4Me.Infra.CrossCutting.Identity.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Proj4Me.Infra.CrossCutting.Identity.Models.AccountViewModels;
using Proj4Me.Domain.Colaboradores.Commands;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
//using Proj4Me.Infra.CrossCutting.Identity.Authorization;
//using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Proj4Me.Domain.Colaboradores.Repository;
using Proj4Me.Domain.Core.Notification;
using Proj4Me.Infra.CrossCutting.Identity.Authorization;
using Microsoft.Extensions.Options;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using MediatR;
using Microsoft.AspNetCore.Cors;

namespace Proj4Me.Services.Api.Controllers
{


  public class AccountController : BaseController
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ILogger _logger;
    private readonly IConfiguration _configuration;
    private readonly IMediatorHandler _mediator;
    private readonly TokenDescriptor _tokenDescriptor;
    //private readonly JwtTokenOptions _jwtTokenOptions;


    public AccountController(
                UserManager<ApplicationUser> userManager,
                SignInManager<ApplicationUser> signInManager,
                ILoggerFactory loggerFactory,
                IOptions<JwtTokenOptions> jwtTokenOptions,
                IMediatorHandler mediator,         
                TokenDescriptor tokenDescriptor,
                IConfiguration configuration,
                INotificationHandler<DomainNotification> notifications,
                IUser user
                ) : base(notifications, user, mediator)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _mediator = mediator;
      //_jwtTokenOptions = jwtTokenOptions.Value;
      _tokenDescriptor = tokenDescriptor;
      _configuration = configuration;
      _logger = loggerFactory.CreateLogger<AccountController>();
      //ThrowIfInvalidOptions(_jwtTokenOptions);// assim que o token for passado ele vai ser validado      
    }

    // padrao para trabalhar com a data no token
    private static long ToUnixEpochDate(DateTime date)
    => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);


    
   
    //[EnableCors("CorsApi")]
    [AllowAnonymous]
    [Route("nova-conta")]
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterViewModel model, int version)
    {
      if (version == 2)
      {
        return Response(new { Message = "API V2 não disponível" });
      }

      if (!ModelState.IsValid)
      {
        NotificarErroModelInvalida();
        return Response();
      }

      var user = new ApplicationUser { UserName = model.Email, Email = model.Email };

      var result = await _userManager.CreateAsync(user, model.Password);

      if (result.Succeeded)
      {
        await _userManager.AddClaimAsync(user, new Claim("Projetos", "Ler"));
        await _userManager.AddClaimAsync(user, new Claim("Projetos", "Gravar"));

        var registroCommand = new RegistrarColaboradorCommand(Guid.Parse(user.Id),model.Nome, user.Email);
        await _mediator.EnviarComando(registroCommand);

        if (!OperacaoValida())
        {
          await _userManager.DeleteAsync(user);
          return Response(model);
        }

        _logger.LogInformation(1, "Usuario criado com sucesso!");

        var response = await GerarTokenUsuario(new LoginViewModel { Email = model.Email, Password = model.Password, RememberMe = false});
       
        return Response(response);
        
      }
      AdicionarErrosIdentity(result);
      return Response(model);
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("conta")]
    public async Task<IActionResult> Login([FromBody] LoginViewModel model)
    {
      if (!ModelState.IsValid)
      {
        NotificarErroModelInvalida();
        return Response(model);
      }

      var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, true);

      if (result.Succeeded)
      {
        _logger.LogInformation(1, "Usuario logado com sucesso");
        //var response = generateJwtToken(model);
        var response = await GerarTokenUsuario(model);
        return Response(response);
      }

      NotificarErro(result.ToString(), "Falha ao realizar o login");
      return Response(model);
    }

    private void AdicionarErrosIdentity(IdentityResult result)
    {
      foreach (var erro in result.Errors)
      {
        NotificarErro(result.ToString(), erro.Description);
      }
    }
    private string generateJwtToken(LoginViewModel login)
    {
      var user = _userManager.FindByEmailAsync(login.Email);// encontrar usuario pelo email
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes("EventosIoTokenServer");
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new Claim[]
          {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
          }),
        Expires = DateTime.UtcNow.AddMinutes(15),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };
      var token = tokenHandler.CreateToken(tokenDescriptor);
      return tokenHandler.WriteToken(token);
    }


    private async Task<object> GerarTokenUsuario(LoginViewModel login)
    {
      var user = await _userManager.FindByEmailAsync(login.Email);// encontrar usuario pelo email
      var userClaims = await _userManager.GetClaimsAsync(user);// pegar as claims do usuario
      var key = Encoding.ASCII.GetBytes("EventosIoTokenServer");

      userClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
      userClaims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
      userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
      userClaims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));// quando o token foi gerado

      // Necessário converver para IdentityClaims
      var identityClaims = new ClaimsIdentity();
      identityClaims.AddClaims(userClaims);

      var handler = new JwtSecurityTokenHandler();
      var signingConf = new SigningCredentialsConfiguration();

      var securityToken = handler.CreateToken(new SecurityTokenDescriptor
      {
        Issuer = _tokenDescriptor.Issuer,
        Audience = _tokenDescriptor.Audience,
        SigningCredentials = signingConf.SigningCredentials,
        Subject = identityClaims,
        NotBefore = DateTime.Now,
        Expires = DateTime.Now.AddMinutes(_tokenDescriptor.MinutesValid)
      });

      //var jwt = new JwtSecurityToken(
      //      issuer: _configuration["JwtTokenOptions:Issuer"], //_jwtTokenOptions.Issuer, 
      //      audience:  _jwtTokenOptions.Audience,
      //      claims: userClaims,
      //      notBefore: _jwtTokenOptions.NotBefore,
      //      expires: _jwtTokenOptions.Expiration,
      //      signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature));//_jwtTokenOptions.SigningCredentials);

      //criar o encode do token para ficar como uma string
      //var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
      var encodedJwt = handler.WriteToken(securityToken);

      var response = new
      {
        access_token = encodedJwt,
        expires_in = DateTime.Now.AddMinutes(_tokenDescriptor.MinutesValid),///(int)_jwtTokenOptions.ValidFor.TotalSeconds,// informa quantos segundos vai expirar
        user = new
        {
          id = user.Id,
          nome = user.UserName,
          email = user.Email,
          claims = userClaims.Select(c => new { c.Type, c.Value })
        }
      };

      return response;
    }

    // tratar token assim que chegar na controller
    private static void ThrowIfInvalidOptions(JwtTokenOptions options)
    {
      if (options == null) throw new ArgumentNullException(nameof(options));

      if (options.ValidFor <= TimeSpan.Zero)
      {
        throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtTokenOptions.ValidFor));
      }

      if (options.SigningCredentials == null)
      {
        throw new ArgumentNullException(nameof(JwtTokenOptions.SigningCredentials));
      }

      if (options.JtiGenerator == null)
      {
        throw new ArgumentNullException(nameof(JwtTokenOptions.JtiGenerator));
      }
    }

  }
}