import React from "react"
import { BrowserRouter as Router, Route, Link } from "react-router-dom"
import "./App.css"

import Header from "./components/layout/Header"
import Footer from "./components/layout/Footer"
import HomePage from "./pages/HomePage"
import AboutPage from "./pages/AboutPage"
import SignInPage from "./pages/SignInPage"
import SignUpPage from "./pages/SignUpPage"

const App: React.FC = () => {
  return (
    <Router>
      <div className="App">
        <header>
          <Header />
        </header>
        <main>
          <Route exact path="/" component={HomePage} />
          <Route exact path="/about" component={AboutPage} />
          <Route exact path="/signin" component={SignInPage} />
          <Route exact path="/signup" component={SignUpPage} />
        </main>
        <footer>
          <Footer />
        </footer>
      </div>
    </Router>
  )
}

export default App
