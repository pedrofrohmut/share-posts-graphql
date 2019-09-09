import React from "react"

interface Props {
  text: string
}

const InlineError: React.FC<Props> = ({ text }) => (
  <div className="text-danger py-2 px-4">{text}</div>
)

export default InlineError
