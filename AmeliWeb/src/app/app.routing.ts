export const AppRoutes: Routes = [
  { path: '', redirectTo: 'user', pathMatch: 'full' },
  {
    path: 'user',
    loadChildren: './user/user.module#UserModule'
  },
  {
    path: '**',
    redirectTo: 'user'
  }
];
