<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/SI_00.master" AutoEventWireup="true" CodeBehind="pa01.aspx.cs" Inherits="Presentacion.PA01" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <script type="text/javascript">
        $(document).ready(function () {
            $('select').material_select();
            $('.datepicker').pickadate();
            $('.dropdown-button').dropdown();
            $('.button-collapse').sideNav();
            $('.modal-trigger').leanModal();
        });
    </script>
    <div class="row inicioWeb">
        <div class="col s6">
            <div class="card-panel" style="min-width:280px;">
                <h1>Tienda</h1>
                <span class="significado">Version 1.0</span>
            </div>
            <div class="card-panel" style="min-width:280px;">
                <p>Sistema basico para la administracion de ventas.</p>
                <p><i class="material-icons left">label</i>Productos</p>
                <p><i class="material-icons left">label</i>Ventas</p>
            </div>
        </div>
        <div class="col s6 center-align">
        </div>
    </div>
</asp:Content>
