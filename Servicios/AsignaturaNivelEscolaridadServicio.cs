using Colegio.Interfaz;
using Colegio.Modelos.Asignatura_Nivel_Escolaridad.Procedimientos;
using Colegio.Modelos.Asignatura_Nivel_Escolaridad.Salidas_Procedimientos;

namespace Colegio.Servicios
{
    public class AsignaturaNivelEscolaridadServicio
    {
        private readonly IAsignaturaNivelEscolaridad asignaturaNivelEscolaridad;

        public AsignaturaNivelEscolaridadServicio(IAsignaturaNivelEscolaridad asignaturaNivelEscolaridad)
        {
            this.asignaturaNivelEscolaridad = asignaturaNivelEscolaridad;
        }

        private string ConstruirListaIntensidadHoraria(RegistrarAsignatura registrarAsignatura)
        {
            IEnumerable<string> nombresIntensidadHoraria = registrarAsignatura.listaIntensidadHoraria.Select(i => i.ToString());
            return string.Join(",", nombresIntensidadHoraria);
        }

        private string  ConstruirListaNivelEscolaridad(RegistrarAsignatura registrarAsignatura)
        {
            IEnumerable<string> nombresNivelEscolaridad = registrarAsignatura.listaNivelEscolaridad.Select(n => n.nombreNivelEscolaridad);
            return string.Join(",", nombresNivelEscolaridad);
        }

        public async Task<ResultadoMensajeAsignatuarNivelEscolaridad> ValidarInformacionRegistrarAsignaturaAsync(RegistrarAsignatura registrarAsignatura)
        {
            string listaIntensidadHoraria = ConstruirListaIntensidadHoraria(registrarAsignatura);
            string listaNivelEscolaridad = ConstruirListaNivelEscolaridad(registrarAsignatura);

            ResultadoMensajeAsignatuarNivelEscolaridad resultado = await asignaturaNivelEscolaridad.RegistrarAsignaturaAsync(
                registrarAsignatura.nombreAsignatura,
                listaIntensidadHoraria,
                listaNivelEscolaridad
                );

            return resultado;
        }
    }
}
