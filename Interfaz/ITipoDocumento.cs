using Colegio.Modelos.Tipo_Documento.Vistas;

namespace Colegio.Interfaz
{
    public interface ITipoDocumento
    {
        Task<List<ListarTipoDocumento>> InformacionTipoDocumentoAsync();
    }
}
