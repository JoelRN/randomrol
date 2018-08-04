
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
                console.log(response.result);  
                eventoOk();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                var mensaje = "";
                if (xhr.status != 400) {
                    mensaje += "<h4>Error " + xhr.status + "</h4>";
                }

                mensaje += "<p>" + xhr.responseText + "</p>";

                servicio_alerta.error(mensaje);
                eventoError();                
            }
        });
    }
}
