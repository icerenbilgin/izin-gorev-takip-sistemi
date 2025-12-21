import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { MessageService } from 'primeng/api';

import { HttpEntityService } from '../../core/services/http-entity.service';
import { DataResultModel } from '../../core/models/data-result.model';
import { ApiEndpoints } from '../../core/api-endpoints';
import { DepartmentsDto } from '../../core/dtos/departments.dto';
import { UserRolesDto } from '../../core/dtos/userRoles.dto';
import { ApiService } from '../../core/api.service';
import { SharedUiModule } from '../../shared/shared-ui.module';
import { UserContextService } from '../../core/services/user-context.service';

@Component({
  standalone: true,
  selector: 'app-users-add',
  templateUrl: './users-add.component.html',
  imports: [SharedUiModule],
})

export class UsersAddComponent implements OnInit {

  formGroup: FormGroup = {} as FormGroup;
  saving = false;

  departments: DepartmentsDto[] = [];
  userRoles: UserRolesDto[] = [];

  userId!: number;
  userRoleId!: number;

  constructor(
    private httpEntityService: HttpEntityService,
    private messageService: MessageService,
    private formBuilder: FormBuilder,
    private apiService: ApiService,
    private userContext: UserContextService,
  ) { }

  ngOnInit(): void {
    this.setUserContext();

    if (!this.userId) {
      console.warn('UserId bulunamadı');
      return;
    }

    this.initForm();
    this.getAllUserRoles();
    this.getAllDepartments();
  }

  initForm(): void {
    this.formGroup = this.formBuilder.group({
      userId: [null],
      name: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
      userRoleId: [0, Validators.min(1)],
      departmentId: [0, Validators.min(1)],
      isActive: [true]
    });
  }

  private setUserContext(): void {
    console.log('USERS ADD USER:', this.userContext.user);

    const userId = this.userContext.userId;
    const roleId = this.userContext.userRoleId;

    console.log('userId:', userId);
    console.log('roleId:', roleId);

    if (!userId || !roleId) {
      console.warn('Kullanıcı bilgisi eksik');
      return;
    }

    this.userId = userId;
    this.userRoleId = roleId;
  }


  save() {
    const formGroup = this.formGroup.getRawValue();
    const formData = new FormData();
    let messages: string[] = [];

    if (formGroup.name === null) {
      messages.push("Lütfen isim giriniz.");
    }
    if (formGroup.lastName === null) {
      messages.push("Lütfen soyisim giriniz.");
    }

    if (formGroup.email === null) {
      messages.push("Lütfen email giriniz.");
    }

    if (formGroup.password === null) {
      messages.push("Lütfen şifre giriniz.");
    }

    if (formGroup.userRoleId === 0) {
      messages.push("Lütfen kullanıcı rolünü seçiniz.");
    }

    if (formGroup.departmentId === 0) {
      messages.push("Lütfen departmanı seçiniz.");
    }

    if (messages.length > 0) {
      this.showMessage("warn", "Uyarı!", messages.join("<br><br>"));
      return;
    }

    this.httpEntityService
      .post<{ success: boolean; message: string }>(
        ApiEndpoints.AddOrUpdateUser,
        formData
      )
      .subscribe({
        next: (res) => {
          const durum = res.success ? 'success' : 'error';
          const baslik = res.success ? 'Başarılı' : 'Başarısız!';
          this.showMessage(durum, baslik, res.message);
        },
        error: (err: HttpErrorResponse) => {
          this.showMessage(
            'error',
            'Hata',
            err.error?.message || 'Kaydetme işlemi başarısız oldu.'
          );
        }
      });
  }

  getAllDepartments(): void {
    this.httpEntityService
      .get<DataResultModel<DepartmentsDto[]>>(ApiEndpoints.GetAllDepartments)
      .subscribe({
        next: (res) => {
          if (res.success && res.data) {
            this.departments = [
              { departmentId: 0, departmentName: 'Seçiniz...' },
              ...res.data
            ];
          } else {
            this.departments = [];
          }
        },
        error: () => {
          this.departments = [];
        }
      });
  }

  getAllUserRoles(): void {
    this.httpEntityService
      .get<DataResultModel<UserRolesDto[]>>(ApiEndpoints.GetAllUserRoles)
      .subscribe({
        next: (res) => {
          if (res.success && res.data) {
            this.userRoles = [
              { userRoleId: 0, userRoleName: 'Seçiniz...' },
              ...res.data
            ];
          } else {
            this.userRoles = [];
          }
        },
        error: () => {
          this.userRoles = [];
        }
      });
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