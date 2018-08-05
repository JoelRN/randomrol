
var api_usuarios = {
    init: function () {

    },

    registraUsuario: function (usuario) {
        var eventoOk = function (respuesta) {
            servicio_alerta.Ok("Usuario " + usuario.Alias + " creado correctamente");
        };

        api.llamadaAjax('Usuarios', usuario, eventoOk, function () { });
    },

    autenticaUsuario: function (usuario) {
        if (jQuery("#recordarme input")[0].checked) {
            localStorage.setItem("login", usuario.Alias);
            localStorage.setItem("pwd", usuario.Password);
        } else {
            localStorage.clear();
        }

        var eventOk = function (respuesta) {
            localStorage.setItem("usuario", respuesta);
            servicio_alerta.Ok("Usuario logueado.");
        };

        var eventError = function () {
            localStorage.clear();
            servicio_alerta.error("Datos de acceso incorrectos.");
        };

        api.llamadaAjax('Usuarios/authenticate', usuario, eventOk, eventError);
    }
};