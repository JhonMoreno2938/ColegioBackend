namespace Colegio.Modelos.Usuario.Salidas_Procedimientos
{
    public class SalidaIniciarSesion
    {
        public bool exito { get; set; }
        public string nombrePersona { get; set; }
        public string nombreUsuario { get; set; }
        public string contraseinaUsuario { get; set; }
        public string nombreRolUsuario { get; set; }
        public string mensaje { get; set; }
    }
}
