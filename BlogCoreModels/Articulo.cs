using BlogCore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Models
{
    public class Articulo
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre del producto es obligatorio")]
        [Display(Name  = "Nombre del Articulo")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "La descripcion del producto es obligatoria")]
        [Display(Name  = "Descripcion del Articulo")]
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Imagen del articulo")]
        public string? UrlImagen { get; set; }

        [Required(ErrorMessage = "La categoria es obligatoria")]
        [Display(Name = "Categoria")]
        [ForeignKey("Categoria")]
        public int CategoriaId { get; set; }

        public Categoria? Categoria { get; set; }
    }
}
