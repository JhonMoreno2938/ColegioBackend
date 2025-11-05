using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Tipo_Sede.Vistas
{
    public class ListarTipoSede
    {
        [JsonPropertyName("nombreTipoSede")]
        [StringLength(15)]
        public string nombreTipoSede { get; set; }


    }
}
