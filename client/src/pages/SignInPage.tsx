import React from "react"
import { Link } from "react-router-dom"
import useForm from "../hooks/useForm"
import IUser, { IUserErrors, getEmailErrors, getPasswordErrors } from "../models/iuser"
import IApplicationUserState from "../store/states/IApplicationUserState"
import InlineError from "../components/messages/InlineError"

import { connect } from "react-redux"
import { applicationUserLoggedInThunk } from "../store/thunks/applicationUser"

import { gql } from "apollo-boost"
import { useMutation } from "@apollo/react-hooks"

const SIGN_IN_APPLICATION_USER = gql`
  mutation SignInUser($email: String!, $password: String!) {
    signIn(email: $email, password: $password) {
      id
      userName
      email
      emailConfirmed
      authenticationToken
    }
  }
`

const INITIAL_USER: IUser = {
  email: "",
  password: ""
}

const INITIAL_USER_ERRORS: IUserErrors = {
  email: "",
  password: ""
}

const validateUserSignIn = (formData: IUser): IUserErrors => ({
  email: getEmailErrors(formData.email),
  password: getPasswordErrors(formData.password)
})

interface Props {
  thunkSignIn: (signedInUser: IApplicationUserState) => void
  history: {
    push: (location: string) => void
  }
}

const SignInPage: React.FC<Props> = ({ thunkSignIn, history }) => {
  const [signIn] = useMutation(SIGN_IN_APPLICATION_USER)
  const onSubmit = (values: IUser): void => {
    signIn({ variables: { email: values.email, password: values.password } }).then(res => {
      const authenticatedUser: IApplicationUserState = res.data.signIn
      authenticatedUser.isAuthenticated = true
      thunkSignIn(authenticatedUser)
      history.push("/")
    })
  }
  const [values, errors, hasErrors, isLoading, handleChange, handleBlur, handleSubmit] = useForm<
    IUser,
    IUserErrors
  >(INITIAL_USER, INITIAL_USER_ERRORS, validateUserSignIn, onSubmit)
  return (
    <div className="container SignInPage">
      <div className="row">
        <div className="col-lg-7 col-md-10 mx-auto">
          <div className="card card-body bg-light">
            <h1>Sign In | User Authentication</h1>
            <p>Provide your credentials so you can sign in as a registered user</p>
            <form onSubmit={handleSubmit}>
              <div className="form-group">
                <label htmlFor="email">
                  E-mail <sup>*</sup>
                </label>
                <input
                  id="email"
                  type="text"
                  name="email"
                  value={values.email}
                  onChange={handleChange}
                  onKeyUp={handleBlur}
                  onBlur={handleBlur}
                  className={`form-control form-control-lg ${errors.email && "is-invalid"}`}
                />
                {errors.email && <InlineError text={errors.email} />}
              </div>

              <div className="form-group">
                <label htmlFor="password">
                  Password <sup>*</sup>
                </label>
                <input
                  id="password"
                  type="password"
                  name="password"
                  value={values.password}
                  onChange={handleChange}
                  onKeyUp={handleBlur}
                  onBlur={handleBlur}
                  className={`form-control form-control-lg ${errors.password && "is-invalid"}`}
                />
                {errors.password && <InlineError text={errors.password} />}
              </div>

              <div className="row">
                <div className="col">
                  <button type="submit" disabled={hasErrors()} className="btn btn-primary">
                    <i className="fa fa-send"></i> Sign In
                  </button>
                </div>
                <div className="col">
                  <Link to="/signup" className="btn btn-light">
                    No Account? Register here.
                  </Link>
                </div>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  )
}

export default connect(
  null,
  { thunkSignIn: applicationUserLoggedInThunk }
)(SignInPage)
