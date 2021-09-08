
export class ResponseGetLibros {
    constructor(isbn, titulo, autor, editorial, edicion, stock, imagen){
        this.isbn = isbn,
        this.titulo = titulo,
        this.autor = autor,
        this.editorial = editorial
        this.edicion = edicion
        this.stock = stock
        this.imagen = imagen
    }
}

export class LibroDeClienteDTO {
    constructor(estado, nombreCliente, apellidoCliente, isbn, titulo, autor, editorial,imagen){
        this.estado = estado,
        this.nombreCliente = nombreCliente,
        this.apellidoCliente = apellidoCliente,
        this.isbn = isbn
        this.titulo = titulo
        this.autor = autor
        this.editorial = editorial
        this.imagen = imagen
    }
}

export const clienteConstant = {
    "clienteId": 1,
    "dni": "12345678",
    "nombre": "Pepe",
    "apellido": "Garcia",
    "email": "pepegarcia@gmail.com"
}


