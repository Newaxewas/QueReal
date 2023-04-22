import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { QuestCreatePageComponent } from './quest-create-page/quest-create-page.component';
import { MatInputModule } from '@angular/material/input';
import { ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { RouterModule, Routes } from '@angular/router';
import { MatListModule } from "@angular/material/list";
import { MatIconModule } from "@angular/material/icon";

const routes: Routes = [
  {
    path: '',
    component: QuestCreatePageComponent
  }
];

@NgModule({
  declarations: [
    QuestCreatePageComponent
  ],
  imports: [
    RouterModule.forChild(routes),
    CommonModule,
    MatInputModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatButtonModule,
    MatListModule,
    MatIconModule,
  ]
})
export class QuestCreatePageModule { }
