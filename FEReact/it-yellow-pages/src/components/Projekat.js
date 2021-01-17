import React, {useState} from 'react'
import Spinner from '../components/Spinner.js';

import useFetch from '../services/useFetch.js'

function Projekat({projekat}) {

    const [prikaziFirme, setPrikaziFirme]=useState(false);
    const [prikaziZaposlene, setPrikaziZaposlene]=useState(false);

    //const [sviZaposleni, setSviZaposleni]=useState(false);
   /* projekat.firmeNaProjektu.map(firma=>
        {

        })*/
    const idFirme=projekat.firmeNaProjektu.length===0 ? 0 : projekat.firmeNaProjektu[0].naziv;
    /*const {data:zaposleniDateFirme, loading, error}=useFetch("https://localhost:44392/Zaposleni/vratiSveZaposlenePrekoNazivaFirme/"
    +idFirme);*/
    const dugmeFirme=prikaziFirme===true ? "Zatvorite firme" : "Prikazite firme";
    const dugmeZaposleni=prikaziZaposlene===true ? "Zatvorite zaposlene" : "Prikazite zaposlene"; 

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
    //if(error) throw error;
    //if(loading) return <Spinner/>
    //console.log(zaposleniDateFirme);
        return(
        <div key={projekat.id}>
            <h2>{projekat.naziv}</h2>
            <p>Pocetak:{projekat.datum_od}</p>
            {projekat.datum_do!=="0001-01-01T00:00:00" ?  <p>Kraj: {projekat.datum_do}</p> : <p>Nije gotov</p>}

            {projekat.firmeNaProjektu.length!==0 ?
             <button onClick={()=>setPrikaziFirme(!prikaziFirme)}>{dugmeFirme}</button> : 
                    <p>Trenutno nijedna firma ne radi na projektu</p>}
            {prikaziFirme && 
                <div>{projekat.firmeNaProjektu.map(f=>{
                    return(
                        <p key={f.naziv}>{f.naziv}</p>
                    )
                    })}
                </div>
            }

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
            <button onClick={()=>obrisiProjekat()}>Obrisi projekat</button>
        </div>)

}

export default Projekat
