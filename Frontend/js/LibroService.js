import * as Constant from "./Constant.js"
import {
    URLIBROS
} from "./urls.js";

const GetLibrosDisponibles = () => {
    let url = URLIBROS + "?stock=" +"true";
    return fetch(url, {
        method:"GET",
        headers: {
            'Content-Type': 'application/json',
        },
    })
            .then(response => { 
                return response.json()
            })
            .then(json => {
                sessionStorage.setItem("libros",JSON.stringify(json));
                return json;
            })
            .catch(err => console.log('ERROR: ' + err))
}

export default GetLibrosDisponibles;

