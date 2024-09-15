import {
  Component,
  HostListener,
  inject,
  OnInit,
  ViewChild,
} from '@angular/core';
import { Member } from '../../Models/Member';
import { AccountService } from '../../_services/account.service';
import { MembersService } from '../../_services/members.service';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { FormsModule, NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-member-edit',
  standalone: true,
  imports: [TabsModule, FormsModule],
  templateUrl: './member-edit.component.html',
  styleUrl: './member-edit.component.css',
})

/**
 * This component is responsible for editing the user's profile.
 * It displays the user's profile and allows the user to edit it.
 * It also displays a message if the user has unsaved changes.
 */
export class MemberEditComponent implements OnInit {
  @ViewChild('editForm') editForm?: NgForm; // get the formdata from the template
  @HostListener('window:beforeunload', ['$event']) notify($event: any) {
    // Browser emit events, we can listen to this event and display a message
    if (this.editForm?.dirty) {
      $event.returnValue = true;
    }
  }
  member?: Member;
  private accountService = inject(AccountService);
  private memberService = inject(MembersService);
  private toastr = inject(ToastrService);

  ngOnInit(): void {
    this.loadMember();
  }

  loadMember() {
    const user = this.accountService.currentUser();
    if (!user) return;
    this.memberService.getMember(user.username).subscribe({
      next: (fetchedMember) => (this.member = fetchedMember),
      error: (error) => this.toastr.error('Failed to load member data'),
    });
  }

  updateMember() {
    this.memberService.updateMember(this.editForm?.value).subscribe({
      // update the member with the new values from the form
      next: (_) => {
        this.toastr.success('Profile updated successfully');
        this.editForm?.reset(this.member); // reset the form to the original values (No alert message)
      },
      error: (error) => this.toastr.error('Failed to update profile'),
    });
  }
}

// form name has to equal the ngModel
