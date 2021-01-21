import React, {useState} from 'react'
import useFetch from '../services/useFetch.js';
import Spinner from '../components/Spinner.js';


function CreateZaposleni(){
    const [newIme, setNewIme]=useState("");
    const [newPrezime, setNewPrezime]=useState("");
    const [newStarost, setNewStarost]=useState("");
    const [newPol, setNewPol]=useState("M");
    const [showSpinner, setShowSpinner]=useState(false);
    const [idFirme,setIdFirme]=useState(-1);
    const [datumOd,setDatumOd]=useState("");
    const [pozicija,setPozicija]=useState("");
    const [alertFirma,setAlertFirma]=useState(false);


    const {data:firme, loading, error}=useFetch("Firma/vratiSveFirme");

    if(error) throw error;
    if(loading) return <Spinner/>

    return(
        <div class="float-container" style={{textAlign:"center"}}>
            {showSpinner && <Spinner/>}
            <div>
                <div class="float-child" style={{width:"30%"}}>
                    <label>Ime </label>
                    <input class="form-control" type="text" onChange={(event)=>setNewIme(event.target.value)}/>{(newIme.length==0)&&<p style={{color:"red", display:"inline"}}>*Ovo polje je obavezno!</p>}<br/>
                </div>
                <div class="float-child" style={{width:"30%"}}>
                    <label>Prezime </label>
                    <input class="form-control" type="text" onChange={(event)=>setNewPrezime(event.target.value)}/>{(newPrezime.length==0)&&<p style={{color:"red", display:"inline"}}>*Ovo polje je obavezno!</p>}<br/>
                </div>
                <div class="float-child" style={{width:"30%"}}>
                    <label>Starost </label>
                    <input class="form-control" type="number" onChange={(event)=>setNewStarost(event.target.value)}/>{(newStarost.length==0)&&<p style={{color:"red", display:"inline"}}>*Ovo polje je obavezno!</p>}<br/>
                </div>
                <div class="float-child" style={{width:"10%"}}>
                    <label>Pol:</label> 
                    <br/>
                    <input name="pol" type="radio" value="M" checked={true} onChange={(event)=>setNewPol(event.target.value)} />M <br/>
                    <input style={{marginBottom:"50px"}} name="pol" type="radio" value="Z"  onChange={(event)=>setNewPol(event.target.value)} />Z<br/>
                    <br/>
                </div>
                
            </div>
            <br/>
            <div class="float-container" style={{textAlign:"center"}}>
                <div class="float-child" style={{width:"30%"}}>
                    <label>Firma:</label><br/>
                    <select class="form-control" onChange={(event)=>setIdFirme(event.target.value)}> {alertFirma && <p style={{color:"red", display:"inline"}}>*Izaberite firmu!</p>}
                        <option value="-1">Izaberi zeljenu firmu</option>
                        {firme.map(firma=>{
                            return <option value={firma.id}>{firma.naziv}</option>  
                        })}
                    </select><br/>
                </div>
                <div class="float-child" style={{width:"30%"}}>
                    <label>Datum od:</label> <br/>
                    <input class="form-control" type="text"onChange={(event)=>setDatumOd(event.target.value)}/><br/>
                </div>
                <div class="float-child" style={{width:"30%"}}>
                    <label>Pozicija:</label><br/>
                    <input class="form-control" type="text" onChange={(event)=>setPozicija(event.target.value)}/><br/>
                </div>
                <div>
                    <button type="submit" class="btn btn-primary" disabled={((newIme.length>0) && (newPrezime.length>0) && (newStarost.length>0)) ? false : true} onClick={async ()=>await CreateZaposleniFunction()}>Dodaj zaposlenog</button><br/><br/>
                </div>
            </div>
        </div>
    )

    async function CreateZaposleniFunction(){
        setShowSpinner(true);

        if(idFirme==-1){
            setAlertFirma(true);
        }else{ 
        await fetch("https://localhost:44392/Zaposleni/kreirajZaposlenogSaPodacima/",{
            method:"POST",
            headers:{"Content-Type":"application/json"},
            body:JSON.stringify({"ime":newIme,"prezime":newPrezime,"pol":newPol.charAt(0),"starost":parseInt(newStarost),"idFirme":parseInt(idFirme),"datum_od":datumOd,"pozicija":pozicija})
        }).then(p=>{
            if(p.ok){
                window.location.reload(false);
            }
        }).catch(ex=>{
            console.log("EX:"+ex);
        })
    }
        setShowSpinner(false);
    }
}

export default CreateZaposleni;