using System;
using System.Data;
using System.Data.SqlClient;
using VentasCal.Datos.Recursos;

//| || //  (( |+|====================================================//
//| ||// ((   |+| Tienda V 1.0                                      //
//| ||\\ ((   |+| Kyocode | www.kyocode.com                         \\
//| || \\  (( |+|====================================================\\
namespace VentasCal.Datos
{
    public partial class Productos : DBAcceso<Producto>
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
            Actualizar = 4,
            ValidarDescripcion = 5,
            ObtenerListadoProductos = 6
        }
        #endregion

        #region constructor
        /// <summary>
        /// Metodo constructor para la clase GArticulos.
        /// </summary>
        public Productos()
            : base()
        {
            this.esquema = ParametrosConstantes.EsquemaSDI;
        }

        #endregion

        #region Insertar
        /// <summary>
        /// Metodo que ejecuta la operación de Insertar el registro en la BD.
        /// </summary>
        public void Insertar(Producto dato)
        {
            SqlParameter[] parametros =
            {
                new SqlParameter(ParametrosConstantes.PC_NOPCION,           SqlDbType.TinyInt),
                new SqlParameter(ParametrosConstantes.PC_ClaveEntidad,      SqlDbType.Int),
                new SqlParameter(ParametrosConstantes.PC_Codigo,            SqlDbType.NVarChar),
                new SqlParameter(ParametrosConstantes.PC_Descripcion,       SqlDbType.NVarChar),
                new SqlParameter(ParametrosConstantes.PC_PrecioUnitario,    SqlDbType.Decimal),
                new SqlParameter(ParametrosConstantes.PC_Cantidad,          SqlDbType.Int),
                new SqlParameter(ParametrosConstantes.PC_PrecioReal,        SqlDbType.Decimal),
                new SqlParameter(ParametrosConstantes.PC_ClaveEstado,       SqlDbType.TinyInt),
                new SqlParameter(ParametrosConstantes.PC_FechaCambio,       SqlDbType.DateTime),
                new SqlParameter(ParametrosConstantes.PC_Comentarios,       SqlDbType.NVarChar)
            };
            parametros[0].Value = SPOpcion.Insertar;
            parametros[1].Direction = ParameterDirection.Output;
            parametros[2].Value = dato.Codigo;
            parametros[3].Value = dato.Descripcion;
            parametros[4].Value = dato.PrecioUni;
            parametros[5].Value = dato.Cantidad;
            parametros[6].Value = dato.PrecioReal;
            parametros[7].Value = dato.ClaveEstado;
            parametros[8].Value = dato.FechaCambio;
            parametros[9].Value = dato.Comentarios;

            EjecutaProcedimientoCrud(SPConstantes.Productos_SP, parametros);
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

            return EjecutaProcedimientoDT(SPConstantes.Productos_SP, parametros);
        }
        #endregion

        #region Obtener por claveentidad
        /// <summary>
        /// Método que ejecuta la operacion de Búsqueda por Clave Entidad en la BD
        /// </summary>
        /// <param name="claveEntidad">Valor de Búsqueda Correspondiente al Articulo</param>
        /// <returns>Articulo consultado</returns>
        public Producto ObtenerPorClaveEntidad(int claveEntidad)
        {
            SqlParameter[] parametros =
            {
                new SqlParameter (ParametrosConstantes.PC_NOPCION,      SqlDbType.TinyInt),
                new SqlParameter(ParametrosConstantes.PC_ClaveEntidad,  SqlDbType.Int)
            };
            parametros[0].Value = SPOpcion.ObtenerPorClaveEntidad;
            parametros[1].Value = claveEntidad;

            return EjecutaProcedimientoOne(SPConstantes.Productos_SP, parametros, new ProductoFactoria());
        }
        #endregion

        #region Actualizar
        /// <summary>
        /// Metodo que ejecuta la operación de Actualizar el registro en la BD.
        /// </summary>
        /// <param name="articulo">Objeto de Articulo con los valores a insertar</param>
        public int Actualizar(Producto dato)
        {
            SqlParameter[] parametros =
            {
                new SqlParameter(ParametrosConstantes.PC_NOPCION,           SqlDbType.TinyInt),
                new SqlParameter(ParametrosConstantes.PC_ClaveEntidad,      SqlDbType.Int),
                new SqlParameter(ParametrosConstantes.PC_Codigo,            SqlDbType.NVarChar),
                new SqlParameter(ParametrosConstantes.PC_Descripcion,       SqlDbType.NVarChar),
                new SqlParameter(ParametrosConstantes.PC_PrecioUnitario,    SqlDbType.Decimal),
                new SqlParameter(ParametrosConstantes.PC_Cantidad,          SqlDbType.Int),
                new SqlParameter(ParametrosConstantes.PC_PrecioReal,        SqlDbType.Decimal),
                new SqlParameter(ParametrosConstantes.PC_ClaveEstado,       SqlDbType.TinyInt),
                new SqlParameter(ParametrosConstantes.PC_FechaCambio,       SqlDbType.DateTime),
                new SqlParameter(ParametrosConstantes.PC_Comentarios,       SqlDbType.NVarChar)
            };
            parametros[0].Value = SPOpcion.Actualizar;
            parametros[1].Value = dato.ClaveEntidad;
            parametros[2].Value = dato.Codigo;
            parametros[3].Value = dato.Descripcion;
            parametros[4].Value = dato.PrecioUni;
            parametros[5].Value = dato.Cantidad;
            parametros[6].Value = dato.PrecioReal;
            parametros[7].Value = dato.ClaveEstado;
            parametros[8].Value = dato.FechaCambio;
            parametros[9].Value = dato.Comentarios;

            DataTable TablaRecibir = EjecutaProcedimientoDT(SPConstantes.Productos_SP, parametros);
            int datoTot = int.Parse(TablaRecibir.Rows[0][0].ToString());
            return datoTot;
        }
        #endregion

        #region Validar campo existente
        /// <summary>
        /// Metodo que valida la existencia de un correo igual.
        /// </summary>
        public int ValidarDescripcion(string descripcion)
        {
            SqlParameter[] parametros =
            {
                new SqlParameter(ParametrosConstantes.PC_NOPCION,       SqlDbType.TinyInt),
                new SqlParameter(ParametrosConstantes.PC_Descripcion,   SqlDbType.VarChar)
            };
            parametros[0].Value = SPOpcion.ValidarDescripcion;
            parametros[1].Value = descripcion;

            EjecutaProcedimientoCrud(SPConstantes.Productos_SP, parametros);
            return Convert.ToInt32(parametros[1].Value.ToString());
        }
        #endregion

        #region Obtener lista de productos al cliente
        /// <summary>
        /// Obtener todos los productos a la venta
        /// </summary>
        /// <returns></returns>
        public DataTable ObtenerListadoProductos()
        {
            SqlParameter[] parametros =
            {
                new SqlParameter (ParametrosConstantes.PC_NOPCION,    SqlDbType.TinyInt)
            };
            parametros[0].Value = SPOpcion.ObtenerListadoProductos;

            return EjecutaProcedimientoDT(SPConstantes.Productos_SP, parametros);
        }
        #endregion
    }
}