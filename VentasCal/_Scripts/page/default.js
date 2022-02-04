jQuery(function ($) {
    HideNavbar();
    ValidacionCampo();
    AgregarValidador();

    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(ValidacionCampo);
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(AgregarValidador);
});

function ValidacionCampo() {
    $("input[id$='tbxBuscar']").attr("maxlength", 30).bind("keypress", validarNumerosYLetrasConEspacio);
    $("input[id$='txtNombre']").attr("maxlength", 100).bind("keypress", validarNumerosYLetrasConEspacio);
}

function AgregarValidador() {
    $("input[id$='txtNombre']").addClass("required");
    $('[id$=lnkComprar]').on('click', function (event) {
        var isValid = ValidateAndSubmit(".validationGroup");
        if (!isValid)
            return false;
    });
    ValidateAndSubmit(".validationGroup");
}
