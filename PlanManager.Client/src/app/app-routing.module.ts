import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { HomeComponent, MainComponent } from './pages';
import {
  PlanManagerHomeComponent,
  ProfileComponent,
  ProfileSettingsComponent,
} from './pages/pm';
import {
  AuthComponent,
  AuthLoginComponent,
  AuthPasswordResetComponent,
  AuthRegistrationComponent,
} from './pages/auth';
import { EmHomeComponent } from './pages/em/em-home/em-home.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  {
    path: 'pm',
    component: MainComponent,
    children: [
      { path: '', component: PlanManagerHomeComponent },
      {
        path: 'profile',
        component: MainComponent,
        children: [
          { path: '', component: ProfileComponent },
          { path: 'settings', component: ProfileSettingsComponent },
        ],
      },
    ],
  },
  {
    path: 'em',
    component: MainComponent,
    children: [{ path: '', component: EmHomeComponent }],
  },
  {
    path: 'auth',
    component: AuthComponent,
    children: [
      { path: '', pathMatch: 'full', redirectTo: 'login' },
      { path: 'login', component: AuthLoginComponent },
      {
        path: 'password-reset',
        component: AuthPasswordResetComponent,
      },
      {
        path: 'registration',
        component: AuthRegistrationComponent,
      },
    ],
  } /*,
  {
    path: 'plans',
    pathMatch: 'full',
    children: []
  },
  {
    path: 'profile',
    pathMatch: 'full',
    children: [
      { path: '', pathMatch: 'full' },
      { path: 'settings', pathMatch: 'full' }
    ]
  },
  {
    path: 'groups',
    pathMatch: 'full',
    children: [
      { path: '', pathMatch: 'full' },
      {
        path: '{id}',
        pathMatch: 'full',
        children: [
          { path: '', pathMatch: 'full' },
          { path: 'settings', pathMatch: 'full' },
          { path: 'plans', pathMatch: 'full' },
          { path: 'ideas', pathMatch: 'full' },
          { path: 'messages', pathMatch: 'full' }
        ]
      }
    ]
  }*/,
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
