
var servicio_datos = {
    guarda: function (clave, datos) {
        if (typeof datos === "string") {
            localStorage.setItem(clave, datos);
        } else {
            localStorage.setItem(clave, JSON.stringify(datos));
        }
    },
    get: function (clave) {
        var datos = localStorage.getItem(clave);
        datos = JSON.parse(datos);
        return datos;
    },
    borrar: function (clave) {
        localStorage.removeItem(clave);
    },
    limpiar: function () {
        localStorage.clear();
    }
};

const claves = {
    LOGIN: "login",
    USUARIO: "usuario"
};