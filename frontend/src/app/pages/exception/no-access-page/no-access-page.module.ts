import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NoAccessPageComponent } from './no-access-page/no-access-page.component';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    component: NoAccessPageComponent
  }
];

@NgModule({
  declarations: [
    NoAccessPageComponent
  ],
  imports: [
    RouterModule.forChild(routes),
    CommonModule,
  ]
})
export class NoAccessPageModule { }
