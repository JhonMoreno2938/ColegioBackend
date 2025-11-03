using Microsoft.AspNetCore.Mvc;

namespace Colegio.Utilidades
{
    public class ArchivoEstudiante
    {
        [FromForm(Name = "archivo")]
        public IFormFile Archivo { get; set; }
    }
}
