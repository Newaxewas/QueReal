import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    loadChildren: ()=>import("./pages/home/home-page/home-page.module").then(module=> module.HomePageModule),
  },
  {
    path: 'quest/create',
    pathMatch: 'full',
    loadChildren: ()=>import("./pages/quest/quest-create-page/quest-create-page.module").then(module=> module.QuestCreatePageModule),
  },
  {
    path: 'quest/list',
    pathMatch: 'full',
    loadChildren: ()=>import("./pages/quest/quest-list-page/quest-list-page.module").then(module=> module.QuestListPageModule),
  },
  {
    path: 'quest/view/:questId',
    pathMatch: 'full',
    loadChildren: ()=>import("./pages/quest/quest-view-page/quest-view-page.module").then(module=> module.QuestViewPageModule),
  },
  {
    path: 'quest/edit/:questId',
    pathMatch: 'full',
    loadChildren: ()=>import("./pages/quest/quest-edit-page/quest-edit-page.module").then(module=> module.QuestEditPageModule),
  },
  {
    path: 'user/login',
    pathMatch: 'full',
    loadChildren: ()=>import("./pages/user/user-login-page/user-login-page.module").then(module=> module.UserLoginPageModule),
  },
  {
    path: 'user/register',
    pathMatch: 'full',
    loadChildren: ()=>import("./pages/user/user-register-page/user-register-page.module").then(module=> module.UserRegisterPageModule),
  },
  {
    path: 'exception/no-access',
    pathMatch: 'full',
    loadChildren: ()=>import("./pages/exception/no-access-page/no-access-page.module").then(module=> module.NoAccessPageModule),
  },
  {
    path: '**',
    loadChildren: ()=>import("./pages/exception/not-found-page/not-found-page.module").then(module=> module.NotFoundPageModule),
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
