using System;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Data;
using System.Collections.Generic;
using Microsoft.Reporting.WebForms;
using VentasCal.Datos;
using VentasCal.Datos.Recursos;
using System.IO;
using System.Web;

//| || //  (( |+|====================================================//
//| ||// ((   |+| Tienda V 1.0                                      //
//| ||\\ ((   |+| Kyocode | www.kyocode.com                         \\
//| || \\  (( |+|====================================================\\
namespace Presentacion
{
    public partial class PA03 : Page
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
        private enum GrvVentas
        {
            ClaveEntidad = ConsNumericos.Cero,
            Codigo = ConsNumericos.Uno,
            Cliente = ConsNumericos.Dos,
            Importe = ConsNumericos.Tres,
            FechaAlta = ConsNumericos.Cuatro,
            ClaveEstado = ConsNumericos.Cinco,
            Modificar = ConsNumericos.Seis,
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
                    CargarVentas();
            }
            else
                Response.Redirect("~/admin");
        }

        /// <summary>
        /// Evento que cambia los valores de las celdas del grid
        /// </summary>
        protected void gvVentas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[(byte)GrvVentas.ClaveEstado].Text == ((byte)ConsNumericos.Uno).ToString())
                {
                    e.Row.Cells[(byte)GrvVentas.ClaveEstado].Text = "Pendiente";
                    e.Row.Cells[(byte)GrvVentas.ClaveEstado].ForeColor = System.Drawing.Color.Yellow;
                }
                else if (e.Row.Cells[(byte)GrvVentas.ClaveEstado].Text == ((byte)ConsNumericos.Dos).ToString())
                {
                    e.Row.Cells[(byte)GrvVentas.ClaveEstado].Text = "Pagado";
                    e.Row.Cells[(byte)GrvVentas.ClaveEstado].ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    e.Row.Cells[(byte)GrvVentas.ClaveEstado].Text = "Cancelada";
                    e.Row.Cells[(byte)GrvVentas.ClaveEstado].ForeColor = System.Drawing.Color.Red;
                }
                e.Row.Cells[(byte)GrvVentas.Modificar].ToolTip = Mensajes.C_Editar;
            }
        }

        /// <summary>
        /// Evento que se ejecuta al editar un registro
        /// </summary>
        protected void gvVentas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", MetodosGenerales.mostrarMensaje((int)TipoMensaje.MostrarPop), true);

                AgregarModificar = (int)AccionTipo.Modificar;
                int index = ConsNumericos.Cero;
                if (e.CommandName.Equals(Mensajes.C_Page))
                    index = ConsNumericos.Cero;
                else
                    index = int.Parse(e.CommandArgument.ToString());
                GridView grid = (GridView)e.CommandSource;
                claveEntidad = short.Parse(grid.DataKeys[index].Value.ToString());

                Ventas DACBien = new Ventas();
                Venta bienes = DACBien.ObtenerPorClaveEntidad(claveEntidad);
                fechaCambio = bienes.FechaCambio;

                txtCodigo.Text = bienes.Codigo;
                txtCliente.Text = bienes.Cliente;
                txtImporte.Text = bienes.Importe.ToString();

                CargarEstado();
                ddlEstado.SelectedValue = bienes.ClaveEstado.ToString();

                string prodLista = string.Empty;
                DataTable dtProductos = new Ventas().ObtenerProductosTxt(claveEntidad);
                foreach (DataRow r in dtProductos.Rows)
                {
                    prodLista = prodLista + r[CamposConstantes.CC_Descripcion].ToString() + "" + Environment.NewLine;
                }
                txtProductos.Text = prodLista;

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
            ddlEstado.Enabled = false;
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
                Venta bienes = new Venta();

                if ((int)AccionTipo.Agregar == AgregarModificar)
                {
                    bienes.ClaveEstado = 1;
                    bienes.FechaCambio = DateTime.Now;
                    bienes.Comentarios = Mensajes.C_AgregarBien;

                    new Ventas().Insertar(bienes);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", MetodosGenerales.mostrarMensaje((int)TipoMensaje.Agregar), true);
                }
                else if ((int)AccionTipo.Modificar == AgregarModificar)
                {
                    bienes.ClaveEntidad = claveEntidad;
                    bienes.FechaCambio = fechaCambio;
                    bienes.Comentarios = "Se ha actualizado una venta";
                    bienes.ClaveEstado = Convert.ToByte(ddlEstado.SelectedValue);

                    new Ventas().Actualizar(bienes);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", MetodosGenerales.mostrarMensaje((int)TipoMensaje.Actualizar), true);
                }
                CargarVentas();
            }
            catch (Exception ex)
            {
                string ErrorMsg = ex.Message;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", MetodosGenerales.mostrarMensaje((int)TipoMensaje.Error), true);
            }
        }

        /// <summary>
        /// Evento que se ejecuta al cancelar la edicion o agregado de registros por la venta emergente
        /// </summary>
        protected void lbtnPDF_Click(object sender, EventArgs e)
        {
            descargarArchivo("application/pdf", "PDF", ".pdf");
        }

        /// <summary>
        /// Evento que se ejecuta al cancelar la edicion o agregado de registros por la venta emergente
        /// </summary>
        protected void lbtnExcel_Click(object sender, EventArgs e)
        {
            descargarArchivo("application/xls", "EXCEL", ".xls");
        }

        /// <summary>
        /// Evento que se ejecuta al cancelar la edicion o agregado de registros por la venta emergente
        /// </summary>
        protected void lbtnWord_Click(object sender, EventArgs e)
        {
            descargarArchivo("application/ms-word", "WORD", ".doc");
        }

        #endregion

        #region FUNCIONES

        /// <summary>
        /// Obtener y cargar las ventas al grid
        /// </summary>
        private void CargarVentas()
        {
            DataTable dtVentas = new Ventas().ObtenerTodo();
            gvVentas.DataSource = dtVentas;
            gvVentas.DataBind();
        }

        /// <summary>
        /// Descargar reporte
        /// </summary>
        protected void descargarArchivo(string typeArchivo, string archivo, string extencionArchivo)
        {
            try
            {
                List<RptVGral> lstBien = new RptVGrals().ObtenReporteBien();
                if (lstBien.Count > 0)
                {
                    ReportDataSource rds = new ReportDataSource("dtsBienes", new RptVGrals().ObtenReporteBien());

                    string contentType = string.Empty;
                    contentType = typeArchivo;

                    string FileName = "Reporte_de_bienes" + extencionArchivo;
                    string extension;
                    string encoding;
                    string mimeType;
                    string[] streams;
                    Warning[] warnings;

                    LocalReport report = new LocalReport();
                    report.ReportPath = @"Reportes/rptBienes.rdlc";
                    report.DataSources.Add(rds);

                    //string direccion = (String)(HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.PathAndQuery, "/"));

                    Byte[] mybytes = report.Render(archivo, null,
                                    out extension, out encoding,
                                    out mimeType, out streams, out warnings);
                    //string ruta = Server.MapPath("~/Temp/"+FileName);
                    //direccion = direccion +""+FileName;
                    string dir = Server.HtmlEncode(Request.PhysicalApplicationPath);
                    dir = dir + "" + FileName;
                    using (FileStream fs = File.Create(dir))
                    {
                        fs.Write(mybytes, 0, mybytes.Length);
                    }

                    Response.ClearHeaders();
                    Response.ClearContent();
                    Response.Buffer = true;
                    Response.Clear();
                    Response.ContentType = contentType;
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName);
                    Response.WriteFile(dir);
                    Response.Flush();
                    Response.Close();
                    //Response.End();
                    HttpContext.Current.ApplicationInstance.CompleteRequest();

                    //string rutaDelete = "Reportes/" + FileName;
                    //File.Delete(direccion);
                }
            }
            catch (Exception ex)
            {
                string ErrorMsg = ex.Message;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", MetodosGenerales.mostrarMensaje((int)TipoMensaje.Error), true);
            }
        }

        /// <summary>
        /// Cargar lista con estados
        /// </summary>
        private void CargarEstado()
        {
            string[] itemsList = new string[] { "Cancelada", "Pendiente", "Pagada" };
            MetodosGenerales.CargarDdlArrayDesordenado(itemsList, ddlEstado, ConsNumericos.Cero);
        }

        #endregion
    }
}