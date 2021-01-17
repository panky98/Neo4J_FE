import React, {useState} from 'react'
import useFetch from '../services/useFetch.js';

import { Link, NavLink } from "react-router-dom";

import Spinner from '../components/Spinner.js';
import Projekat from './Projekat.js';
import NapraviProjekat from '../components/NapraviProjekat.js';

function Projekti(id) {
    async function obrisiProjekat(id)
    {

        console.log("brisem " + id);
        await fetch("https://localhost:44392/Projekat/obrisiProjekat/"+id,{
        method:"DELETE",
        headers:{"Content-Type":"application/json"},
    }).then(p=>{
        if(p.ok){
            console.log("Uspesno obrisano!");
        }
    }).catch(exc=>{
        console.log(exc);
    });
    window.location.reload(false);
    }

    const [tipProjekta, setTipProjekta]=useState("svi");

 

    let pathUpita="Projekat/vratiSveProjekte";
    if(tipProjekta==='gotov')
        pathUpita="Projekat/vratiSveGotoveProjekte";
    if(tipProjekta==='trenutno')
        pathUpita="Projekat/vratiSveTrenutneProjekte";
    const {data:projekti, loading, error}=useFetch(pathUpita);
   

    if(error) throw error;
    if(loading) return <Spinner/>

    
    console.log(projekti)

    
    return (
        <div>
            <NapraviProjekat/>
            <p>Izaberite tip projekta:</p>
            <select value={tipProjekta} onChange={(ev)=>setTipProjekta(ev.target.value)}>

                <option key={"svi"} value={"svi"}></option>
                <option key={"gotov"} value={"gotov"}>gotov</option>
                <option key={"trenutno"} value={"trenutno"}>trenutno</option>
            </select>
            {projekti.map(p=>
            {
                return(
                    <div key={p.id}>
                        <h2>{p.naziv}</h2>
                        <p>Pocetak:{p.datum_od}</p>
                        {(p.datum_do!=="0001-01-01T00:00:00" && p.datum_do!=="1901-01-31T23:00:00Z") ?  <p>Kraj: {p.datum_do}</p> : <p>Nije gotov</p>}
                        <Link to={`/projekti/${p.id}`} className="btn">Saznaj vise</Link>
                        <button onClick={()=>obrisiProjekat(p.id)}>Obrisi projekat</button>
                    </div>

                //return <Projekat key={p.id} projekat={p} firme={firme}/>
            )})}
        
        </div>
    )
}

export default Projekti
