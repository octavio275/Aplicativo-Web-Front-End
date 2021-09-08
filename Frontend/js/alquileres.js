import {
    clienteConstant
} from "./Constant.js";
import * as ServiceAlquiler from "./AlquilerService.js";

window.onload = () => {
    CargarModo();
    CargarPerfil();
    CargarAlquileres();

    sessionStorage.removeItem("libros_filtrados");
    /*Para que no perdure el filtrado al volver a la página principal*/
}

function CargarModo(){
    if(sessionStorage.getItem("modo") == 1){
        console.log("Estaba en dark");
        $('input.checkbox').click();
    }
}

function CargarPerfil() {
    $('.datos-cliente').empty();
    var alquileres = $('.datos-cliente');
    var card = `
    <label for="" class="nombre-cliente">${clienteConstant.nombre + " "+ clienteConstant.apellido}</label>
    <label for="" class="email">${clienteConstant.email}</label>
    `
    alquileres.append(card);
}

function CargarAlquileres() {
    ServiceAlquiler.default(clienteConstant.clienteId).then(x => MostrarAlquileres(x))
}

function MostrarAlquileres(Alquiler) {
    if (Alquiler.length > 0) {
        CargarAlquileresYReservas(Alquiler);
    } else {
        var alquileres = $('.contenedor-principal');
        var card = `
        <div class="vacio">
        <label for="vacio" id="vacio">No posee libros alquilados ni reservados... </label>
        </div>
        `
        alquileres.append(card);
    }
}

function CargarAlquileresYReservas(Alquiler) {
    var i = 1;

    var alquileres = $('.contenedor-principal');
    var card = `<div class="table">`
    alquileres.append(card);

    var alquileres = $('.table');
    var card = `<div class="titulo-tabla">MIS POSESIONES</div>
    <div class="contenido-tabla"></div>
    `
    alquileres.append(card);

    var alquileres = $('.contenido-tabla');

    var card = `<div class="cabecera-tabla">Titulo</div>
    <div class="cabecera-tabla">Autor</div>
    <div class="cabecera-tabla">ISBN</div>
    <div class="cabecera-tabla">Editorial</div>
    <div class="cabecera-tabla">Estado </div>
    `
    alquileres.append(card);


    Alquiler.forEach(element => {
        var card = `
        <div class="item-tabla" id="item-table">${element.titulo} </div>
        <div class="item-tabla">${element.autor}  </div>
        <div class="item-tabla">${element.isbn}  </div>
        <div class="item-tabla">${element.editorial}  </div>
        <div class="item-tabla">${element.estado}  </div>
        `
        alquileres.append(card);

        i++;
    });
    var card = `
    </div>
    </div>
    `
    alquileres.append(card);

}