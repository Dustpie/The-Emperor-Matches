import { inject, Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root',
})
export class BusyService {
  busyRquestCount = 0;
  private spinnerService = inject(NgxSpinnerService);

  busy() {
    this.busyRquestCount++;
    this.spinnerService.show(undefined, {
      type: 'pacman',
      bdColor: 'rgba(255, 255, 255, 0)',
      color: '#fff',
      size: 'medium',
    });
  }

  idle() {
    this.busyRquestCount--;
    if (this.busyRquestCount <= 0) {
      this.busyRquestCount = 0;
      this.spinnerService.hide();
    }
  }
}
