jQuery(function ($) {
    ValidacionCampo();
    AgregarValidador();

    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(ValidacionCampo);
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(AgregarValidador);
});

function ValidacionCampo() {
    $("input[id$='tbxBuscar']").attr("maxlength", 30).bind("keypress", validarNumerosYLetrasConEspacio);
    $("input[id$='txtCodigo']").attr("maxlength", 20).bind("keypress", validarAlfanumerico);
    $("input[id$='txtArticulo']").attr("maxlength", 80).bind("keypress", validarNumerosYLetrasConEspacio);
    $("input[id$='txtPrecioUni']").attr("maxlength", 250).bind("keypress", validarNumerico);
    $("input[id$='txtCantidad']").attr("maxlength", 250).bind("keypress", validarSoloNumero);
    $("input[id$='txtPrecioReal']").attr("maxlength", 250).bind("keypress", validarNumerico);
}

function AgregarValidador() {
    $("input[id$='txtCodigo']").addClass("required");
    $("input[id$='txtArticulo']").addClass("required");
    $("input[id$='txtPrecioUni']").addClass("required");
    $("input[id$='txtCantidad']").addClass("required");
    $("input[id$='txtPrecioReal']").addClass("required");

    $('[id$=lnkAceptarPpAgregar]').on('click', function (event) {
        var isValid = ValidateAndSubmit(".validationGroup");
        if (!isValid)
            return false;
    });
    ValidateAndSubmit(".validationGroup");
}
