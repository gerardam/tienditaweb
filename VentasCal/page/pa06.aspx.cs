using System;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Data;
using VentasCal.Datos;
using VentasCal.Datos.Recursos;

//| || //  (( |+|====================================================//
//| ||// ((   |+| Tienda V 1.0                                      //
//| ||\\ ((   |+| Kyocode | www.kyocode.com                         \\
//| || \\  (( |+|====================================================\\
namespace Presentacion
{
    public partial class PA06 : Page
    {
        #region PROPIEDADES
        /// <summary>
        /// Propiedad de claveEntidad.
        /// </summary>
        private int claveEntidad
        {
            set { ViewState[Mensajes.C_ClaveEntidad] = value; }
            get { return (int)ViewState[Mensajes.C_ClaveEntidad]; }
        }

        /// <summary>
        /// Propiedad de idUsuarioP
        /// </summary>
        private string idUsuarioP
        {
            set { ViewState["idUsuarioP"] = value; }
            get { return (string)ViewState["idUsuarioP"]; }
        }

        /// <summary>
        /// Propiedad de FechaCambio.
        /// </summary>
        private DateTime fechaCambio
        {
            set { ViewState[Mensajes.C_fechaCambio] = value; }
            get { return (DateTime)ViewState[Mensajes.C_fechaCambio]; }
        }

        /// <summary>
        /// Propiedad para agregar o modificar.
        /// </summary>
        private int AgregarModificar
        {
            set { ViewState[Mensajes.C_AgregarModificar] = value; }
            get { return (int)ViewState[Mensajes.C_AgregarModificar]; }
        }
        #endregion

        #region numeradores
        /// <summary>
        /// Numerador de columnas del grid
        /// </summary>
        private enum ColumnaGrvDepartamento
        {
            ClaveEntidad = ConsNumericos.Cero,
            Nombre = ConsNumericos.Uno,
            Email = ConsNumericos.Dos,
            Telefono = ConsNumericos.Tres,
            Permisos = ConsNumericos.Cuatro,
            ClaveEstado = ConsNumericos.Cinco,
            FechaCambio = ConsNumericos.Siete
        }
        #endregion

        #region EVENTOS

        /// <summary>
        /// Evento que se ejecuta al cargarse la pagina
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UsuarioLogueado"] != null)
            {
                if (!Page.IsPostBack)
                    cargarUsuarios();
            }
            else
                Response.Redirect("~/admin");
        }

