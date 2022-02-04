<%@ Page Title="Perfil" Language="C#" MasterPageFile="~/SI_00.master" AutoEventWireup="true" CodeBehind="pa05.aspx.cs" Inherits="Presentacion.PA05" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <script type="text/javascript">
        $(document).ready(function () {
            $('select').material_select();
            $('.dropdown-button').dropdown();
            $('.button-collapse').sideNav();
            $('.modal-trigger').leanModal();
            $('.datepicker').pickadate({
                selectMonths: true,
                selectYears: 15
            });
        });
    </script>
    <asp:ScriptManagerProxy ID="smpSchedule" runat="server">
        <Scripts>
            <asp:ScriptReference Path="~/_Scripts/page/pa05.js" />
        </Scripts>
    </asp:ScriptManagerProxy>
    <div class="row">
        <div class="col s12">
            <div class="card-panel blue-grey darken-3">
                <h5 class="white-text">Perfil de usuario</h5>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col s12">
            <div class="card-panel">
                <div class="row">
                    <div class="col s6 validationGroup">
                        <div class="input-field">
                            <i class="material-icons prefix">perm_identity</i>
                            <asp:TextBox runat="server" class="txtNombre validate" name="txtNombre" ID="txtNombre"></asp:TextBox>
                            <label for="txtNombre">
                                <asp:Label runat="server" for="Nombre">Nombre *</asp:Label></label>
                        </div>
                        <div class="input-field">
                            <i class="material-icons prefix">perm_identity</i>
                            <asp:TextBox runat="server" class="txtApellidoP validate" name="txtApellidoP" ID="txtApellidoP"></asp:TextBox>
                            <label for="txtApellidoP">
                                <asp:Label runat="server" for="ApellidoPaterno">Apellido paterno *</asp:Label></label>
                        </div>
                        <div class="input-field">
                            <i class="material-icons prefix">perm_identity</i>
                            <asp:TextBox runat="server" class="txtApellidoM validate" name="txtApellidoM" ID="txtApellidoM"></asp:TextBox>
                            <label for="txtApellidoM">
                                <asp:Label runat="server" for="ApellidoMaterno">Apellido materno</asp:Label></label>
                        </div>
                        <asp:Label runat="server" CssClass="rojo" ID="lblError">Campos obligatorios*</asp:Label>
                    </div>
                    <div class="col s6 validationGroup">
                        <div class="input-field">
                            <i class="material-icons prefix">email</i>
                            <asp:TextBox runat="server" class="txtEmail validate" name="txtEmail" ID="txtEmail" Enabled="false"></asp:TextBox>
                            <label for="txtEmail">
                                <asp:Label runat="server" for="Email">E-mail *</asp:Label></label>
                        </div>
                        <div class="input-field">
                            <i class="material-icons prefix">phone</i>
                            <asp:TextBox runat="server" class="txtTelefono validate" name="txtTelefono" ID="txtTelefono"></asp:TextBox>
                            <label for="txtTelefono">
                                <asp:Label runat="server" for="Telefono">Teléfono</asp:Label></label>
                        </div>
                        <div class="input-field">
                            <i class="material-icons prefix">lock_outline</i>
                            <asp:TextBox type="password" runat="server" class="txtContrasena validate" name="password" ID="password"></asp:TextBox>
                            <label for="password">
                                <asp:Label runat="server" for="password">Contraseña *</asp:Label></label>
                        </div>

                        <div class="right-align">
                            <asp:LinkButton ID="lnkAceptar" class="btn btn-large waves-effect waves-light green" runat="server" OnClick="btnAceptar_Click"><i class="material-icons md-36 left">save</i>Guardar</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
