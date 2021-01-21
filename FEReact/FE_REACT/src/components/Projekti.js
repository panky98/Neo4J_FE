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
            <div class="float-container">
                <NapraviProjekat/>
                <div class="float-child">
                    <h3>Sortiranje - izaberite tip projekta:</h3>
                    <select style={{width:"80%"}} class="form-control" value={tipProjekta} onChange={(ev)=>setTipProjekta(ev.target.value)}>

                        <option key={"svi"} value={"svi"}>Svi projekti</option>
                        <option key={"gotov"} value={"gotov"}>Gotovi</option>
                        <option key={"trenutno"} value={"trenutno"}>Trenutno aktivni</option>
                    </select>
                    <div  style={{height:"200px"}}>

                    </div>
                </div>
            </div>
            
            {projekti.map(p=>
            {
                return(
                    <div class="float-child card" key={p.id} style={{marginLeft:"20px", marginTop:"20px", width : "30%", display: "flex", flexDirection : "column"}}>
                        <div class="card-body">
                            <h2 class="card-title" style={{color:"#3399FF"}} >{p.naziv}</h2>
                            <p>Pocetak: {p.datum_od}</p>
                            {(p.datum_do!=="0001-01-01T00:00:00" && p.datum_do!=="1901-01-31T23:00:00Z") ?  <p>Kraj: {p.datum_do}</p> : <p>Nije gotov</p>}
                            <Link to={`/projekti/${p.id}`} className="btn">Saznaj vise</Link>
                            <button type="submit" class="btn btn-primary" onClick={()=>obrisiProjekat(p.id)}>Obrisi projekat</button>
                        </div>
                    </div>
            )})}
        
        </div>
    )
}

export default Projekti
