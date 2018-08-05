class Autenticacion {

    constructor(respuesta) {
        this.Id = respuesta.Id;
        this.Alias = respuesta.Alias;
        this.Token = respuesta.Token;
    }
}