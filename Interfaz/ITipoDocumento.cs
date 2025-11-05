using Colegio.Modelos.Tipo_Documento;

namespace Colegio.Interfaz
{
    public interface ITipoDocumento
    {
        Task<List<TipoDocumentoModelo>> InformacionTipoDocumentoAsync();
    }
}
