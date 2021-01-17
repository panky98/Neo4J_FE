import React, {useState} from 'react'

import DateTimePicker from 'react-datetime-picker';

function NapraviProjekat() {
    const [nazivProjekta, setNazivProjekta]=useState('');
    const [opis, setOpis]=useState('');
    const [datumOd, setDatumOd]=useState(new Date());
    const [datumDo, setDatumDo]=useState(null);

    async function handleSubmit()
    {
        const obj=datumDo===null ? 
            {"naziv":nazivProjekta,"opis":opis,"datum_od":datumOd} :
            {"naziv":nazivProjekta,"opis":opis,"datum_od":datumOd, "datum_do":datumDo};

        
        await fetch("https://localhost:44392/Projekat/dodajProjekat",{
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

    const changeDatumOd=(ev)=>
    {
        console.log(ev);
        setDatumOd(ev);
    }

    const changeDatumDo=(ev)=>
    {
        setDatumDo(ev);
    }
    return (
        <form onSubmit={()=>handleSubmit()}>
            <div>
                <input placeholder="Naziv projekta" type="text" value={nazivProjekta} onChange={(ev)=>setNazivProjekta(ev.target.value)}/>
                <input type="text" placeholder="Opis projekta" value={opis} onChange={(ev)=>setOpis(ev.target.value)}/>

                <label>Datum od:<DateTimePicker onChange={(ev)=>changeDatumOd(ev)} value={datumOd}/></label>
                <label>Datum do:<DateTimePicker onChange={(ev)=>changeDatumDo(ev)} value={datumDo}/></label>
                <button>Dodaj novi projekat</button>
            </div>
        </form>
    )
}

export default NapraviProjekat
