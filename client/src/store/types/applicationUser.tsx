import IApplicationUserState from "../states/IApplicationUserState"

export const USER_LOGGED_IN = "@applicationUser/USER_LOGGED_IN"
export const USER_LOGGED_OUT = "@applicationUser/USER_LOGGED_OUT"

interface UserLoggedInAction {
  type: typeof USER_LOGGED_IN
  payload: {
    id: string
    email: string
    emailConfirmed: boolean
    authenticationToken: string
    isAuthenticated: boolean
  }
}

interface UserLoggedOutAction {
  type: typeof USER_LOGGED_OUT
  payload: {
    isAuthenticated: boolean
  }
}

export type UserActionTypes = UserLoggedInAction | UserLoggedOutAction
