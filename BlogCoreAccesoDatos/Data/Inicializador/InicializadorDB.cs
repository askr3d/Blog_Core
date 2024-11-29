using BlogCore.Data;
using BlogCore.Models;
using BlogCoreUtilidades;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCoreAccesoDatos.Data.Inicializador
{
    public class InicializadorDB : IInicializadorDB
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public InicializadorDB(
                ApplicationDbContext context,
                UserManager<AppUser> userManager,
                RoleManager<IdentityRole> roleManager
            )
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Inicializar()
        {
            try
            {
                if (_context.Database.GetPendingMigrations().Count() > 0)
                {
                    _context.Database.Migrate();
                }
            }
            catch (Exception)
            {

                throw;
            }

            if (_context.Roles.Any(ro => ro.Name == CNT.Administrador)) return;

            _roleManager.CreateAsync(new IdentityRole(CNT.Administrador)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(CNT.Registrado)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(CNT.Cliente)).GetAwaiter().GetResult();

            _userManager.CreateAsync(new AppUser
            {
                UserName = "askr3d@gmail.com",
                Email = "askr3d@gmail.com",
                EmailConfirmed = true,
                Nombre = "askr3d"
            }, "Aa123456!").GetAwaiter().GetResult();

            AppUser usuario = _context.AppUser
                .Where(us => us.Email == "askr3d@gmail.com").FirstOrDefault();

            _userManager.AddToRoleAsync(usuario, CNT.Administrador).GetAwaiter().GetResult();


        }
    }
}
