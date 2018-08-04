

var servicio_alerta = {
    init: function () {

    },

    Ok: function (mensaje) {
        $('alertas').innerHTML += '<div class="alert alert-success"><span>' + mensaje + '</span></div>';
        this.quitarAlertas();
    },

    advertencia: function (mensaje) {
        $('alertas').innerHTML += '<div class="alert alert-warning"><span>' + mensaje + '</span></div>';
        this.quitarAlertas();
    },

    error: function (mensaje) {
        $('alertas').innerHTML += '<div class="alert alert-danger"><span>' + mensaje + '</span></div>';
        this.quitarAlertas();
    },

    info: function (mensaje) {
        $('alertas').innerHTML += '<div class="alert alert-info"><span>' + mensaje + '</span></div>';
        this.quitarAlertas();
    },

    quitarAlertas: function () {
        jQuery(".alert").fadeTo(2000, 500).slideUp(500, function () {
            jQuery(".alert").slideUp(500);
            jQuery(".alert").remove();
        });        
    }
};