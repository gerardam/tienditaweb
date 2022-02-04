using System;

//| || //  (( |+|====================================================//
//| ||// ((   |+| Tienda V 1.0                                      //
//| ||\\ ((   |+| Kyocode | www.kyocode.com                         \\
//| || \\  (( |+|====================================================\\
namespace VentasCal.Datos
{
    public partial class Usuario
    {
        /// <summary>
        /// Metodo constructor para la clase Usuario.
        /// </summary>
        public Usuario()
        {

        }

        /// <summary>
        /// Metodo constructor con parametros de la clase Usuario
        /// </summary>
        public Usuario(Int32 claveEntidad, String idUsuario, String nombre, String apellidoP, String apellidoM, String telefono, String contrasena, Byte tipoUsuario, Byte claveEstado, DateTime fechaCambio, String comentarios)
        {
            this.claveEntidadField = claveEntidad;
            this.idUsuarioField = idUsuario;
            this.nombreField = nombre;
            this.apellidoPField = apellidoP;
            this.apellidoMField = apellidoM;
            this.telefonoField = telefono;
            this.contrasenaField = contrasena;
            this.tipoUsuarioField = tipoUsuario;
            this.claveEstadoField = claveEstado;
            this.fechaCambioField = fechaCambio;
            this.comentariosField = comentarios;
        }

        private Int32 claveEntidadField;
        /// <summary>
        /// Obtiene o establece la propiedad de ClaveEntidad.
        /// </summary>
        /// <value>ClaveEntidad tipo de dato Int32.</value>
        public Int32 ClaveEntidad
        {
            get { return this.claveEntidadField; }
            set { this.claveEntidadField = value; }
        }

        private String idUsuarioField;
        /// <summary>
        /// Obtiene o establece la propiedad de IdUsuario.
        /// </summary>
        /// <value>IdUsuario tipo de dato String.</value>
        public String IdUsuario
        {
            get { return this.idUsuarioField; }
            set { this.idUsuarioField = value; }
        }

        private String nombreField;
        /// <summary>
        /// Obtiene o establece la propiedad de Nombre.
        /// </summary>
        /// <value>Nombre tipo de dato String.</value>
        public String Nombre
        {
            get { return this.nombreField; }
            set { this.nombreField = value; }
        }

        private String apellidoPField;
        /// <summary>
        /// Obtiene o establece la propiedad de ApellidoPaterno.
        /// </summary>
        /// <value>ApellidoPaterno tipo de dato String.</value>
        public String ApellidoPaterno
        {
            get { return this.apellidoPField; }
            set { this.apellidoPField = value; }
        }

        private String apellidoMField;
        /// <summary>
        /// Obtiene o establece la propiedad de ApellidoMaterno.
        /// </summary>
        /// <value>ApellidoMaterno tipo de dato String.</value>
        public String ApellidoMaterno
        {
            get { return this.apellidoMField; }
            set { this.apellidoMField = value; }
        }

        private String telefonoField;
        /// <summary>
        /// Obtiene o establece la propiedad de Telefono.
        /// </summary>
        /// <value>Telefono tipo de dato String.</value>
        public String Telefono
        {
            get { return this.telefonoField; }
            set { this.telefonoField = value; }
        }

        private String contrasenaField;
        /// <summary>
        /// Obtiene o establece la propiedad de Contrasena.
        /// </summary>
        /// <value>Contrasena tipo de dato String.</value>
        public String Contrasena
        {
            get { return this.contrasenaField; }
            set { this.contrasenaField = value; }
        }

        private Byte tipoUsuarioField;
        /// <summary>
        /// Obtiene o establece la propiedad de TipoUsuario.
        /// </summary>
        /// <value>TipoUsuario tipo de dato byte.</value>
        public Byte TipoUsuario
        {
            get { return this.tipoUsuarioField; }
            set { this.tipoUsuarioField = value; }
        }

        private Byte claveEstadoField;
        /// <summary>
        /// Obtiene o establece la propiedad de ClaveEstado.
        /// </summary>
        /// <value>ClaveEstado tipo de dato Byte.</value>
        public Byte ClaveEstado
        {
            get { return this.claveEstadoField; }
            set { this.claveEstadoField = value; }
        }

        private DateTime fechaCambioField;
        /// <summary>
        /// Obtiene o establece la propiedad de FechaCambio.
        /// </summary>
        /// <value>FechaCambio tipo de dato DateTime.</value>
        public DateTime FechaCambio
        {
            get { return this.fechaCambioField; }
            set { this.fechaCambioField = value; }
        }

        private String comentariosField;
        /// <summary>
        /// Obtiene o establece la propiedad de Comentarios.
        /// </summary>
        /// <value>Comentarios tipo de dato String.</value>
        public String Comentarios
        {
            get { return this.comentariosField; }
            set { this.comentariosField = value; }
        }
    }
}