using BlogCore.Models;
using BlogCoreAccesoDatos.Data.Repository.IRepository;
using BlogCoreModels.ViewModels;
using BlogCoreUtilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Areas.Admin.Controllers
{
    [Authorize(Roles = CNT.Administrador)]
    [Area("Admin")]
    public class ArticulosController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ArticulosController(
            IContenedorTrabajo contenedorTrabajo,
            IWebHostEnvironment hostEnvironment
        )
        {
            _contenedorTrabajo = contenedorTrabajo;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            ArticuloVM artiVm = new ArticuloVM
            {
                Articulo = new Articulo(),
                ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategorias()
            };
            return View(artiVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ArticuloVM model)
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = _hostEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;
                if (model.Articulo.Id == 0 && archivos.Count() > 0)
                {
                    string nombreArchivo = Guid.NewGuid().ToString();

                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\articulos");
                    var extension = Path.GetExtension(archivos[0].FileName);

                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }

                    model.Articulo.UrlImagen = @"\imagenes\articulos\" + nombreArchivo + extension;
                    model.Articulo.FechaCreacion = DateTime.Now;

                    _contenedorTrabajo.Articulo.Add(model.Articulo);
                    _contenedorTrabajo.Save();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("Imagen", "Debes seleccionar una imagen");
                }
            }

            model.ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategorias();

            return View(model);
        }

        public IActionResult Edit(int? id)
        {
            ArticuloVM artiVm = new ArticuloVM
            {
                Articulo = new Articulo(),
                ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategorias()
            };

            if (id != null)
            {
                artiVm.Articulo = _contenedorTrabajo.Articulo.Get(id.GetValueOrDefault());
            }

            return View(artiVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ArticuloVM model)
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = _hostEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;

                var articuloDesdeDb = _contenedorTrabajo.Articulo.Get(model.Articulo.Id);




                if (archivos.Count() > 0)
                {
                    //Nuea imagen para el articulo
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\articulos");
                    var extension = Path.GetExtension(archivos[0].FileName);
                    var nuevaExtension = Path.GetExtension(archivos[0].FileName);

                    var rutaImagen = Path.Combine(rutaPrincipal, articuloDesdeDb.UrlImagen?.TrimStart('\\') ?? "--");

                    if (System.IO.File.Exists(rutaImagen))
                    {
                        System.IO.File.Delete(rutaImagen);
                    }

                    //Nuevamente subimos el archivo
                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }

                    model.Articulo.UrlImagen = @"\imagenes\articulos\" + nombreArchivo + extension;
                    model.Articulo.FechaCreacion = DateTime.Now;

                    _contenedorTrabajo.Articulo.Update(model.Articulo);
                    _contenedorTrabajo.Save();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    model.Articulo.UrlImagen = articuloDesdeDb.UrlImagen ?? "";
                }

                _contenedorTrabajo.Articulo.Update(model.Articulo);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }

            model.ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategorias();

            return View(model);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var articulo = _contenedorTrabajo.Articulo.Get(id);
            string rutaDirectorioPrincipal = _hostEnvironment.WebRootPath;
            var rutaImagen = Path.Combine(rutaDirectorioPrincipal, articulo.UrlImagen?.TrimStart('\\') ?? "--");

            if (System.IO.File.Exists(rutaImagen))
            {
                System.IO.File.Delete(rutaImagen);
            }

            if (articulo == null)
            {
                return Json(new { success = false, message = "Error borrando articulo" });
            }

            _contenedorTrabajo.Articulo.Remove(articulo);
            _contenedorTrabajo.Save();

            return Json(new { success = true, message = "Articulo borrado correctamente" });
        }

        #region
        public IActionResult GetAll()
        {
            return Json(new { data = _contenedorTrabajo.Articulo.GetAll(includeProperties: "Categoria") });
        }
        #endregion
    }
}
