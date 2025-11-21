using Colegio.Interfaz;
using Colegio.Modelos.Estudiante_Periodo_Academico.Procedimientos;
using Colegio.Utilidades;

namespace Colegio.Servicios
{
    public class EstudiantePeriodoAcademicoServicio
    {
        private readonly IEstudiantePeriodoAcademico estudiantePeriodoAcademico;

        public EstudiantePeriodoAcademicoServicio(IEstudiantePeriodoAcademico estudiantePeriodoAcademico)
        {
            this.estudiantePeriodoAcademico = estudiantePeriodoAcademico;
        }

        public async Task<ResultadoOperacion> ValidarPrematricularEstudiantePeriodoAcademico(PrematricularEstudiantePeriodoAcademico prematricularEstudiantePeriodoAcademico)
        {
            ResultadoOperacion resultadoOperacion = await estudiantePeriodoAcademico.PrematricularEstudiantePeriodoAcademico(
                prematricularEstudiantePeriodoAcademico.numeroDocumento,
                prematricularEstudiantePeriodoAcademico.nombrePeriodoAcademico
                );

            return resultadoOperacion;
        }
    }
}
