using Colegio.Modelos.Rh.Vistas;

namespace Colegio.Interfaz
{
    public interface IRh
    {
        Task<List<ListarRh>> InformacionRhAsync();
    }
}
