using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;
using System.Xml.Serialization;

//| || //  (( |+|====================================================//
//| ||// ((   |+| Tienda V 1.0                                      //
//| ||\\ ((   |+| Kyocode | www.kyocode.com                         \\
//| || \\  (( |+|====================================================\\
namespace VentasCal.Datos
{
    public static class MetodosGenerales
    {
        #region MENSAJES PREDEFINIDOS
        /// <summary>
        /// Metodo que lanza las ventanas y los mensajes emergentes
        /// </summary>
        public static string mostrarMensaje(int tipomensaje)
        {
            string javaScript = string.Empty;
            if (tipomensaje == (int)TipoMensaje.MostrarPop)
                javaScript = "$('#modal1').openModal();";
            else if (tipomensaje == (int)TipoMensaje.CerrarPop)
                javaScript = "$('#modal1').closeModal();";
            else if (tipomensaje == (int)TipoMensaje.Agregar)
                javaScript = "Materialize.toast('Registro agregado!', 4000)";
            else if (tipomensaje == (int)TipoMensaje.Actualizar)
                javaScript = "Materialize.toast('Registro actualizado!', 4000)";
            else if (tipomensaje == (int)TipoMensaje.Error)
                javaScript = "Materialize.toast('Ha ocurrido un error, vuelva a intentarlo!', 4000)";
            else if (tipomensaje == (int)TipoMensaje.Advertencia)
                javaScript = "Materialize.toast('Existe un registro con la misma descripción, intente con otro!', 4000)";
            else if (tipomensaje == (int)TipoMensaje.AdvertenciaMail)
                javaScript = "Materialize.toast('El correo ya existe, intente con otro!', 4000)";
            else if (tipomensaje == (int)TipoMensaje.AdvertenciaFolio)
                javaScript = "Materialize.toast('Existe un registro con el mismo Folio, intente con otro!', 4000)";

            return javaScript;
        }

        /// <summary>
        /// Construye y muestra una alerta con mensaje personalizado.
        /// </summary>
        public static string MostrarMensaje(string msg)
        {
            string javaScript = string.Format("Materialize.toast('{0}', 4000)", msg);
            return javaScript;
        }
        #endregion

        #region SERIALIZAR OBJETOS A XML
        /// <summary>
        /// Método para serializar un objeto a xml
        /// </summary>
        /// <param name="Lista">Tipo de objeto para serializar</param>
        /// <returns>Retorna un arreglo de byte</returns>
        public static Byte[] Serializar(this object Lista)
        {
            Byte[] Archivo;
            XmlSerializer serializer = new XmlSerializer(Lista.GetType());
            StringWriter writer = new StringWriter();
            serializer.Serialize(writer, Lista);
            string serializedValue = writer.ToString();

            using (var stream = new MemoryStream())
            {
                serializer.Serialize(stream, Lista);
                stream.Position = 0;
                Archivo = new Byte[stream.Length];
                System.IO.BinaryReader reader = new BinaryReader(stream);
                Archivo = reader.ReadBytes((int)stream.Length);
                stream.Read(Archivo, 0, (int)stream.Length);
            }
            return Archivo;
        }
        #endregion

        #region CARGAR DATOS EN DROPDOWNLIST ESTATICOS DESORDENADO
        /// <summary>
        /// Cargar dropdownlist a partir de un arreglo de datos
        /// </summary>
        public static void CargarDdlArrayDesordenado(string[] itemList, DropDownList ddlLista, int valorInicial)
        {
            ddlLista.Items.Clear();
            ListItem item;
            ddlLista.Items.Add(new ListItem("Seleccione", String.Empty));
            for (int i = 0; i < itemList.Length; i++)
            {
                item = new ListItem(itemList[i], (valorInicial).ToString());
                ddlLista.Items.Add(item);
                valorInicial++;
            }
        }
        #endregion

        #region VALIDAR SI EL IENUMERABLE CONTIENE REGISTROS
        /// <summary>
        /// Validar si el ienumerable contiene registros
        /// </summary>
        /// <param name="ieNumerable">Ienumerable a partir de una tabla</param>
        /// <returns>True=Lleno, False=Vacio</returns>
        public static bool IsIENumerableFull(IEnumerable<DataRow> ieNumerable)
        {
            bool isFull = false;
            foreach (DataRow item in ieNumerable)
            {
                isFull = true;
                break;
            }
            return isFull;
        }
        #endregion
    }
}