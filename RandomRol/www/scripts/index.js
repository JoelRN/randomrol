var app = {

    // inicializa la aplicacion cuando el dispositivo está preparado (ondeviceready)
    init: function () {
        this.historial = [];
        this.titulo = $('titulo');      // reference to the title bar
        this.volver = $('volver');      // reference to the back button

        this.volver.addEventListener('click', function (e) {
            this.navega('vuelve', 'left');
        }.bind(this));

        this.navega('menu.html');
    },

    // Enrutador de navegacion
    navega: function (url, direccion) {

        // add this view to the history stack
        url = this.historiza(url);

        // if the history stack is empty, hide the back button
        this.volver.style.opacity = (this.historial.length > 1) ? 1 : 0;

        // load new views with an XHR request
        var xhr = new XMLHttpRequest();
        xhr.open('GET', url, true);
        xhr.onreadystatechange = function (e) {
            var response = e.currentTarget;
            if (response.readyState !== 4) return;
            if (response.status !== 200 && response.status !== 0) {
                console.error('error al cargar la url: ', response.status, response.responseText);
            }

            // crear vista y publicarla
            var el = document.createElement('div');
            el.className = 'aplicacion';
            el.innerHTML = response.responseText;

            this.hidrata(el);                   // crear enlaces de enrutador y cargar scripts
            this.entra(el, direccion);          // anima la entrada en la nueva vista
            this.sale(this.page, direccion);    // anima la salida de la anterior vista
            this.page = el;                     // create a reference to the active view

        }.bind(this);
        xhr.send();
    },

    // anima una nueva vista en la pantalla
    entra: function (el, direccion) {
        if (!el) return; // paranoia

        // cargar la nueva vista fuera de la pantalla
        var x = document.body.offsetWidth;
        if (direccion == 'left') x = -x;
        el.style.transform = 'translateX(' + x + 'px)';
        document.body.appendChild(el);

        // después de un breve retraso para permitir pintar el DOM, anima la vista en la pantalla
        window.setTimeout(function () {
            el.style.transform = 'translateX(0)';
        }, 50);

    },

    // crear enlaces de enrutador y cargar scripts
    hidrata: function (el) {
        var hrefs = el.querySelectorAll('*[data-href]');
        for (var i = 0; i < hrefs.length; i++) {
            hrefs[i].addEventListener('click', this.navega.bind(this, hrefs[i].getAttribute('data-href')), false);
        }

        var scripts = el.querySelectorAll('*[data-script]');
        for (var i = 0; i < scripts.length; i++) {
            this.require(scripts[i].getAttribute('data-script'));
        }

        var titulo = el.querySelectorAll('.titulo');
        if (titulo.length > 0) {
            this.titulo.innerHTML = '';
            this.titulo.appendChild(titulo[0]);
        }
    },

    // animate an outgoing view off of the screen
    sale: function (el, direccion) {
        if (!el) return; // paranoia

        // encuentra lo que constituye "fuera de pantalla"
        var x = -document.body.offsetWidth;
        if (direccion == 'left') x = -x;
        
        // después de un breve retraso para permitir el dibujado del DOM, anima la vista anterior fuera de la pantalla
        window.setTimeout(function () {
            el.style.transform = 'translateX(' + x + 'px)';
        }, 50);

        // eliminar el elemento después de que la transición haya terminado
        window.setTimeout(function () {
            el.parentElement.removeChild(el);
        }, 1000);

    },

    // administra la pila del historial
    historiza: function (url) {
        if (url == 'vuelve') {
            this.historial.pop();
            url = this.historial[this.historial.length - 1];
        } else {
            this.historial.push(url);
        }
        return url;
    }
}

// funciones de conveniencia
function $(str) {
    return document.getElementById(str);
}

function $$(str) {
    return document.querySelectorAll(str);
}

document.addEventListener('deviceready', function (e) { app.init(); }, false);