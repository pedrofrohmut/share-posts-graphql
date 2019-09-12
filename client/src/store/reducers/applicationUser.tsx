import { Reducer } from "redux"
import { USER_LOGGED_IN, USER_LOGGED_OUT, UserActionTypes } from "../types/applicationUser"
import IApplicationUserState from "../states/IApplicationUserState"

const INITIAL_STATE: IApplicationUserState = {
  id: "",
  email: "",
  emailConfirmed: false,
  authenticationToken: "",
  isAuthenticated: false
}

export default (state = INITIAL_STATE, action: UserActionTypes): IApplicationUserState => {
  const newState: IApplicationUserState = JSON.parse(JSON.stringify(state))
  switch (action.type) {
    case USER_LOGGED_IN:
      return { ...newState, ...action.payload }
    case USER_LOGGED_OUT:
      return { ...newState, ...action.payload }
    default:
      return newState
  }
}
