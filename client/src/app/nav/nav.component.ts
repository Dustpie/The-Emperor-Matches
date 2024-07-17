import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { TitleCasePipe } from '@angular/common';

@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [
    FormsModule,
    BsDropdownModule,
    RouterLink,
    RouterLinkActive,
    TitleCasePipe,
  ],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css',
})
export class NavComponent {
  accountService = inject(AccountService);
  private router = inject(Router);
  private snackbar = inject(ToastrService);
  stateLog: any = {};
  model: any = {};

  login() {
    this.accountService.login(this.model).subscribe({
      next: (response) => {
        this.router.navigateByUrl('/members');
        this.snackbar.success('Login was succesful');
        console.log(this.model);
      },
      error: (error) => this.snackbar.error(error.error),
    });
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }
}
