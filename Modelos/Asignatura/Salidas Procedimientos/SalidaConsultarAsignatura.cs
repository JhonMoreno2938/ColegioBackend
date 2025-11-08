using System.Text.Json.Serialization;

namespace Colegio.Modelos.Asignatura.Salidas_Procedimientos;

public class SalidaConsultarAsignatura
{
    public bool exito { get; set; }

    public string mensaje { get; set; }

    // Información de la asignatura a consultar.
    [JsonPropertyName("nombreAsignatura")]
    public string nombreAsignatura { get; set; } = string.Empty;

    // Información de los grados grupos vinculados.
    [JsonPropertyName("gradosGruposVinculados")]
    public List<InformacionConsultarAsignatura> gradosGruposVinculados { get; set; } = new List<InformacionConsultarAsignatura>();

}
