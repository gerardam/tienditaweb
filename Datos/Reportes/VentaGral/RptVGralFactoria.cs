using System;
using System.Data;
using VentasCal.Datos;
using VentasCal.Datos.Recursos;

namespace VentasCal.Datos
{
    internal class RptVGralFactoria : IDomainObjectFactory<RptVGral>
    {
        /// <summary>
        /// Metodo que construye un objeto RolReporte de un datareader.
        /// </summary>
        /// <param name="reader">Objeto tipo IDataReader con el que se contruirá el objeto RolReporte.</param>
        /// <returns>Regresa un Objeto "RolReporte".</returns>
        public RptVGral Construct(IDataReader reader)
        {
            if (reader == null) throw new Exception("El objeto es de solo lectura");
            RptVGral reporteBien = new RptVGral();

            int folioIndex = reader.GetOrdinal(CamposConstantes.CC_Codigo);
            if (!reader.IsDBNull(folioIndex))
                reporteBien.Folio = reader.GetString(folioIndex);

            int articuloIndex = reader.GetOrdinal(CamposConstantes.CC_Articulo);
            if (!reader.IsDBNull(articuloIndex))
                reporteBien.Articulo = reader.GetString(articuloIndex);

            int areaIndex = reader.GetOrdinal(CamposConstantes.CC_Area);
            if (!reader.IsDBNull(areaIndex))
                reporteBien.Area = reader.GetString(areaIndex);

            int responsableIndex = reader.GetOrdinal(CamposConstantes.CC_Responsable);
            if (!reader.IsDBNull(responsableIndex))
                reporteBien.Responsable = reader.GetString(responsableIndex);

            int estadoIndex = reader.GetOrdinal(CamposConstantes.CC_ClaveEstado);
            if (!reader.IsDBNull(estadoIndex))
                reporteBien.Estado = reader.GetString(estadoIndex);

            return reporteBien;
        }
    }
}