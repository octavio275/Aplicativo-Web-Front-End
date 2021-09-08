import {
    ResponseGetLibros,
    clienteConstant
} from "./Constant.js";
import {
    URLALQUILER
} from "./urls.js";

import * as ServiceLibro from "./LibroService.js";
import * as ServiceAlquiler from "./AlquilerService.js";

window.onload = () => {
    CargarModo();
    CargarAlquileres();
    GetLibrosDisponibles();
}
function CargarModo(){
    if(sessionStorage.getItem("modo") == 1){
        console.log("Estaba en dark");
        $('input.checkbox').click();
    }
}

function CargarAlquileres() {
    ServiceAlquiler.default(clienteConstant.clienteId).then(x => GuardarAlquileres(x))
}

function GuardarAlquileres(Alquileres) {
    sessionStorage.setItem("alquilados", JSON.stringify(Alquileres));
}

function ComprobarReserva(isbn) {
    var alquileres = JSON.parse(sessionStorage.getItem("alquilados"));
    var estado;
    alquileres.forEach(alquiler => {
        if (alquiler.isbn == isbn) {
            estado = alquiler.estado;
        }
    })
    if (estado) {
        return estado;
    } else {
        return null;
    }
}

function GetLibrosDisponibles() {
    ServiceLibro.default().then(x => CargarLibros(x))
}

function CargarLibros(Libro) {
    var i = 0;
    var main = $('#main-books');
    Libro.forEach(libro => {
        var card = `
        <button class="libros" data-toggle="modal" data-target="#exampleModal"
        value = '${i}'>
        <img src="${libro.imagen}" alt="Image not found" onerror="this.onerror=null;this.src='../img/not-found3.jpg';"
        id=${"imagen-libro"+i}>
        <div class="text-bottom">
        <p>${libro.titulo}</p>
        </div>
        </button>
        `
        main.append(card);
        i++;
    })
}



$(document).on('click', '.libros', function () {
    var objeto_libro;
    if (sessionStorage.getItem("libros_filtrados")) {
        objeto_libro = JSON.parse(sessionStorage.getItem("libros_filtrados"))[this.value];
    } else {
        objeto_libro = JSON.parse(sessionStorage.getItem("libros"))[this.value];
    }

    $('.estado-accion').empty();

    $('.titulo-libro').empty();
    var contenido = $('.titulo-libro');
    var text = `
    <h5 class="modal-title" id="exampleModalLabel"> ${objeto_libro.titulo}</h5>
    `;
    contenido.append(text);

    $('.modal-img').empty();
    var contenido = $('.modal-img');
    var text = `<div class="div-imagen"
    style=" background-image: url('${objeto_libro.imagen}');"
    ></div>`;
    contenido.append(text);

    $('.autor-libro').empty();
    var contenido = $('.autor-libro');
    var text = `<label for="autor" id="resp">${objeto_libro.autor}</label>`;
    contenido.append(text);

    $('.isbn-libro').empty();
    var contenido = $('.isbn-libro');
    var text = `<label for="isbn" id="resp">${objeto_libro.isbn}</label>`;
    contenido.append(text);

    $('.editorial-libro').empty();
    var contenido = $('.editorial-libro');
    var text = `<label for="editorial" id="resp">${objeto_libro.editorial}</label>`;
    contenido.append(text);

    $('.edicion-libro').empty();
    var contenido = $('.edicion-libro');
    var text = `<label for="edicion" id="resp">${objeto_libro.edicion}</label>`;
    contenido.append(text);

    $('.stock-libro').empty();
    var contenido = $('.stock-libro');
    var text = `<label for="stock" id="resp">${objeto_libro.stock}</label>`;
    contenido.append(text);

    $('.modal-footer').empty();
    var contenido = $('.modal-footer');
    var text = `
    <button type="submit" class="alquilar btn-sm" id="alquilar" value = '${this.value}'>Alquilar</button>
    <button type="submit" class="reservar  btn-sm" id="reservar" value = '${this.value}'>Reservar</button>
    `;
    contenido.append(text);

    var estado = ComprobarReserva(objeto_libro.isbn);
    if (estado) {
        DeshabilitarBoton(estado);
    }
});

