﻿using System.Threading.Tasks;

namespace Proj4Me.Infra.CrossCutting.Identity.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
