using Colegio.Modelos.Genero;

namespace Colegio.Interfaz
{
    public interface IGenero
    {
        Task<List<GeneroModelo>> InformacionGeneroAsync();
    }
}
