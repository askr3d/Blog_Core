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
    public class SliderRepository : Repository<Slider>, ISliderRepository
    {
        private readonly ApplicationDbContext _contexto;
        public SliderRepository(ApplicationDbContext contexto): base(contexto)
        {
            _contexto = contexto;
        }


        public void Update(Slider slider)
        {
            var objDesdeDb = _contexto.Slider.FirstOrDefault(s =>  s.Id == slider.Id);
            objDesdeDb.Nombre = slider.Nombre;
            objDesdeDb.Estado = slider.Estado;
            objDesdeDb.UrlImagen = slider.UrlImagen;

            //_contexto.SaveChanges();
        }
    }
}
