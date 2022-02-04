jQuery(function ($) {
    ValidacionCampo();
    AgregarValidador();

    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(ValidacionCampo);
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(AgregarValidador);

    $.datepicker.regional['es'] = {
        closeText: 'Cerrar',
        prevText: '<Ant',
        nextText: 'Sig>',
        currentText: 'Hoy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio',
            'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun',
            'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
        dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
        dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Juv', 'Vie', 'Sáb'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
        weekHeader: 'Sm',
        dateFormat: 'dd/mm/yy',
        firstDay: 1,
        isRTL: false,
        changeMonth: true,
        yearRange: '-100:+0',
        showMonthAfterYear: false,
        yearSuffix: ''
    };
    $.datepicker.setDefaults($.datepicker.regional['es']);

});

function ValidacionCampo() {
    $("input[id$='txtFechaC']").datepicker();
    $("input[id$='txtFechaC']").keypress(function () { return false; });
    $("input[id$='tbxBuscar']").attr("maxlength", 30).bind("keypress", validarNumerosYLetrasConEspacio);
}

function AgregarValidador() {
    $("input[id$='txtFechaC']").addClass("required");
    $("select[id$='ddlCodigo']").addClass("required");
    $("select[id$='ddlProveedor']").addClass("required");
    $("select[id$='ddlArea']").addClass("required");
    $("select[id$='ddlResponsable']").addClass("required");

    $('[id$=lnkAceptarPpAgregar]').on('click', function (event) {
        var isValid = ValidateAndSubmit(".validationGroup");
        if (!isValid)
            return false;
    });
    ValidateAndSubmit(".validationGroup");
}
