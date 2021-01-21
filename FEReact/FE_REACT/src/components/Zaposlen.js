import React, {useState} from 'react'
import Spinner from './Spinner';
import FormPromeniRadnoMesto from"./FormPromeniRadnoMesto.js"

function Zaposlen({zaposlen}) {
    const [showSpinner,setShowSpinner]=useState(false);
    const [showForm,setShowForm]=useState(false);
    return(
        <div class="float-child card" style={{width:"30%", marginLeft:"20px", marginTop:"20px"}}>
            <div class="card-body">
              {showSpinner && <Spinner/>}
              <h4 style={{color:"#3399FF", textAlign:"center"}}>{zaposlen.ime}  {zaposlen.prezime}</h4>
             
              <label>Pol: </label> <label style={{fontStyle:"italic"}}>{zaposlen.pol}</label><br/>
              <label>Starost: </label> <label style={{fontStyle:"italic"}}>{zaposlen.starost}</label><br/>
              <label>Trenutna kompanija: </label> <label style={{fontStyle:"italic"}}>{(zaposlen.istorija.find((el)=>el.datum_do==="")!=null && zaposlen.istorija.find((el)=>el.datum_do==="")!=undefined)?zaposlen.istorija.find((el)=>el.datum_do==="").naziv:"Nezaposlen"}</label>
              <br/>
              <label>Istorija rada:</label>
              <ul>
                  {zaposlen.istorija!=undefined && zaposlen.istorija!=null && zaposlen.istorija.map((el)=>{
                      return <li >Firma: {el.naziv} Datum od:  {el.datum_od} Datum do: {el.datum_do}</li>
                  })}
              </ul><br/>
              <button type="submit" class="btn btn-primary" onClick={()=>setShowForm(!showForm)}>Promeni radno mesto:</button><br/>
              {showForm && <FormPromeniRadnoMesto idZaposlenog={zaposlen.id}/>}
              <br/><br/>
            </div>
        </div>
    )
}

export default Zaposlen;