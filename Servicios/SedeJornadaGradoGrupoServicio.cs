using Colegio.Interfaz;
using Colegio.Modelos.Sede_Jornada_Grado_Grupo.Procedimientos;
using Colegio.Modelos.Sede_Jornada_Grado_Grupo.Salidas_Procedimientos;

namespace Colegio.Servicios
{
    public class SedeJornadaGradoGrupoServicio
    {
        private readonly ISedeJornadaGradoGrupo sedeJornadaGradoGrupo;

        public SedeJornadaGradoGrupoServicio(ISedeJornadaGradoGrupo sedeJornadaGradoGrupo)
        {
            this.sedeJornadaGradoGrupo = sedeJornadaGradoGrupo;
        }
        private string ConstruirListaGrado(RegistrarGradoGrupoNivelEscolaridadSede registrarGradoGrupoNivelEscolaridadSede)
        {
            IEnumerable<string> nombresGrado = registrarGradoGrupoNivelEscolaridadSede.listaGrado.Select(g => g.nombreGrado);
            return string.Join(",", nombresGrado);
        }

        private string ConstruirListaGrupo(RegistrarGradoGrupoNivelEscolaridadSede registrarGradoGrupoNivelEscolaridadSede)
        {
            IEnumerable<string> nombresGrupo = registrarGradoGrupoNivelEscolaridadSede.listaGrupo.Select(gr => gr.nombreGrupo);
            return string.Join(",", nombresGrupo);
        }

        public async Task<ResultadoMensajeSedeJornadaGradoGrupo> ValidarInformacionRegistrarGradoGrupoNivelEscolaridadSedeeAsync(RegistrarGradoGrupoNivelEscolaridadSede registrarGradoGrupoNivelEscolaridadSede)
        {
            string listaGrado = ConstruirListaGrado(registrarGradoGrupoNivelEscolaridadSede);
            string listaGrupo = ConstruirListaGrupo(registrarGradoGrupoNivelEscolaridadSede);

            ResultadoMensajeSedeJornadaGradoGrupo resultado = await sedeJornadaGradoGrupo.RegistrarGradoGrupoNivelEscolaridadAsync(
                registrarGradoGrupoNivelEscolaridadSede.codigoDaneSede,
                registrarGradoGrupoNivelEscolaridadSede.nombreJornada,
                listaGrado,
                listaGrupo
            );

            return resultado;
        }

        public async Task<ResultadoMensajeSedeJornadaGradoGrupo> ValidarGestionarEstadoSedeJornadaGradpGrupoAsync(GestionarEstadoGradoGrupoSede gestionarEstadoGradoGrupoSede)
        {
            string nombreGrado = string.Empty;
            string nombreGrupo = string.Empty;

            if (!string.IsNullOrEmpty(gestionarEstadoGradoGrupoSede.nombreGradoGrupo))
            {
                var partes = gestionarEstadoGradoGrupoSede.nombreGradoGrupo.Split('-');
                nombreGrado = partes[0].Trim();
                nombreGrupo = partes.Length > 1 ? partes[1].Trim() : string.Empty;
            }

            ResultadoMensajeSedeJornadaGradoGrupo resultado = await sedeJornadaGradoGrupo.GestionarEstadoGradoGrupoSedeAsync(
                gestionarEstadoGradoGrupoSede.nombreOperacion,
                nombreGrado,
                nombreGrupo,
                gestionarEstadoGradoGrupoSede.nombreNivelEscolaridad,
                gestionarEstadoGradoGrupoSede.codigoDaneSede,
                gestionarEstadoGradoGrupoSede.nombreJornada
                );

            return resultado;
        }

    }
}
