import IApplicationUserState from "../states/IApplicationUserState"
import { applicationUserLoggedIn, applicationUserLoggedOut } from "../actions/applicationUser"
import IUser from "../../models/iuser"
import { LS_JWT } from "../../constants/localStorage"
import { ThunkAction } from "redux-thunk"
import { Action } from "redux"
import { AppState } from "../rootReducer"
import api from "../../api/shareposts"
// TODO: import { setAuthorizationHeaders } from "../../utils/authorizationHeaders"

export const applicationUserLoggedInThunk = (
  credentials: IUser
): ThunkAction<void, AppState, null, Action> => (dispatch: any) =>
  api.applicationUser.signin(credentials).then((applicationUser: IApplicationUserState) => {
    console.log("THUNKS", credentials)
    const { id, email, emailConfirmed, authenticationToken, isAuthenticated } = applicationUser
    localStorage.setItem(LS_JWT, authenticationToken)
    // TODO: setAuthorizationHeaders(token)
    const authenticatedUser: IApplicationUserState = {
      id,
      email,
      emailConfirmed,
      authenticationToken,
      isAuthenticated: true
    }
    dispatch(applicationUserLoggedIn(authenticatedUser))
  })
