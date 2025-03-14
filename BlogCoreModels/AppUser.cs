﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Models
{
    public class AppUser: IdentityUser
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La direccion es obligatoria")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "La ciudad es obligatoria")]
        public string Ciudad { get; set; }

        [Required(ErrorMessage = "La pais es obligatoria")]
        public string Pais { get; set; }

    }
}
