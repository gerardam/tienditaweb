using System;
using System.Data;
using VentasCal.Datos.Recursos;

//| || //  (( |+|====================================================//
//| ||// ((   |+| Tienda V 1.0                                      //
//| ||\\ ((   |+| Kyocode | www.kyocode.com                         \\
//| || \\  (( |+|====================================================\\
namespace VentasCal.Datos
{
    internal class VentaFactoria : IDomainObjectFactory<Venta>
    {
        /// <summary>
        /// Metodo que construye un objeto Producto de un datareader.
        /// </summary>
        /// <param name="reader">Objeto tipo IDataReader con el que se contruira el objeto Producto.</param>
        /// <returns>Regresa un Objeto Producto.</returns>
        public Venta Construct(IDataReader reader)
        {
            if (reader == null) throw new Exception(CamposConstantes.CC_ObjetoLectura);
            Venta dato = new Venta();

            int codigoIndex = reader.GetOrdinal(CamposConstantes.CC_Codigo);
            if (!reader.IsDBNull(codigoIndex))
                dato.Codigo = reader.GetString(codigoIndex);

            int descripcionIndex = reader.GetOrdinal(CamposConstantes.CC_Cliente);
            if (!reader.IsDBNull(descripcionIndex))
                dato.Cliente = reader.GetString(descripcionIndex);

            int preUniIndex = reader.GetOrdinal(CamposConstantes.CC_Importe);
            if (!reader.IsDBNull(preUniIndex))
                dato.Importe = reader.GetDecimal(preUniIndex);

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