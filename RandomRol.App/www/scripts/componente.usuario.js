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
    },

    autenticaUsuario: function () {
        usuario = new Usuario(
            $("inputUsuario").value,
            "",
            $("inputPassword").value);
        api_usuarios.autenticaUsuario(usuario);
    }
};

componente_usuario.init();