function DeshabilitarBoton(estado) {
    if (estado == "Alquilado") {
        document.getElementById("alquilar").disabled = true;

        $('.estado-accion').empty();
        var contenido = $('.estado-accion');
        var text = `
        <label for="accion" id="respuesta-error">Ya lo tienes alquilado</label>
        `;
        contenido.append(text);

    } else {
        $('.estado-accion').empty();
        var contenido = $('.estado-accion');
        var text = `
        <label for="accion" id="respuesta-201">Lo tenés reservado, podés alquilarlo</label>
        `;
        contenido.append(text);
    }
    document.getElementById("reservar").disabled = true;
}

$(document).on('click', '#alquilar', function () {
    var objeto_libro;
    if (sessionStorage.getItem("libros_filtrados")) {
        objeto_libro = JSON.parse(sessionStorage.getItem("libros_filtrados"))[this.value];
    } else {
        objeto_libro = JSON.parse(sessionStorage.getItem("libros"))[this.value];
    }

    document.getElementById("alquilar").disabled = true;
    var isbn = objeto_libro.isbn;

    var fecha_actual = new Date();
    var fecha = fecha_actual.getDate() + '-' + fecha_actual.getMonth() + '-' + fecha_actual.getFullYear();

    var alquiler = new AlquileryReservaRequestDTO(clienteConstant.clienteId, isbn, fecha, "");

    var alquileres = JSON.parse(sessionStorage.getItem("alquilados"));
    var indice = 0;
    var reservado = false;


    if (alquileres) {
        while (indice < alquileres.length) {
            if (alquileres[indice].isbn == isbn && alquileres[indice].estado =="Reservado") {
                indice = alquileres.length + 1;
                reservado = true;
            }
            indice++;
        }
    }

    if (reservado) {
        var alquilarReserva = new RequestAlquilarReserva(clienteConstant.clienteId, isbn);

        var options = {
            method: 'PATCH',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(alquilarReserva),
            mode: 'cors'
        };

        fetch(URLALQUILER, options)
            .then(response => {
                if (response.status === 200) {
                    $('.estado-accion').empty();
                    var contenido = $('.estado-accion');
                    var text = `
                <label for="accion" id="respuesta-201">Se alquiló correctamente</label>
                `;
                    contenido.append(text);
                    popupSwal('success','Se alquiló correctamente');

                    return response.json();
                } else {
                    popupSwal('error','No se pudo alquilar');
                    $('.estado-accion').empty();
                    var contenido = $('.estado-accion');
                    var text = `
                <label for="accion" id="respuesta-error">No se pudo alquilar</label>
                `;
                    contenido.append(text);
                    const error = new Error(response.error);
                    throw error;
                }
            })
            .then(json => {
                return json;
            })
            .catch(err => console.log('ERROR: ' + err))

    } else {
        var options = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(alquiler),
            mode: 'cors'
        };
        fetch(URLALQUILER, options)
            .then(response => {
                if (response.status === 201) {
                    $('.estado-accion').empty();
                    var contenido = $('.estado-accion');
                    var text = `
                <label for="accion" id="respuesta-201">Se alquiló correctamente</label>
                `;
                    contenido.append(text);
                    popupSwal('success','Se alquiló correctamente');

                    if (sessionStorage.getItem("libros_filtrados")) {
                        var local = JSON.parse(sessionStorage.getItem("libros_filtrados"));
                        local[this.value].stock--;
                        sessionStorage.setItem("libros_filtrados", JSON.stringify(local));
                    } else {
                        var local = JSON.parse(sessionStorage.getItem("libros"));
                        local[this.value].stock--;
                        sessionStorage.setItem("libros", JSON.stringify(local));
                    }

                    $(".busqueda").click();
                    return response.json();
                } else {
                    popupSwal('error','No se pudo alquilar');
                    $('.estado-accion').empty();
                    var contenido = $('.estado-accion');
                    var text = `
                    <label for="accion" id="respuesta-error">No se pudo alquilar</label>
                    `;
                    contenido.append(text);
                    const error = new Error(response.error);
                    throw error;
                }
            })
            .then(json => {
                return json;
            })
            .catch(err => console.log('ERROR: ' + err))
    }
});


