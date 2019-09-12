import { combineReducers } from "redux"

import applicationUser from "./reducers/applicationUser"

const rootReducer = combineReducers({
  applicationUser
})

export type AppState = ReturnType<typeof rootReducer>

export default rootReducer
