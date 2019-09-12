// TODO: implement interface with graphql queries and mutations
import IUser from "../models/iuser"
import IApplicationUserState from "../store/states/IApplicationUserState"

export default {
  applicationUser: {
    signup: () => {},
    signin: (credentials: IUser) => {
      const applicationUser: IApplicationUserState = {
        id: "123",
        email: "test@test.com",
        emailConfirmed: false,
        authenticationToken: "Hard_Coded_token",
        isAuthenticated: true
      }
      console.log("API", credentials)
      return Promise.resolve(applicationUser)
    }
  },
  posts: {
    create: () => {},
    read: () => {},
    readAll: () => {},
    update: () => {},
    delete: () => {}
  }
}
