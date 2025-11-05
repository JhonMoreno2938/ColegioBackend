using Colegio.Modelos.Departamento;

namespace Colegio.Interfaz
{
    public interface IDepartamento
    {
        Task<List<DepartamentoModelo>> InformacionDepartamentoAsync();
    }
}
