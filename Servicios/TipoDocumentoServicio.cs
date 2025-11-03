using Colegio.Interfaz;
using Colegio.Modelos.Tipo_Documento.Vistas;

namespace Colegio.Servicios
{
    public class TipoDocumentoServicio
    {
        private readonly ITipoDocumento tipoDocumento;

        public TipoDocumentoServicio(ITipoDocumento tipoDocumento)
        {
            this.tipoDocumento = tipoDocumento;
        }
        public async Task<List<ListarTipoDocumento>> ValidarInformacionTipoDocumentoAsync()
        {
            var informacionTipoDocumento = await tipoDocumento.InformacionTipoDocumentoAsync();

            return informacionTipoDocumento;
        }
    }
}
