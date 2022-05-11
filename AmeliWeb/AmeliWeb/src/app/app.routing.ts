import { Routes } from '@angular/router';
import { AuthRequired } from './auth/auth.guard';
import { NotFoundComponent } from './not-found/not-found.component';

export const AppRoutes: Routes = [

  { path: '404', component: NotFoundComponent },
  { path: '**', redirectTo: '404' },


  {
    path: '',
    redirectTo: 'users',
    pathMatch: 'full',
    loadChildren: () => import('./auth/auth.module').then(m => m.AuthModule),
  },

  {
    path: 'users', canActivate: [AuthRequired],
    loadChildren: () => import('./userManagement/user.module').then(m => m.UserModule)
  }
];
