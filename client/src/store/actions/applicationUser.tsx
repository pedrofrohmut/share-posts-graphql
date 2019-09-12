import { USER_LOGGED_IN, USER_LOGGED_OUT, UserActionTypes } from "../types/applicationUser"
import IApplicationUserState from "../states/IApplicationUserState"

export const applicationUserLoggedIn = (user: IApplicationUserState): UserActionTypes => {
  const { id, email, emailConfirmed, authenticationToken } = user
  return {
    type: USER_LOGGED_IN,
    payload: {
      id,
      email,
      emailConfirmed,
      authenticationToken,
      isAuthenticated: true
    }
  }
}

export const applicationUserLoggedOut = (): UserActionTypes => ({
  type: USER_LOGGED_OUT,
  payload: {
    isAuthenticated: false
  }
})
