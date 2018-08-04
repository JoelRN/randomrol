
var api_usuarios = {
    init: function () {

    },

    registraUsuario: function (usuario) {
        var eventoOk = function () {
            servicio_alerta.Ok("Usuario " + usuario.Alias + " creado correctamente");
        };

        api.llamadaAjax('Usuarios', usuario, eventoOk, function () { });
    },

    autenticaUsuario: function (n, p) {
        var datos = JSON.stringify({ "nombre": n, "pwd": p });
        var eventOk = null;
        var eventoError = null;

        api.llamadaAjax('Usuarios/authenticate', datos, eventOk, eventoError);
    }
};