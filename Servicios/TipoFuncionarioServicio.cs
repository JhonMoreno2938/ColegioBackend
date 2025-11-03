using Colegio.Interfaz;
using Colegio.Modelos.Tipo_Funcionario.Vistas;

namespace Colegio.Servicios
{
    public class TipoFuncionarioServicio
    {
        private readonly ITipoFuncionario tipoFuncionario;

        public TipoFuncionarioServicio(ITipoFuncionario tipoFuncionario)
        {
            this.tipoFuncionario = tipoFuncionario;
        }
        public async Task<List<ListarTipoFuncionario>> ValidarInformacionTipoFuncionarioAsync()
        {
            var informacionTipoFuncionario = await tipoFuncionario.InformacionTipoFuncionarioAsync();

            return informacionTipoFuncionario;
        }
    }
}
