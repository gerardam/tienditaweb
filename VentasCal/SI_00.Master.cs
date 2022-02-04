using System;
using System.Web.UI;
using VentasCal.Datos;
using VentasCal.Negocio;

//| || //  (( |+|====================================================//
//| ||// ((   |+| Tienda V 1.0                                      //
//| ||\\ ((   |+| Kyocode | www.kyocode.com                         \\
//| || \\  (( |+|====================================================\\
namespace VentasCal
{
    public partial class SI_00 : System.Web.UI.MasterPage
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

        #endregion

        #region EVENTOS

        /// <summary>
        /// Metodo que se ejecuta al cargarse la pagina
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            datosUsuario = (DatosSesion)Session["UsuarioLogueado"];
            if (datosUsuario != null)
                mostrarOcultar(true);
            else
                mostrarOcultar(false);
        }

        /// <summary>
        /// Evento que lanza una ventana para el inicio de sesion
        /// </summary>
        protected void lnkInicio_Click(object sender, EventArgs e)
        {
            string javaScript = "$('#modalInicio').openModal();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", javaScript, true);
        }

        /// <summary>
        /// Evento que se ejecuta al aceptar los datos de sesion para validar el usuario
        /// </summary>
        protected void lnkAceptarInicio_Click(object sender, EventArgs e)
        {
            try
            {
                string idUsuario = txtEmail.Text;
                string contrasena = txtPassword.Text;
                if (idUsuario != String.Empty && contrasena != String.Empty)
                {
                    Usuarios DACusuario = new Usuarios();
                    int Result = DACusuario.ValidaUsuario(idUsuario, contrasena);
                    Usuario usuario = new Usuarios().ObtenerDatosUsuarioPorIdUsuario(idUsuario);
                    if (Result == (int)ConsNumericos.Uno)
                    {
                        Usuarios u = new Usuarios();
                        DatosSesion usuarioLogueado = new DatosSesion();
                        usuarioLogueado.EmailUsuario = usuario.IdUsuario;
                        usuarioLogueado.CveEntUsuario = usuario.ClaveEntidad;
                        usuarioLogueado.TipoUsuario = usuario.TipoUsuario;
                        Session["UsuarioLogueado"] = usuarioLogueado;

                        mostrarOcultar(true);
                        if (usuario.TipoUsuario == (int)TipoUsuario.Responsable)
                        {
                            hplUsuario.Visible = false;
                            hplUsuarios.Visible = false;
                        }
                        else if (usuario.TipoUsuario == (int)TipoUsuario.Usuario)
                        {
                            hplPerfil.Visible = false;
                            hplPerfils.Visible = false;
                            hplUsuario.Visible = false;
                            hplUsuarios.Visible = false;
                        }

                        txtEmail.Text = string.Empty;
                        txtPassword.Text = string.Empty;
                        string javaScript = "$('#modalInicio').closeModal();";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", javaScript, true);
                    }
                    else
                    {
                        string javaScript = "$('#modalInicio').openModal();";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", javaScript, true);
                        txtEmail.Text = string.Empty;
                        txtPassword.Text = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                string ErrorMsg = ex.Message;
            }
        }

        /// <summary>
        /// Evento que se ejecuta al cerrar sesion
        /// </summary>
        protected void lnkSalir_Click(object sender, EventArgs e)
        {
            System.Web.Security.FormsAuthentication.SignOut();
            Session.Clear();
            Session["UsuarioLogueado"] = null;
            Session.Remove("UsuarioLogueado");
            Session.Abandon();
            Response.Redirect("~/");

            mostrarOcultar(false);
        }

        #endregion

        #region FUNCIONES

        /// <summary>
        /// Metodo que oculta o muestra las opciones dsipobibles para el usuario logueado
        /// </summary>
        protected void mostrarOcultar(bool dato)
        {
            hplProducto.Visible = dato;
            hplVenta.Visible = dato;
            hplPerfil.Visible = dato;
            hplUsuario.Visible = dato;

            hplProductos.Visible = dato;
            hplVentas.Visible = dato;
            hplPerfils.Visible = dato;
            hplUsuarios.Visible = dato;

            lnkInicio.Visible = !dato;
            lnkLogin.Visible = !dato;
            lnkSalir.Visible = dato;
            lnkLogout.Visible = dato;

            if (datosUsuario != null)
            {
                if (datosUsuario.TipoUsuario == (int)TipoUsuario.Responsable)
                {
                    hplUsuario.Visible = false;
                    hplUsuarios.Visible = false;
                }
                else if (datosUsuario.TipoUsuario == (int)TipoUsuario.Usuario)
                {
                    hplPerfil.Visible = false;
                    hplPerfils.Visible = false;
                    hplUsuario.Visible = false;
                    hplUsuarios.Visible = false;
                }
            }
        }

        #endregion
    }
}