import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  {
    path: 'signup',
    loadChildren: () =>
      import('../app/components/signup/signup.module').then(
        (m) => m.SignupModule,
      ),
  },
  {
    path: '',
    loadChildren: () =>
      import('../app/components/main-page/main-page.module').then(
        (m) => m.MainPageModule,
      ),
  },
  { path: '**', component: PageNotFoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
