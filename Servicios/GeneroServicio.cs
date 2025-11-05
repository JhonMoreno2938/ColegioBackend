using Colegio.Interfaz;
using Colegio.Modelos.Genero;
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

        private List<ListarGenero> MapearListarGenero(List<GeneroModelo> generoModelo)
        {
            return generoModelo.Select(modelo => new ListarGenero
            {
                nombreGenero = modelo.nombreGenero
            }).ToList();
        }

        public async Task<List<ListarGenero>> ValidarInformacionGeneroAsync()
        {
            var modeloGenero = await genero.InformacionGeneroAsync();
            
            var resultado = MapearListarGenero(modeloGenero);

            return resultado;
        }
    }
}
