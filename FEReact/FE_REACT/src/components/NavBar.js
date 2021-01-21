import React from 'react'

import { Link, NavLink } from "react-router-dom";

function NavBar() {
    return (

      <div>
      <nav class="navbar navbar-light bg-light">
        <ul>
          <li class="navbar-brand">
            <Link to="/">
                IT yellow pages
            </Link>
          </li>
          <li class="navbar-brand">
            <NavLink to="/projekti">
              Projekti
            </NavLink>
          </li>
          <li class="navbar-brand">
            <NavLink to="/firme">
              Firme
            </NavLink>
          </li>

          <li class="navbar-brand">
            <NavLink to="/zaposleni">
              Zaposleni
            </NavLink>
          </li>
		  
		  <li class="navbar-brand">
            <NavLink to="/nagrade">
              Nagrade
            </NavLink>
          </li>
          
        </ul>
      </nav>
      </div>
    )
}

export default NavBar
