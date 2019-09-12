import React from "react"
import ReactDOM from "react-dom"
import "./index.css"
import App from "./App"
import store from "./store"
import { Provider } from "react-redux"
import ApolloClient from "apollo-boost"
import { ApolloProvider } from "@apollo/react-hooks"
import { LS_JWT } from "./constants/localStorage"

const client = new ApolloClient({
  uri: "http://localhost:5000/graphql",
  request: (operation): void => {
    const token = localStorage.getItem(LS_JWT)
    operation.setContext({
      headers: {
        authorization: token ? `Bearer ${token}` : ""
      }
    })
  }
})

ReactDOM.render(
  <ApolloProvider client={client}>
    <Provider store={store}>
      <App />
    </Provider>
  </ApolloProvider>,
  document.getElementById("root")
)
