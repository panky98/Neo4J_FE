import React, {useState} from 'react'
import DateTimePicker from 'react-datetime-picker';
function DodajNoviTipNagrade() {

    async function handleSubmit()
    {
        const obj=
        {
            "naziv":nazivNagrade,
            "kategorija":kategorijaNagarde,
            "datum":datumNagrade
        }

        await fetch("https://localhost:44392/Nagrada/dodajNagradu",{
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
    }
    const [nazivNagrade, setNazivNagrade]=useState('');
    const [kategorijaNagarde, setKategorijaNagrade]=useState('');
    const [datumNagrade, setDatumNagrade]=useState(new Date());
    return (
        <div>
        <p>Ovde mozete dodati novu nagradu:</p>
        <form onSubmit={()=>handleSubmit()}>
            <input placeholder="Naziv" value={nazivNagrade} onChange={(e)=>setNazivNagrade(e.target.value)}/>
            <input placeholder="Kategorija" value={kategorijaNagarde} onChange={(e)=>setKategorijaNagrade(e.target.value)}/>
            <label>Datum:<DateTimePicker onChange={(ev)=>setDatumNagrade(ev)} value={datumNagrade}/></label>

            <button>Dodaj nagradu</button>

        </form>
        </div>
    )
}

export default DodajNoviTipNagrade
