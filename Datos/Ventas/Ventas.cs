using System.Data;
using System.Data.SqlClient;
using VentasCal.Datos.Recursos;

//| || //  (( |+|====================================================//
//| ||// ((   |+| Tienda V 1.0                                      //
//| ||\\ ((   |+| Kyocode | www.kyocode.com                         \\
//| || \\  (( |+|====================================================\\
namespace VentasCal.Datos
{
    public partial class Ventas : DBAcceso<Venta>
    {
        #region Numeradores
        /// <summary>
        /// Enumeración de valores para las opciones disponibles del Store Procedure
        /// </summary>
        enum SPOpcion
        {
            Insertar = 1,
            ObtenerTodo = 2,
            ObtenerPorClaveEntidad = 3,
            ObtenerListaProductos = 4,
            Actualizar = 5,
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Metodo constructor para la clase GArticulos.
        /// </summary>
        public Ventas()
            : base()
        {
            this.esquema = ParametrosConstantes.EsquemaSDI;
        }

        #endregion

        #region Insertar
        /// <summary>
        /// Metodo que ejecuta la operación de Insertar el registro en la BD.
        /// </summary>
        public void Insertar(Venta dato)
        {
            SqlParameter[] parametros =
            {
                new SqlParameter(ParametrosConstantes.PC_NOPCION,           SqlDbType.TinyInt),
                new SqlParameter(ParametrosConstantes.PC_ClaveEntidad,      SqlDbType.BigInt),
                new SqlParameter(ParametrosConstantes.PC_Codigo,            SqlDbType.NVarChar),
                new SqlParameter(ParametrosConstantes.PC_Cliente,           SqlDbType.NVarChar),
                new SqlParameter(ParametrosConstantes.PC_Importe,           SqlDbType.Decimal),
                new SqlParameter(ParametrosConstantes.PC_Observaciones,     SqlDbType.NVarChar),
                new SqlParameter(ParametrosConstantes.PC_Archivo,           SqlDbType.VarBinary),
                new SqlParameter(ParametrosConstantes.PC_ClaveEstado,       SqlDbType.TinyInt),
                new SqlParameter(ParametrosConstantes.PC_FechaCambio,       SqlDbType.DateTime),
                new SqlParameter(ParametrosConstantes.PC_Comentarios,       SqlDbType.NVarChar)
            };
            parametros[0].Value = SPOpcion.Insertar;
            parametros[1].Direction = ParameterDirection.Output;
            parametros[2].Value = dato.Codigo;
            parametros[3].Value = dato.Cliente;
            parametros[4].Value = dato.Importe;
            parametros[5].Value = dato.Observaciones;
            parametros[6].Value = dato.Archivo;
            parametros[7].Value = dato.ClaveEstado;
            parametros[8].Value = dato.FechaCambio;
            parametros[9].Value = dato.Comentarios;

            EjecutaProcedimientoCrud(SPConstantes.Ventas_SP, parametros);
        }
        #endregion

        #region Obtener todo
        /// <summary>
        /// Obtener todos los productos de la BD
        /// </summary>
        /// <returns></returns>
        public DataTable ObtenerTodo()
        {
            SqlParameter[] parametros =
            {
                new SqlParameter (ParametrosConstantes.PC_NOPCION,    SqlDbType.TinyInt)
            };
            parametros[0].Value = SPOpcion.ObtenerTodo;

            return EjecutaProcedimientoDT(SPConstantes.Ventas_SP, parametros);
        }
        #endregion

        #region Obtener por claveentidad
        /// <summary>
        /// Método que ejecuta la operacion de Búsqueda por Clave Entidad en la BD
        /// </summary>
        public Venta ObtenerPorClaveEntidad(int claveEntidad)
        {
            SqlParameter[] parametros =
            {
                new SqlParameter (ParametrosConstantes.PC_NOPCION,      SqlDbType.TinyInt),
                new SqlParameter(ParametrosConstantes.PC_ClaveEntidad,  SqlDbType.Int)
            };
            parametros[0].Value = SPOpcion.ObtenerPorClaveEntidad;
            parametros[1].Value = claveEntidad;

            return EjecutaProcedimientoOne(SPConstantes.Ventas_SP, parametros, new VentaFactoria());
        }
        #endregion

        #region Actualizar
        /// <summary>
        /// Metodo que ejecuta la operación de Actualizar el registro en la BD.
        /// </summary>
        public void Actualizar(Venta dato)
        {
            SqlParameter[] parametros =
            {
                new SqlParameter(ParametrosConstantes.PC_NOPCION,           SqlDbType.TinyInt),
                new SqlParameter(ParametrosConstantes.PC_ClaveEntidad,      SqlDbType.BigInt),
                new SqlParameter(ParametrosConstantes.PC_Observaciones,     SqlDbType.NVarChar),
                new SqlParameter(ParametrosConstantes.PC_ClaveEstado,       SqlDbType.TinyInt),
                new SqlParameter(ParametrosConstantes.PC_FechaCambio,       SqlDbType.DateTime),
                new SqlParameter(ParametrosConstantes.PC_Comentarios,       SqlDbType.NVarChar)
            };
            parametros[0].Value = SPOpcion.Actualizar;
            parametros[1].Value = dato.ClaveEntidad;
            parametros[2].Value = dato.Observaciones;
            parametros[3].Value = dato.ClaveEstado;
            parametros[4].Value = dato.FechaCambio;
            parametros[5].Value = dato.Comentarios;

            EjecutaProcedimientoCrud(SPConstantes.Ventas_SP, parametros);
        }
        #endregion

        #region Obtener productos comprados
        /// <summary>
        /// Obtener productos comprados (Campo de texto)
        /// </summary>
        /// <returns></returns>
        public DataTable ObtenerProductosTxt(int claveEntidad)
        {
            SqlParameter[] parametros =
            {
                new SqlParameter (ParametrosConstantes.PC_NOPCION,      SqlDbType.TinyInt),
                new SqlParameter(ParametrosConstantes.PC_ClaveEntidad,  SqlDbType.Int)
            };
            parametros[0].Value = SPOpcion.ObtenerListaProductos;
            parametros[1].Value = claveEntidad;

            return EjecutaProcedimientoDT(SPConstantes.Ventas_SP, parametros);
        }
        #endregion
    }
}