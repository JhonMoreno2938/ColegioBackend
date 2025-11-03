using Colegio.Modelos.Genero.Vistas;

namespace Colegio.Interfaz
{
    public interface IGenero
    {
        Task<List<ListarGenero>> InformacionGeneroAsync();
    }
}
