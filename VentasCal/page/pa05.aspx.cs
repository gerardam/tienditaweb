using System;
using System.Web.UI;
using VentasCal.Datos;
using VentasCal.Datos.Recursos;
using VentasCal.Negocio;

//| || //  (( |+|====================================================//
//| ||// ((   |+| Tienda V 1.0                                      //
//| ||\\ ((   |+| Kyocode | www.kyocode.com                         \\
//| || \\  (( |+|====================================================\\
namespace Presentacion
{
    public partial class PA05 : Page
    {
        #region PROPIEDADES

        /// <summary>
        /// Propiedad de datosUsuario.
        /// </summary>
        private DatosSesion datosUsuario
        {
            set { ViewState["datosUsuario"] = value; }
            get { return (DatosSesion)ViewState["datosUsuario"]; }
        }

        /// <summary>
        /// Propiedad de fechaCambio.
        /// </summary>
        private DateTime fechaCambio
        {
            set { ViewState[Mensajes.C_fechaCambio] = value; }
            get { return (DateTime)ViewState[Mensajes.C_fechaCambio]; }
        }

        #endregion

        #region EVENTOS

        /// <summary>
        /// Evento que se ejecuta al cargarse la pagina
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            datosUsuario = (DatosSesion)Session["UsuarioLogueado"];
            if (datosUsuario != null)
            {
                if (!Page.IsPostBack)
                    CargarPersonal();
            }
            else
                Response.Redirect("~/admin");
        }

        /// <summary>
        /// Evento que se ejecuta al guardar los datos
        /// </summary>
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNombre.Text != String.Empty && txtApellidoP.Text != String.Empty && txtEmail.Text != String.Empty && password.Text != String.Empty)
                {
                    Usuario Upersonal = new Usuario();
                    Upersonal.ClaveEntidad = datosUsuario.CveEntUsuario;
                    Upersonal.Nombre = txtNombre.Text;
                    Upersonal.ApellidoPaterno = txtApellidoP.Text;
                    Upersonal.ApellidoMaterno = txtApellidoM.Text;
                    Upersonal.Telefono = txtTelefono.Text;
                    Upersonal.IdUsuario = txtEmail.Text;
                    Upersonal.Contrasena = password.Text;
                    Upersonal.TipoUsuario = datosUsuario.TipoUsuario;
                    Upersonal.ClaveEstado = (int)CveEstado.Activo;
                    Upersonal.FechaCambio = fechaCambio;
                    Upersonal.Comentarios = "Se ha editado el perfil";

                    new Usuarios().ActualizarDatosPersonal(Upersonal);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", MetodosGenerales.mostrarMensaje((int)TipoMensaje.Actualizar), true);
                }
                CargarPersonal();
            }
            catch (Exception ex)
            {
                string ErrorMsg = ex.Message;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", MetodosGenerales.mostrarMensaje((int)TipoMensaje.Error), true);
            }
        }

        #endregion

        #region FUNCIONES

        /// <summary>
        /// Metodo que carga los datos en el formulario
        /// </summary>
        private void CargarPersonal()
        {
            try
            {
                Usuarios DACUsuario = new Usuarios();
                Usuario personal = DACUsuario.ObtenerPersonalPorIdUsuario(datosUsuario.CveEntUsuario);
                txtNombre.Text = personal.Nombre;
                txtApellidoP.Text = personal.ApellidoPaterno;
                txtApellidoM.Text = personal.ApellidoMaterno;
                txtEmail.Text = personal.IdUsuario;
                txtTelefono.Text = personal.Telefono;
                password.Text = string.Empty;
                fechaCambio = personal.FechaCambio;
            }
            catch (Exception ex)
            {
                string ErrorMsg = ex.Message;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", MetodosGenerales.mostrarMensaje((int)TipoMensaje.Error), true);
            }
        }

        #endregion
    }
}