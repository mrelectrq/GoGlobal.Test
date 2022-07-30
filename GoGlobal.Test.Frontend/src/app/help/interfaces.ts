export interface User {
    username: string
    password: string
    //returnSecureToken?: boolean
  }
  
  export interface FbAuthResponse {
    idToken: string
    expiresIn: string
  }
  
  export interface IPost {
    repositoryId?:string,
    repositoryName: string,
    avatar: string,
    repositoryDescription: string,
  }
  
  export interface FbCreateResponse {
    name: string
  }

  export interface Environment {
    apiKeyFb: string,
    production: boolean,
    fbDbUrl: string,
    apiKey: string
  }