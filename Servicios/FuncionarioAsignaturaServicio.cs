using Colegio.Interfaz;
using Colegio.Modelos.Funcionario_Asignatura.Procedimientos;
using Colegio.Utilidades;

namespace Colegio.Servicios
{
    public class FuncionarioAsignaturaServicio
    {
        private readonly IFuncionarioAsignatura funcionarioAsignatura;

        public FuncionarioAsignaturaServicio(IFuncionarioAsignatura funcionarioAsignatura)
        {
            this.funcionarioAsignatura = funcionarioAsignatura;
        }

        private string ConstruirListaGrado(AsignarFuncionarioAsignatura asignarFuncionarioAsignatura)
        {
            if (asignarFuncionarioAsignatura.listaGradoGrupoJornada == null || !asignarFuncionarioAsignatura.listaGradoGrupoJornada.Any())
            {
                return string.Empty; // Devolver cadena vacía si no hay elementos
            }
            IEnumerable<string> listaGrados = asignarFuncionarioAsignatura.listaGradoGrupoJornada
                .Select(detalle =>
                {
                    var partes = detalle.nombreGradoGrupo.Split('-');
                    return partes[0].Trim();
                });

            return string.Join(",", listaGrados);
        }

        private string ConstruirListaGrupo(AsignarFuncionarioAsignatura asignarFuncionarioAsignatura)
        {
            if (asignarFuncionarioAsignatura.listaGradoGrupoJornada == null || !asignarFuncionarioAsignatura.listaGradoGrupoJornada.Any())
            {
                return string.Empty; // Devolver cadena vacía si no hay elementos
            }
            IEnumerable<string> listaGrupos = asignarFuncionarioAsignatura.listaGradoGrupoJornada
                .Select(detalle =>
                {
                    var partes = detalle.nombreGradoGrupo.Split('-');
                    return partes.Length > 1 ? partes[1].Trim() : string.Empty;
                });

            return string.Join(",", listaGrupos);

        }

        private string ConstruirListaSede(AsignarFuncionarioAsignatura asignarFuncionarioAsignatura)
        {
            IEnumerable<string> nombreSede = asignarFuncionarioAsignatura.listaGradoGrupoJornada.Select(detalle => detalle.nombreSede);
            return string.Join(",", nombreSede);
        }

        private string ConstruirListaJornada(AsignarFuncionarioAsignatura asignarFuncionarioAsignatura)
        {
            IEnumerable<string> nombreJornada = asignarFuncionarioAsignatura.listaGradoGrupoJornada.Select(detalle => detalle.nombreJornada);
            return string.Join(",", nombreJornada);
        }

        public async Task<bool> ValidarInformacionAsignarFuncionarioAsignaturaAsync(AsignarFuncionarioAsignatura asignarFuncionarioAsignatura)
        {
            string listaGrado = ConstruirListaGrado(asignarFuncionarioAsignatura);
            string listaGrupo = ConstruirListaGrupo(asignarFuncionarioAsignatura);
            string listaSede = ConstruirListaSede(asignarFuncionarioAsignatura);
            string listaJornada = ConstruirListaJornada(asignarFuncionarioAsignatura);

            bool resultado = await funcionarioAsignatura.AsignarFuncionarioAsignaturaAsync(
                asignarFuncionarioAsignatura.numeroDocumento,
                asignarFuncionarioAsignatura.nombreAsignatura,
                listaGrado,
                listaGrupo,
                listaSede,
                listaJornada
                );

            return resultado;
        }

        public async Task<ResultadoOperacion> ValidarGestionarEstadoFuncionarioAsignaturaAsync(GestionarEstadoFuncionarioAsignatura gestionarEstadoFuncionarioAsignatura)
        {
            ResultadoOperacion resultadoOperacion = await funcionarioAsignatura.GestionarEstadoFuncionarioAsignaturaAsync(
                gestionarEstadoFuncionarioAsignatura.nombreOperacion,
                gestionarEstadoFuncionarioAsignatura.idFuncionarioAsignaturaGradoGrupo
                );

            return resultadoOperacion;
        }

    }
}
