import { NgModule } from '@angular/core';
import { MaterialModule } from './material.module';
import { CustomErrorStateMatcher } from './customErrorStateMatcher';

@NgModule({
  imports: [
    MaterialModule,
    CustomErrorStateMatcher
  ],
  exports: [
    MaterialModule,
    CustomErrorStateMatcher
  ],
})
export class SharedModule { }
