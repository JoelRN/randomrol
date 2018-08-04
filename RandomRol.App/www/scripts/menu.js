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
            jQuery("#botonDesplegable").attr("hidden", "hidden");
            jQuery("#botonVolver").removeAttr("hidden");
        } else {
            jQuery("#botonDesplegable").removeAttr("hidden");
            jQuery("#botonVolver").attr("hidden", "hidden");
        }
    }
};

document.addEventListener('deviceready', function (e) { menu.init(); }, false);