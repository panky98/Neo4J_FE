import React, {useState} from 'react'
import Spinner from './Spinner';
import useFetch from '../services/useFetch.js';


function FormPromeniRadnoMesto({idZaposlenog}) {
    const[showSpinner,setShowSpinner]=useState(false);

    const {data:firme, loading, error}=useFetch("Firma/vratiSveFirme");
    const[selectedFirma,setSelectedFirma]=useState(-1);
    const[newPozicija,setNewPozicija]=useState(" ");
    const[showAlert,setShowAlert]=useState(false);

    if(error) throw error;
    if(loading) return <Spinner/>

    return(
        <div>
            {showSpinner && <Spinner/>}
            <select style = {{margin:"10px 0 10px 0"}} class="form-control" onChange={(event)=>setSelectedFirma(event.target.value)}>
                <option value="-1">Izaberi kompaniju</option>
                <option value="-2">Nezaposlen</option>
                {firme.map(firma=>{
                    return <option key={firma.id} value={firma.id}>{firma.naziv}</option>
                })}
            </select> 
            <label>Pozicija: </label> 
            <input class="form-control" type="text" onChange={(event)=>setNewPozicija(event.target.value)}/> 
            <br/>
            <button type="submit" class="btn btn-primary" onClick={async ()=>await Change()}>Promeni</button>
            <br/>
            {showAlert && <p style={{color:"red", display:"inline"}}>*Niste izabrali firmu!</p>}
        </div>
    )

    async function Change(){
        if(selectedFirma==-1){
            setShowAlert(true);
        }
        else
        {
            setShowSpinner(true);
            await fetch("https://localhost:44392/Zaposleni/promeniRadnomesto/"+parseInt(idZaposlenog)+"/"+parseInt(selectedFirma)+"/"+((newPozicija=="" || newPozicija==" ")?"empty_string":newPozicija),{
                method:"PUT"
            }).then(p=>{
                if(p.ok){
                    window.location.reload(false);
                }
            }).catch(exception=>{
                console.log(exception);
            });
            setShowSpinner(false);
        }
    }
}

export default FormPromeniRadnoMesto;