using Colegio.Modelos.Tipo_Sede.Vistas;

namespace Colegio.Interfaz
{
    public interface ITipoSede
    {
        Task<List<ListarTipoSede>> InformacionTipoSedeAsync();
    }
}
