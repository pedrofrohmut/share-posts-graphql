export default interface IApplicationUserState {
  id: string
  userName?: string
  email: string
  emailConfirmed: boolean
  authenticationToken: string
  isAuthenticated: boolean
}
