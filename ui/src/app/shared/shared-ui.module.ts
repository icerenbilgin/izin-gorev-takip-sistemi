import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { CalendarModule } from 'primeng/calendar';
import { DropdownModule } from 'primeng/dropdown';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { TableModule } from 'primeng/table';
import { TagModule } from 'primeng/tag';
import { PasswordModule } from 'primeng/password';
import { ToastModule } from 'primeng/toast';

@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    InputTextModule,
    CalendarModule,
    DropdownModule,
    ButtonModule,
    CardModule,
    TableModule,
    TagModule,
    PasswordModule
  ],
  exports: [
    CommonModule,
    ReactiveFormsModule,
    InputTextModule,
    CalendarModule,
    DropdownModule,
    ButtonModule,
    CardModule,
    TableModule,
    TagModule,
    PasswordModule,
    ToastModule
  ]
})
export class SharedUiModule {}