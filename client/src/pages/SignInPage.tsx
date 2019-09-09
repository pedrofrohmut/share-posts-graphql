import React, { useState } from "react"
import IUser from "../models/iuser"
import IUserErrors from "../models/iuserErrors"
import InlineError from "../components/messages/InlineError"

const INITIAL_USER: IUser = {
  email: "",
  password: ""
}

const INITIAL_USER_ERRORS: IUserErrors = {
  email: "",
  password: ""
}

const SignInPage: React.FC = () => {
  const [user, setUser] = useState<IUser>(INITIAL_USER)
  const [errors, setErrors] = useState<IUserErrors>(INITIAL_USER_ERRORS)
  if (!user) {
    return null
  }
  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault()
    console.log("Submit!")
  }
  return (
    <div className="container SignInPage">
      <div className="row">
        <div className="col-md-10 mx-auto">
          <div className="card card-body bg-light mt-5">
            <h1>Sign In | User Authentication</h1>
            <p>
              Provide your credentials so you can sign in as a registered user
            </p>
            <form onSubmit={handleSubmit}>
              <div className="form-group">
                <label htmlFor="email">
                  E-mail <sup>*</sup>
                </label>
                <input
                  id="email"
                  type="text"
                  name="email"
                  value={user.email}
                  onChange={e => setUser({ ...user, email: e.target.value })}
                  className={`form-control form-control-lg ${errors.email &&
                    "is-invalid"}`}
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
                  value={user.password}
                  onChange={e => setUser({ ...user, password: e.target.name })}
                  className={`form-control form-control-lg ${errors.password &&
                    "is-invalid"}`}
                />
                {errors.password && <InlineError text={errors.password} />}
              </div>

              <button type="submit" className="btn btn-primary">
                <i className="fa fa-send"></i> Sign In
              </button>
            </form>
          </div>
        </div>
      </div>
    </div>
  )
}

export default SignInPage
