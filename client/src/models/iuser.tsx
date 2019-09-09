import validator from "validator"

export default interface IUser {
  userName?: string
  email: string
  password: string
  confirmPassword?: string
}

export interface IUserErrors {
  userName?: string
  email: string
  password: string
  confirmPassword?: string
}

export const isValidUserName = (userName: string): boolean =>
  validator.isLength(userName, { min: 2, max: 20 }) &&
  validator.isAlphanumeric(userName)

export const getUserNameErrors = (userName: string): string =>
  !validator.isLength(userName, { min: 2, max: 20 })
    ? "UserName must be between 2 and 20 characters long"
    : !validator.isAlphanumeric(userName)
    ? "UserName cannot contain special character, only letters and numbers"
    : ""

export const isValidEmail = (email: string): boolean =>
  !validator.isEmpty(email) &&
  validator.isLength(email, { min: 0, max: 64 }) &&
  validator.isEmail(email)

export const getEmailErrors = (email: string): string =>
  validator.isEmpty(email)
    ? "E-mail cannot not be blank"
    : !validator.isLength(email, { min: 6, max: 64 })
    ? "E-mail must be between 6 and 64 characters long"
    : !validator.isEmail(email)
    ? "Invalid format of e-mail"
    : ""

export const isValidPassword = (password: string): boolean =>
  !validator.isEmpty(password) &&
  validator.isLength(password, { min: 6, max: 20 })

export const getPasswordErrors = (password: string): string =>
  validator.isEmpty(password)
    ? "Password cannot be blank"
    : !validator.isLength(password, { min: 6, max: 20 })
    ? "Password must be between 6 and 20 characters long"
    : ""
