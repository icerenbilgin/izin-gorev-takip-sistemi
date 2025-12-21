import { NgModule, Optional, SkipSelf } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { MessageService } from 'primeng/api';
import { ApiService } from './api.service';
import { HttpEntityService } from './services/http-entity.service';

@NgModule({
  imports: [
    HttpClientModule
  ],
  providers: [
    MessageService,
    ApiService,
    HttpEntityService
  ]
})
export class CoreModule {
  constructor(@Optional() @SkipSelf() parent: CoreModule) {
    if (parent) {
      throw new Error('');
    }
  }
}