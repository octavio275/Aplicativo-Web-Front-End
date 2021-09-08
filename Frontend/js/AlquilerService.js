import {
    URLALQUILER_CLIENTEID
} from "./urls.js";

const GetAlquileres = (idCliente) => {
    let url = URLALQUILER_CLIENTEID + idCliente;
    return fetch(url, {
        method:"GET",
        headers: {
            'Content-Type': 'application/json',
        },
        mode: 'cors'
    })
            .then(response => { 
                return response.json()
            })
            .then(json => {
                return json;
            })
            .catch(err => console.log('ERROR: ' + err))
}

export default GetAlquileres;

