using Colegio.Modelos.Rh;

namespace Colegio.Interfaz
{
    public interface IRh
    {
        Task<List<RhModelo>> InformacionRhAsync();
    }
}
