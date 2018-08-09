componente_usuario = {
    init: async function () {
        console.log(servicio_datos.get(claves.USUARIO));

        if (servicio_datos.get(claves.USUARIO) == null) {
            this.cargaLogin();
        } else {
            this.cargaUsuario();
        }

        this.cargaControles();

        if (servicio_datos.get(claves.LOGIN) != undefined) {
            $("inputUsuario").value = servicio_datos.get(claves.LOGIN).Alias;
            $("inputPassword").value = servicio_datos.get(claves.LOGIN).Password;
        } else {
            servicio_datos.limpiar();
        }
    },

    cargaLogin: function () {
        util_jQuery.muestra("#contenedor-login");
        util_jQuery.oculta("#contenedor-usuario");

        util_jQuery.bloquea("botonDesplegable");
    },

    cargaUsuario: function () {
        util_jQuery.oculta("#contenedor-login");
        util_jQuery.muestra("#contenedor-usuario");

        util_jQuery.desbloquea("#botonDesplegable");
    },

    cargaControles: function () {
        this.formLogin = $('form-login');
        this.formLogin.addEventListener('submit', function (event) {
            event.preventDefault();
            event.stopPropagation();

            if ($('form-login').checkValidity() === false) {
                servicio_alerta.advertencia("Datos no validos.");
            } else {
                componente_usuario.autenticaUsuario();
            }
        });

        this.formUsuario = $('form-usuario');
        this.formUsuario.addEventListener('submit', function (event) {
            event.preventDefault();
            event.stopPropagation();

            servicio_datos.borrar(claves.USUARIO);
            app.navega("usuario.html");
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

document.addEventListener('deviceready', function (e) { componente_usuario.init(); }, false);