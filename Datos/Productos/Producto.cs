using System;

//| || //  (( |+|====================================================//
//| ||// ((   |+| Tienda V 1.0                                      //
//| ||\\ ((   |+| Kyocode | www.kyocode.com                         \\
//| || \\  (( |+|====================================================\\
namespace VentasCal.Datos
{
    public partial class Producto
    {
        /// <summary>
        /// Metodo constructor para la clase Producto.
        /// </summary>
        public Producto()
        {

        }

        /// <summary>
        /// Metodo constructor con parametros de la clase Producto
        /// </summary>
        public Producto(String _codigo, String _descripcion, decimal _precioUni, int _cantidad, decimal _precioReal, Byte claveEstado, DateTime fechaCambio,
            String comentarios)
        {
            this.codigoField = _codigo;
            this.descripcionField = _descripcion;
            this.precioUniField = _precioUni;
            this.cantidadField = _cantidad;
            this.precioRealField = _precioReal;
            this.claveEstadoField = claveEstado;
            this.fechaCambioField = fechaCambio;
        }

        private int claveEntidadField;
        /// <summary>
        /// Obtiene o establece la propiedad de ClaveEntidad.
        /// </summary>
        /// <value>ClaveEntidad tipo de dato Int32.</value>
        public int ClaveEntidad
        {
            get { return this.claveEntidadField; }
            set { this.claveEntidadField = value; }
        }

        private string codigoField;
        /// <summary>
        /// Obtiene o establece la propiedad de Codigo.
        /// </summary>
        /// <value>Codigo tipo de dato String.</value>
        public string Codigo
        {
            get { return this.codigoField; }
            set { this.codigoField = value; }
        }

        private string descripcionField;
        /// <summary>
        /// Obtiene o establece la propiedad de Articulo.
        /// </summary>
        /// <value>Articulo tipo de dato String.</value>
        public string Descripcion
        {
            get { return this.descripcionField; }
            set { this.descripcionField = value; }
        }

        private decimal precioUniField;
        /// <summary>
        /// Obtiene o establece la propiedad de Marca.
        /// </summary>
        /// <value>Marca tipo de dato String.</value>
        public decimal PrecioUni
        {
            get { return this.precioUniField; }
            set { this.precioUniField = value; }
        }

        private int cantidadField;
        /// <summary>
        /// Obtiene o establece la propiedad de Marca.
        /// </summary>
        /// <value>Marca tipo de dato String.</value>
        public int Cantidad
        {
            get { return this.cantidadField; }
            set { this.cantidadField = value; }
        }

        private decimal precioRealField;
        /// <summary>
        /// Obtiene o establece la propiedad de Marca.
        /// </summary>
        /// <value>Marca tipo de dato String.</value>
        public decimal PrecioReal
        {
            get { return this.precioRealField; }
            set { this.precioRealField = value; }
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