jQuery(function ($) {
    ValidacionCampo();
    AgregarValidador();

    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(ValidacionCampo);
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(AgregarValidador);
});

function ValidacionCampo() {

}

function AgregarValidador() {

}
