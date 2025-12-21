import { Routes } from '@angular/router';
import { MainLayoutComponent } from './layout/main-layout/main-layout.component';

export const routes: Routes = [

  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  },

  {
    path: 'login',
    loadComponent: () =>
      import('./auth/login/login.component')
        .then(m => m.LoginComponent)
  },

  {
    path: '',
    component: MainLayoutComponent,
    children: [

      {
        path: 'dashboard',
        loadComponent: () =>
          import('./pages/dashboard/dashboard.component')
            .then(m => m.DashboardComponent)
      },

      {
        path: 'task/ekle',
        loadComponent: () =>
          import('./pages/tasks-add/tasks-add.component')
            .then(m => m.TasksAddComponent)
      },
      {
        path: 'task/liste',
        loadComponent: () =>
          import('./pages/tasks-list/tasks-list.component')
            .then(m => m.TasksListComponent)
      },

      {
        path: 'izin/ekle',
        loadComponent: () =>
          import('./pages/leave-add/leave-add.component')
            .then(m => m.LeaveAddComponent)
      },
      {
        path: 'izin/liste',
        loadComponent: () =>
          import('./pages/leave-list/leave-list.component')
            .then(m => m.LeaveListComponent)
      },

      {
        path: 'kisi/ekle',
        loadComponent: () =>
          import('./pages/users-add/users-add.component')
            .then(m => m.UsersAddComponent)
      },
      {
        path: 'kisi/liste',
        loadComponent: () =>
          import('./pages/users-list/users-list.component')
            .then(m => m.UsersListComponent)
      }
    ]
  },

  {
    path: '**',
    redirectTo: 'login'
  }
];