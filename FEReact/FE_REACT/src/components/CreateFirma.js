import React, {useState} from 'react'
import useFetch from '../services/useFetch.js';
import Spinner from '../components/Spinner.js';


function CreateFirma(){
    const [newPib, setNewPib]=useState("");
    const [newNaziv, setNewNaziv]=useState("");
    const [newAdresa, setNewAdresa]=useState("");
    const [showSpinner, setShowSpinner]=useState(false);


    return(
        <div>
            {showSpinner && <Spinner/>}
            <div class="float-container" style={{textAlign:"center"}}>
                <div class="float-child" style={{width:"30%"}}>  
                    <label>PIB: </label> 
                    <input class="form-control" type="number" onChange={(event)=>setNewPib(event.target.value)}/> {(newPib.length!=13)&&<p style={{color:"red", display:"inline"}}>Duzina PIB-a mora biti 13 karaktera!</p>}<br/>
                </div>
                <div class="float-child" style={{width:"30%"}}>
                    <label>Naziv: </label> 
                    <input class="form-control" type="text" onChange={(event)=>setNewNaziv(event.target.value)}/><br/>
                </div>
                <div class="float-child" style={{width:"30%"}}>
                    <label>Adresa: </label> 
                    <input class="form-control" type="text" onChange={(event)=>setNewAdresa(event.target.value)}/><br/> <br/>
                </div>
            </div>
            <br/>
            <div style={{textAlign:"center"}}>
                <button type="submit" class="btn btn-primary" disabled={((newPib.length==13) && (newNaziv.length>0) && (newAdresa.length>0)) ? false : true} onClick={async ()=>{await CreateCompany();}}>Dodaj</button>
            </div>
        </div>
    )

    async function CreateCompany(){
        setShowSpinner(true);
        await fetch("https://localhost:44392/Firma/kreirajFirmu",{
            method:"POST",
            headers:{"Content-Type":"application/json"},
            body:JSON.stringify({"pib":newPib,"naziv":newNaziv,"adresa":newAdresa})
        }).then(p=>{
            if(p.ok){
                window.location.reload(false);
            }
        }).catch(ex=>{
            console.log("EX:"+ex);
        })
        setShowSpinner(false);
    }
}

export default CreateFirma;