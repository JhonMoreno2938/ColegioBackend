using Colegio.Interfaz;
using Colegio.Modelos.Tipo_Sede.Vistas;
using Colegio.Modelos.Tipo_Sede;

namespace Colegio.Servicios
{
    public class TipoSedeServicio
    {
        private readonly ITipoSede tipoSede;

        public TipoSedeServicio(ITipoSede tipoSede)
        {
            this.tipoSede = tipoSede;
        }


        private List<ListarTipoSede> MapearListarTipoSede(List<TipoSedeModelo> tipoSedeModelo)
        {
            return tipoSedeModelo.Select(modelo => new ListarTipoSede
            {
                nombreTipoSede = modelo.nombreTipoSede
            }).ToList();
        }


        public async Task<List<ListarTipoSede>> ValidarInformacionTipoSedeAsync()
        {
            var modeloTipoSede = await tipoSede.InformacionTipoSedeAsync();

            var resultado = MapearListarTipoSede(modeloTipoSede);

            return resultado;
        }
    }
}
