using Colegio.Interfaz;
using Colegio.Modelos.Tipo_Documento.Vistas;
using Colegio.Modelos.Tipo_Documento;

namespace Colegio.Servicios
{
    public class TipoDocumentoServicio
    {
        private readonly ITipoDocumento tipoDocumento;

        public TipoDocumentoServicio(ITipoDocumento tipoDocumento)
        {
            this.tipoDocumento = tipoDocumento;
        }

        private List<ListarTipoDocumento> MapearListarTipoDocumento(List<TipoDocumentoModelo> tipoDocumentoModelo)
        {
            return tipoDocumentoModelo.Select(modelo => new ListarTipoDocumento
            {
                nombreTipoDocumento = modelo.nombreTipoDocumento
            }).ToList();
        }


        public async Task<List<ListarTipoDocumento>> ValidarInformacionTipoDocumentoAsync()
        {
            var modeloTipoDocumento = await tipoDocumento.InformacionTipoDocumentoAsync();

            var resultado = MapearListarTipoDocumento(modeloTipoDocumento);

            return resultado;
        }
    }
}
