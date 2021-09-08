window.onload = () => {

    CargarLibros();
}


function CargarLibros() {
    var items = $('#items');

    fetch('https://localhost:44305/api/Libro/ListaLibros')

        .then(responce => responce.json())
        .then(data => {
            console.log(data);
            sessionStorage.setItem("libros", JSON.stringify(data));
            Renderizar2(data);;
        })
}

function Renderizar2(Libro) { //MODIFIAR ALFO DE LOS BOTONES
    console.log(JSON.parse(sessionStorage.getItem("libros")));
    var i = 0;
    var main = $('#items');
    Libro.forEach(libro => {
        var card = `
        <div class="conteiner-bloque">
        <div class="imagen-libro">
        <div class="libros" 
        value = '${i}'>
        <img src="/IMG/${libro.imagen}" alt="Imagen Libro" onerror="this.onerror=null;this.src='../img/not-found3.jpg';"
        id=${"imagen-libro"+i}>
        </div>
        </div>
        
        <div class="informacion">
            <div class="titulo">
                <label for="titulo" id="info">Titulo: ${libro.titulo}</label>
            </div>

            <div class="autor">
                <label for="autor" id="info">Autor: ${libro.autor}</label>
            </div>
            <div class="isbn">
                <label for="isbn" id="info">ISBN: ${libro.isbn}</label>
            </div>
            <div class="editorial">
                <label for="isbn" id="info">Editorial: ${libro.editorial}</label>
            </div>
            <div class="edicion">
                <label for="isbn" id="info">Edicion: ${libro.edicion}</label>
            </div>
            <div class="stock">
                <label for="isbn" id="info">Stock: ${libro.stock}</label>
            </div>
            <div class="botones" id="botones">
                <div class="reservar-libro">
                <button type="button" class="alta btn btn-success " id = "${libro.clienteId} "value= "${libro.isbn}"> Reservar </button>
                </div>
                <div class="alquilar-libro">
                <button type="button" class="baja btn btn-primary " id = "${libro.clienteId} "value= "${libro.isbn} "value2= "${libro.fechaAlquiler}"> Alquilar </button>
                </div>
            </div>

        </div>

        </div>
        `
        main.append(card);
        i++;
    })

    

}
$(document).on('click', '#boton-busqueda', function () {
    var input_text = document.getElementById("search").value;
    var libros_storage = JSON.parse(sessionStorage.getItem("libros"));

    input_text = input_text.toLowerCase();
    var listaDeLibros = [];
    var autor_minus;
    var titulo_minus;
    libros_storage.forEach(libro => {
        autor_minus = libro.autor.toLowerCase();
        titulo_minus = libro.titulo.toLowerCase();
        if ((autor_minus.includes(input_text) || titulo_minus.includes(input_text)) && libro.stock > 0) {
            listaDeLibros.push(libro);
        }
    });

    $('#items').empty();
    console.log(listaDeLibros);
    Renderizar2(listaDeLibros);
});
/*
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
    if (texto_a_buscar != "") {
        var text = `<label for="libros-presentados"> ${listaDeLibros.length} resultados de la búsqueda de: "${texto_a_buscar}".</label>`;
    } else {
        var text = `<label for="libros-presentados"> Libros disponibles: </label>`;
    }
    contenido.append(text);
});
*/
$('.buscar-texto').keypress(function (e) {
    if (e.which == 13) {
        $(".busqueda").click();
    }
});
// function Renderizar(data){
//     var items = $('#items');

//     $.each(data, function(index, libros) {

//         var card = `

//         <div class="col-md-3 col-sm-4 inline-block" style="max-width: 540px;">
//             <div class="animated fadeInDown">
//                 <div class="col-md-12">
//                 <img src="/IMG/${libros.imagen}" class="col-md-10 quitar-float" style="max-width: 100px;>

//                 </div>
//                 <div class="col-md-8">
//                     <div class="card-body">
//                         <h4 class="card-title"> ${libros.titulo} </h4>

//                         <span class="badge badge-success"> Autor: ${libros.autor}</span>
//                         <button type="button" class="alta btn btn-block btn-primary "  style="background:green" id = "${libros.clienteId} "value= "${libros.isbn}"> Reservar </button>
//                         <button type="button" class="alta btn btn-block btn-primary " id = "${libros.clienteId} "value= "${libros.isbn} "value2= "${libros.fechaAlquiler}"> Alquilar </button>

//                         <div class = "tramite">

//                         </div>
//                     </div>
//                 </div>
//             </div>
//         </div>`;

//         items.append(card);

//     })
// }

$(document).on('click', '.alta', function () {
    let objeto = {
        "clienteId": 2,
        "isbn": this.value

    }

    fetch('https://localhost:44305/api/Alquiler', {
            method: 'POST',
            body: JSON.stringify(objeto), // data can be `string` or {object}!
            mode: 'cors',
            headers: {
                'Content-Type': 'application/json'
            },
        })
        .then(response => {
            return response.json()
        })
        .then(function (objeto) {
            alert('Reserva realizada');
        })
        .catch(err => console.log('ERROR: ' + err));
});

