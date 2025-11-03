using Colegio.Modelos.Grado_Grupo.Salidas_Procedimientos;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Colegio.Modelos.Sede.Salidas_Procedimientos
{
    public class SalidaConsultarSede
    {
        [JsonIgnore]
        SedeModelo sedeModelo = new SedeModelo();
        public bool exito { get; set; }

        public string mensaje { get; set; }

        // Información de la sede a consultar.

        [JsonPropertyName("codigoDaneSede")]
        [StringLength(10)]
        public string codigoDaneSede
        {
            get => sedeModelo.codigoDaneSede;
            set => sedeModelo.codigoDaneSede = value;
        }

        [JsonPropertyName("nombreSede")]
        [StringLength(100)]
        public string nombreSede
        {
            get => sedeModelo.nombreSede;
            set => sedeModelo.nombreSede = value;
        }

        [JsonPropertyName("direccionSede")]
        [StringLength(100)]
        public string direccionSede
        {
            get => sedeModelo.direccionSede;
            set => sedeModelo.direccionSede = value;
        }

        [JsonPropertyName("numeroContactoSede")]
        [StringLength(10)]
        public string numeroContactoSede
        {
            get => sedeModelo.numeroContactoSede;
            set => sedeModelo.numeroContactoSede = value;
        }

        [JsonPropertyName("estadoSede")]
        [StringLength(10)]
        public string estadoSede
        {
            get => sedeModelo.estadoSede;
            set => sedeModelo.estadoSede = value;
        }

        [JsonPropertyName("nombreTipoSede")]
        [StringLength(15)]
        public string nombreTipoSede
        {
            get => sedeModelo.tipoSedeModelo.nombreTipoSede;
            set => sedeModelo.tipoSedeModelo.nombreTipoSede = value;
        }

        // Información de los grados grupos que estan vinculados a la sede a consultar.

        [JsonPropertyName("gradosGruposVinculados")]
        public List<GradoGrupoDetalle> GradosGruposVinculados { get; set; } = new List<GradoGrupoDetalle>();
    }
}
