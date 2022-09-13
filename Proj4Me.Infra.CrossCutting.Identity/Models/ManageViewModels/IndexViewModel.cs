using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Proj4Me.Infra.CrossCutting.Identity.Models.ManageViewModels
{
    public class IndexViewModel
    {// essas propriedades estavam antes
     ////public string Username { get; set; }

    ////public bool IsEmailConfirmed { get; set; }

    ////[Required]
    ////[EmailAddress]
    ////public string Email { get; set; }

    ////[Phone]
    ////[Display(Name = "Phone number")]
    ////public string PhoneNumber { get; set; }

    ////public string StatusMessage { get; set; }


    public bool HasPassword { get; set; }

    public IList<UserLoginInfo> Logins { get; set; }

    public string PhoneNumber { get; set; }

    public bool TwoFactor { get; set; }

    public bool BrowserRemembered { get; set; }
  }
}
