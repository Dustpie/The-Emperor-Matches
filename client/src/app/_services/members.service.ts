import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { Member } from '../Models/Member';
import { of, tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class MembersService {
  private http = inject(HttpClient);
  baseUrl = environment.apiUrl;
  members = signal<Member[]>([]);

  getMembers() {
    return this.http.get<Member[]>(this.baseUrl + 'users').subscribe({
      // Subscribing to observables menas to tell it to start emitting vlaues
      // The subscribe method takes object with handlers for different notifications )next, error, complete
      // next is called when the observable emits a value
      // The arrow function takes the emitted value (members) as its params
      // members
      next: (fetchedMembers) => this.members.set(fetchedMembers),
    });
  }

  getMember(username: string) {
    const member = this.members().find((x) => x.username === username);
    if (member !== undefined) return of(member);

    return this.http.get<Member>(this.baseUrl + 'users/' + username); // I forgot to type the get Method
  }

  updateMember(member: Member) {
    return this.http.put(this.baseUrl + 'users', member).pipe(
      tap(() => {
        this.members.update((members) =>
          members.map((m) => (m.username === member.username ? member : m))
        );
      })
    );
  }
}
