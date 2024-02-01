import { TransactionsComponent } from './transactions/transactions.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RoyaltyRoutingModule } from './royalty-routing.module';
import { RoyaltyMonthComponent } from './royalty-month/royalty-month.component';
import { RoyaltyUserComponent } from './royalty-user/royalty-user.component';
import { IconModule } from '@coreui/icons-angular';
import { ChartjsModule } from '@coreui/angular-chartjs';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { PanelModule } from 'primeng/panel';
import { BlockUIModule } from 'primeng/blockui';
import { PaginatorModule } from 'primeng/paginator';
import { BadgeModule } from 'primeng/badge';
import { CheckboxModule } from 'primeng/checkbox';
import { TableModule } from 'primeng/table';
import { KeyFilterModule } from 'primeng/keyfilter';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { DropdownModule } from 'primeng/dropdown';
import { InputNumberModule } from 'primeng/inputnumber';
import { ImageModule } from 'primeng/image';
import { DynamicDialogModule } from 'primeng/dynamicdialog';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { EditorModule } from 'primeng/editor';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { ReactiveFormsModule } from '@angular/forms';
import { VTSharedModule } from '../../shared/modules/vt-shared.module';



@NgModule({
  declarations: [
    RoyaltyMonthComponent,
    RoyaltyUserComponent,
    TransactionsComponent
  ],
  imports: [
    CommonModule,
    RoyaltyRoutingModule,
    IconModule,
    ReactiveFormsModule,
    ChartjsModule,
    ProgressSpinnerModule,
    PanelModule,
    BlockUIModule,
    PaginatorModule,
    BadgeModule,
    CheckboxModule,
    TableModule,
    KeyFilterModule,
    VTSharedModule,
    ButtonModule,
    InputTextModule,
    InputTextareaModule,
    DropdownModule,
    EditorModule,
    InputNumberModule,
    ImageModule,
    AutoCompleteModule,
    DynamicDialogModule,
  ],
})
export class RoyaltyModule { }
