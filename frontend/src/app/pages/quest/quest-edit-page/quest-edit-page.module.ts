import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { QuestEditPageComponent } from './quest-edit-page/quest-edit-page.component';
import { MatInputModule } from '@angular/material/input';
import { ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { RouterModule, Routes } from '@angular/router';
import { questResolver } from 'src/app/resolvers';

const routes: Routes = [
  {
    path: '',
    component: QuestEditPageComponent,
    resolve: { quest: questResolver },
  }
];

@NgModule({
  declarations: [
    QuestEditPageComponent
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
export class QuestEditPageModule { }
