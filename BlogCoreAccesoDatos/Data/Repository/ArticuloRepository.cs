using BlogCore.Data;
using BlogCoreAccesoDatos.Data.Repository.IRepository;
using BlogCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCoreAccesoDatos.Data.Repository
{
    public class ArticuloRepository : Repository<Articulo>, IArticuloRepository
    {
        private readonly ApplicationDbContext _context;
        public ArticuloRepository(ApplicationDbContext context): base(context)
        {
            _context = context;
        }

        public void Update(Articulo articulo)
        {
            var objDesdeDb = _context.Articulo.FirstOrDefault(a => a.Id == articulo.Id);
            objDesdeDb.Nombre = articulo.Nombre;
            objDesdeDb.Descripcion = articulo.Descripcion;
            objDesdeDb.UrlImagen = articulo.UrlImagen;
            objDesdeDb.Categoria = articulo.Categoria;
            
            //_context.SaveChanges();
        }

        public IQueryable<Articulo> AsQueryable()
        {
            return _context.Set<Articulo>().AsQueryable();
        }
    }
}
