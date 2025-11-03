using Colegio.Interfaz;
using Colegio.Modelos.Genero.Vistas;

namespace Colegio.Servicios
{
    public class GeneroServicio
    {
        private readonly IGenero genero;

        public GeneroServicio(IGenero genero)
        {
            this.genero = genero;
        }
        public async Task<List<ListarGenero>> ValidarInformacionGeneroAsync()
        {
            var informacionGenero = await genero.InformacionGeneroAsync();

            return informacionGenero;
        }
    }
}
