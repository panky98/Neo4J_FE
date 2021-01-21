import React, {useState} from 'react'
import DateTimePicker from 'react-datetime-picker';
import useFetch from '../services/useFetch.js';
import Spinner from '../components/Spinner.js';

//const kategorijeNagrada=["Firma godine", "Najbolji sportisti", "Najbolji projekat"];
//const naziviNagrada=["Nagrada za 2021", "Nagrada za 2019","Nagrada za 2018","Nagrada za 2017","Nagrada za 2016"];


function DodajNagradu({pom, promeniPom}) {

    async function handleSubmit()
    {
        

        await fetch("https://localhost:44392/Osvojila/dodeliNagraduFirmi/"+firma+"/"+nagrada,{
        method:"POST",
        headers:{"Content-Type":"application/json"} 
    }).then(p=>{
        if(p.ok){
            console.log("Uspesno dodato!");
            promeniPom();
            
        }
    }).catch(exc=>{
        console.log(exc);
    });
    window.location.reload(false);
    }
    const [nagrada, setNagrada]=useState(-1);
   
    const [firma, setFirma]=useState(-1);

    const {data:sveFirme, loading, error}=useFetch("Firma/vratiSveFirme");
    const {data:nagrade, loadingNagrade, errorNagrade}=useFetch("Nagrada/vratiSveNagrade");


    if(error) throw error;
    if(loading) return <Spinner/>

    if(errorNagrade) throw error;
    if(loadingNagrade) return <Spinner/>

    if(nagrade===null) return <Spinner/>
    return (
        <div>

            <div style={{textAlign:"center"}}>
                <h4>Ovde mozete dodeliti konkretnoj firmi nagradu:</h4>
            </div>
            <form class="float-container" style={{textAlign:"center"}} onSubmit={()=>handleSubmit()}>

                <div class="float-child" style={{width:"50%"}}>
                    <label>Nagrada:</label><br/>
                    <select class="form-control" value={nagrada} onChange={(e)=>setNagrada(e.target.value)}>
                    <option key={-1} value={-1}></option>
                        {nagrade.map(n=>{
                            return(
                                <option key={n.id} value={n.id}>{n.naziv}</option>
                            )
                        })}
                    </select>
                </div>

                <div class="float-child" style={{width:"50%"}}>
                    <label>Firma:</label><br/>
                    <select class="form-control" value={firma} onChange={(ev)=>setFirma(ev.target.value)}>
                        <option key={-1} value={-1}></option>
                        {sveFirme.map(f=>{
                            return(
                                <option key={f.id} value={f.id}>{f.naziv}</option>
                            )
                        })}
                    </select>
                </div>
                <button type="submit" class="btn btn-primary">Dodaj nagradu firmi</button>
            </form>
        </div>       
    )
}

export default DodajNagradu
