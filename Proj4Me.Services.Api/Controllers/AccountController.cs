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
using Proj4Me.Domain.Core.Bus;
using Proj4Me.Infra.CrossCutting.Identity.Authorization;
using Microsoft.Extensions.Options;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
//teste branch

namespace Proj4Me.Services.Api.Controllers
{
  public class AccountController : BaseController
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ILogger _logger;
    private readonly IBus _bus;
    private readonly IConfiguration _configuration;
    //private readonly IMediatorHandler _mediator;
    private readonly JwtTokenOptions _jwtTokenOptions;
    
    public AccountController(
                UserManager<ApplicationUser> userManager,
                SignInManager<ApplicationUser> signInManager,
                ILoggerFactory loggerFactory,
                IOptions<JwtTokenOptions> jwtTokenOptions,
                IBus bus,
                //TokenDescriptor tokenDescriptor,
                IConfiguration configuration,
                IDomainNotificationHandler<DomainNotification> notifications,
                IUser user
                ) : base(notifications, user, bus)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _bus = bus;
      _jwtTokenOptions = jwtTokenOptions.Value;
      _configuration = configuration;
      _logger = loggerFactory.CreateLogger<AccountController>();
      //ThrowIfInvalidOptions(_jwtTokenOptions);// assim que o token for passado ele vai ser validado      
    }

    // padrao para trabalhar com a data no token
    private static long ToUnixEpochDate(DateTime date)
    => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);


    [HttpPost]
    [AllowAnonymous]
    [Route("nova-conta")]
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

        var registroCommand = new RegistrarColaboradorCommand(model.Nome, user.Email);
        //await _mediator.EnviarComando(registroCommand);

        if (!OperacaoValida())
        {
          await _userManager.DeleteAsync(user);
          return Response(model);
        }

        _logger.LogInformation(1, "Usuario criado com sucesso!");

        var response = GerarTokenUsuario(new LoginViewModel { Email = model.Email, Password = model.Password });
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
        _bus.RaiseEvent(new DomainNotification(result.ToString(), erro.Description));
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
      userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, await _jwtTokenOptions.JtiGenerator()));
      userClaims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtTokenOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64));// quando o token foi gerado
    
      var jwt = new JwtSecurityToken(
            issuer: _configuration["JwtTokenOptions:Issuer"], //_jwtTokenOptions.Issuer, 
            audience:  _jwtTokenOptions.Audience,
            claims: userClaims,
            notBefore: _jwtTokenOptions.NotBefore,
            expires: _jwtTokenOptions.Expiration,
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature));//_jwtTokenOptions.SigningCredentials);

      //criar o encode do token para ficar como uma string
      var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

      var response = new
      {
        access_token = encodedJwt,
        expires_in = (int)_jwtTokenOptions.ValidFor.TotalSeconds,// informa quantos segundos vai expirar
        user = user
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