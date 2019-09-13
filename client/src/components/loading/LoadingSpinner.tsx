import React from "react"

import { FontAwesomeIcon } from "@fortawesome/react-fontawesome"
import { faSpinner } from "@fortawesome/free-solid-svg-icons"

interface Props {
  text?: string
}

const LoadingSpinner: React.FC<Props> = ({ text = "Loading..." }) => (
  <div className="LoadingSpinner">
    <FontAwesomeIcon
      icon={faSpinner}
      pulse
      size="8x"
      style={{ animationDuration: "900ms", margin: "0 auto 2rem", display: "block" }}
    />
    <div className="text-center" style={{ fontSize: "1.5rem" }}>
      {text}
    </div>
  </div>
)

export default LoadingSpinner
