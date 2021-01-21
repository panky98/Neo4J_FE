import React, {useState} from 'react'
import useFetch from '../services/useFetch.js';

import Spinner from '../components/Spinner.js';
import Firma from '../components/Firma.js';
import CreateFirma from '../components/CreateFirma.js';



function Firme() {  
    const {data:firme, loading, error}=useFetch("Firma/vratiSveFirme");
    const [showForm,setShowForm]=useState(false);

    if(error) throw error;
    if(loading) return <Spinner/>
    console.log(firme);
    return (
        <div>
            <div style={{textAlign:"center", marginTop:"20px"}}>
                <button type="submit" class="btn btn-primary" onClick={()=>setShowForm(!showForm)}>Kreiraj firmu</button>
            </div>
            {showForm&& <CreateFirma/>}
            {firme.map(f=>
            {
                return <Firma key={f.id} firma={f}/>
            })}
        </div>
    )
}

export default Firme
