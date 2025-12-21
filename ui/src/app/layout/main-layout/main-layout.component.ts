import { Component, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { SharedUiModule } from '../../shared/shared-ui.module';
import { UserContextService } from '../../core/services/user-context.service';
import { ToastModule } from 'primeng/toast';

@Component({
  standalone: true,
  selector: 'app-main-layout',
  imports: [
    RouterModule,
    SharedUiModule,
    ToastModule
  ],
  templateUrl: './main-layout.component.html',
  styleUrls: ['./main-layout.component.scss']
})
export class MainLayoutComponent implements OnInit {

  userRoleId!: number;

  constructor(
    private userContext: UserContextService,
    private router: Router
  ) { }

  ngOnInit(): void {
    const user = this.userContext.user;

    if (!user) {
      this.router.navigate(['/login']);
      return;
    }

    this.userRoleId = user.userRoleId;
  }

  logout(): void {
    this.userContext.clear();
    this.router.navigate(['/login']);
  }
}