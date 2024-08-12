import { ApiClient } from './ApiClient'

class Api extends ApiClient {
  constructor() {
    super("https://localhost:7198")
  }
}

const client = new Api()
export default client
