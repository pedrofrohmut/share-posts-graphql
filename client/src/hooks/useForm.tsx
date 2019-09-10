import React from "react"

const useForm = <T, K>(
  initialState: T,
  initialErrors: K,
  onValidate: (formData: T) => K,
  onSubmit: (formData: T) => any
): Array<any> => {
  const [values, setValues] = React.useState(initialState)
  const [errors, setErrors] = React.useState(initialErrors)
  const [isLoading, setIsLoading] = React.useState(false)
  const hasErrors = (): boolean => !Object.values(errors).every(error => error === "")
  const handleChange = (event: React.ChangeEvent<HTMLInputElement>): void => {
    const { name } = event.target
    const value = event.target.checked ? true : event.target.value
    setValues({ ...values, [name]: value })
  }
  const handleBlur = (event: React.FocusEvent<HTMLInputElement>): void => {
    const updatedErrors = onValidate(values)
    setErrors(updatedErrors)
  }
  const handleSubmit = (event: React.FormEvent<HTMLFormElement>): any => {
    event.preventDefault()
    if (!hasErrors()) {
      setIsLoading(true)
      // TODO: check it again when connected to redux thunk
      return onSubmit(values)
    }
    return undefined
  }
  return [values, errors, hasErrors, isLoading, handleChange, handleBlur, handleSubmit]
}

export default useForm
