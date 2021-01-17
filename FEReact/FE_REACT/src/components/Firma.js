import React, {useState} from 'react'
import Spinner from './Spinner';


function Firma({firma}) {

    const [zaposleniFirme, setZaposleneFirme]=useState([]);
    const [showSpinner,setShowSpinner]=useState(false);

    return(
        <div>
              {showSpinner && <Spinner/>}
              <h1>{firma.naziv} <a href="#"><span className="material-icons" style={{"color":"red"}} onClick={async ()=>await ObrisiFirmu()}>delete</span></a></h1>  
              <label>PIB: {firma.pib}</label><br/>
              <label>Adresa: {firma.adresa}</label><br/>
              <select onChange={async (event)=>await onChange(event)}>
                  <option value={0}>Izaberi opciju</option>
                  <option value={1}>Svi zaposleni ikad</option>
                  <option value={2}>Trenutno zaposleni</option>
              </select>
              <ul>
              {zaposleniFirme!=[] && zaposleniFirme.map(zaposleni=>{
                  return <li key={zaposleni["id"]}>{zaposleni["ime"] + " "+ zaposleni["prezime"]}</li>
              })}
              </ul>
        </div>
    )

    async function onChange(event){
       if(event.target.value==0){
           setZaposleneFirme([]);
           return;
       }

       setShowSpinner(true);
       if(event.target.value==1){
       await fetch("https://localhost:44392/Zaposleni/vratiSveZaposlenePrekoIdFirme/"+firma.id,{
           method:"GET"
       }).then(p=>{
           if(p.ok){
               p.json().then(data=>{
                   setZaposleneFirme(data);
               })
           }
       }).catch(ex=>{
           console.log(ex);
       });
    }
    else{
        await fetch("https://localhost:44392/Zaposleni/vratiSveZaposlenePrekoIdFirmeTrenutnoZaposleni/"+firma.id,{
            method:"GET"
        }).then(p=>{
            if(p.ok){
                p.json().then(data=>{
                    setZaposleneFirme(data);
                })
            }
        }).catch(ex=>{
            console.log(ex);
        });
    }
       setShowSpinner(false);
    }

    async function ObrisiFirmu(){
        setShowSpinner(true);
        await fetch("https://localhost:44392/Firma/obrisiFirmu/"+firma.id,{
            method:"DELETE"
        }).then(p=>{
            if(p.ok){
                window.location.reload(false);
            }
        }).catch(ex=>{
            console.log(ex);
        });
        setShowSpinner(false);
    }
}

export default Firma;