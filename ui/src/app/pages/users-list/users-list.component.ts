import { Component } from '@angular/core';
import { UsersDto } from '../../core/dtos/users.dto';
import { HttpEntityService } from '../../core/services/http-entity.service';
import { DataResultModel } from '../../core/models/data-result.model';
import { ConfirmationService, MessageService } from 'primeng/api';
import { ApiEndpoints } from '../../core/api-endpoints';
import { SharedUiModule } from '../../shared/shared-ui.module';
import { UserContextService } from '../../core/services/user-context.service';

@Component({
  selector: 'app-users-list',
  standalone: true,
  templateUrl: './users-list.component.html',
  styleUrl: './users-list.component.scss',
  imports: [SharedUiModule],
})
export class UsersListComponent {
  users: UsersDto[] = [];
  loading = false;

  userId!: number;

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

    this.getAllUsers();
  }

  private setUserId(): void {
    const id = this.userContext.userId;
    if (id) {
      this.userId = id;
    }
  }
  
  getAllUsers(): void {
    this.loading = true;

    this.httpEntityService
      .get<DataResultModel<UsersDto[]>>(ApiEndpoints.GetAllUsers)
      .subscribe({
        next: (res: any) => {
          this.users = res?.success ? (res?.data ?? []) : [];
          this.loading = false;
        },
        error: () => {
          this.users = [];
          this.loading = false;
        }
      });
  }

  deleteUser(userId: number): void {
    this.confirmationService.confirm({
      message: 'Kullanıcı silinsin mi?',
      header: 'Onay',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.httpEntityService
          .delete(ApiEndpoints.DeleteUser, userId)
          .subscribe({
            next: (res: any) => {
              if (res.success) {
                this.showMessage('success', 'Başarılı', 'Kullanıcı silindi');
                this.getAllUsers();
              } else {
                this.showMessage('error', 'Hata', res.message);
              }
            },
            error: () => {
              this.showMessage('error', 'Hata', 'Silme işlemi başarısız');
            }
          });
      }
    });
  }

  getStatusSeverity(isActive: boolean): 'success' | 'danger' {
    return isActive ? 'success' : 'danger';
  }

  getStatusText(isActive: boolean): string {
    return isActive ? 'Aktif' : 'Pasif';
  }


  showMessage(_severity: string, _summary: string, _detail: string) {
    this.messageService.add({
      key: "DefaultToaster",
      severity: _severity,
      summary: _summary,
      detail: _detail,
      life: 5000
    });
  }
}