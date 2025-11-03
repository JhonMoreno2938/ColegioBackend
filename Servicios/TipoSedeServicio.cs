using Colegio.Interfaz;
using Colegio.Modelos.Tipo_Sede.Vistas;

namespace Colegio.Servicios
{
    public class TipoSedeServicio
    {
        private readonly ITipoSede tipoSede;

        public TipoSedeServicio(ITipoSede tipoSede)
        {
            this.tipoSede = tipoSede;
        }
        public async Task<List<ListarTipoSede>> ValidarInformacionTipoSedeAsync()
        {
            var informacionTipoSede = await tipoSede.InformacionTipoSedeAsync();

            return informacionTipoSede;
        }
    }
}
