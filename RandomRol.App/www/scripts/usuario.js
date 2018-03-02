app.usuario = {
    init: function () {
        this.button = $('btnLoguea');
        this.button.addEventListener('click', this.autenticaUsuario.bind(this));
    },

    autenticaUsuario: function () {
        nombre = 'admin';
        pwd = 'admin';
        app.servicioInicioSesion.autenticaUsuario(nombre, pwd);
    }
};

app.usuario.init();