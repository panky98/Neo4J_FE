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
            <div style={{textAlign:"center", marginTop:"20px"}}>
                <h4>Ovde mozete dodati novu nagradu:</h4>
            </div>
            <form class="float-container" style={{textAlign:"center"}} onSubmit={()=>handleSubmit()}>
                <div class="float-child" style={{width:"33%"}}> 
                    <label>Naziv:</label><br/>
                    <input class="form-control" value={nazivNagrade} onChange={(e)=>setNazivNagrade(e.target.value)}/>
                </div>
                <div class="float-child" style={{width:"33%"}}>
                    <label>Kategorija:</label><br/>
                    <input  class="form-control" value={kategorijaNagarde} onChange={(e)=>setKategorijaNagrade(e.target.value)}/>
                </div>
                <div class="float-child" style={{width:"33%"}}>
                    <label>Datum:</label><br/>
                    <DateTimePicker onChange={(ev)=>setDatumNagrade(ev)} value={datumNagrade}/>  
                </div>

                <button type="submit" class="btn btn-primary">Dodaj nagradu</button>
            </form>
        </div>
    )
}

export default DodajNoviTipNagrade
