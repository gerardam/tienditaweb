//| || //  (( |+|====================================================//
//| ||// ((   |+| Tienda V 1.0                                      //
//| ||\\ ((   |+| Kyocode | www.kyocode.com                         \\
//| || \\  (( |+|====================================================\\
namespace VentasCal.Datos
{
    /// <summary>
    /// Opciones de tipo de accion a ejecutar
    /// </summary>
    public enum AccionTipo
    {
        Agregar = 1,
        Modificar = 2,
        Eliminar = 3,
        Aceptar = 4,
        Cancelar = 5
    }

    /// <summary>
    /// Opciones de tipo de accion a ejecutar
    /// </summary>
    public enum CveEstado
    {
        Activo = 1,
        Inactivo = 0
    }

    /// <summary>
    /// Opciones de tipo de usuario de acceso
    /// </summary>
    public enum TipoUsuario
    {
        Administrador = 1,
        Responsable = 2,
        Usuario = 3
    }

    /// <summary>
    /// Opciones de tpos de mensajes 
    /// </summary>
    public enum TipoMensaje
    {
        MostrarPop = 1,
        CerrarPop = 2,
        Agregar = 3,
        Actualizar = 4,
        Error = 5,
        Advertencia = 6,
        AdvertenciaMail = 7,
        AdvertenciaFolio = 8
    }
}
