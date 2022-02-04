<%@ Page Title="Ventas" Language="C#" MasterPageFile="~/SI_00.master" AutoEventWireup="true" CodeBehind="pa03.aspx.cs" Inherits="Presentacion.PA03" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManagerProxy ID="smpSchedule" runat="server">
        <Scripts>
            <asp:ScriptReference Path="~/_Scripts/page/pa03.js" />
        </Scripts>
    </asp:ScriptManagerProxy>

    <div class="row">
        <div class="col s12">
            <div class="card-panel blue-grey darken-3">
                <h5 class="white-text">Ventas</h5>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col s12">
            <div class="card-panel">
                <div class="row">
                    <div class="input-field col s6">
                        <i class="material-icons prefix">search</i>
                        <asp:TextBox runat="server" class="tbxBuscar validate" name="tbxBuscar" ID="tbxBuscar"></asp:TextBox>
                        <label for="tbxBuscar">
                            <asp:Label runat="server" for="Buscar">Buscar</asp:Label></label>
                    </div>
                    <%--<div class="input-field col s3">
                        <div class="fixed-action-btn" style="bottom: 45px; right: 24px;">
                            <a class="btn-floating btn-large amber accent-3">
                                <i class="large material-icons md-36">file_download</i>
                            </a>
                            <ul>
                                <li>
                                    <asp:LinkButton ID="lbtnWord" class="btn-floating tooltipped waves-effect waves-light blue" data-position="left" data-tooltip="Word" data-delay="5" runat="server" OnClick="lbtnWord_Click"><i class="material-icons">description</i></asp:LinkButton>
                                </li>
                                <li>
                                    <asp:LinkButton ID="lbtnExcel" class="btn-floating tooltipped waves-effect waves-light green" data-position="left" data-tooltip="Excel" data-delay="5" runat="server" OnClick="lbtnExcel_Click"><i class="material-icons">insert_chart</i></asp:LinkButton>
                                </li>
                                <li>
                                    <asp:LinkButton ID="lbtnPDF" class="btn-floating tooltipped waves-effect waves-light red" data-position="left" data-tooltip="PDF" data-delay="5" runat="server" OnClick="lbtnPDF_Click"><i class="material-icons">picture_as_pdf</i></asp:LinkButton>
                                </li>
                            </ul>
                        </div>
                    </div>--%>
                    <%--<div class="input-field col s3 right-align">
                        <asp:LinkButton ID="lnkAgregar" class="btn-floating  btn-large waves-effect waves-light grey darken-3" runat="server"   OnClick="btnAgregarReg_Click" ><i class="material-icons md-48">add</i></asp:LinkButton>
                    </div>--%>
                </div>
            </div>
        </div>
    </div>

    <div id="modal1" class="modal modal-fixed-footer">
        <div class="modal-content validationGroup">
            <div class="row">
                <h4 class="center-align">Venta</h4>
                <div class="col s6">
                    <div class="input-field">
                        <label for="lblCodigo">
                            <asp:Label runat="server" ID="lblCodigo">Codigo</asp:Label></label>
                        <asp:TextBox ID="txtCodigo" name="txtCodigo" class="validate" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="input-field">
                        <label for="lblCliente">
                            <asp:Label runat="server" ID="lblCliente">Cliente</asp:Label></label>
                        <asp:TextBox ID="txtCliente" name="txtCliente" class="validate" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                    <label for="lblEstado">
                        <asp:Label runat="server" ID="lblEstado">Estado *</asp:Label></label>
                    <asp:DropDownList ID="ddlEstado" runat="server" CssClass="ddlCampos" />
                </div>
                <div class="col s6">
                    <div class="input-field">
                        <label for="lblImporte">
                            <asp:Label runat="server" ID="lblImporte">Total</asp:Label></label>
                        <asp:TextBox ID="txtImporte" name="txtImporte" class="datepicker" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="input-field">
                        <label for="lblProductos">
                            <asp:Label runat="server" ID="lblProductos">Productos</asp:Label></label>
                        <asp:TextBox ID="txtProductos" runat="server" class="materialize-textarea" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
                    </div>
                </div>
                <div class="col s6">
                    <asp:Label runat="server" CssClass="rojo" ID="lblError">Campos obligatorios*</asp:Label>
                </div>
            </div>
        </div>
        <div class="modal-footer">
            <asp:LinkButton ID="lnkAceptarPpAgregar" class="btn-flat waves-effect waves-green btn-flat" runat="server" OnClick="btnAceptarPpAgregar_Click"><i class="material-icons left green-text">check</i>Aceptar</asp:LinkButton>
            <asp:LinkButton ID="lnkCancelarPpAgregar" href="#!" class="btn-flat modal-close waves-effect waves-red btn-flat" runat="server" OnClick="btnCancelarPpAgregar_Click"><i class="material-icons left red-text">close</i>Cancelar</asp:LinkButton>
        </div>
    </div>

    <div class="card-panel">
        <div id="Grid">
            <asp:GridView ID="gvVentas" runat="server" AutoGenerateColumns="false" DataKeyNames="ClaveEntidad" EmptyDataText="Agregue registros"
                OnRowDataBound="gvVentas_RowDataBound" OnRowCommand="gvVentas_RowCommand" CssClass="filtrar bordered hoverable">
                <Columns>
                    <asp:BoundField DataField="ClaveEntidad" HeaderText="ID" Visible="false"></asp:BoundField>
                    <asp:BoundField DataField="Codigo" HeaderText="Codigo"></asp:BoundField>
                    <asp:BoundField DataField="Cliente" HeaderText="Cliente"></asp:BoundField>
                    <asp:BoundField DataField="Importe" HeaderText="Total"></asp:BoundField>
                    <asp:BoundField DataField="FechaAlta" HeaderText="Fecha de compra"></asp:BoundField>
                    <asp:BoundField DataField="ClaveEstado" HeaderText="Estado"></asp:BoundField>
                    <asp:ButtonField ButtonType="Image" ImageUrl="~/_Imagenes/Editar.png" HeaderText="Editar"
                        CommandName="Editar" Text="Editar" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
