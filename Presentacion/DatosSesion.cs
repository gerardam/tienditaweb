using System;

//| || //  (( |+|====================================================//
//| ||// ((   |+| Tienda V 1.0                                      //
//| ||\\ ((   |+| Kyocode | www.kyocode.com                         \\
//| || \\  (( |+|====================================================\\
namespace VentasCal.Negocio
{
    [Serializable]
    public class DatosSesion
    {
        #region PROPIEDADES

        private string _emailUsuario;
        /// <summary>
        /// Propiedad para guardar el Email del usuario
        /// </summary>
        public string EmailUsuario
        {
            set { _emailUsuario = value; }
            get { return _emailUsuario; }
        }

        private int _cveEntUsuario;
        /// <summary>
        /// Propiedad para guardar la claveentidad del usuario
        /// </summary>
        public int CveEntUsuario
        {
            set { _cveEntUsuario = value; }
            get { return _cveEntUsuario; }
        }

        private byte _tipoUsuario;
        /// <summary>
        /// Propiedad para guardar los permisos del usuario
        /// </summary>
        public byte TipoUsuario
        {
            set { _tipoUsuario = value; }
            get { return _tipoUsuario; }
        }

        #endregion
    }
}