$(document).on('click', '.baja', function () {
    let objeto = {
        "clienteId": 2,
        "isbn": this.value,
        "fechaAlquiler": "2020-11-17T12:29:51.844Z"

    }

    fetch('https://localhost:44305/api/Alquiler', {
            method: 'POST',
            body: JSON.stringify(objeto), // data can be `string` or {object}!
            mode: 'cors',
            headers: {
                'Content-Type': 'application/json'
            },
        })
        .then(response => {
            return response.json()
        })
        .then(function (objeto) {
            alert('Alquiler realizado');
        })
        .catch(err => console.log('ERROR: ' + err));
});







function PostAlquiler(id) {
    if (carrito.length > 0) {
        fetch(`https://localhost:44305/api/Alquiler`, {
                method: 'PUT',
                mode: 'cors',
                headers: {
                    'Content-Type': 'application/json'
                },
            })
            .then(response => {
                return response.json()
            })
            .then(function (venta) {
                alert('Alquiler Realizada');
            })
            .catch(err => console.log('ERROR: ' + err));

    } else {
        alert('No hay libros reservados')
    }
}









function Alquilar() {

    PostAlquiler(1);


    alert('la operecion fue satisfactoria');

}


// var buscador = document.getElementById('formulario');
// buscador.addEventListener('submit', function(e) {
//     e.preventDefault();



//     var autor = document.getElementById("search").value;
//     if (document.getElementById("contenedorMaestro") != null) {
//         var maestro = document.getElementById("contenedorMaestro");
//         var aprendiz = document.getElementById("contenedorAprendiz");
//         maestro.remove(aprendiz);
//         if (document.getElementById("borrar") != null) {

//             var borrar = document.getElementById("borrar");
//             var seccion = document.getElementById("seccionComentarios");
//             borrar.remove(seccion);
//         }

//         if (document.getElementById("borrar2") != null) {

//             var borrar2 = document.getElementById("borrar2");
//             var footer = document.getElementById("footer");
//             borrar2.remove(footer);
//         }


//     }
//     var main = document.createElement('main');
//     main.className = "seccion-top";
//     var ddiv = document.createElement('div');
//     ddiv.className = "container-fluid";
//     main.append(ddiv);
//     var dddiv = document.createElement('div');
//     dddiv.className = "row";
//     var divv = document.createElement('div');
//     divv.id = "panel";
//     divv.className = "panel";
//     dddiv.append(divv);
//     ddiv.append(dddiv);
//     var maestro2 = document.createElement('main');
//     maestro2.className = "contenedor seccion";
//     maestro2.id = "contenedorMaestro";
//     maestro2.append(main);

//     $('body').append(maestro2);

//     $.ajax({
//         type: "GET",
//         url: "https://localhost:44305/api/LIbro?autor=" + autor,




//         dataType: "json",
//         success: function(data) {
//             var maestro2 = document.createElement('div');
//             maestro2.className = "col-md-12";
//             maestro2.id = "maestro";
//             var divcontenedor = document.createElement('div');
//             divcontenedor.className = "w3-row-padding";
//             divcontenedor.id = "contenedor";
//             $.each(data, function(i, item) {



//                 var div = document.createElement('div');
//                 div.className = "w3-col s4  w3-hover-shadow  w3-margin-top containere";


//                 var imagen = document.createElement('IMG');
//                 imagen.src = 'images/' + item.imagen;
//                 imagen.id = item.publicacionID;

//                 var div2 = document.createElement('div');
//                 div2.className = "middle";

//                 var boton = document.createElement('button');
//                 boton.className = "text btn";
//                 boton.textContent = "Añadir Al Carrito";
//                 boton.id = item.productoID;

//                 boton.onclick = function() {
//                     $.ajax({
//                         type: "POST",
//                         url: "https://localhost:44310/api/CarritoProducto/InsertarCarritoProductoCliente?carritoID=" + localStorage.getItem("carritoID") + "&productoID=" + item.productoID,
//                         dataType: "json",
//                         success: function(data) {

//                         }


//                     });
//                 }
//                 div2.append(boton);

//                 var div3 = document.createElement('div');
//                 div3.className = "w3-center btn-link description-producto";


//                 var a = document.createElement('a');
//                 a.onclick = function() {
//                     llenarLocalStorage(item.publicacionID);
//                     location.href = "index.html";
//                 }
//                 var p = document.createElement('p');
//                 p.className = "autor";
//                 p.textContent = item.nombre + " / " + "$" + item.precio;
//                 a.append(p);
//                 div3.append(a);

//                 div.append(imagen);
//                 div.append(div2);
//                 div.append(div3);
//                 divcontenedor.append(div);
//                 $('#panel').append(maestro2);
//                 maestro2.append(divcontenedor);





//             });


//         }



//     });
// });



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
    if (texto_a_buscar != "") {
        var text = `<label for="libros-presentados"> ${listaDeLibros.length} resultados de la búsqueda de: "${texto_a_buscar}".</label>`;
    } else {
        var text = `<label for="libros-presentados"> Libros disponibles: </label>`;
    }
    contenido.append(text);
});

$('.buscar-texto').keypress(function (e) {
    if (e.which == 13) {
        $(".busqueda").click();
    }
});