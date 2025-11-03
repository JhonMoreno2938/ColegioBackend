using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Tipo_Sede.Vistas
{
    public class ListarTipoSede
    {
        [JsonIgnore]
        TipoSedeModelo tipoSedeModelo = new TipoSedeModelo();

        [JsonPropertyName("nombreTipoSede")]
        [StringLength(15)]
        public string nombreTipoSede { get; set; }


    }
}
