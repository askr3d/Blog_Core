using BlogCore.Data;
using BlogCore.Models;
using BlogCoreAccesoDatos.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCoreAccesoDatos.Data.Repository
{
    public class UsuarioRepository : Repository<AppUser>, IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;
        public UsuarioRepository(ApplicationDbContext context): base(context)
        {
            _context = context;
        }

        public void BloquearUsuario(string IdUsuario)
        {
            var usuarioDesdeBd = _context.AppUser.FirstOrDefault(u => u.Id == IdUsuario);
            usuarioDesdeBd.LockoutEnd = DateTime.Now.AddYears(1000);
            _context.SaveChanges();
        }

        public void DesbloquearUsuario(string IdUsuario)
        {
            var usuarioDesdeBd = _context.AppUser.FirstOrDefault(u => u.Id == IdUsuario);
            usuarioDesdeBd.LockoutEnd = DateTime.Now;
            _context.SaveChanges();
        }
    }
}
