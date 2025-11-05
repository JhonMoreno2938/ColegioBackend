using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Tipo_Documento.Vistas
{
    public class ListarTipoDocumento
    {
        [JsonPropertyName("nombreTipoDocumento")]
        [StringLength(35)]
        public string nombreTipoDocumento { get; set; } = string.Empty;
    }
}
