componente_usuario = {
    init: function () {

        this.form = $('form-usuario');
        this.form.addEventListener('submit', function (event) {
            event.preventDefault();
            event.stopPropagation();

            if ($('form-usuario').checkValidity() === false) {
                servicio_alerta.advertencia("Datos no validos.");
            } else {
                componente_usuario.autenticaUsuario();
            }

        });

        if (servicio_datos.get(claves.LOGIN) != undefined) {
            $("inputUsuario").value = servicio_datos.get(claves.LOGIN).Alias;
            $("inputPassword").value = servicio_datos.get(claves.LOGIN).Password;
        } else {
            servicio_datos.limpiar();
        }
    },

    autenticaUsuario: function () {
        usuario = new Usuario(
            $("inputUsuario").value,
            "",
            $("inputPassword").value);
        api_usuarios.autenticaUsuario(usuario);
    }
};

document.addEventListener('deviceready', function (e) { componente_usuario.init(); }, false);