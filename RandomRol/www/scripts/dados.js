app.dados = {
    init: function () {
        this.button = $('btnLanzaDados');
        this.button.addEventListener('click', this.lanzaDados.bind(this));

        $('resultado').innerHTML = '';
    },

    lanzaDados: function () {
        numeroDados = Number($('numeroDados').value);
        numeroCarasDado = Number($('numeroCaras').value);

        $('resultado').innerHTML = '';

        for (i = 1; i <= numeroDados; i++) {
            var num = (Math.random() * numeroCarasDado).toFixed(0);
            num = Number(num) == 0 ? '1' : num;
            $('resultado').innerHTML += 'Dado nº' + i + ' a salido: ' + num  + '<br />';
        }
    }
};

app.dados.init();