export interface User {
    email: string
    password: string
    returnSecureToken?: boolean
  }
  
  export interface FbAuthResponse {
    idToken: string
    expiresIn: string
  }
  
  export interface Post {
    id?: string
    title: string
    city: string
    description: string
    date: Date,
    tags: string
    weather: {
      icon:string,
      feels_like: number,
      temp: number,
      description: string
    }
  }
  
  export interface FbCreateResponse {
    name: string
  }

  export interface Environment {
    apiKeyFb: string,
    production: boolean,
    fbDbUrl: string,
    apiKey: string,
    apiUrl: string
  }