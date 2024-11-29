using BlogCore.Models;
using BlogCoreAccesoDatos.Data.Repository.IRepository;
using BlogCoreModels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Blog_Core.Areas.Cliente.Controllers
{
    [Area("Cliente")]
    public class HomeController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public HomeController(
            IContenedorTrabajo contenedorTrabajo
        )
        {
            _contenedorTrabajo = contenedorTrabajo;
        }

        public IActionResult Index(int page = 1, int pageSize = 6)
        {
            var articulos = _contenedorTrabajo.Articulo.AsQueryable();
            var paginatedEntries = articulos.Skip((page - 1) * pageSize).Take(pageSize);
            HomeVM homeVm = new HomeVM
            {
                Sliders = _contenedorTrabajo.Slider.GetAll(),
                ListArticulos = paginatedEntries.ToList(),
                PageIndex = page,
                TotalPages = (int)Math.Ceiling(articulos.Count() / (double)pageSize)
        };

            ViewBag.IsHome = true;

            return View(homeVm);
        }

        public IActionResult ResultadoBusqueda(string searchString, int page = 1, int pageSize = 6)
        {
            var articulos = _contenedorTrabajo.Articulo.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                articulos = articulos.Where(e => e.Nombre.Contains(searchString));
            }

            var paginatedEntries = articulos.Skip((page - 1) * pageSize).Take(pageSize);

            var model = new ListaPaginada<Articulo>(paginatedEntries.ToList(), articulos.Count(), page, pageSize, searchString);

            return View(model);
        }

        public IActionResult Detalle(int id)
        {
            var articuloDesdeDb = _contenedorTrabajo.Articulo.Get(id);
            return View(articuloDesdeDb);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
