using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using VentasCal.Datos;
using VentasCal.Datos.Recursos;

namespace VentasCal.Datos
{
    public partial class RptVGrals : DBAcceso<RptVGral>
    {
        #region Numeradores
        /// <summary>
        /// Enumeración de valores para las opciones disponibles del Store Procedure
        /// </summary>
        enum SPOpcion
        {
            GenerarReporte = 20,
        }
        #endregion

        #region constructor
        /// <summary>
        /// Metodo constructor para la clase GBienes.
        /// </summary>
        public RptVGrals()
            : base()
        {
            this.esquema = ParametrosConstantes.EsquemaSDI;
        }
        #endregion

        #region Muestra los Roles

        public List<RptVGral> ObtenReporteBien()
        {
            List<RptVGral> lstRoles = new List<RptVGral>();

            SqlParameter[] parametros =
            {
                new SqlParameter(ParametrosConstantes.PC_NOPCION,                  SqlDbType.TinyInt),
            };
            parametros[0].Value = SPOpcion.GenerarReporte;

            return lstRoles = base.EjecutaProcedimiento(SPConstantes.Ventas_SP, parametros, new RptVGralFactoria());
        }
        #endregion
    }
}
