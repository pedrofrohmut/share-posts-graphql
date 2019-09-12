import IApplicationUserState from "../states/IApplicationUserState"
import { applicationUserLoggedIn, applicationUserLoggedOut } from "../actions/applicationUser"
import { LS_JWT } from "../../constants/localStorage"
import { ThunkAction } from "redux-thunk"
import { Action, Dispatch } from "redux"
import { AppState } from "../rootReducer"

export const applicationUserLoggedInThunk = (
  credentials: IApplicationUserState
): ThunkAction<void, AppState, null, Action> => (dispatch: Dispatch): void => {
  localStorage.setItem(LS_JWT, credentials.authenticationToken)
  dispatch(applicationUserLoggedIn(credentials))
}

export const applicationUserLoggedOutThunk = (): ThunkAction<void, AppState, null, Action> => (
  dispatch: Dispatch
): void => {
  localStorage.removeItem(LS_JWT)
  dispatch(applicationUserLoggedOut())
}
