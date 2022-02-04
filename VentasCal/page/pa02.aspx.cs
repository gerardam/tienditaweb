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
    public partial class PA02 : Page
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

        #region NUMERADORES

        /// <summary>
        /// Numerador de columnas del grid
        /// </summary>
        private enum GrvProductos
        {
            ClaveEntidad = ConsNumericos.Cero,
            Descripcion = ConsNumericos.Uno,
            Precio = ConsNumericos.Dos,
            Cantidad = ConsNumericos.Tres,
            ClaveEstado = ConsNumericos.Cuatro,
            Modificar = ConsNumericos.Cinco,
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
                    CargarProductos();
            }
            else
                Response.Redirect("~/admin");
        }

        /// <summary>
        /// Evento que cambia los valores de las celdas del grid
        /// </summary>
        protected void gvArticulos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[(byte)GrvProductos.ClaveEstado].Text == ((byte)CveEstado.Activo).ToString())
                {
                    e.Row.Cells[(byte)GrvProductos.ClaveEstado].Text = Mensajes.C_Activo;
                    e.Row.Cells[(byte)GrvProductos.ClaveEstado].ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    e.Row.Cells[(byte)GrvProductos.ClaveEstado].Text = Mensajes.C_Inactivo;
                    e.Row.Cells[(byte)GrvProductos.ClaveEstado].ForeColor = System.Drawing.Color.Red;
                }
                e.Row.Cells[(byte)GrvProductos.Modificar].ToolTip = Mensajes.C_Editar;
            }
        }

        /// <summary>
        /// Evento que se ejecuta al editar un registro
        /// </summary>
        protected void gvArticulos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                chkEstatus.Enabled = true;
                AgregarModificar = (int)AccionTipo.Modificar;
                int index = ConsNumericos.Cero;
                if (e.CommandName.Equals(Mensajes.C_Page))
                    index = ConsNumericos.Cero;
                else
                    index = int.Parse(e.CommandArgument.ToString());

                GridView grid = (GridView)e.CommandSource;
                claveEntidad = short.Parse(grid.DataKeys[index].Value.ToString());
                Productos DACArticulo = new Productos();
                Producto dacDato = DACArticulo.ObtenerPorClaveEntidad(claveEntidad);
                this.fechaCambio = dacDato.FechaCambio;
                txtArticulo.Text = dacDato.Descripcion;
                txtPrecioUni.Text = dacDato.PrecioUni.ToString().Replace(',', '.');
                txtCantidad.Text = dacDato.Cantidad.ToString();
                txtPrecioReal.Text = dacDato.PrecioReal.ToString().Replace(',', '.');

                string Estado = dacDato.ClaveEstado == 1 ? Mensajes.C_Activo : Mensajes.C_Inactivo;
                if (Estado.Equals(Mensajes.C_Activo))
                    chkEstatus.Checked = true;
                else
                    chkEstatus.Checked = false;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", MetodosGenerales.mostrarMensaje((int)TipoMensaje.MostrarPop), true);
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
            chkEstatus.Enabled = false;
            txtArticulo.Text = string.Empty;
            txtPrecioUni.Text = string.Empty;
            txtCantidad.Text = string.Empty;
            txtPrecioReal.Text = string.Empty;
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
                if (txtArticulo.Text != string.Empty && txtPrecioUni.Text != string.Empty && txtCantidad.Text != string.Empty && txtPrecioReal.Text != string.Empty)
                {
                    Producto dacDatos = new Producto();
                    dacDatos.Descripcion = txtArticulo.Text;
                    dacDatos.PrecioUni = Convert.ToDecimal(txtPrecioUni.Text);
                    dacDatos.Cantidad = Convert.ToInt32(txtCantidad.Text);
                    dacDatos.PrecioReal = Convert.ToDecimal(txtPrecioReal.Text);

                    if ((int)AccionTipo.Agregar == AgregarModificar)
                    {
                        dacDatos.ClaveEstado = 1;
                        dacDatos.FechaCambio = DateTime.Now;
                        dacDatos.Comentarios = Mensajes.C_AgregarArticulo;

                        new Productos().Insertar(dacDatos);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", MetodosGenerales.mostrarMensaje((int)TipoMensaje.Agregar), true);
                    }
                    else if ((int)AccionTipo.Modificar == AgregarModificar)
                    {
                        dacDatos.ClaveEntidad = claveEntidad;
                        dacDatos.FechaCambio = fechaCambio;
                        dacDatos.Comentarios = Mensajes.C_EditarArticulo;

                        if (chkEstatus.Checked)
                            dacDatos.ClaveEstado = (byte)CveEstado.Activo;
                        else
                            dacDatos.ClaveEstado = (byte)CveEstado.Inactivo;

                        if (chkEstatus.Checked)
                        {
                            new Productos().Actualizar(dacDatos);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", MetodosGenerales.mostrarMensaje((int)TipoMensaje.Actualizar), true);
                        }
                        else
                        {
                            Productos DACArticulo = new Productos();
                            int DatoDevuelto = DACArticulo.Actualizar(dacDatos);
                            //int x;//TODO: cambiar y condiciones para la concurrencia al editar al mismo tiempo
                            //if (DatoDevuelto == 1)
                            //    x = DatoDevuelto;
                            ////Message.Show(this, Message.Icon.Success, string.Empty, Mensajes.C_MS5_ModificacionExitosa, this.btnAceptar);
                            //else if (DatoDevuelto == -1)
                            //    x = DatoDevuelto;
                            ////Message.Show(this, Message.Icon.Warning, string.Empty, Mensajes.C_MS6_OpeFallida, this.btnAceptar);
                            ////}
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", MetodosGenerales.mostrarMensaje((int)TipoMensaje.Actualizar), true);
                        }
                    }
                }
                CargarProductos();
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
        /// Obtener y cargar los productos al grid
        /// </summary>
        private void CargarProductos()
        {
            DataTable dtsProductos = new Productos().ObtenerTodo();
            gvArticulos.DataSource = dtsProductos;
            gvArticulos.DataBind();
        }
        #endregion
    }
}