namespace Colegio.Interfaz
{
    public interface IAuditoria
    {
        Task<bool> AuditoriaCargueCsv(string nombreUsuario);
    }
}
