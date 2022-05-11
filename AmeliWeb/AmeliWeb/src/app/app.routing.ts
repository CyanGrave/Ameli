import { Routes } from '@angular/router';
import { AuthRequired } from './auth/auth.guard';
import { NotFoundComponent } from './not-found/not-found.component';

export const AppRoutes: Routes = [

  { path: '404', component: NotFoundComponent },
  { path: '**', redirectTo: '404' },


  { path: '', redirectTo: 'users', pathMatch: 'full' },
  {
  path: 'users', canActivate: [AuthRequired],
    loadChildren: () => import('./users/users.module').then(m => m.UsersModule),
  }
];
