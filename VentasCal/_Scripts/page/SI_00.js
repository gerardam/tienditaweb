$(document).ready(function () {
    $('select').material_select();
    $('.dropdown-button').dropdown();
    $('.button-collapse').sideNav();
    $('.modal-trigger').leanModal();
    $('.datepicker').pickadate({
        selectMonths: true, // Creates a dropdown to control month
        selectYears: 15 // Creates a dropdown of 15 years to control year
    });
});

jQuery(function ($) {
    asignar();
    agregarValidadorLogin();
    ValidateAndSubmit();
    aplicarFiltro();

    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(asignar);
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(agregarValidadorLogin);
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(ValidateAndSubmit);
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(aplicarFiltro);
});

function asignar() {
    $(".itext").bind("keypress", BloqueoIntro);
}

function agregarValidadorLogin() {
    $("input[id$='txtEmail']").addClass("required");
    $("input[id$='txtPassword']").addClass("required");

    $('[id$=lnkAceptarInicio]').on('click', function (event) {
        var isValid = ValidateAndSubmit(".validationGroupLogin");
        if (!isValid)
            return false;
    });
    ValidateAndSubmit(".validationGroupLogin");
}

function ValidateAndSubmit(validationGroup) {
    var $group = $(validationGroup);
    var isValid = true;

    $group.find(".required").each(function (i, item) {
        var hijo = $(this).parents("div.validate:first");

        if ((item.type == "text" || item.type == "password" || item.type == "radio" || item.type == "checkbox") && !$(item).valid()) {
            hijo.addClass("invalid");
            isValid = false;
        }
        else {
            if ($(item).val() == "") {
                hijo.addClass("invalid");
                isValid = false;
            }
        }

        if (hijo.hasClass("invalid"))
            isValid = false;
    });

    return isValid;
}

function aplicarFiltro() {
    $(".RowStyle, .AltRowStyle").filter(function () {
        return $('td', this).length && !$('table', this).length;
    }).hover(
        function () {
            classFila = $(this).attr('class');
            $(this).removeClass(classFila).addClass("highlight");

        },
        function () {
            $(this).removeClass("highlight").addClass("classFila");
        });
    $(".filtrar tr[id!='excluir']:has(td)").each(function () {
        var t = $(this).text().toLowerCase();
        $("<td class='indexColumn'></td>")
            .hide().text(t).appendTo(this);
    });
    //Agregar el comportamiento al texto (se selecciona por el ID)
    $(".tbxBuscar").keyup(function () {
        var s = $(this).val().toLowerCase().split(" ");
        $(".filtrar tr[id!='excluir']:hidden").show();
        $(".filtrar tr[id='excluir']").hide();
        $.each(s, function () {
            $(".filtrar tr:visible .indexColumn:not(:contains('"
                + this + "'))").parent().hide();
        });
        $(".filtrar tr th:first-child").show();
    });
}

function HideNavbar() {
    $("#divNavbar").remove();
}