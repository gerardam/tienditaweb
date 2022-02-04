using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VentasCal.Datos
{
    public class RptVGral
    {
        #region CONSTRUCTORES
        /// <summary>
        /// Metodo constructor para la clase ReporteBien.
        /// </summary>
        public RptVGral()
        {
        }

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="_folio"></param>
        /// <param name="_articulo"></param>
        /// <param name="_area"></param>
        /// <param name="_responsable"></param>
        /// <param name="_estado"></param>
        public RptVGral(string _folio, string _articulo, string _area, string _responsable, string _estado)
        {
            this.folio = _folio;
            this.articulo = _articulo;
            this.area = _area;
            this.responsable = _responsable;
            this.estado = _estado;
        }
        #endregion

        #region PROPIEDADES

        private string folio;
        /// <summary>
        /// Obtiene o establece la propiedad de Rol
        /// </summary>
        /// <value>ClaveEntidad tipo de dato String.</value>
        public string Folio
        {
            get { return this.folio; }
            set { this.folio = value; }
        }

        private string articulo;
        /// <summary>
        /// Obtiene o establece la propiedad de Rol(Descripción)
        /// </summary>
        /// <value>ClaveEntidad tipo de dato String.</value>
        public string Articulo
        {
            get { return this.articulo; }
            set { this.articulo = value; }
        }

        private string area;
        /// <summary>
        /// Obtiene o establece la propiedad de Funcionalidad
        /// </summary>
        /// <value>ClaveEntidad tipo de dato String.</value>
        public string Area
        {
            get { return this.area; }
            set { this.area = value; }
        }

        private string responsable;
        /// <summary>
        /// Obtiene o establece la propiedad de Funcionalidad(Descripción)
        /// </summary>
        /// <value>ClaveEntidad tipo de dato String.</value>
        public string Responsable
        {
            get { return this.responsable; }
            set { this.responsable = value; }
        }

        private string estado;
        /// <summary>
        /// Obtiene o establece la propiedad de Proceso (descripción)
        /// </summary>
        /// <value>ClaveEntidad tipo de dato String.</value>
        public string Estado
        {
            get { return this.estado; }
            set { this.estado = value; }
        }
        #endregion
    }
}