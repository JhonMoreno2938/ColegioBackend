using Colegio.Interfaz;
using Colegio.Modelos.Rh;
using Colegio.Modelos.Rh.Vistas;

namespace Colegio.Servicios
{
    public class RhServicio
    {
        private readonly IRh rh;

        public RhServicio(IRh rh)
        {
            this.rh = rh;
        }

        private List<ListarRh> MapearListarRh(List<RhModelo> rhModelo)
        {
            return rhModelo.Select(modelo => new ListarRh
            {
                nombreRh = modelo.nombreRh
            }).ToList();
        }

        public async Task<List<ListarRh>> ValidarInformacionRhAsync()
        {
            var modeloRh = await rh.InformacionRhAsync();

            var resultado = MapearListarRh(modeloRh);

            return resultado;
        }

    }
}
