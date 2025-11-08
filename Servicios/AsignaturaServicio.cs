using Colegio.Interfaz;
using Colegio.Modelos.Asignatura.Salidas_Procedimientos;
using Colegio.Modelos.Asignatura.Vistas;
using Colegio.Modelos.Asignatura;

namespace Colegio.Servicios
{
    public class AsignaturaServicio
    {
        private readonly IAsignatura asignatura;

        public AsignaturaServicio(IAsignatura asignatura)
        {
            this.asignatura = asignatura;
        }

        private List<ListarAsignatura> MapearListarAsignatura(List<AsignaturaModelo> asignaturaModelo)
        {
            return asignaturaModelo.Select(modelo => new ListarAsignatura
            {
                nombreAsignatura = modelo.nombreAsignatura,
                estadoAsgiantura = modelo.estadoAsignatura
            }).ToList();
        }
        private List<ListarAsiganturaEstadoActivo> MapearListarAsignaturaEstadoActivo(List<AsignaturaModelo> asignaturaModelo)
        {
            return asignaturaModelo.Select(modelo => new ListarAsiganturaEstadoActivo
            {
                nombreAsignatura = modelo.nombreAsignatura
            }).ToList();
        }

        public async Task<SalidaConsultarAsignatura> ValidarConsultarAsignaturaAsync(string nombreAsignatura)
        {
            SalidaConsultarAsignatura resultado = await asignatura.ConsultarAsignaturaAsync(nombreAsignatura);

            return resultado;
        }

        public async Task<List<SalidaConsultarGradoGrupoAsignaturaEstadoActivo>> ValidarConsultarGradoGrupoAsignaturaEstadoActivo(string nombreAsignatura)
        {
            List<SalidaConsultarGradoGrupoAsignaturaEstadoActivo> resultado = await asignatura.ConsultarGradoGrupoAsignaturaEstadoActivoAsync(nombreAsignatura);

            return resultado;
        }

        public async Task<List<ListarAsignatura>> ValidarInformacionAsignaturaAsync()
        {
            var modeloAsignatura = await asignatura.InformacionAsignaturaAsync();

            var resultado = MapearListarAsignatura(modeloAsignatura);

            return resultado;
        }

        public async Task<List<ListarAsiganturaEstadoActivo>> ValidarInformacionAsignaturaEstadoActivoAsync()
        {
            var modeloAsignatura = await asignatura.InformacionAsignaturaEstadoActivoAsync();

            var resultado = MapearListarAsignaturaEstadoActivo(modeloAsignatura);

            return resultado;
        }
    }
}
