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
            <button onClick={()=>setShowForm(!showForm)}>Dodaj zaposlenog</button><br/><br/>
            {showForm && <CreateZaposleni/>}
            {zaposleni.map((zaposlen)=>{
                return <Zaposlen key={zaposlen.id} zaposlen={zaposlen}/>
            })}
        </div>
    )
}

export default Zaposleni;