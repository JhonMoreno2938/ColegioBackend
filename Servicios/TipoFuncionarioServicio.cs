using Colegio.Interfaz;
using Colegio.Modelos.Tipo_Funcionario.Vistas;
using Colegio.Modelos.Tipo_Funcionario;

namespace Colegio.Servicios
{
    public class TipoFuncionarioServicio
    {
        private readonly ITipoFuncionario tipoFuncionario;

        public TipoFuncionarioServicio(ITipoFuncionario tipoFuncionario)
        {
            this.tipoFuncionario = tipoFuncionario;
        }


        private List<ListarTipoFuncionario> MapearListarTipoFuncionario(List<TipoFuncionarioModelo> tipoFuncionarioModelo)
        {
            return tipoFuncionarioModelo.Select(modelo => new ListarTipoFuncionario
            {
                nombreTipoFuncionario = modelo.nombreTipoFuncionario
            }).ToList();
        }


        public async Task<List<ListarTipoFuncionario>> ValidarInformacionTipoFuncionarioAsync()
        {
            var modeloTipoFuncionario = await tipoFuncionario.InformacionTipoFuncionarioAsync();

            var resultado = MapearListarTipoFuncionario(modeloTipoFuncionario);

            return resultado;
        }
    }
}
