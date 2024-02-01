import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ValidationMessageComponent } from './validation-message/validation-message.component';
import { PermissionDirective } from './permission.directive';

@NgModule({
    imports: [CommonModule],
    declarations: [ValidationMessageComponent, PermissionDirective],
    exports: [ValidationMessageComponent]
})
export class VTSharedModule { }