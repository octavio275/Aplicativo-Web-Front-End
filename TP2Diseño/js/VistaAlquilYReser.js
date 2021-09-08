	//--------------------------------------------------------------------->
 //Metodo get 
 $(document).ready(function() {
     
    Cargar();
   });
 function Cargar() {

     var items = $('#items');

     fetch("https://localhost:44305/api/Alquiler/clientes/"+ 2)
    
     .then(responce => responce.json())
     .then(data => {
console.log(data);

sessionStorage.setItem("libros", JSON.stringify(data));
Renderizar2(data);;
})
}

function Renderizar2(Libro) {
console.log(JSON.parse(sessionStorage.getItem("libros")));
var i = 0;
var main = $('#items');
Libro.forEach(libro => {
var card = `
<div class="conteiner-bloque">
<div class="imagen-libro">
<div class="libros" 
value = '${i}'>
<img src="/IMG/${libro.imagen}" alt="Image not found" onerror="this.onerror=null;this.src='../img/not-found3.jpg';"
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

<div class="botones" id="botones">
  
</div>

</div>

</div>
`
main.append(card);
i++;
})



}
 
	 

