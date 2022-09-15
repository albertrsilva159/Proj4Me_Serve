using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Proj4Me.Infra.CrossCutting.Identity.Models;
using System.Collections.Generic;
using System.Linq;

namespace Proj4Me.Infra.CrossCutting.Identity.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // Criar permissoes
            List<IdentityRole> roles = new List<IdentityRole>() {
                new IdentityRole { Name = "Administrator", NormalizedName = "ADMINISTRATOR" },
                new IdentityRole { Name = "Analista", NormalizedName = "COACH" }
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);

            // Criar superuser
            List<ApplicationUser> users = new List<ApplicationUser>() {
                new ApplicationUser {
                    UserName = "superuser",
                    NormalizedUserName = "superuser@simply.com",
                    Email = "superuser@simply.com",
                    NormalizedEmail = "superuser@simply.com"
                }               
            };
            modelBuilder.Entity<ApplicationUser>().HasData(users);

            // Agregar senha ao usuario
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            users[0].PasswordHash = passwordHasher.HashPassword(users[0], "superuser");            

            // Agregar roles ao usuario
            List<IdentityUserRole<string>> userRoles = new List<IdentityUserRole<string>>();
            userRoles.Add(new IdentityUserRole<string> {
                UserId = users[0].Id,
                RoleId = roles.First(q => q.Name == "Administrator").Id
            });    
           
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(userRoles);
   
        }
    }
}
