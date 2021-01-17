import React, {useState} from 'react'
import DateTimePicker from 'react-datetime-picker';
import Spinner from '../components/Spinner.js'
import useFetch from '../services/useFetch.js'

function DodajProjekatFirmi({nazivProjekta}) {

    const [nazivFirme, setNazivFirme]=useState('');
    const [datumOd, setDatumOd]=useState(new Date());
    const [datumDo, setDatumDo]=useState(null);

    const changeDatumOd=()=>{

    }

    const changeDatumDo=()=>{

    }

    async function handleSubmit(){
        

        const obj=datumDo===null ? 
            {"datum_od":datumOd} :
            {"datum_od":datumOd, "datum_do":datumDo};

        await fetch("https://localhost:44392/Pripada/dodajProjekatFirmi/"+nazivProjekta+"/"+nazivFirme,{
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

    const {data:firme, loading, error}=useFetch("Firma/vratiSveFirme");

    console.log(firme);
   

    
    if(error) throw error;
    if(loading) return <Spinner/>
    return (
        
     <form onSubmit={()=>handleSubmit()}>
            <p>Dodaj datom projektu novu firmu</p>
            <select value={nazivFirme} onChange={(ev)=>setNazivFirme(ev.target.value)}>
            <option key={"svi"} value={" "}></option>
            {firme.map(f=>{
                return <option key={f.id} value={f.naziv}>{f.naziv}</option>
            })}
            </select>
            
            <label>Datum od:<DateTimePicker onChange={(ev)=>setDatumOd(ev)} value={datumOd}/></label>
                <label>Datum do:<DateTimePicker onChange={(ev)=>setDatumDo(ev)} value={datumDo}/></label>

           <button>Dodaj</button>
    </form>
                
    )
}

export default DodajProjekatFirmi
