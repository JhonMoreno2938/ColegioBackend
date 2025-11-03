using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Tipo_Documento.Vistas
{
    public class ListarTipoDocumento
    {
        [JsonIgnore]
        private readonly TipoDocumentoModelo tipoDocumentoModelo = new TipoDocumentoModelo();

        [JsonPropertyName("nombreTipoDocumento")]
        [StringLength(35)]
        public string nombreTipoDocumento
        {
            get => tipoDocumentoModelo.nombreTipoDocumento;
            set => tipoDocumentoModelo.nombreTipoDocumento = value;
        }
    }
}
