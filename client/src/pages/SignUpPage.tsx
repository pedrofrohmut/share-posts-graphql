import React, { useState } from "react"
import { Link } from "react-router-dom"
import IUser from "../models/iuser"
import IUserErrors from "../models/iuserErrors"
import InlineError from "../components/messages/InlineError"

const INITIAL_USER: IUser = {
  userName: "",
  email: "",
  password: "",
  confirmPassword: ""
}

const INITIAL_USER_ERRORS: IUserErrors = {
  userName: "",
  email: "",
  password: "",
  confirmPassword: ""
}

const SignUpPage: React.FC = () => {
  const [user, setUser] = useState<IUser>(INITIAL_USER)
  const [errors, setErrors] = useState<IUserErrors>(INITIAL_USER_ERRORS)
  if (!user) {
    return null
  }
  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault()
    console.log("Submit!!")
  }
  return (
    <div className="container SignUpPage">
      <div className="row">
        <div className="col-md-10 mx-auto">
          <div className="card card-body bg-light mt-5">
            <h1>Sign Up | Create a new User</h1>
            <p>Please fill put this form to register with us</p>
            <form onSubmit={handleSubmit}>
              <div className="form-group">
                <label htmlFor="userName">UserName</label>
                <input
                  id="userName"
                  type="text"
                  name="userName"
                  className={`form-control form-control-lg ${errors.userName &&
                    "is-invalid"}`}
                  value={user.userName}
                  onChange={e => setUser({ ...user, userName: e.target.value })}
                />
                {errors.userName && <InlineError text={errors.userName} />}
              </div>

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

              <div className="form-group">
                <label htmlFor="confirmPassword">
                  Confirm Password <sup>*</sup>
                </label>
                <input
                  id="confirmPassword"
                  type="password"
                  name="confirmPassword"
                  value={user.confirmPassword}
                  onChange={e =>
                    setUser({ ...user, confirmPassword: e.target.name })
                  }
                  className={`form-control form-control-lg ${errors.confirmPassword &&
                    "is-invalid"}`}
                />
                {errors.confirmPassword && (
                  <InlineError text={errors.confirmPassword} />
                )}
              </div>

              <div className="row">
                <div className="col">
                  <button type="submit" className="btn btn-primary">
                    <i className="fa fa-send"></i> Sign Up
                  </button>
                </div>
                <div className="col">
                  <Link to="/signin" className="btn btn-light">
                    Have an account? Sign In Here.
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

export default SignUpPage
