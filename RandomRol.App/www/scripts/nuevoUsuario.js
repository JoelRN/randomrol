app.nuevoUsuario = {
    init: function () {
        this.button = $('btnNuevoUsuario');
        this.button.addEventListener('click', this.registraNuevoUsuario.bind(this));
    },

    registraNuevoUsuario: async function () {
        usuario = {
            "Alias": $('txtUsuario').value,
            "Email": $('txtEmail').value,
            "Password": $('txtPassword').value
        };
        
        await app.servicioInicioSesion.registraUsuario(usuario);
    }
};

app.nuevoUsuario.init();