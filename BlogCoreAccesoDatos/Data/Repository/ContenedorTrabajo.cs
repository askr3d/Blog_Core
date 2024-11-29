using BlogCore.Data;
using BlogCoreAccesoDatos.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCoreAccesoDatos.Data.Repository
{
    public class ContenedorTrabajo: IContenedorTrabajo
    {
        private readonly ApplicationDbContext _context;
        public ICategoriaRepository Categoria { get; private set; }
        public IArticuloRepository Articulo { get; private set; }
        public ISliderRepository Slider { get; private set; }
        public IUsuarioRepository Usuario { get; private set; }

        public ContenedorTrabajo(ApplicationDbContext context)
        {
            _context = context;
            Categoria = new CategoriaRepository(_context);
            Articulo = new ArticuloRepository(_context);
            Slider = new SliderRepository(_context);
            Usuario =  new UsuarioRepository(_context);
        }


        public void Dispose()
        {
            _context.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
