import React from "react"
import { NavLink } from "react-router-dom"

const Header: React.FC = () => (
  <nav className="navbar navbar-expand-lg navbar-dark bg-dark mb-3">
    <div className="container">
      <a className="navbar-brand mr-5" href="/">
        SharePosts
      </a>
      <button
        className="navbar-toggler"
        type="button"
        data-toggle="collapse"
        data-target="#navbarSupportedContent"
        aria-controls="navbarSupportedContent"
        aria-expanded="false"
        aria-label="Toggle navigation"
      >
        <span className="navbar-toggler-icon"></span>
      </button>

      <div className="collapse navbar-collapse" id="navbarSupportedContent">
        <ul className="navbar-nav mr-auto">
          <li className="nav-item">
            <NavLink exact to="/" activeClassName="active" className="nav-link">
              Home
            </NavLink>
          </li>
          <li className="nav-item">
            <NavLink to="/about" activeClassName="active" className="nav-link">
              About
            </NavLink>
          </li>
        </ul>

        <ul className="ml-auto navbar-nav">
          <li className="nav-item">
            <NavLink to="/signin" activeClassName="active" className="nav-link">
              Sign In
            </NavLink>
          </li>
          <li className="nav-item">
            <NavLink to="/signup" activeClassName="active" className="nav-link">
              Sign Up
            </NavLink>
          </li>
        </ul>
      </div>
    </div>
  </nav>
)

export default Header
