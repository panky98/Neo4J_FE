import React from 'react';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';
import './App.css';


//components
import Projekti from './components/Projekti.js';
import Projekat from './components/Projekat.js';
import NavBar from './components/NavBar.js';
import Error from './components/Error.js';
import Home from './components/Home.js';
import Firme from './components/Firme.js';
import Zaposleni from './components/Zaposleni';
import Nagrade from './components/Nagrade.js';



function App() {
  return (
    <Router>
    <NavBar/>
    <Switch>
      <Route exact path="/">
        <Home/>
      </Route>
	  
	  <Route exact path="/projekti/:id" component={Projekat}></Route>

      <Route path="/firme">
        <Firme/>
      </Route>
	  
	  <Route path="/nagrade">
        <Nagrade/>
      </Route>

      <Route path="/projekti">
        <Projekti/>
      </Route>

      <Route path="/zaposleni">
        <Zaposleni/>
      </Route>

      <Route  path="*">
        <Error />
      </Route>
    </Switch>
</Router>
  );
}

export default App;
