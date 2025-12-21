import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { MessageService } from 'primeng/api';

import { HttpEntityService } from '../../core/services/http-entity.service';
import { ApiEndpoints } from '../../core/api-endpoints';
import { DataResultModel } from '../../core/models/data-result.model';

import { TaskStatusesDto } from '../../core/dtos/taskStatuses.dto';
import { UsersDto } from '../../core/dtos/users.dto';

import { SharedUiModule } from '../../shared/shared-ui.module';
import { UserContextService } from '../../core/services/user-context.service';

@Component({
  selector: 'app-tasks-add',
  standalone: true,
  templateUrl: './tasks-add.component.html',
  styleUrl: './tasks-add.component.scss',
  imports: [SharedUiModule],
})
export class TasksAddComponent implements OnInit {

  formGroup!: FormGroup;

  taskStatuses: TaskStatusesDto[] = [];
  users: UsersDto[] = [];

  saving = false;

  userId!: number;

  constructor(
    private formBuilder: FormBuilder,
    private httpEntityService: HttpEntityService,
    private messageService: MessageService,
    private userContext: UserContextService
  ) { }

  ngOnInit(): void {
    this.initForm();
    this.getAllTaskStatuses();
    this.getAllUsers();
  }

  initForm(): void {
    const userId = this.userContext.userId;

    if (!userId) {
      this.showMessage('error', 'Hata', 'Oturum bilgisi bulunamadı.');
      return;
    }

    this.formGroup = this.formBuilder.group({
      taskId: 0,
      taskContent: ['', Validators.required],
      createdUserId: [userId, Validators.required],

      assignedUserId: [0, [Validators.required, Validators.min(1)]],
      taskStatusId: [0, [Validators.required, Validators.min(1)]],

      isActive: [true],
      createdDate: [new Date()],
      assignedDate: [new Date()],
      startedDate: [new Date()],
      finishedDate: [new Date()],
    });
  }

  save(): void {
    if (this.formGroup.invalid) {
      this.showMessage('warn', 'Uyarı', 'Zorunlu alanları doldurunuz.');
      return;
    }

    const assignedUserId = this.formGroup.get('assignedUserId')?.value;
    if (assignedUserId && assignedUserId > 0) {
      this.formGroup.patchValue({
        assignedDate: new Date()
      });
    }

    this.saving = true;

    const payload = this.formGroup.getRawValue();

    this.httpEntityService
      .post(ApiEndpoints.AddOrUpdateTask, payload)
      .subscribe({
        next: (res: any) => {
          this.saving = false;

          if (res.success) {
            this.showMessage('success', 'Başarılı', 'Task eklendi');

            this.formGroup.reset({
              taskId: null,
              taskContent: '',
              createdUserId: this.userContext.userId,
              assignedUserId: 0,
              taskStatusId: 0,
              isActive: true,
              createdDate: new Date(),
              assignedDate: null
            });
          } else {
            this.showMessage('error', 'Hata', res.message);
          }
        },
        error: (err: HttpErrorResponse) => {
          this.saving = false;
          this.showMessage(
            'error',
            'Hata',
            err.error?.message || 'Kaydetme işlemi başarısız oldu.'
          );
        }
      });
  }

  getAllTaskStatuses(): void {
    this.httpEntityService
      .get<DataResultModel<TaskStatusesDto[]>>(ApiEndpoints.GetAllTaskStatuses)
      .subscribe(res => {
        this.taskStatuses = res.success ? res.data : [];
        this.taskStatuses.unshift({
          taskStatusId: 0,
          taskStatusName: 'Seçiniz...'
        });
      });
  }

  getAllUsers(): void {
    this.httpEntityService
      .get<DataResultModel<UsersDto[]>>(ApiEndpoints.GetAllUsers)
      .subscribe(res => {
        this.users = res.success ? res.data : [];
        this.users.unshift({
          userId: 0,
          name: 'Seçiniz...'
        });
      });
  }

  showMessage(severity: string, summary: string, detail: string): void {
    this.messageService.add({
      key: 'DefaultToaster',
      severity,
      summary,
      detail,
      life: 5000
    });
  }
}