$(document).on('click', '#reservar', function () {
    var objeto_libro;
    if (sessionStorage.getItem("libros_filtrados")) {
        objeto_libro = JSON.parse(sessionStorage.getItem("libros_filtrados"))[this.value];
    } else {
        objeto_libro = JSON.parse(sessionStorage.getItem("libros"))[this.value];
    }

    document.getElementById("reservar").disabled = true;
    var libro = objeto_libro;

    var isbn = libro.isbn;
    var fecha_actual = new Date();
    var fecha = fecha_actual.getDate() + '-' + fecha_actual.getMonth() + '-' + fecha_actual.getFullYear();

    var alquiler = new AlquileryReservaRequestDTO(clienteConstant.clienteId, isbn, "", fecha);

    var options = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(alquiler),
        mode: 'cors'
    };

    fetch('https://localhost:44371/api/Alquiler', options)
        .then(response => {
            if (response.status === 201) {
                $('.estado-accion').empty();
                popupSwal('success','Se reservó correctamente');
                var contenido = $('.estado-accion');
                var text = `
                <label for="accion" id="respuesta-201">Se reservó correctamente</label>
                `;
                contenido.append(text);

                if (sessionStorage.getItem("libros_filtrados")) {
                    var local = JSON.parse(sessionStorage.getItem("libros_filtrados"));
                    local[this.value].stock--;
                    sessionStorage.setItem("libros_filtrados", JSON.stringify(local));
                } else {
                    var local = JSON.parse(sessionStorage.getItem("libros"));
                    local[this.value].stock--;
                    sessionStorage.setItem("libros", JSON.stringify(local));
                }
                var alquileresYReservas = JSON.parse(sessionStorage.getItem("alquilados"));

                var alquilado = new Alquilado("Reservado", clienteConstant.nombre, 
                clienteConstant.apellido, alquiler.isbn,libro.titulo,libro.autor,
                libro.editorial, libro.edicion,libro.imagen);
                alquileresYReservas.push(alquilado);
                sessionStorage.setItem("alquilados",JSON.stringify(alquileresYReservas));


                $(".busqueda").click();

                return response.json();
            } else {
                popupSwal('error','No se pudo reservar');
                $('.estado-accion').empty();
                var contenido = $('.estado-accion');
                var text = `
                <label for="accion" id="respuesta-error">No se pudo reservar</label>
                `;
                contenido.append(text);
                const error = new Error(response.error);
                throw error;
            }
        })
        .then(json => {
            return json;
        })
        .catch(err => console.log('ERROR: ' + err))

});

function popupSwal(tipo, mensaje){
    Swal.fire({
        type: tipo,
        title: mensaje,
        showConfirmButton: true,
        confirmButtonColor: '#48D1CC'
    })
}

class AlquileryReservaRequestDTO {
    constructor(clienteId, isbn, fechaAlquiler, fechaReserva) {
        this.clienteId = clienteId,
            this.isbn = isbn,
            this.fechaAlquiler = fechaAlquiler,
            this.fechaReserva = fechaReserva
    }
}
class RequestAlquilarReserva {
    constructor(cliente, isbn) {
        this.cliente = cliente,
            this.isbn = isbn
    }
}
class Alquilado {
    constructor(estado, nombreCliente, apellidoCliente, isbn, titulo, autor, editorial, edicion, imagen) {
        this.estado = estado,
            this.nombreCliente = nombreCliente,
            this.apellidoCliente = apellidoCliente,
            this.isbn = isbn,
            this.titulo = titulo,
            this.autor = autor,
            this.editorial = editorial,
            this.edicion = edicion,
            this.imagen = imagen
    }
}

$(document).on('click', '.busqueda', function () {
    var texto_a_buscar = document.getElementById("input-texto").value;
    var libros_filtrados = JSON.parse(sessionStorage.getItem("libros"));

    texto_a_buscar = texto_a_buscar.toLowerCase();
    var listaDeLibros = [];
    var autor_minus;
    var titulo_minus;
    libros_filtrados.forEach(libro => {
        autor_minus = libro.autor.toLowerCase();
        titulo_minus = libro.titulo.toLowerCase();
        if ((autor_minus.includes(texto_a_buscar) || titulo_minus.includes(texto_a_buscar)) && libro.stock > 0) {
            listaDeLibros.push(libro);
        }

    });


    $('#main-books').empty();


    sessionStorage.setItem("libros_filtrados", JSON.stringify(listaDeLibros));

    CargarLibros(listaDeLibros);


    $('.texto-libros-mostrados').empty();
    var contenido = $('.texto-libros-mostrados');
    if(texto_a_buscar!=""){
        var text = `<label for="libros-presentados"> ${listaDeLibros.length} resultados de la búsqueda de: "${texto_a_buscar}".</label>`;
    }
    else{
        var text = `<label for="libros-presentados"> Libros disponibles: </label>`;
    }
    contenido.append(text);
});

$('.buscar-texto').keypress(function (e) {
    if (e.which == 13) {
        $(".busqueda").click();
    }
});




