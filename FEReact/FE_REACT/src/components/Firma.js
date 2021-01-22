import React, {useState} from 'react'
import Spinner from './Spinner';


function Firma({firma}) {

    const [zaposleniFirme, setZaposleneFirme]=useState([]);
    const [showSpinner,setShowSpinner]=useState(false);

    return(
        <div class="float-child card" style={{width:"30%", marginLeft:"20px", marginTop:"20px"}}>
            <div class="card-body">
              {showSpinner && <Spinner/>}
              <h1 style={{color:"#3399FF"}}>{firma.naziv} <a href="#"><span className="material-icons" style={{"color":"red"}} onClick={async ()=>await ObrisiFirmu()}>delete</span></a></h1>  
              <label>PIB: {firma.pib}</label><br/>
              <label>Adresa: {firma.adresa}</label><br/>
              <select class="form-control" onChange={async (event)=>await onChange(event)}>
                  <option value={0}>Prikazi zaposlene</option>
                  <option value={1}>Svi zaposleni ikad</option>
                  <option value={2}>Trenutno zaposleni</option>
              </select>
              <ul class="list-group list-group-flush">
              {zaposleniFirme!=[] && zaposleniFirme.map(zaposleni=>{
                  return <li class="list-group-item" key={zaposleni["id"]}>{zaposleni["ime"] + " "+ zaposleni["prezime"]}</li>
              })}
              </ul>
            </div>
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
                let lista=new Array();
                data.forEach(el=>{
                    if(lista.find(value=>value.id==el.id)==undefined || lista.find(value=>value.id==el.id)==null){
                        lista.push(el);
                    }
                })
                setZaposleneFirme(lista);
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
                    let lista=new Array();
                    data.forEach(el=>{
                        if(lista.find(value=>value.id==el.id)==undefined || lista.find(value=>value.id==el.id)==null){
                            lista.push(el);
                        }
                    })
                    setZaposleneFirme(lista);
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