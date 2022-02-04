<%@ Page Title="Productos" Language="C#" MasterPageFile="~/SI_00.master" AutoEventWireup="true" CodeBehind="pa02.aspx.cs" Inherits="Presentacion.PA02" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
	<asp:ScriptManagerProxy ID="smpSchedule" runat="server">
		<Scripts>
			<asp:ScriptReference Path="~/_Scripts/page/pa02.js" />
		</Scripts>
	</asp:ScriptManagerProxy>

	<div class="row">
		<div class="col s12">
			<div class="card-panel blue-grey darken-3">
				<h5 class="white-text">Productos</h5>
			</div>
		</div>
	</div>
	<div class="row">
		<div class="col s12">
			<div class="card-panel">
				<div class="row">
					<div class="input-field col s6 ">
						<i class="material-icons prefix">search</i>
						<asp:TextBox runat="server" class="tbxBuscar validate" name="tbxBuscar" id="tbxBuscar"></asp:TextBox>
						<label for="tbxBuscar"><asp:Label runat="server" for="Buscar">Buscar</asp:Label></label>
					</div>
					<div class="input-field col s6 right-align">
						<asp:LinkButton ID="lnkAgregar" class="btn-floating  btn-large waves-effect waves-light grey darken-3" runat="server"   OnClick="btnAgregarReg_Click" ><i class="material-icons md-48">add</i></asp:LinkButton>
					</div>
				</div>
			</div>
		</div>
	</div>

	<div id="modal1" class="modal modal-fixed-footer">
		<div class="modal-content validationGroup">
			<div class="row">
				<h4 class="center-align">Producto</h4>
				<div class="col s6 ">
					<%--<div class="input-field">
						<label for="lblCodigo"><asp:Label runat="server" ID="lblCodigo">Codigo*</asp:Label></label>
						<asp:TextBox ID="txtCodigo" class="validate" name="txtCodigo" runat="server"></asp:TextBox>
					</div>--%>
					<div class="input-field">
						<label for="lblArticulo"><asp:Label runat="server" ID="lblArticulo">Producto *</asp:Label></label>
						<asp:TextBox ID="txtArticulo" class="validate" name="txtArticulo" runat="server"></asp:TextBox>
					</div>
					<div class="input-field">
						<label for="lblPrecioUni"><asp:Label runat="server" ID="lblPrecioUni">Precio unitario *</asp:Label></label>
						<asp:TextBox ID="txtPrecioUni" class="validate" name="txtPrecioUni" runat="server"></asp:TextBox>
					</div>
					<asp:Label runat="server" CssClass="rojo" ID="lblError">Campos obligatorios*</asp:Label>
				</div>
				<div class="col s6 ">
					<div class="input-field">
						<label for="lblCantidad"><asp:Label runat="server" ID="lblCantidad">Cantidad *</asp:Label></label>
						<asp:TextBox ID="txtCantidad" class="validate" name="txtCantidad" runat="server"></asp:TextBox>
					</div>
					<div class="input-field">
						<label for="lblPrecioReal"><asp:Label runat="server" ID="lblPrecioReal">Precio real *</asp:Label></label>
						<asp:TextBox ID="txtPrecioReal" class="validate" name="txtPrecioReal" runat="server"></asp:TextBox>
					</div>

					<label for="lblStatus"><asp:Label runat="server" ID="lblStatus">Estatus *</asp:Label></label>
					<div class="switch">
						<label>
							Inactivo
							<asp:CheckBox ID="chkEstatus" runat="server"/>
							<span class="lever"></span>
							Activo
						</label>
					</div>
				</div>
			</div>
		</div>
		<div class="modal-footer">
			<asp:LinkButton ID="lnkAceptarPpAgregar" class="btn-flat waves-effect waves-green btn-flat" runat="server" OnClick="btnAceptarPpAgregar_Click" ><i class="material-icons left green-text">check</i>Aceptar</asp:LinkButton>
			<asp:LinkButton ID="lnkCancelarPpAgregar" href="#!" class="btn-flat modal-close waves-effect waves-red btn-flat" runat="server" OnClick="btnCancelarPpAgregar_Click"><i class="material-icons left red-text">close</i>Cancelar</asp:LinkButton>
		</div>
	</div>
	<asp:HiddenField ID="agregaModifica" runat="server" />

	<div class="card-panel">
	<div id="Grid">
		<asp:GridView ID="gvArticulos" runat="server" AutoGenerateColumns="false" DataKeyNames="ClaveEntidad" EmptyDataText="Agregue registros"
			OnRowDataBound="gvArticulos_RowDataBound" OnRowCommand="gvArticulos_RowCommand" CssClass="filtrar bordered hoverable" >
			<Columns>
				<asp:BoundField DataField="ClaveEntidad" HeaderText="ID" Visible="false"></asp:BoundField>
				<asp:BoundField DataField="Descripcion" HeaderText="Descripcion"></asp:BoundField>
				<asp:BoundField DataField="PrecioUnitario" HeaderText="Precio unitario"></asp:BoundField>
				<asp:BoundField DataField="Cantidad" HeaderText="Cantidad"></asp:BoundField>
				<asp:BoundField DataField="ClaveEstado" HeaderText="Estado" Visible="true"></asp:BoundField>
				<asp:ButtonField ButtonType="Image" ImageUrl="~/_Imagenes/Editar.png" HeaderText="Editar"
					CommandName="Editar" Text="Editar" />
			</Columns>
		</asp:GridView>
	</div>
	</div>
</asp:Content>
