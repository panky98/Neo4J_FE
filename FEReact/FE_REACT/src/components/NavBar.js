import React from 'react'

import { Link, NavLink } from "react-router-dom";

function NavBar() {
    return (

      <nav>
        <ul>
          <li>
            <Link to="/">
                Home
            </Link>
          </li>
          <li>
            <NavLink to="/projekti">
              Projekti
            </NavLink>
          </li>
          
        </ul>
      </nav>

    )
}

export default NavBar
