import React, {useState} from 'react'

import Spinner from '../components/Spinner.js';
import DateTimePicker from 'react-datetime-picker';
import useFetch from '../services/useFetch.js';

function DodajProjekatZaposlenom({firmaId, projekatId}) {
    async function handleSubmit(){
        
        //alert(projekatId+" " + zaposleniNaProjektu);
        const obj=datumDo===null ? 
            {"datum_od":datumOd} :
            {"datum_od":datumOd, "datum_do":datumDo};

        await fetch("https://localhost:44392/RadiNa/dodajProjekatZaposlenom/"+projekatId+"/"+zaposleniNaProjektu,{
        method:"POST",
        headers:{"Content-Type":"application/json"},
        body: JSON.stringify(obj)    
    }).then(p=>{
        if(p.ok){
            console.log("Uspesno dodato!");
        }
    }).catch(exc=>{
        console.log(exc);
    });
    window.location.reload(false);
    }

    const {data:zaposleni, loading, error}=useFetch("Zaposleni/vratiSveZaposlenePrekoIdFirme/"+firmaId);

    const [zaposleniNaProjektu, setZaposleniNaProjektu]=useState(-1);
    const [datumOd, setDatumOd]=useState(new Date());
    const [datumDo, setDatumDo]=useState(null);

    if(error) throw error;
    if(loading) return <Spinner/>
    return (
        <form>
            <p>Dodaj zaposlenog na projektu</p>
            <select value={zaposleniNaProjektu} onChange={(e)=>setZaposleniNaProjektu(e.target.value)}>
                <option key={"svi"} value={" "}></option>
                {zaposleni.map(z=>{
                    return <option key={z.id} value={z.id}>{z.ime} {z.prezime}</option>
                })}
            </select>

            <label>Datum od:<DateTimePicker onChange={(ev)=>setDatumOd(ev)} value={datumOd}/></label>
            <label>Datum do:<DateTimePicker onChange={(ev)=>setDatumDo(ev)} value={datumDo}/></label>

           <button onClick={()=>handleSubmit()}>Dodaj</button>
        </form>
    )
}

export default DodajProjekatZaposlenom
