using Colegio.Interfaz;
using Colegio.Modelos.Asignatura_Grado_Grupo.Procedimientos;
using Colegio.Modelos.Asignatura_Grado_Grupo.Salidas_Procedimientos;

namespace Colegio.Servicios
{
    public class AsignaturaGradoGrupoServicio
    {
        private readonly IAsignaturaGradoGrupo asignaturaGradoGrupo;

        public AsignaturaGradoGrupoServicio(IAsignaturaGradoGrupo asignaturaGradoGrupo)
        {
            this.asignaturaGradoGrupo = asignaturaGradoGrupo;
        }

        private string ConstruirListaGrado(RegistrarAsignaturaGradoGrupo registrarAsignaturaGradoGrupo)
        {
            if (registrarAsignaturaGradoGrupo.listaGradoGrupo == null || !registrarAsignaturaGradoGrupo.listaGradoGrupo.Any())
            {
                return string.Empty; // Devolver cadena vacía si no hay elementos
            }
            IEnumerable<string> listaGrados = registrarAsignaturaGradoGrupo.listaGradoGrupo
                .Select(detalle =>
                {
                    var partes = detalle.nombreGradoGrupo.Split('-');
                    return partes[0].Trim();
                });

            return string.Join(",", listaGrados.Distinct());
        }

        private string ConstruirListaGrupo(RegistrarAsignaturaGradoGrupo registrarAsignaturaGradoGrupo)
        {
            if(registrarAsignaturaGradoGrupo.listaGradoGrupo == null || !registrarAsignaturaGradoGrupo.listaGradoGrupo.Any())
            {
                return string.Empty; // Devolver cadena vacía si no hay elementos
            }
            IEnumerable<string> listaGrupos = registrarAsignaturaGradoGrupo.listaGradoGrupo
                .Select(detalle =>
                {
                    var partes = detalle.nombreGradoGrupo.Split('-');
                    return partes.Length > 1 ? partes[1].Trim() : string.Empty;
                });

            return string.Join(",", listaGrupos.Distinct());

        }
        private string ConstruirListaNivelEscolaridad(RegistrarAsignaturaGradoGrupo registrarAsignaturaGradoGrupo)
        {
            IEnumerable<string> nombreNivelEscolaridad = registrarAsignaturaGradoGrupo.listaNivelEscolaridad.Select(n => n.nombreNivelEscolaridad);
            return string.Join(",", nombreNivelEscolaridad);
        }

        public async Task<ResultadoMensajeAsignaturaGradoGrupo> ValidarInformacionRregistrarAsignaturaGradoGrupoAsync(RegistrarAsignaturaGradoGrupo registrarAsignaturaGradoGrupo)
        {
            string listaGrado = ConstruirListaGrado(registrarAsignaturaGradoGrupo);
            string listaGrupo = ConstruirListaGrupo(registrarAsignaturaGradoGrupo);
            string listaNivelEscolaridad = ConstruirListaNivelEscolaridad(registrarAsignaturaGradoGrupo);

            ResultadoMensajeAsignaturaGradoGrupo resultado = await asignaturaGradoGrupo.RegistrarAsignaturaGradoGrupoAsync(
                registrarAsignaturaGradoGrupo.nombreAsignatura,
                listaGrado,
                listaGrupo,
                listaNivelEscolaridad
                );

            return resultado;
        }

        public async Task<ResultadoMensajeAsignaturaGradoGrupo> ValidarGestionarEstadoAsignaturaGradoGrupoAsync(GestionarEstadoAsignaturaGradoGrupo gestionarEstadoAsignaturaGradoGrupo)
        {
            string nombreGrado = string.Empty;
            string nombreGrupo = string.Empty;

            if (!string.IsNullOrEmpty(gestionarEstadoAsignaturaGradoGrupo.nombreGradoGrupo))
            {
                var partes = gestionarEstadoAsignaturaGradoGrupo.nombreGradoGrupo.Split('-');
                nombreGrado = partes[0].Trim();
                nombreGrupo = partes.Length > 1 ? partes[1].Trim() : string.Empty;
            }

            ResultadoMensajeAsignaturaGradoGrupo resultado = await asignaturaGradoGrupo.GestionarEstadoAsignaturaGradoGrupoAsync(
                gestionarEstadoAsignaturaGradoGrupo.nombreOperacion,
                nombreGrado,
                nombreGrupo,
                gestionarEstadoAsignaturaGradoGrupo.nombreNivelEscolaridad,
                gestionarEstadoAsignaturaGradoGrupo.nombreAsignatura
                );

            return resultado;
        }


    }
}
