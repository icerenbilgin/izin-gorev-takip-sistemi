import { Component, OnInit } from '@angular/core';
import { ConfirmationService, MessageService } from 'primeng/api';

import { TasksDto } from '../../core/dtos/tasks.dto';
import { HttpEntityService } from '../../core/services/http-entity.service';
import { ApiEndpoints } from '../../core/api-endpoints';
import { SharedUiModule } from '../../shared/shared-ui.module';
import { DataResultModel } from '../../core/models/data-result.model';
import { UserContextService } from '../../core/services/user-context.service';

@Component({
  selector: 'app-tasks-list',
  standalone: true,
  templateUrl: './tasks-list.component.html',
  styleUrl: './tasks-list.component.scss',
  imports: [SharedUiModule],
})
export class TasksListComponent implements OnInit {

  tasks: TasksDto[] = [];
  loading = false;

  userId!: number;

  usersMap: Record<number, string> = {};

  constructor(
    private httpEntityService: HttpEntityService,
    private confirmationService: ConfirmationService,
    private messageService: MessageService,
    private userContext: UserContextService,
  ) { }

  ngOnInit(): void {
    this.setUserId();

    if (!this.userId) {
      console.warn('UserId bulunamadı');
      return;
    }

    this.getAllTasks();
  }

  private setUserId(): void {
    const id = this.userContext.userId;
    if (id) {
      this.userId = id;
    }
  }

  getAllTasks(): void {
    this.loading = true;

    this.httpEntityService
      .get<DataResultModel<TasksDto[]>>(ApiEndpoints.GetAllTasks)
      .subscribe({
        next: (res: any) => {
          this.tasks = res?.success ? (res?.data ?? []) : [];
          this.loading = false;
        },
        error: () => {
          this.tasks = [];
          this.loading = false;
        }
      });
  }

  getTasksByUserId(userId: number): void {
    this.loading = true;

    this.httpEntityService
      .get(ApiEndpoints.GetTaskByUserId + '/' + userId)
      .subscribe({
        next: (res: any) => {
          this.tasks = res?.success ? (res?.data ?? []) : [];
          this.loading = false;
        },
        error: () => {
          this.tasks = [];
          this.loading = false;
        }
      });
  }

  deleteTask(taskId: number): void {
    this.confirmationService.confirm({
      message: 'Görev silinsin mi?',
      header: 'Onay',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.httpEntityService
          .delete(ApiEndpoints.DeleteTask, taskId)
          .subscribe({
            next: (res: any) => {
              if (res?.success) {
                this.showMessage('success', 'Başarılı', 'Görev silindi');
                this.getTasksByUserId(this.userId);
              } else {
                this.showMessage('error', 'Hata', res?.message ?? 'Silme başarısız');
              }
            },
            error: () => {
              this.showMessage('error', 'Hata', 'Silme işlemi başarısız');
            }
          });
      }
    });
  }

  getUserName(userId?: number | null): string {
    if (!userId) return '-';
    return this.usersMap[userId] ?? '-';
  }

  getStatusText(statusId: number): string {
    switch (statusId) {
      case 1: return 'Yeni';
      case 2: return 'Devam Ediyor';
      case 3: return 'Tamamlandı';
      case 4: return 'İptal';
      default: return '-';
    }
  }

  getStatusSeverity(
    statusId: number
  ): 'success' | 'info' | 'warning' | 'danger' {
    switch (statusId) {
      case 1: return 'info';
      case 2: return 'warning';
      case 3: return 'success';
      case 4: return 'danger';
      default: return 'info';
    }
  }

  showMessage(
    severity: 'success' | 'info' | 'warning' | 'error',
    summary: string,
    detail: string
  ): void {
    this.messageService.add({
      key: 'DefaultToaster',
      severity,
      summary,
      detail,
      life: 5000
    });
  }
}