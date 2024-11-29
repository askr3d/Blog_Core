using BlogCore.Data;
using BlogCore.Models;
using BlogCoreAccesoDatos.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCoreAccesoDatos.Data.Repository
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        private readonly ApplicationDbContext _contexto;
        public CategoriaRepository(ApplicationDbContext contexto): base(contexto)
        {
            _contexto = contexto;
        }


        public void Update(Categoria categoria)
        {
            var objDesdeDb = _contexto.Categoria.FirstOrDefault(s =>  s.Id == categoria.Id);
            objDesdeDb.Nombre = categoria.Nombre;
            objDesdeDb.Orden = categoria.Orden;

            //_contexto.SaveChanges();
        }
        public IEnumerable<SelectListItem> GetListaCategorias()
        {
            return _contexto.Categoria.Select(i => new SelectListItem() {
                Value= i.Id.ToString(),
                Text = i.Nombre
            });
        }
    }
}
