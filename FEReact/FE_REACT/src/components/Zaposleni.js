import React, {useState} from 'react'
import useFetch from '../services/useFetch.js';

import Spinner from '../components/Spinner.js';
import Zaposlen from '../components/Zaposlen.js';
import CreateZaposleni from '../components/CreateZaposleni.js';


function Zaposleni(){
    const {data:zaposleni, loading, error}=useFetch("Zaposleni/vratiSveZaposlene");
    const [showForm,setShowForm]=useState(false);
    
    if(error) throw error;
    if(loading) return <Spinner/>
    console.log(zaposleni);
    return(
        <div>
            <div style={{textAlign:"center", marginTop:"20px"}}>
                <button type="submit" class="btn btn-primary" onClick={()=>setShowForm(!showForm)}>Dodaj zaposlenog</button><br/><br/>
            </div>
            {showForm && <CreateZaposleni/>}
            {zaposleni.map((zaposlen)=>{
                return <Zaposlen key={zaposlen.id} zaposlen={zaposlen}/>
            })}
        </div>
    )
}

export default Zaposleni;