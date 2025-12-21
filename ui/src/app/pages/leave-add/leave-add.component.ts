import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { MessageService } from 'primeng/api';

import { HttpEntityService } from '../../core/services/http-entity.service';
import { ApiEndpoints } from '../../core/api-endpoints';
import { DataResultModel } from '../../core/models/data-result.model';

import { LeaveTypesDto } from '../../core/dtos/leaveTypes.dto';
import { LeaveRequestsDto } from '../../core/dtos/leaveRequests.dto';

import { SharedUiModule } from '../../shared/shared-ui.module';
import { UserContextService } from '../../core/services/user-context.service';

@Component({
  standalone: true,
  templateUrl: './leave-add.component.html',
  imports: [SharedUiModule],
})
export class LeaveAddComponent implements OnInit {

  formGroup!: FormGroup;
  leaveTypes: LeaveTypesDto[] = [];

  userId!: number;

  constructor(
    private formBuilder: FormBuilder,
    private httpEntityService: HttpEntityService,
    private messageService: MessageService,
    private userContext: UserContextService,
  ) { }

  ngOnInit(): void {
    this.setUserId();

    if (!this.userId) {
      console.warn('UserId bulunamadı');
      return;
    }

    this.initForm();
    this.getAllLeaveTypes();

  }

  private setUserId(): void {
    const id = this.userContext.userId;
    if (id) {
      this.userId = id;
    }
  }
  
  initForm(): void {
    this.formGroup = this.formBuilder.group({
      leaveTypeId: [0, Validators.min(1)],
      startDate: [null, Validators.required],
      endDate: [null, Validators.required],
      description: ['']
    });
  }

  save(): void {
    if (this.formGroup.invalid) {
      this.showMessage('warn', 'Uyarı', 'Lütfen zorunlu alanları doldurunuz.');
      return;
    }

    const payload = this.formGroup.getRawValue();

    this.httpEntityService
      .post<{ success: boolean; message: string }>(
        ApiEndpoints.AddOrUpdateLeaveRequest,
        payload
      )
      .subscribe({
        next: (res) => {
          const severity = res.success ? 'success' : 'error';
          this.showMessage(severity, 'Sonuç', res.message);

          if (res.success) {
            this.formGroup.reset();
            this.formGroup.patchValue({ leaveTypeId: 0 });
          }
        },
        error: (err: HttpErrorResponse) => {
          this.showMessage(
            'error',
            'Hata',
            err.error?.message || 'Kaydetme işlemi başarısız'
          );
        }
      });
  }

  getAllLeaveTypes(): void {
    this.httpEntityService
      .get<DataResultModel<LeaveTypesDto[]>>(
        ApiEndpoints.GetAllLeaveTypes
      )
      .subscribe({
        next: (res) => {
          if (res.success && res.data) {
            this.leaveTypes = [
              { leaveTypeId: 0, leaveTypeName: 'Seçiniz...' },
              ...res.data
            ];
          } else {
            this.leaveTypes = [];
          }
        },
        error: () => {
          this.leaveTypes = [];
        }
      });
  }

  getStatusText(row: LeaveRequestsDto): string {
    if (row.seniorManagerApprovalDate) return 'Onaylandı';
    if (row.rejectionStatement) return 'Reddedildi';
    return 'Beklemede';
  }

  getStatusSeverity(
    row: LeaveRequestsDto
  ): 'success' | 'warning' | 'danger' {
    if (row.seniorManagerApprovalDate) return 'success';
    if (row.rejectionStatement) return 'danger';
    return 'warning';
  }

  private showMessage(
    severity: 'success' | 'warn' | 'error',
    summary: string,
    detail: string
  ): void {
    this.messageService.add({
      key: 'DefaultToaster',
      severity,
      summary,
      detail,
      life: 4000
    });
  }
}