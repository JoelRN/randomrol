
var api_usuarios = {
    init: function () {

    },

    registraUsuario: function (usuario) {
        servicio_datos.guarda(claves.LOGIN, usuario);

        var eventoOk = function (respuesta) {
            servicio_alerta.Ok("Usuario " + usuario.Alias + " creado correctamente");            
            app.navega("usuario.html");
        };

        var eventoError = function () {
            servicio_datos.limpiar();
        }

        api.llamadaAjax('Usuarios', usuario, eventoOk, eventoError);
    },

    autenticaUsuario: function (usuario) {
        if (jQuery("#recordarme input")[0].checked) {
            servicio_datos.guarda(claves.LOGIN, usuario);
        } else {
            servicio_datos.limpiar();
        }

        var eventOk = async function (respuesta) {
            await servicio_datos.guarda(claves.USUARIO, respuesta);            
            await servicio_alerta.Ok("Usuario logueado.");
            await componente_usuario.init();
        };

        var eventError = async function () {
            await servicio_datos.limpiar();
            await servicio_alerta.error("Datos de acceso incorrectos.");            
        };

        api.llamadaAjax('Usuarios/authenticate', usuario, eventOk, eventError);
    }
};