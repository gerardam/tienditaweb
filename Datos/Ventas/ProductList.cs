using System;
using System.Xml.Serialization;

namespace VentasCal.Datos
{
    [Serializable]
    public class ProductList
    {
        private string _id;
        [XmlAttribute("ClaveEntidad")]
        public string ClaveEntidad
        {
            get { return _id; }
            set { _id = value; }
        }

        private decimal _precio;
        [XmlAttribute("PrecioUnitario")]
        public decimal PrecioUnitario
        {
            get { return _precio; }
            set { _precio = value; }
        }
    }
}