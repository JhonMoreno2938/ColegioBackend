using Colegio.Interfaz;
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
        public async Task<List<ListarRh>> ValidarInformacionRhAsync()
        {
            var informacionRh = await rh.InformacionRhAsync();

            return informacionRh;
        }

    }
}
