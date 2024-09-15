import { CanDeactivateFn } from '@angular/router';
import { MemberEditComponent } from '../members/member-edit/member-edit.component';

/**
 * This guard checks if the user has unsaved changes.
 * If the user has unsaved changes, it displays a message and returns false.
 * @param component
 * @returns
 */

export const preventUnsavedChangesGuard: CanDeactivateFn<
  MemberEditComponent
> = (component) => {
  if (component.editForm?.dirty) {
    return confirm(
      'Are you sure ypu want to continue? Any unsaved changes will be lost'
    );
  }
  return true;
};
