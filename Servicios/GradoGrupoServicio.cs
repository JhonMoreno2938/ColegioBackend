using Colegio.Interfaz;
using Colegio.Modelos.Grado;
using Colegio.Modelos.Grado_Grupo;
using Colegio.Modelos.Grado_Grupo.Procedimientos;
using Colegio.Modelos.Grado_Grupo.Salidas_Procedimientos;
using Colegio.Modelos.Grado_Grupo.Vistas;
using Colegio.Modelos.Grupo;
using Colegio.Modelos.Nivel_Escolaridad;

namespace Colegio.Servicios
{
    public class GradoGrupoServicio
    {
        private readonly IGradoGrupo gradoGrupo;

        public GradoGrupoServicio(IGradoGrupo gradoGrupo)
        {
            this.gradoGrupo = gradoGrupo;
        }

        private GradoGrupoModelo MapearRegistrarGradoGrupoNivelEscolaridad(RegistrarGradoGrupoNivelEscolaridad registrarGradoGrupoNivelEscolaridad)
        {
            var grado = new GradoModelo()
            {
                pkIdGrado = 0,
                nombreGrado = registrarGradoGrupoNivelEscolaridad.nombreGrado
            };

            var grupo = new GrupoModelo()
            {
                pkIdGrupo = 0,
                nombreGrupo = registrarGradoGrupoNivelEscolaridad.nombreGrupo
            };

            var nivelEscolaridad = new NivelEscolaridadModelo()
            {
                pkIdNivelEscolaridad = 0,
                nombreNivelEscolaridad = registrarGradoGrupoNivelEscolaridad.nombreNivelEscolaridad
            };

            return new GradoGrupoModelo
            {
                gradoModelo = grado,
                grupoModelo = grupo,
                nivelEscolaridadModelo = nivelEscolaridad
            };
        }

        private List<ListarGradoGrupo> MapearListarGradoGrupo(List<ListaGradoGrupoModelo> listaGradoGrupoModelo)
        {
            return listaGradoGrupoModelo.Select(modelo => new ListarGradoGrupo
            {
                nombreGradoGrupo = modelo.nombreGradoGrupo,
                nombreNivelEscolaridad = modelo.nombreNivelEscolaridad,
                estadoGradoGrupo = modelo.estadoGradoGrupo
            }).ToList();
        }

        private List<ListarGradoGrupoEstadoActivo> MapearListarGradoGrupoEstadoActivo(List<ListaGradoGrupoModelo> listaGradoGrupoModelo)
        {
            return listaGradoGrupoModelo.Select(modelo => new ListarGradoGrupoEstadoActivo
            {
                nombreGradoGrupo = modelo.nombreGradoGrupo,
                nombreNivelEscolaridad = modelo.nombreNivelEscolaridad
            }).ToList();
        }

        public async Task<ResultadoMensajeGradoGrupo> ValidarInformacionRegistrarGradoGrupoNivelEscolaridadAsync(RegistrarGradoGrupoNivelEscolaridad registrarGradoGrupoNivelEscolaridad)
        {
            GradoGrupoModelo gradoGrupoModelo = MapearRegistrarGradoGrupoNivelEscolaridad(registrarGradoGrupoNivelEscolaridad);

            ResultadoMensajeGradoGrupo resultado = await gradoGrupo.RegistrarGradoGrupoNivelEscolaridadAsync(gradoGrupoModelo);

            return resultado;
        }

        public async Task<ResultadoMensajeGradoGrupo> ValidarGestionarEstadoGradoGrupoNivelEscolaridadAsync(GestionarEstadoGradoGrupoNivelEscolaridad gestionarEstadoGradoGrupoNivelEscolaridad)
        {
            string nombreGrado = string.Empty;
            string nombreGrupo = string.Empty;

            if (!string.IsNullOrEmpty(gestionarEstadoGradoGrupoNivelEscolaridad.nombreGradoGrupo))
            {
                var partes = gestionarEstadoGradoGrupoNivelEscolaridad.nombreGradoGrupo.Split('-');
                nombreGrado = partes[0].Trim();
                nombreGrupo = partes.Length > 1 ? partes[1].Trim() : string.Empty;
            }

            ResultadoMensajeGradoGrupo resultado = await gradoGrupo.GestionarEstadoGradoGrupoNivelEscolaridadAsync(
                gestionarEstadoGradoGrupoNivelEscolaridad.nombreOperacion,
                nombreGrado,
                nombreGrupo,
                gestionarEstadoGradoGrupoNivelEscolaridad.nombreNivelEscolaridad
                );

            return resultado;
        }

        public async Task<List<ListarGradoGrupo>> ValidarInformacionGradoGrupoAsync()
        {
            var modeloGradoGrupo = await gradoGrupo.InformacionGradoGrupoAsync();

            var resultado = MapearListarGradoGrupo(modeloGradoGrupo);

            return resultado;
        }

        public async Task<List<ListarGradoGrupoEstadoActivo>> ValidarInformacionGradoGrupoEstadoActivoAsync()
        {
            var modeloGradoGrupo = await gradoGrupo.InformacionGradoGrupoEstadoActivoAsync();

            var resultado = MapearListarGradoGrupoEstadoActivo(modeloGradoGrupo);
            
            return resultado;
        }
    }
}
