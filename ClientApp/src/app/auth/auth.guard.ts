import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../auth/auth.service';
import { inject } from '@angular/core';

export const authGuard: CanActivateFn = (route, state) => {
  
  const currentUser = inject(AuthService).currentUser;

  if (!currentUser)
  inject(Router).navigate(['/register-passenger', { requestedUrl: state.url }]);
  
  return true;
};
