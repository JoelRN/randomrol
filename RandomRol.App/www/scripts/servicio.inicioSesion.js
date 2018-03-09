urlServ = 'http://localhost:4500/Users';

app.servicioInicioSesion = {
    init: function () {

    },

    registraUsuario: async function (usuario) {
        url = urlServ + '';
        data = usuario;

        jQuery.ajax({
            type: "POST",
            url: url,
            data: JSON.stringify(data),
            contentType: "application/json",
            dataType: "json",
            success: this.onSucces()
        });
    },

    autenticaUsuario: function (n, p) {
        url = urlServ + '/authenticate';
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

    },

    onError: function () {
        alert('onError');
    }
};