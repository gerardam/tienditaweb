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
    public partial class Usuarios : DBAcceso<Usuario>
    {
        #region Numeradores
        /// <summary>
        /// Enumeración de valores para las opciones disponibles del Store Procedure
        /// </summary>
        enum SPOpcion
        {
            ValidarUsuario = 1,
            ObtenerDatosUsuarioPorIdUsuario = 2,
            ObtenerPersonalPorIdUsuario = 3,
            ActualizarDatosPersonal = 4,
            ObtenerTodo = 5,
            InsertarUsuario = 7,
            ValidarEmail = 8
        }
        #endregion

        #region constructor
        /// <summary>
        /// Metodo constructor para la clase Usuarios.
        /// </summary>
        public Usuarios()
            : base()
        {
            this.esquema = ParametrosConstantes.EsquemaSDI;
        }

        #endregion

        #region Valida Usuario por ID y Contraseña
        /// <summary>
        /// Metodo que valida al usuario logueado.
        /// </summary>
        /// <param name="idUsario"></param>
        /// <param name="contraseha"></param>
        /// <returns></returns>
        public int ValidaUsuario(string idUsario, string contrasenha)
        {
            SqlParameter[] parametros =
            {
                new SqlParameter(ParametrosConstantes.PC_NOPCION,                   SqlDbType.TinyInt),
                new SqlParameter(ParametrosConstantes.PC_ValidaUsuario,             SqlDbType.Int),
                new SqlParameter(ParametrosConstantes.PC_IdUsuario,                 SqlDbType.VarChar),
                new SqlParameter(ParametrosConstantes.PC_Contrasena,                SqlDbType.VarChar),
            };
            parametros[0].Value = SPOpcion.ValidarUsuario;
            parametros[1].Direction = ParameterDirection.Output;
            parametros[2].Value = idUsario;
            parametros[3].Value = contrasenha;

            EjecutaProcedimientoCrud(SPConstantes.USUARIO_SP, parametros);
            return Convert.ToInt32(parametros[1].Value.ToString());
        }
        #endregion

        #region Obtener datos por Id Usuario
        /// <summary>
        /// Método que ejecuta la operacion obtener datos de usuario por IdUsuario.
        /// </summary>
        /// <param name="IdUsuario">Valor de Búsqueda correspondiente al Usuario</param>
        /// <returns>Proveedor consultado</returns>
        public Usuario ObtenerDatosUsuarioPorIdUsuario(String IdUsuario)
        {
            SqlParameter[] parametros =
            {
                new SqlParameter(ParametrosConstantes.PC_NOPCION,            SqlDbType.TinyInt),
                new SqlParameter(ParametrosConstantes.PC_IdUsuario,          SqlDbType.VarChar)
            };
            parametros[0].Value = SPOpcion.ObtenerDatosUsuarioPorIdUsuario;
            parametros[1].Value = IdUsuario;

            return EjecutaProcedimientoOne(SPConstantes.USUARIO_SP, parametros, new UsuarioFactoria());
        }
        #endregion

        #region Obtener Usuario por ClaveEntidad
        /// <summary>
        /// Método que ejecuta la operacion de Búsqueda por Clave Entidad en la BD
        /// </summary>
        /// <param name="claveEntidad">Valor de Búsqueda Correspondiente al Personal</param>
        /// <returns>Personal consultado</returns>
        public Usuario ObtenerPersonalPorIdUsuario(int claveEntidad)
        {
            SqlParameter[] parametros =
            {
                new SqlParameter (ParametrosConstantes.PC_NOPCION,      SqlDbType.TinyInt),
                new SqlParameter(ParametrosConstantes.PC_ClaveEntidad,  SqlDbType.Int)
            };
            parametros[0].Value = SPOpcion.ObtenerPersonalPorIdUsuario;
            parametros[1].Value = claveEntidad;

            return EjecutaProcedimientoOne(SPConstantes.USUARIO_SP, parametros, new PersonalFactoria());
        }
        #endregion

        #region Actualiza datos del personal
        /// <summary>
        /// Metodo que ejecuta la operación de Actualiza el registro en la BD.
        /// </summary>
        /// <param name="usuario">Objeto de usuario con los valores a insertar</param>
        public int ActualizarDatosPersonal(Usuario usuario)
        {
            SqlParameter[] parametros =
            {
                new SqlParameter(ParametrosConstantes.PC_NOPCION,                   SqlDbType.TinyInt),
                new SqlParameter(ParametrosConstantes.PC_ClaveEntidad,              SqlDbType.Int),
                new SqlParameter(ParametrosConstantes.PC_Nombre,                    SqlDbType.NVarChar),
                new SqlParameter(ParametrosConstantes.PC_ApellidoPaterno,           SqlDbType.NVarChar),
                new SqlParameter(ParametrosConstantes.PC_ApellidoMaterno,           SqlDbType.NVarChar),
                new SqlParameter(ParametrosConstantes.PC_Telefono,                  SqlDbType.NVarChar),
                new SqlParameter(ParametrosConstantes.PC_IdUsuario,                 SqlDbType.NVarChar),
                new SqlParameter(ParametrosConstantes.PC_Contrasena,                SqlDbType.NVarChar),
                new SqlParameter(ParametrosConstantes.PC_TipoUsuario,               SqlDbType.TinyInt),
                new SqlParameter(ParametrosConstantes.PC_ClaveEstado,               SqlDbType.TinyInt),
                new SqlParameter(ParametrosConstantes.PC_FechaCambio,               SqlDbType.DateTime),
                new SqlParameter(ParametrosConstantes.PC_Comentarios,               SqlDbType.NVarChar)
            };
            parametros[0].Value = SPOpcion.ActualizarDatosPersonal;
            parametros[1].Value = usuario.ClaveEntidad;
            parametros[2].Value = usuario.Nombre;
            parametros[3].Value = usuario.ApellidoPaterno;
            parametros[4].Value = usuario.ApellidoMaterno;
            parametros[5].Value = usuario.Telefono;
            parametros[6].Value = usuario.IdUsuario;
            parametros[7].Value = usuario.Contrasena;
            parametros[8].Value = usuario.TipoUsuario;
            parametros[9].Value = usuario.ClaveEstado;
            parametros[10].Value = usuario.FechaCambio;
            parametros[11].Value = usuario.Comentarios;

            DataSet TablaRecibir = EjecutaProcedimientoDS(SPConstantes.USUARIO_SP, parametros, CamposConstantes.CC_Modificaciones);
            int dato = int.Parse(TablaRecibir.Tables[CamposConstantes.CC_Modificaciones].Rows[0][0].ToString());
            return dato;
        }
        #endregion

        #region Obtener Todos los Usuarios
        /// <summary>
        /// Obtener todos los Usuarios de la BD
        /// </summary>
        /// <returns></returns>
        public DataSet ObtenerUsuarioTodo()
        {
            SqlParameter[] parametros =
            {
                new SqlParameter (ParametrosConstantes.PC_NOPCION,    SqlDbType.TinyInt)
            };
            parametros[0].Value = SPOpcion.ObtenerTodo;

            return EjecutaProcedimientoDS(SPConstantes.USUARIO_SP, parametros, SPConstantes.USUARIO_SP);
        }
        #endregion

        #region Insertar Usuarios
        /// <summary>
        /// Metodo que ejecuta la operación de Insertar el registro en la BD.
        /// </summary>
        /// <param name="area"></param>
        public void InsertarUsuario(Usuario usuario)
        {
            SqlParameter[] parametros =
            {
                new SqlParameter(ParametrosConstantes.PC_NOPCION,                       SqlDbType.TinyInt),
                new SqlParameter(ParametrosConstantes.PC_ClaveEntidad,                  SqlDbType.Int),
                new SqlParameter(ParametrosConstantes.PC_Nombre,                        SqlDbType.NVarChar),
                new SqlParameter(ParametrosConstantes.PC_ApellidoPaterno,               SqlDbType.NVarChar),
                new SqlParameter(ParametrosConstantes.PC_ApellidoMaterno,               SqlDbType.NVarChar),
                new SqlParameter(ParametrosConstantes.PC_Telefono,                      SqlDbType.NVarChar),
                new SqlParameter(ParametrosConstantes.PC_IdUsuario,                     SqlDbType.NVarChar),
                new SqlParameter(ParametrosConstantes.PC_Contrasena,                    SqlDbType.NVarChar),
                new SqlParameter(ParametrosConstantes.PC_TipoUsuario,                   SqlDbType.TinyInt),
                new SqlParameter(ParametrosConstantes.PC_ClaveEstado,                   SqlDbType.TinyInt),
                new SqlParameter(ParametrosConstantes.PC_FechaCambio,                   SqlDbType.DateTime),
                new SqlParameter(ParametrosConstantes.PC_Comentarios,                   SqlDbType.NVarChar)
            };
            parametros[0].Value = SPOpcion.InsertarUsuario;
            parametros[1].Direction = ParameterDirection.Output;
            parametros[2].Value = usuario.Nombre;
            parametros[3].Value = usuario.ApellidoPaterno;
            parametros[4].Value = usuario.ApellidoMaterno;
            parametros[5].Value = usuario.Telefono;
            parametros[6].Value = usuario.IdUsuario;
            parametros[7].Value = usuario.Contrasena;
            parametros[8].Value = usuario.TipoUsuario;
            parametros[9].Value = usuario.ClaveEstado;
            parametros[10].Value = usuario.FechaCambio;
            parametros[11].Value = usuario.Comentarios;

            EjecutaProcedimientoCrud(SPConstantes.USUARIO_SP, parametros);
        }
        #endregion

        #region Validar Email existente
        /// <summary>
        /// Metodo que valida la existencia de un correo igual.
        /// </summary>
        /// <param name="idUsario"></param>
        /// <param name="contraseha"></param>
        /// <returns></returns>
        public int ValidarEmail(string idUsario)
        {
            SqlParameter[] parametros =
            {
                new SqlParameter(ParametrosConstantes.PC_NOPCION,                   SqlDbType.TinyInt),
                new SqlParameter(ParametrosConstantes.PC_ValidaMail,                SqlDbType.Int),
                new SqlParameter(ParametrosConstantes.PC_IdUsuario,                 SqlDbType.VarChar)
            };
            parametros[0].Value = SPOpcion.ValidarEmail;
            parametros[1].Direction = ParameterDirection.Output;
            parametros[2].Value = idUsario;

            EjecutaProcedimientoCrud(SPConstantes.USUARIO_SP, parametros);
            return Convert.ToInt32(parametros[1].Value.ToString());
        }
        #endregion
    }
}