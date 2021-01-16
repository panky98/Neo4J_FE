import React, {useState} from 'react'
import useFetch from '../services/useFetch.js';

import Spinner from '../components/Spinner.js';
import Projekat from './Projekat.js';
import NapraviProjekat from '../components/NapraviProjekat.js';

function Projekti() {


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
                return <Projekat key={p.id} projekat={p}/>
            })}
        </div>
    )
}

export default Projekti
