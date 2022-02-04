<%@ Page Title="Usuarios" Language="C#" MasterPageFile="~/SI_00.master" AutoEventWireup="true" CodeBehind="pa06.aspx.cs" Inherits="Presentacion.PA06" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManagerProxy ID="smpSchedule" runat="server">
        <Scripts>
            <asp:ScriptReference Path="~/_Scripts/page/pa06.js" />
        </Scripts>
    </asp:ScriptManagerProxy>
    <div class="row">
        <div class="col s12">
            <div class="card-panel blue-grey darken-3">
                <h5 class="white-text">Usuarios</h5>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col s12">
            <div class="card-panel">
                <div class="row">
                    <div class="input-field col s6 ">
                        <i class="material-icons prefix">search</i>
                        <asp:TextBox runat="server" class="tbxBuscar validate" name="tbxBuscar" ID="tbxBuscar"></asp:TextBox>
                        <label for="tbxBuscar">
                            <asp:Label runat="server" for="Buscar">Buscar</asp:Label></label>
                    </div>
                    <div class="input-field col s6 right-align">
                        <asp:LinkButton ID="lnkAgregar" class="btn-floating  btn-large waves-effect waves-light grey darken-3" runat="server" OnClick="btnAgregarReg_Click"><i class="material-icons md-48">add</i></asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="modal1" class="modal modal-fixed-footer">
        <div class="modal-content">
            <div class="row">
                <h4 class="center-align">Usuario</h4>
                <div class="col s6 validationGroup">
                    <div class="input-field">
                        <label for="lblNombre">
                            <asp:Label runat="server" ID="lblNombre">Nombre *</asp:Label></label>
                        <asp:TextBox ID="txtNombre" class="validate" name="txtNombre" runat="server"></asp:TextBox>
                    </div>
                    <div class="input-field">
                        <label for="lblApellidoP">
                            <asp:Label runat="server" ID="lblApellidoP">Apellido paterno *</asp:Label></label>
                        <asp:TextBox ID="txtApellidoP" class="validate" name="txtApellidoP" runat="server"></asp:TextBox>
                    </div>
                    <div class="input-field">
                        <label for="lblApellidoM">
                            <asp:Label runat="server" ID="lblApellidoM">Apellido materno</asp:Label></label>
                        <asp:TextBox ID="txtApellidoM" class="validate" name="txtApellidoM" runat="server"></asp:TextBox>
                    </div>
                    <div class="input-field">
                        <label for="lblTelefono">
                            <asp:Label runat="server" ID="lblTelefono">Teléfono</asp:Label></label>
                        <asp:TextBox ID="txtTelefono" class="validate" name="txtTelefono" runat="server"></asp:TextBox>
                    </div>
                    <asp:Label runat="server" CssClass="rojo" ID="lblError">Campos obligatorios*</asp:Label>
                </div>
                <div class="col s6 validationGroup">
                    <div class="input-field">
                        <label for="lblEmail">
                            <asp:Label runat="server" ID="lblEmail">Correo electrónico *</asp:Label></label>
                        <asp:TextBox ID="txtEmail" class="validate" name="txtMarca" runat="server"></asp:TextBox>
                    </div>
                    <div class="input-field">
                        <label for="lblPassword">
                            <asp:Label runat="server" ID="lblPassword">Contraseña *</asp:Label></label>
                        <asp:TextBox ID="txtPassword" type="password" class="validate" name="txtPassword" runat="server"></asp:TextBox>
                    </div>

                    <label for="lblPermisos">
                        <asp:Label runat="server" ID="lblPermisos">Permisos *</asp:Label></label>
                    <asp:DropDownList ID="ddlPermisos" runat="server" CssClass="ddlPermisos">
                        <asp:ListItem Value="0">Seleccione</asp:ListItem>
                    </asp:DropDownList>

                    <label for="lblStatus">
                        <asp:Label runat="server" ID="lblStatus">Estatus*</asp:Label></label>
                    <div class="switch">
                        <label>
                            Inactivo
                            <asp:CheckBox ID="chkEstatus" runat="server" />
                            <span class="lever"></span>
                            Activo
                        </label>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal-footer">
            <asp:LinkButton ID="lnkAceptarPpAgregar" class="btn-flat waves-effect waves-green btn-flat" runat="server" OnClick="btnAceptarPpAgregar_Click"><i class="material-icons left green-text">check</i>Aceptar</asp:LinkButton>
            <asp:LinkButton ID="lnkCancelarPpAgregar" href="#!" class="btn-flat modal-close waves-effect waves-red btn-flat" runat="server" OnClick="btnCancelarPpAgregar_Click"><i class="material-icons left red-text">close</i>Cancelar</asp:LinkButton>
        </div>
    </div>
    <asp:HiddenField ID="agregaModifica" runat="server" />

    <div class="card-panel">
        <div id="Grid">
            <asp:GridView ID="gvUsuarios" runat="server" AutoGenerateColumns="false" DataKeyNames="ClaveEntidad" EmptyDataText="Agregue registros"
                OnRowDataBound="gvUsuarios_RowDataBound" OnRowCommand="gvUsuarios_RowCommand" CssClass="filtrar bordered hoverable">
                <Columns>
                    <asp:BoundField DataField="ClaveEntidad" HeaderText="ID" Visible="false"></asp:BoundField>
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre"></asp:BoundField>
                    <asp:BoundField DataField="Email" HeaderText="Email"></asp:BoundField>
                    <asp:BoundField DataField="Telefono" HeaderText="Teléfono"></asp:BoundField>
                    <asp:BoundField DataField="Permisos" HeaderText="Permisos"></asp:BoundField>
                    <asp:BoundField DataField="ClaveEstado" HeaderText="Estado"></asp:BoundField>
                    <asp:ButtonField ButtonType="Image" ImageUrl="~/_Imagenes/Editar.png" HeaderText="Editar"
                        CommandName="Editar" Text="Editar" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
