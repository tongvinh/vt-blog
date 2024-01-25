import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { PanelModule } from 'primeng/panel';
import { BlockUIModule } from 'primeng/blockui';
import { PaginatorModule } from 'primeng/paginator';
import { BadgeModule } from 'primeng/badge';
import { CheckboxModule } from 'primeng/checkbox';
import { TableModule } from 'primeng/table';
import { KeyFilterModule } from 'primeng/keyfilter';
import { ButtonModule } from 'primeng/button';
import { ContentRoutingModule } from './content-routing.module';
import { PostCategoryComponent } from './post-category/post-category.component';
import { PostCategoryDetailComponent } from './post-category/post-category-detail.component';
import { InputTextModule } from 'primeng/inputtext';
import { VTSharedModule } from 'src/app/shared/modules/vt-shared.module';
import { InputTextareaModule } from 'primeng/inputtextarea';

@NgModule({
  imports: [
    ContentRoutingModule,
    CommonModule,
    ReactiveFormsModule,
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
    InputTextareaModule 
  ],
  declarations: [PostCategoryComponent,
    PostCategoryDetailComponent]
})
export class ContentModule {
}
