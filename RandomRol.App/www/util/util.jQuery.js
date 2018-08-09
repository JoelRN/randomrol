
var util_jQuery = {
    oculta: function (id) {
        jQuery(id).attr("hidden", "hidden");
    },
    muestra: function (id) {
        jQuery(id).removeAttr("hidden");
    },
    bloquea: function (id) {
        jQuery(id).attr("disabled", "disabled");
    },
    desbloquea: function (id) {
        jQuery(id).removeAttr("disabled");
    }
};