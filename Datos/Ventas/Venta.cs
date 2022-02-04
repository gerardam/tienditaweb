using System;

//| || //  (( |+|====================================================//
//| ||// ((   |+| Tienda V 1.0                                      //
//| ||\\ ((   |+| Kyocode | www.kyocode.com                         \\
//| || \\  (( |+|====================================================\\
namespace VentasCal.Datos
{
    public partial class Venta
    {
        /// <summary>
        /// Metodo constructor para la clase Venta.
        /// </summary>
        public Venta()
        {

        }

        /// <summary>
        /// Metodo constructor con parametros de la clase Venta
        /// </summary>
        public Venta(string _codigo, string _cliente, decimal _importe, byte _claveEstado, DateTime _fechaCambio)
        {
            this.codigoField = _codigo;
            this.clienteField = _cliente;
            this.importeField = _importe;
            this.claveEstadoField = _claveEstado;
            this.fechaCambioField = _fechaCambio;
        }

        private Int64 claveEntidadField;
        /// <summary>
        /// Obtiene o establece la propiedad de ClaveEntidad.
        /// </summary>
        /// <value>ClaveEntidad tipo de dato Int32.</value>
        public Int64 ClaveEntidad
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

        private string clienteField;
        /// <summary>
        /// Obtiene o establece la propiedad de Cliente.
        /// </summary>
        /// <value>Cliente tipo de dato String.</value>
        public string Cliente
        {
            get { return this.clienteField; }
            set { this.clienteField = value; }
        }

        private decimal importeField;
        /// <summary>
        /// Obtiene o establece la propiedad de Importe.
        /// </summary>
        /// <value>Importe tipo de dato String.</value>
        public decimal Importe
        {
            get { return this.importeField; }
            set { this.importeField = value; }
        }

        private string observacionesField;
        /// <summary>
        /// Obtiene o establece la propiedad de Observaciones.
        /// </summary>
        /// <value>Observaciones tipo de dato String.</value>
        public string Observaciones
        {
            get { return this.observacionesField; }
            set { this.observacionesField = value; }
        }

        private Byte[] archivoField;
        /// <summary>
        /// Obtiene o establece la propiedad de Archivo.
        /// </summary>
        /// <value>Archivo tipo de dato Byte[].</value>
        public Byte[] Archivo
        {
            get { return this.archivoField; }
            set { this.archivoField = value; }
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