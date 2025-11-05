using Colegio.Modelos.Tipo_Sede;

namespace Colegio.Interfaz
{
    public interface ITipoSede
    {
        Task<List<TipoSedeModelo>> InformacionTipoSedeAsync();
    }
}
