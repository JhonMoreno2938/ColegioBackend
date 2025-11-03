using Colegio.Modelos.Departamento.Vistas;

namespace Colegio.Interfaz
{
    public interface IDepartamento
    {
        Task<List<ListarDepartamento>> InformacionDepartamentoAsync();
    }
}
