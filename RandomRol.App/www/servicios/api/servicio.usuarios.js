
var api_usuarios = {
    init: function () {

    },

    registraUsuario: function (usuario) {
        var eventoOk = function () {
            servicio_alerta.Ok("Usuario " + usuario.Alias + " creado correctamente");
        };

        api.llamadaAjax('Usuarios', usuario, eventoOk, function () { });
    },

    autenticaUsuario: function (usuario) {
        var eventOk = function () {
            servicio_alerta.Ok("Usuario logueado.");
        };

        var eventError = function () {
            servicio_alerta.error("Datos de acceso incorrectos.");
        };

        api.llamadaAjax('Usuarios/authenticate', usuario, eventOk, eventError);
    }
};