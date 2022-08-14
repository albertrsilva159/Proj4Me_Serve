using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Proj4Me.Application.ViewModels;

namespace Proj4Me.Web.Data
{
  public class Proj4MeWebContext : DbContext
  {
    public Proj4MeWebContext(DbContextOptions<Proj4MeWebContext> options)
        : base(options)
    {
    }

    public DbSet<Proj4Me.Application.ViewModels.ProjetoAreaServicoViewModel> ProjetoAreaServicoViewModel { get; set; }

    public DbSet<Proj4Me.Application.ViewModels.ColaboradorViewModel> ColaboradorViewModel { get; set; }

    public DbSet<Proj4Me.Application.ViewModels.PerfilViewModel> PerfilViewModel { get; set; }
  }
}
