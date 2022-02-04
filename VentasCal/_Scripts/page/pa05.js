jQuery(function ($) {
    ValidacionCampo();
    AgregarValidador();

    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(ValidacionCampo);
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(AgregarValidador);
});

function ValidacionCampo() {
    $("input[id$='txtNombre']").attr("maxlength", 20).bind("keypress", validarLetrasYEspacio);
    $("input[id$='txtApellidoP']").attr("maxlength", 20).bind("keypress", validarLetrasYEspacio);
    $("input[id$='txtApellidoM']").attr("maxlength", 20).bind("keypress", validarLetrasYEspacio);
    $("input[id$='txtEmail']").attr("maxlength", 50).bind("keypress", validarMail);
    $("input[id$='txtTelefono']").attr("maxlength", 14).bind("keypress", validarTelefono);
    $("input[id$='password']").attr("maxlength", 30).bind("keypress", validarPwd);
}

function AgregarValidador() {
    $("input[id$='txtNombre']").addClass("required");
    $("input[id$='txtApellidoP']").addClass("required");
    $("input[id$='txtEmail']").addClass("required");
    $("input[id$='password']").addClass("required");

    $('[id$=lnkAceptar]').on('click', function (event) {
        var isValid = ValidateAndSubmit(".validationGroup");
        if (!isValid)
            return false;
    });
    ValidateAndSubmit(".validationGroup");
}