using Colegio.Interfaz;

namespace Colegio.Servicios
{
    public class AuditoriaServicio
    {
        public IAuditoria auditoria;

        public AuditoriaServicio(IAuditoria auditoria)
        {
            this.auditoria = auditoria;
        }

        public async Task<bool> ValidarAuditoriaCargueCsv(string nombreUsuario)
        {
            bool resultado = await auditoria.AuditoriaCargueCsv(nombreUsuario);

            return resultado;
        }
    }
}
