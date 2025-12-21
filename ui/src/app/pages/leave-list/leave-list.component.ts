import { Component, OnInit } from '@angular/core';

import { HttpEntityService } from '../../core/services/http-entity.service';
import { ApiEndpoints } from '../../core/api-endpoints';
import { DataResultModel } from '../../core/models/data-result.model';
import { LeaveRequestsDto } from '../../core/dtos/leaveRequests.dto';

import { SharedUiModule } from '../../shared/shared-ui.module';
import { UserContextService } from '../../core/services/user-context.service';
import { MessageService } from 'primeng/api';

@Component({
  standalone: true,
  selector: 'app-leave-list',
  templateUrl: './leave-list.component.html',
  imports: [SharedUiModule],
})
export class LeaveListComponent implements OnInit {

  leaves: LeaveRequestsDto[] = [];
  loading = false;

  userId!: number;
  userRoleId!: number;

  constructor(
    private httpEntityService: HttpEntityService,
    private userContext: UserContextService,
    private messageService: MessageService,
  ) { }

  ngOnInit(): void {
    this.setUserContext();
    if (!this.userId || !this.userRoleId) return;

    this.getAllLeaves();
  }

  private setUserContext(): void {
    const userId = this.userContext.userId;
    const roleId = this.userContext.userRoleId;

    if (!userId || !roleId) {
      console.warn('Kullanıcı bilgisi eksik');
      return;
    }

    this.userId = userId;
    this.userRoleId = roleId;
  }

  getAllLeaves(): void {
    this.loading = true;

    this.httpEntityService
      .get<DataResultModel<LeaveRequestsDto[]>>(
        ApiEndpoints.GetAllLeaveRequest
      )
      .subscribe({
        next: res => {
          this.leaves = res.success ? res.data ?? [] : [];
          this.loading = false;
        },
        error: () => {
          this.leaves = [];
          this.loading = false;
        }
      });
  }

  approve(row: LeaveRequestsDto): void {
    const payload = {
      leaveRequestId: row.leaveRequestId,
      approvedByUserId: this.userId,
      approvedByRoleId: this.userRoleId
    };

    this.httpEntityService
      .post(ApiEndpoints.CheckLeaveRequest, payload)
      .subscribe({
        next: (res: any) => {
          if (res.success) {
            this.messageService.add({
              severity: 'success',
              summary: 'Başarılı',
              detail: 'İzin onaylandı'
            });

            this.getAllLeaves();
          } else {
            this.messageService.add({
              severity: 'error',
              summary: 'Hata',
              detail: res.message
            });
          }
        },
        error: () => {
          this.messageService.add({
            severity: 'error',
            summary: 'Hata',
            detail: 'Onay işlemi başarısız'
          });
        }
      });
  }

  getStatusText(row: LeaveRequestsDto): string {
    if (row.seniorManagerApprovalDate) return 'Onaylandı';
    if (row.rejectionStatement) return 'Reddedildi';
    return 'Beklemede';
  }

  getStatusSeverity(row: LeaveRequestsDto): 'success' | 'danger' | 'warning' {
    if (row.seniorManagerApprovalDate) return 'success';
    if (row.rejectionStatement) return 'danger';
    return 'warning';
  }
}