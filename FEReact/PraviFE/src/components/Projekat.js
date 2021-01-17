import React, {useState} from 'react'
import Spinner from '../components/Spinner.js';
import { Link, NavLink,useParams } from "react-router-dom";

import useFetch from '../services/useFetch.js'
import DodajProjekatFirmi from '../components/DodajProjekatFirmi.js';
import DodajProjekatZaposlenom from './DodajProjekatZaposlenom.js';

function Projekat() {
    
    async function obrisiProjekat()
    {
        await fetch("https://localhost:44392/Projekat/obrisiProjekat/"+projekat.id,{
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

    async function obrisiPripada(idFirme, idProjekta)
    {
        await fetch("https://localhost:44392/Pripada/obrisiPripadaZaProjekatIFirmu/"+idFirme+"/"+idProjekta,{
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

    const {id}=useParams();
    const [prikaziFirme, setPrikaziFirme]=useState(false);
    const [prikaziZaposlene, setPrikaziZaposlene]=useState(false);

    

    const {data:projekat, loading, error}=useFetch("Projekat/vratiProjekat/"+id);

    console.log(projekat);
   

    
    if(error) throw error;
    if(loading) return <Spinner/>

    const idFirme=projekat.firmeNaProjektu.length===0 ? 0 : projekat.firmeNaProjektu[0].naziv;
  
    const dugmeFirme=prikaziFirme===true ? "Zatvorite firme" : "Prikazite firme";
    const dugmeZaposleni=prikaziZaposlene===true ? "Zatvorite zaposlene" : "Prikazite zaposlene"; 
        return(
        <div >
            <h2>{projekat.naziv}</h2>
            <p>Opis:{projekat.opis}</p>
            <p>Pocetak:{projekat.datum_od}</p>
            {(projekat.datum_do!=="0001-01-01T00:00:00" && projekat.datum_do!=="1901-01-31T23:00:00Z") ?  <p>Kraj: {projekat.datum_do}</p> : <p>Nije gotov</p>}

            {projekat.firmeNaProjektu.length!==0 ?
             <button onClick={()=>setPrikaziFirme(!prikaziFirme)}>{dugmeFirme}</button> : 
                    <p>Trenutno nijedna firma ne radi na projektu</p>}
            {prikaziFirme && 
                <div>{projekat.firmeNaProjektu.map(f=>{
                    return(
                        <form onSubmit={()=>obrisiPripada(f.id, projekat.id)}>
                            <p key={f.naziv}>{f.naziv}</p>
                            <button >Obrisi firmu sa projekta</button>
                            <DodajProjekatZaposlenom firmaId={f.id} projekatId={projekat.id}/>
                        </form>
                    )
                    })}
                </div>
            }
            <DodajProjekatFirmi nazivProjekta={projekat.naziv}/>

            {projekat.zaposleniNaProjektu.length!==0 ?
                <button onClick={()=>setPrikaziZaposlene(!prikaziZaposlene)}>{dugmeZaposleni}</button> :
                    <p>Trenutno nema nijednog zaposlenog na projektu</p>
            }
            {prikaziZaposlene &&
                <div>
                    {projekat.zaposleniNaProjektu.map(zap=>{
                        return(
                            <p>{zap.ime} {zap.prezime}</p>
                        )
                    })}
                </div>
            }
     
        </div>)
        
}

export default Projekat
