import React, {useState} from 'react'

import Spinner from '../components/Spinner.js';
import useFetch from '../services/useFetch.js';
import DodajNagradu from './DodajNagradu.js';
import DodajNoviTipNagrade from './DodajNoviTipNagrade.js';

function Nagrade() {

    //const {data:nagrade, loading, error}=useFetch("Osvojila/vratiSveOsvojeneNagrade");
    const {data:nagrade, loading, error}=useFetch("Nagrada/vratiSveNagrade");

    const [pom, setPom]=useState(true);

    const promeniPom=()=>{setPom(!pom)};

    if(error) throw error;
    if(loading) return <Spinner/>

    console.log(nagrade);
    return (
        <div>
            <DodajNoviTipNagrade/>
            <DodajNagradu pom={pom} promeniPom={promeniPom}/>
            {nagrade.map(n=>{
                return(
                    
                    <div class="float-child card" style={{width:"30%", marginLeft:"20px", marginTop:"20px"}} key={n.id}>
                        <div class="card-body">
                            <h2 style={{color:"#3399FF"}}>{n.naziv}</h2>
                            <label>Kategorija: </label><label style={{fontStyle:"italic"}}>{n.kategorija}</label>
                            <br/>
                            <label>Datum: </label><label style={{fontStyle:"italic"}}>{n.datum}</label>

                            {/*n.firme.length===1 && <p>Firma: {n.firme[0].naziv}</p>*/}
                            <h5>Ko je sve osvojio nagradu</h5>
                            {n.firme.length>=1 && <ul class="list-group list-group-flush">
                                {n.firme.map(f=>{
                                    return <li class="list-group-item">{f.naziv}</li>})}
                            
                            </ul>}
                        </div>
                    </div>
                )
            })}
        </div>
    )
}

export default Nagrade
