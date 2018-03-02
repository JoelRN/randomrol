app.servicioInicioSesion = {
    init: function () {

    },

    autenticaUsuario: function (n, p) {
        url = 'http://localhost:4500/api/sesionusuario';
        data = { "nombre": n, "pwd": p };
        
        jQuery.ajax({
            type: "POST",
            url: url,
            data: JSON.stringify(data),
            contentType: "application/json",
            dataType: "json",
            success: this.onSucces()
        });
    },

    onSucces: function () {
        alert('onSucces:');
    },

    onError: function () {
        alert('onError');
    }
};