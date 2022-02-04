using System;
using System.Data;
using VentasCal.Datos.Recursos;

//| || //  (( |+|====================================================//
//| ||// ((   |+| Tienda V 1.0                                      //
//| ||\\ ((   |+| Kyocode | www.kyocode.com                         \\
//| || \\  (( |+|====================================================\\
namespace VentasCal.Datos
{
    internal class ProductoFactoria : IDomainObjectFactory<Producto>
    {
        /// <summary>
        /// Metodo que construye un objeto Producto de un datareader.
        /// </summary>
        /// <param name="reader">Objeto tipo IDataReader con el que se contruira el objeto Producto.</param>
        /// <returns>Regresa un Objeto Producto.</returns>
        public Producto Construct(IDataReader reader)
        {
            if (reader == null) throw new Exception(CamposConstantes.CC_ObjetoLectura);
            Producto dato = new Producto();

            int codigoIndex = reader.GetOrdinal(CamposConstantes.CC_Codigo);
            if (!reader.IsDBNull(codigoIndex))
                dato.Codigo = reader.GetString(codigoIndex);

            int descripcionIndex = reader.GetOrdinal(CamposConstantes.CC_Descripcion);
            if (!reader.IsDBNull(descripcionIndex))
                dato.Descripcion = reader.GetString(descripcionIndex);

            int preUniIndex = reader.GetOrdinal(CamposConstantes.CC_PrecioUnitario);
            if (!reader.IsDBNull(preUniIndex))
                dato.PrecioUni = reader.GetDecimal(preUniIndex);

            int cantidadIndex = reader.GetOrdinal(CamposConstantes.CC_Cantidad);
            if (!reader.IsDBNull(cantidadIndex))
                dato.Cantidad = reader.GetInt32(cantidadIndex);

            int preRealIndex = reader.GetOrdinal(CamposConstantes.CC_PrecioReal);
            if (!reader.IsDBNull(preRealIndex))
                dato.PrecioReal = reader.GetDecimal(preRealIndex);

            int claveEstadoIndex = reader.GetOrdinal(CamposConstantes.CC_ClaveEstado);
            if (!reader.IsDBNull(claveEstadoIndex))
                dato.ClaveEstado = reader.GetByte(claveEstadoIndex);

            int fechaCambioIndex = reader.GetOrdinal(CamposConstantes.CC_FechaCambio);
            if (!reader.IsDBNull(fechaCambioIndex))
                dato.FechaCambio = reader.GetDateTime(fechaCambioIndex);

            return dato;
        }
    }
}