        /// <summary>
        /// Evento que cambia los valores de las celdas del grid
        /// </summary>
        protected void gvUsuarios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[(int)ColumnaGrvDepartamento.ClaveEstado].Text == ((int)CveEstado.Activo).ToString())
                {
                    e.Row.Cells[(int)ColumnaGrvDepartamento.ClaveEstado].Text = Mensajes.C_Activo;
                    e.Row.Cells[(int)ColumnaGrvDepartamento.ClaveEstado].ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    e.Row.Cells[(int)ColumnaGrvDepartamento.ClaveEstado].Text = Mensajes.C_Inactivo;
                    e.Row.Cells[(int)ColumnaGrvDepartamento.ClaveEstado].ForeColor = System.Drawing.Color.Red;
                }
                if (e.Row.Cells[(int)ColumnaGrvDepartamento.Permisos].Text == ((int)TipoUsuario.Administrador).ToString())
                    e.Row.Cells[(int)ColumnaGrvDepartamento.Permisos].Text = Mensajes.C_Administrador;
                else if (e.Row.Cells[(int)ColumnaGrvDepartamento.Permisos].Text == ((int)TipoUsuario.Responsable).ToString())
                    e.Row.Cells[(int)ColumnaGrvDepartamento.Permisos].Text = Mensajes.C_Responsable;
                else if (e.Row.Cells[(int)ColumnaGrvDepartamento.Permisos].Text == ((int)TipoUsuario.Usuario).ToString())
                    e.Row.Cells[(int)ColumnaGrvDepartamento.Permisos].Text = Mensajes.C_Usuario;

                e.Row.Cells[ConsNumericos.Seis].ToolTip = Mensajes.C_Editar;
            }
        }

        /// <summary>
        /// Evento que se ejecuta al editar un registro
        /// </summary>
        protected void gvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", MetodosGenerales.mostrarMensaje((int)TipoMensaje.MostrarPop), true);

                limpiarCampos();
                chkEstatus.Enabled = true;
                AgregarModificar = (int)AccionTipo.Modificar;
                int index = ConsNumericos.Cero;
                if (e.CommandName.Equals(Mensajes.C_Page))
                    index = ConsNumericos.Cero;
                else
                    index = int.Parse(e.CommandArgument.ToString());

                GridView grid = (GridView)e.CommandSource;
                claveEntidad = short.Parse(grid.DataKeys[index].Value.ToString());
                Usuarios DACUsuario = new Usuarios();
                Usuario pUsuario = DACUsuario.ObtenerPersonalPorIdUsuario(claveEntidad);
                fechaCambio = pUsuario.FechaCambio;
                idUsuarioP = pUsuario.IdUsuario;
                txtNombre.Text = pUsuario.Nombre;
                txtApellidoP.Text = pUsuario.ApellidoPaterno;
                txtApellidoM.Text = pUsuario.ApellidoMaterno;
                txtTelefono.Text = pUsuario.Telefono;
                txtEmail.Text = pUsuario.IdUsuario;
                txtPassword.Text = pUsuario.Contrasena;

                cargarPermisos();
                ddlPermisos.SelectedValue = pUsuario.TipoUsuario.ToString();

                string Estado = pUsuario.ClaveEstado == 1 ? Mensajes.C_Activo : Mensajes.C_Inactivo;
                if (Estado.Equals(Mensajes.C_Activo))
                    chkEstatus.Checked = true;
                else
                    chkEstatus.Checked = false;
            }
            catch (Exception ex)
            {
                string ErrorMsg = ex.Message;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", MetodosGenerales.mostrarMensaje((int)TipoMensaje.Error), true);
            }
        }

        /// <summary>
        /// Evento que se ejecuta al agregar un nuevo registro al grid
        /// </summary>
        protected void btnAgregarReg_Click(object sender, EventArgs e)
        {
            AgregarModificar = (int)AccionTipo.Agregar;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", MetodosGenerales.mostrarMensaje((int)TipoMensaje.MostrarPop), true);
            limpiarCampos();
            cargarPermisos();
            chkEstatus.Enabled = false;
        }

        /// <summary>
        /// Evento que se ejecuta al cancelar la edicion o agregado de registros por la venta emergente
        /// </summary>
        protected void btnCancelarPpAgregar_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", MetodosGenerales.mostrarMensaje((int)TipoMensaje.CerrarPop), true);
        }

        /// <summary>
        /// Evento que se ejecuta al editar o agregar registros, seleccionando la opcion de Aceptar de la ventana emergente
        /// </summary>
        protected void btnAceptarPpAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNombre.Text != string.Empty && txtApellidoP.Text != String.Empty && txtEmail.Text != String.Empty && txtPassword.Text != String.Empty && Convert.ToInt32(ddlPermisos.SelectedValue) != (int)ConsNumericos.Cero)
                {
                    Usuario pUsuario = new Usuario();
                    pUsuario.Nombre = txtNombre.Text;
                    pUsuario.ApellidoPaterno = txtApellidoP.Text;
                    pUsuario.ApellidoMaterno = txtApellidoM.Text;
                    pUsuario.Telefono = txtTelefono.Text;
                    pUsuario.IdUsuario = txtEmail.Text;
                    pUsuario.Contrasena = txtPassword.Text;
                    pUsuario.TipoUsuario = Convert.ToByte(ddlPermisos.SelectedValue);

                    int mailExistente = new Usuarios().ValidarEmail(txtEmail.Text);
                    if ((int)AccionTipo.Agregar == AgregarModificar)
                    {
                        if (mailExistente != (int)ConsNumericos.Uno)
                        {
                            pUsuario.ClaveEstado = 1;
                            pUsuario.FechaCambio = DateTime.Now;
                            pUsuario.Comentarios = Mensajes.C_AgregarBien;

                            new Usuarios().InsertarUsuario(pUsuario);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", MetodosGenerales.mostrarMensaje((int)TipoMensaje.Agregar), true);
                        }
                        else
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", MetodosGenerales.mostrarMensaje((int)TipoMensaje.AdvertenciaMail), true);
                    }
                    else if ((int)AccionTipo.Modificar == AgregarModificar)
                    {
                        if (idUsuarioP == txtEmail.Text)
                        {
                            pUsuario.ClaveEntidad = claveEntidad;
                            pUsuario.FechaCambio = fechaCambio;
                            pUsuario.Comentarios = Mensajes.C_EditarBien;

                            if (chkEstatus.Checked)
                                pUsuario.ClaveEstado = (byte)CveEstado.Activo;
                            else
                                pUsuario.ClaveEstado = (byte)CveEstado.Inactivo;

                            if (chkEstatus.Checked)
                            {
                                new Usuarios().ActualizarDatosPersonal(pUsuario);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", MetodosGenerales.mostrarMensaje((int)TipoMensaje.Actualizar), true);
                            }
                            else
                            {
                                //if (rdbInactivo.Checked == true)
                                //{
                                Usuarios DACBien = new Usuarios();
                                //int DatoDevuelto = DACBien.ActualizarBien(pUsuario);
                                //int x;//TODO: cambiar y condiciones para la concurrencia al editar al mismo tiempo
                                //if (DatoDevuelto == 1)
                                //    x = DatoDevuelto;
                                ////Message.Show(this, Message.Icon.Success, string.Empty, Mensajes.C_MS5_ModificacionExitosa, this.btnAceptar);
                                //else if (DatoDevuelto == -1)
                                //    x = DatoDevuelto;
                                //Message.Show(this, Message.Icon.Warning, string.Empty, Mensajes.C_MS6_OpeFallida, this.btnAceptar);
                                //}
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", MetodosGenerales.mostrarMensaje((int)TipoMensaje.Actualizar), true);
                            }
                        }
                        else if (mailExistente != (int)ConsNumericos.Uno)
                        {
                            pUsuario.ClaveEntidad = claveEntidad;
                            pUsuario.FechaCambio = fechaCambio;
                            pUsuario.Comentarios = Mensajes.C_EditarBien;

                            if (chkEstatus.Checked)
                                pUsuario.ClaveEstado = (byte)CveEstado.Activo;
                            else
                                pUsuario.ClaveEstado = (byte)CveEstado.Inactivo;

                            if (chkEstatus.Checked)
                            {
                                new Usuarios().ActualizarDatosPersonal(pUsuario);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", MetodosGenerales.mostrarMensaje((int)TipoMensaje.Actualizar), true);
                            }
                            else
                            {
                                //if (rdbInactivo.Checked == true)
                                //{
                                Usuarios DACBien = new Usuarios();
                                //int DatoDevuelto = DACBien.ActualizarBien(pUsuario);
                                //int x;//TODO: cambiar y condiciones para la concurrencia al editar al mismo tiempo
                                //if (DatoDevuelto == 1)
                                //    x = DatoDevuelto;
                                ////Message.Show(this, Message.Icon.Success, string.Empty, Mensajes.C_MS5_ModificacionExitosa, this.btnAceptar);
                                //else if (DatoDevuelto == -1)
                                //    x = DatoDevuelto;
                                //Message.Show(this, Message.Icon.Warning, string.Empty, Mensajes.C_MS6_OpeFallida, this.btnAceptar);
                                //}
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", MetodosGenerales.mostrarMensaje((int)TipoMensaje.Actualizar), true);
                            }
                        }
                        else
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", MetodosGenerales.mostrarMensaje((int)TipoMensaje.AdvertenciaMail), true);
                    }
                }
                cargarUsuarios();
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
        /// Metodo que carga los usuarios registrados en el sistema
        /// </summary>
        public void cargarUsuarios()
        {
            DataSet dtsUsuario = new Usuarios().ObtenerUsuarioTodo();
            gvUsuarios.DataSource = dtsUsuario;
            gvUsuarios.DataBind();
        }

        /// <summary>
        /// Metodo que carga los permisos en la lista
        /// </summary>
        public void cargarPermisos()
        {
            ddlPermisos.Items.Clear();
            ListItem i;
            i = new ListItem(Mensajes.C_Seleccione, ConsNumericos.Cero.ToString());
            ddlPermisos.Items.Add(i);
            i = new ListItem(Mensajes.C_Administrador, Convert.ToString((int)TipoUsuario.Administrador));
            ddlPermisos.Items.Add(i);
            i = new ListItem(Mensajes.C_Responsable, Convert.ToString((int)TipoUsuario.Responsable));
            ddlPermisos.Items.Add(i);
            i = new ListItem(Mensajes.C_Usuario, Convert.ToString((int)TipoUsuario.Usuario));
            ddlPermisos.Items.Add(i);
        }

        /// <summary>
        /// Metodo que limpia los capos de la venta emergente
        /// </summary>
        protected void limpiarCampos()
        {
            txtNombre.Text = String.Empty;
            txtApellidoP.Text = String.Empty;
            txtApellidoM.Text = String.Empty;
            txtTelefono.Text = String.Empty;
            txtEmail.Text = String.Empty;
            txtPassword.Text = String.Empty;
        }

        #endregion
    }
}