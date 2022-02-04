using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using VentasCal.Datos;
using VentasCal.Datos.Recursos;
using VentasCal.Negocio;

//| || //  (( |+|====================================================//
//| ||// ((   |+| Tienda V 1.0                                      //
//| ||\\ ((   |+| Kyocode | www.kyocode.com                         \\
//| || \\  (( |+|====================================================\\
namespace Presentacion
{
    public partial class Default : System.Web.UI.Page
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
        /// Propiedad del importe.
        /// </summary>
        private decimal importe
        {
            set { ViewState["importe"] = value; }
            get { return (decimal)ViewState["importe"]; }
        }

        /// <summary>
        /// Propiedad contenedora de la tabla del plan
        /// </summary>
        private DataTable dtCatalogo
        {
            get
            {
                if (this.ViewState["dtCatalogo"] == null)
                {
                    this.ViewState["dtCatalogo"] = new DataTable();
                }
                return (DataTable)(this.ViewState["dtCatalogo"]);
            }
            set { this.ViewState["dtCatalogo"] = value; }
        }

        /// <summary>
        /// Propiedad contenedora de la tabla del plan
        /// </summary>
        private DataTable dtCompra
        {
            get
            {
                if (this.ViewState["dtCompra"] == null)
                {
                    this.ViewState["dtCompra"] = new DataTable();
                }
                return (DataTable)(this.ViewState["dtCompra"]);
            }
            set { this.ViewState["dtCompra"] = value; }
        }

        #endregion

        #region NUMERADORES

        #endregion

