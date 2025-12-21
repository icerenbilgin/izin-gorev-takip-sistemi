import { Component, OnInit, OnDestroy } from '@angular/core';
import { SharedUiModule } from '../../shared/shared-ui.module';
import { UserContextService } from '../../core/services/user-context.service';
import { HttpEntityService } from '../../core/services/http-entity.service';
import { DataResultModel } from '../../core/models/data-result.model';
import { UsersDto } from '../../core/dtos/users.dto';
import { ApiEndpoints } from '../../core/api-endpoints';
import { LeaveRequestsDto } from '../../core/dtos/leaveRequests.dto';
import { TasksDto } from '../../core/dtos/tasks.dto';
import { MessageService } from 'primeng/api';
import { SignalRService } from '../../core/services/signalr.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [SharedUiModule],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit, OnDestroy {

  userId!: number;

  user: UsersDto | null = null;
  leaveRequests: LeaveRequestsDto[] = [];
  tasks: TasksDto[] = [];

  constructor(
    private userContext: UserContextService,
    private httpEntityService: HttpEntityService,
    private signalRService: SignalRService,
    private messageService: MessageService
  ) { }

  ngOnInit(): void {
    this.setUserId();

    if (!this.userId) {
      console.warn('UserId bulunamadı');
      return;
    }

    this.loadDashboardData();

    this.signalRService.startConnection(this.userId);

    this.signalRService.onReceiveNotification((message: string) => {
      console.log('📢 SignalR mesaj:', message);

      this.messageService.add({
        severity: 'info',
        summary: 'Yeni Görev',
        detail: message
      });

      this.getTasks();
    });
  }

  ngOnDestroy(): void {
    this.signalRService.stopConnection();
  }

  private setUserId(): void {
    const id = this.userContext.userId;
    if (id) {
      this.userId = id;
    }
  }

  private loadDashboardData(): void {
    this.getUserInfo();
    this.getLeaveRequests();
    this.getTasks();
  }

  getUserInfo(): void {
    this.httpEntityService
      .getById<DataResultModel<UsersDto>>(
        ApiEndpoints.GetUserById,
        this.userId
      )
      .subscribe(res => {
        this.user = res.success ? res.data : null;
      });
  }

  getLeaveRequests(): void {
    this.httpEntityService
      .getById<DataResultModel<LeaveRequestsDto[]>>(
        ApiEndpoints.GetLeaveRequestByUserId,
        this.userId
      )
      .subscribe(res => {
        this.leaveRequests = res.success ? res.data : [];
      });
  }

  getTasks(): void {
    this.httpEntityService
      .getById<DataResultModel<TasksDto[]>>(
        ApiEndpoints.GetTaskByUserId,
        this.userId
      )
      .subscribe(res => {

        this.tasks = res.success ? res.data : [];
      });
  }
}