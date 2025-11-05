using Colegio.Interfaz;
using Colegio.Modelos.Departamento;
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

        private List<ListarDepartamento> MapearListarDepartamento(List<DepartamentoModelo> departamentoModelo)
        {
            return departamentoModelo.Select(modelo => new ListarDepartamento
            {
                nombreDepartamento = modelo.nombreDepartamento
            }).ToList();
        }


        public async Task<List<ListarDepartamento>> ValidarInformacionDepartamentoAsync()
        {
            var modeloDeparamtento = await departamento.InformacionDepartamentoAsync();

            var resultado = MapearListarDepartamento(modeloDeparamtento);

            return resultado;
        }
    }
}