        #region EVENTOS
        /// <summary>
        /// Evento que se ejecuta al cargarse la pagina
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InicializarGridCompra();
                CargarCatalogo();
                importe = 0;
            }
        }

        /// <summary>
        /// Evento que cambia los calores numericos a cadenas para el estatus de los registros dentro del grid
        /// </summary>
        protected void gvCatalogo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }

        /// <summary>
        /// Evento que se ejecuta al editar un registro
        /// </summary>
        protected void gvCatalogo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = ConsNumericos.Cero;
                if (e.CommandName.Equals(Mensajes.C_Page))
                    index = ConsNumericos.Cero;
                else
                    index = int.Parse(e.CommandArgument.ToString());

                GridView grid = (GridView)e.CommandSource;
                claveEntidad = short.Parse(grid.DataKeys[index].Value.ToString());
                CargarCatalogo();

                IEnumerable<DataRow> ieCompra = from fila in dtCompra.AsEnumerable()
                                                where Convert.ToInt32(fila.Field<string>(CamposConstantes.CC_ClaveEntidad)) == claveEntidad
                                                select fila;
                int prodTotal = 0;
                if (MetodosGenerales.IsIENumerableFull(ieCompra))
                {
                    DataTable dtProdAdd = ieCompra.CopyToDataTable<DataRow>();
                    prodTotal = dtProdAdd.Rows.Count + 1;
                }

                if (Convert.ToInt32(dtCatalogo.Rows[index][CamposConstantes.CC_ClaveEntidad]) == claveEntidad &&
                    prodTotal <= Convert.ToInt32(dtCatalogo.Rows[index][CamposConstantes.CC_Cantidad]))
                {
                    importe += Convert.ToDecimal(dtCatalogo.Rows[index][CamposConstantes.CC_PrecioUnitario].ToString().Replace("$ ", ""));
                    txtTotal.Text = importe.ToString();
                    dtCompra.Rows.Add(
                        Convert.ToInt32(dtCatalogo.Rows[index][CamposConstantes.CC_ClaveEntidad]),
                        dtCatalogo.Rows[index][CamposConstantes.CC_Descripcion].ToString(),
                        dtCatalogo.Rows[index][CamposConstantes.CC_PrecioUnitario].ToString()
                        );
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", MetodosGenerales.MostrarMensaje("Producto ya no está disponible."), true);
                }

                gvCompra.DataSource = dtCompra;
                gvCompra.DataBind();
            }
            catch (Exception ex)
            {
                string ErrorMsg = ex.Message;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", MetodosGenerales.mostrarMensaje((int)TipoMensaje.Error), true);
            }
        }

        /// <summary>
        /// Evento que cambia los calores numericos a cadenas para el estatus de los registros dentro del grid
        /// </summary>
        protected void gvCompra_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }

        /// <summary>
        /// Evento que se ejecuta al editar un registro
        /// </summary>
        protected void gvCompra_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = ConsNumericos.Cero;
                if (e.CommandName.Equals(Mensajes.C_Page))
                    index = ConsNumericos.Cero;
                else
                    index = int.Parse(e.CommandArgument.ToString());

                importe -= Convert.ToDecimal(dtCompra.Rows[index][CamposConstantes.CC_PrecioUnitario].ToString().Replace("$ ", ""));
                txtTotal.Text = importe.ToString();
                dtCompra.Rows.RemoveAt(index);
                gvCompra.DataSource = dtCompra;
                gvCompra.DataBind();
            }
            catch (Exception ex)
            {
                string ErrorMsg = ex.Message;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", MetodosGenerales.mostrarMensaje((int)TipoMensaje.Error), true);
            }
        }

        /// <summary>
        /// Evento que se ejecuta al editar o agregar registros, seleccionando la opcion de Aceptar de la ventana emergente
        /// </summary>
        protected void btnComprar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtCompra.Rows.Count > 0)
                {
                    bool exist = true;
                    string sinStock = string.Empty;
                    CargarCatalogo();
                    foreach (DataRow rp in dtCompra.Rows)
                    {
                        foreach (DataRow rc in dtCatalogo.Rows)
                        {
                            if (rc[CamposConstantes.CC_ClaveEntidad].ToString() == rp[CamposConstantes.CC_ClaveEntidad].ToString())
                            {
                                exist = false;
                                break;
                            }
                        }
                        if (exist)
                        {
                            sinStock = sinStock + rp[CamposConstantes.CC_Descripcion].ToString() + "" + Environment.NewLine;
                        }
                        exist = true;
                    }

                    if (string.IsNullOrEmpty(sinStock))
                    {
                        Venta envCompra = new Venta();
                        envCompra.Codigo = GenerarCodigo();
                        envCompra.Cliente = txtNombre.Text;
                        envCompra.Importe = importe;
                        envCompra.Observaciones = string.Empty;
                        envCompra.Archivo = MetodosGenerales.Serializar(CargarDatosXML());

                        envCompra.ClaveEstado = 1;
                        envCompra.FechaCambio = DateTime.Now;
                        envCompra.Comentarios = "Se ha realizado una compra.";

                        new Ventas().Insertar(envCompra);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", MetodosGenerales.mostrarMensaje((int)TipoMensaje.MostrarPop), true);
                        Mail.Compra(txtNombre.Text, "-");
                        InicializarValores();
                    }
                    else
                    {
                        //lblMsgM2.Text = string.Format("Los siguientes productos ya no están disponibles: {0}; por favor seleccione otros productos.", sinStock);
                        txtProductos.Text = sinStock;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "$('#modal2').openModal();", true);
                    }
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", MetodosGenerales.MostrarMensaje("Debe agregar al menos un producto al carrito."), true);
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
        /// Inicializar todos los campos para nueva compra.
        /// </summary>
        private void InicializarValores()
        {
            InicializarGridCompra();
            CargarCatalogo();
            importe = 0;
            txtTotal.Text = string.Empty;
            txtNombre.Text = string.Empty;
        }

        /// <summary>
        /// Metodo que carga las areas obtenidas de la capa de datos.
        /// </summary>
        private void CargarCatalogo()
        {
            dtCatalogo = new Productos().ObtenerListadoProductos();
            gvCatalogo.DataSource = dtCatalogo;
            gvCatalogo.DataBind();
        }

        /// <summary>
        /// Inicializar lista de compras
        /// </summary>
        private void InicializarGridCompra()
        {
            dtCompra = new DataTable();
            dtCompra.Columns.Add(CamposConstantes.CC_ClaveEntidad);
            dtCompra.Columns.Add(CamposConstantes.CC_Descripcion);
            dtCompra.Columns.Add(CamposConstantes.CC_PrecioUnitario);

            gvCompra.DataSource = dtCompra;
            gvCompra.DataBind();
        }

        /// <summary>
        /// Cargar datos en archivos XML
        /// </summary>
        private List<ProductList> CargarDatosXML()
        {
            List<ProductList> listaCompras = new List<ProductList>();
            foreach (DataRow row in dtCompra.Rows)
            {
                listaCompras.Add(new ProductList
                {
                    ClaveEntidad = Convert.ToString(row[CamposConstantes.CC_ClaveEntidad]),
                    //Descripcion = Convert.ToString(row[CamposConstantes.CC_Descripcion]),
                    PrecioUnitario = Convert.ToDecimal(row[CamposConstantes.CC_PrecioUnitario].ToString().Replace("$ ", "").Replace(',', '.'))
                });
            }
            return listaCompras;
        }

        /// <summary>
        /// Generar un codigo de 6 caracteres
        /// </summary>
        /// <returns></returns>
        private string GenerarCodigo()
        {
            int longitud = 4;
            Guid miGuid = Guid.NewGuid();
            string token = Convert.ToBase64String(miGuid.ToByteArray());
            token = token.Replace("=", "").Replace("+", "").Replace("/", "");

            return token.Substring(0, longitud).ToUpper() + DateTime.Now.Second;
        }

        #endregion
    }
}