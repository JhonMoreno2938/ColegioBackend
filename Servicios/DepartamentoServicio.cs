using Colegio.Interfaz;
using Colegio.Modelos.Departamento.Vistas;

namespace Colegio.Servicios
{
    public class DepartamentoServicio
    {
        private readonly IDepartamento departamento;

        public DepartamentoServicio(IDepartamento departamento)
        {
            this.departamento = departamento;
        }
        public async Task<List<ListarDepartamento>> ValidarInformacionDepartamentoAsync()
        {
            var informacionDepartamento = await departamento.InformacionDepartamentoAsync();

            return informacionDepartamento;
        }
    }
}
