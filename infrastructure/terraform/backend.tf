terraform {
  cloud {
    organization = "sillock"

    workspaces {
      name = "sample-azure"
    }
  }
}