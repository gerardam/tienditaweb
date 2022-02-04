<%@ Page Title="Tienda" Language="C#" MasterPageFile="~/SI_00.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Presentacion.Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
	<asp:ScriptManagerProxy ID="smpSchedule" runat="server">
		<Scripts>
			<asp:ScriptReference Path="~/_Scripts/page/default.js" />
		</Scripts>
	</asp:ScriptManagerProxy>

	<div class="row">
		<div class="col s12">
			<div class="card-panel blue-grey darken-3">
				<div class="row">
					<div class="col s6">
						<h5 class="white-text">Tienda</h5>
					</div>
					<div class="col s6 right-align white-text">
						<i class="material-icons md-36">store</i>
					</div>
				</div>
			</div>
		</div>
	</div>
	<div class="row">
		<div class="col s6">
			<div class="card-panel">
				<div class="row">
					<div class="col s12">
						<asp:GridView ID="gvCompra" runat="server" AutoGenerateColumns="false" CssClass="bordered" DataKeyNames="ClaveEntidad"
							EmptyDataText="Agregue productos" OnRowDataBound="gvCompra_RowDataBound" OnRowCommand="gvCompra_RowCommand">
							<Columns>
								<asp:ButtonField ButtonType="Image" ImageUrl="~/_Imagenes/cancel.png" HeaderText="Eliminar"
									CommandName="Editar" Text="Eliminar" />
								<asp:BoundField DataField="ClaveEntidad" HeaderText="ID" Visible="false"></asp:BoundField>
								<asp:BoundField DataField="Descripcion" HeaderText="Producto"></asp:BoundField>
								<asp:BoundField DataField="PrecioUnitario" HeaderText="Precio"></asp:BoundField>
							</Columns>
						</asp:GridView>
					</div>
					<div class="input-field col s12 validationGroup">
						<div class="input-field">
							<label for="lblTotal">
								<asp:Label runat="server" ID="lblTotal">Total</asp:Label></label>
							<asp:TextBox ID="txtTotal" class="validate" name="txtTotal" runat="server" ReadOnly="true"></asp:TextBox>
						</div>
						<div class="input-field">
							<label for="lblNombre">
								<asp:Label runat="server" ID="lblNombre">Nombre *</asp:Label></label>
							<asp:TextBox ID="txtNombre" class="validate" name="txtNombre" runat="server"></asp:TextBox>
						</div>
					</div>
					<div class="input-field col s12 right-align">
						<asp:LinkButton ID="lnkComprar" class="btn-floating  btn-large waves-effect waves-light light-green accent-4" runat="server" OnClick="btnComprar_Click" ToolTip="Comprar"><i class="material-icons md-36">local_grocery_store</i></asp:LinkButton>
					</div>
				</div>
			</div>
		</div>
		<div class="col s6">
			<div class="card-panel">
				<div class="input-field">
					<i class="material-icons prefix">search</i>
					<asp:TextBox runat="server" class="tbxBuscar validate" name="tbxBuscar" ID="tbxBuscar"></asp:TextBox>
					<label for="tbxBuscar">
						<asp:Label runat="server" for="Buscar">Buscar</asp:Label></label>
				</div>
				<div id="Grid">
					<asp:GridView ID="gvCatalogo" runat="server" AutoGenerateColumns="false" CssClass="filtrar bordered" DataKeyNames="ClaveEntidad"
						EmptyDataText="Agregue registros" OnRowDataBound="gvCatalogo_RowDataBound" OnRowCommand="gvCatalogo_RowCommand">
						<Columns>
							<asp:ButtonField ButtonType="Image" ImageUrl="~/_Imagenes/add.png" HeaderText="Agregar"
								CommandName="Editar" Text="Agregar" />
							<asp:BoundField DataField="ClaveEntidad" HeaderText="ID" Visible="false"></asp:BoundField>
							<asp:BoundField DataField="Descripcion" HeaderText="Producto"></asp:BoundField>
							<asp:BoundField DataField="PrecioUnitario" HeaderText="Precio"></asp:BoundField>
							<asp:BoundField DataField="Cantidad" HeaderText="Unid."></asp:BoundField>
						</Columns>
					</asp:GridView>
				</div>
			</div>
		</div>
	</div>

	<div id="modal1" class="modal modal-fixed-footer" style="max-width: 450px; max-height: 400px; min-width:300px;min-height:300px;">
		<div class="modal-content">
			<div class="row">
				<h4 class="center-align">Orden de compra</h4>
				<div class="col s12 center-align">
					<span>
						La orden de compra se ha enviado, en breve será atendido.
					</span>
					<br />
					<span>
						Gracias por su compra.
					</span>
					<br />
					<i class="material-icons md-48 text-accent-4 yellow-text">sentiment_satisfied_alt</i>
				</div>
			</div>
		</div>
		<div class="modal-footer">
			<a href="#!" class="btn-flat modal-close waves-effect waves-red btn-flat"><i class="material-icons left red-text">close</i>Cerrar</a>
		</div>
	</div>

	<div id="modal2" class="modal modal-fixed-footer" style="max-width: 450px; max-height: 400px; min-width:300px;min-height:300px;">
		<div class="modal-content">
			<div class="row">
				<h4 class="center-align">Información</h4>
				<div class="col s12">
					<p>Los siguientes productos ya no están disponibles:</p>
					<asp:TextBox ID="txtProductos" runat="server" class="materialize-textarea" TextMode="MultiLine" Rows="3" ReadOnly="true"></asp:TextBox>
					<p>Por favor seleccione otros productos.</p>
				</div>
			</div>
		</div>
		<div class="modal-footer">
			<a href="#!" class="btn-flat modal-close waves-effect waves-red btn-flat"><i class="material-icons left red-text">close</i>Cerrar</a>
		</div>
	</div>

</asp:Content>
