
var api = {
    urlApi: string = "http://localhost:4500/",

    llamadaAjax: function (urlServicio, datos, eventoOk, eventoError) {
        console.log(this.urlApi + urlServicio);
        
        jQuery.ajax({
            type: "POST",
            url: api.urlApi + urlServicio,
            data: JSON.stringify(datos),
            contentType: "application/json",
            dataType: "json",
            success: function (response) {
                eventoOk(response);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                var mensaje = "";
                if (xhr.status != 400) {
                    mensaje += "<h4>Error " + xhr.status + "</h4>";
                }

                mensaje += "<p>" + xhr.responseText + "</p>";

                if (xhr.status != 401) { 
                    servicio_alerta.error(mensaje);
                }

                eventoError();                
            }
        });
    }
}
