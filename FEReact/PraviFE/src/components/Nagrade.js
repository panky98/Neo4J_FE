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
                    <div key={n.id}>
                        <h2>{n.naziv}</h2>
                        <p>Kategorija: {n.kategorija}</p>
                        <p>Datum: {n.datum}</p>

                        {/*n.firme.length===1 && <p>Firma: {n.firme[0].naziv}</p>*/}
                        {n.firme.length>=1 && <ul>
                            {n.firme.map(f=>{
                                return <li>{f.naziv}</li>})}
                        
                        </ul>}
                             
                        

                        
                    </div>
                )
            })}
        </div>
    )
}

export default Nagrade
