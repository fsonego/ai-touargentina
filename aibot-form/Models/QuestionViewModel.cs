using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace aibot_form.Models
{
    public class QuestionViewModel
    {
        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Pregunta")]
        [MinLength(15, ErrorMessage = "El minimo de caracteres es de 15")]
        [MaxLength(250, ErrorMessage = "El máximo de caracteres es de 250")]
        public string Message { get; set; }
    }
}
