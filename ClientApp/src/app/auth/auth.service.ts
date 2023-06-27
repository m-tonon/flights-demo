  import { Injectable } from '@angular/core';

  interface User {
    email: string | null | undefined;
  }

  @Injectable({
    providedIn: 'root'
  })
  export class AuthService {
    currentUser?: User;

    constructor() { }

    loginUser(user: User) {
      console.log('Log in user with email', user.email);
      this.currentUser = user;
    }
  }
