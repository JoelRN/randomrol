app.nuevoUsuario = {
    init: function () {
        this.form = $('form-nuevoUsuario');
        this.form.addEventListener('submit', function (event) {
            event.preventDefault();
            event.stopPropagation();

            if ($('form-nuevoUsuario').checkValidity() === false) {
                servicio_alerta.advertencia("Datos no validos.");
            } else {
                if ($('txtPassword').value != $('txtPassword2').value) {
                    servicio_alerta.advertencia("Las contraseñas no coinciden.");
                    $('txtPassword').value = "";
                    $('txtPassword2').value = "";
                } else {
                    app.nuevoUsuario.registraNuevoUsuario();
                }
            }
        });
    },

    registraNuevoUsuario: function () {

        var usuario = new Usuario(
            $('txtUsuario').value,
            $('txtEmail').value,
            $('txtPassword').value
        );

        api_usuarios.registraUsuario(usuario);
    }
};

app.nuevoUsuario.init();