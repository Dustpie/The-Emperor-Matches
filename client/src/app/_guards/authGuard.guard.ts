import { CanActivateFn } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { inject } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

/**
 * This guard checks if the user is authenticated.
 * If the user is not authenticated, it displays a message and returns false.
 */

export const authGuard: CanActivateFn = () => {
  const accountService = inject(AccountService);
  const snackbar = inject(ToastrService);

  if (accountService.currentUser()) {
    return true;
  } else {
    snackbar.error('You shall not pass!');
    return false;
  }
};
