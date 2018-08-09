menu = {
    init: function () {

        jQuery('#botonVolver').bind("click", function () {
            app.navega("vuelve");
        });

        jQuery('body').bind("DOMSubtreeModified", function () {
            menu.modificarMenu();
        });
    },

    modificarMenu: function () {
        var muestraVolver = $("muestraVolver");

        if (muestraVolver != null) {
            util_jQuery.oculta("#botonDesplegable");
            util_jQuery.muestra("#botonVolver");
        } else {
            util_jQuery.muestra("#botonDesplegable");
            util_jQuery.oculta("#botonVolver");
        }
    }
};

document.addEventListener('deviceready', function (e) { menu.init(); }, false);