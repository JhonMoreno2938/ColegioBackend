using Colegio.Modelos.Sede_Jornada_Grado_Grupo.Salidas_Procedimientos;

namespace Colegio.Interfaz
{
    public interface ISedeJornadaGradoGrupo
    {
        Task<ResultadoMensajeSedeJornadaGradoGrupo> RegistrarGradoGrupoNivelEscolaridadAsync(string codigoDane, string nombreJornada, string listaGrado, string listaGrupo);

        Task<ResultadoMensajeSedeJornadaGradoGrupo> GestionarEstadoGradoGrupoSedeAsync(string operacion, string nombreGrado, string nombreGrupo, string nombreNivelEscolaridad, string codigoDaneSede, string nombreJornada);
    }
}
