import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UserComponent } from './pages/user/user.component';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './components/user/login/login.component';
import { RegistrationComponent } from './components/user/registration/registration.component';
import { AuthGuard } from './guards/auth.guard';
import { MainComponent } from './pages/home/main/main.component';
import { ProfileComponent } from './pages/home/profile/profile.component';
import {
  LoginTitle,
  RegistrationTitle,
  UserDetailsTitle,
  UserMessagesTitle,
  UserModificationTitle,
  UserNotificationsTitle,
  ToDoSolvedTitle,
  ToDoUnSolvedTitle,
  MoviesMyTitle,
  MoviesAllTitle
} from './services';
import { WorkingManagerComponent } from './pages/home/working-manager/working-manager.component';
import { TodosComponent } from './pages/home/todos/todos.component';
import { UserDetailsComponent } from './components/profile/user-details/user-details.component';
import { UserModifyComponent } from './components/profile/user-modify/user-modify.component';
import { UserMessagesComponent } from './components/profile/user-messages/user-messages.component';
import { UserNotificationsComponent } from './components/profile/user-notifications/user-notifications.component';
import { SeriesComponent } from './pages/home/series/series.component';
import { MoviesComponent } from './pages/home/movies/movies.component';
import { MoviesAllComponent } from './components/movies/movies-all/movies-all.component';
import { MoviesMyComponent } from './components/movies/movies-my/movies-my.component';
import { SeriesAllComponent } from './components/series/series-all/series-all.component';
import { SeriesMyComponent } from './components/series/series-my/series-my.component';
import { UnSolvedTodosComponent } from './components/todos/un-solved-todos/un-solved-todos.component';
import { SolvedTodosComponent } from './components/todos/solved-todos/solved-todos.component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    canActivate: [AuthGuard],
    children: [
      { path: '', component: MainComponent },
      {
        path: 'profile',
        component: ProfileComponent,
        children: [
          { path: '', pathMatch: 'full', redirectTo: 'details' },
          {
            path: 'details',
            component: UserDetailsComponent,
            data: { title: UserDetailsTitle }
          },
          {
            path: 'modify',
            component: UserModifyComponent,
            data: { title: UserModificationTitle }
          },
          {
            path: 'messages',
            component: UserMessagesComponent,
            data: { title: UserMessagesTitle }
          },
          {
            path: 'notifications',
            component: UserNotificationsComponent,
            data: { title: UserNotificationsTitle }
          }
        ]
      },
      {
        path: 'working-manager',
        children: [
          {
            path: '',
            component: WorkingManagerComponent
          },
          {
            path: ':date',
            component: WorkingManagerComponent,
            runGuardsAndResolvers: 'always'
          }
        ]
      },
      {
        path: 'todos',
        component: TodosComponent,
        children: [
          { path: '', pathMatch: 'full', redirectTo: 'unsolved' },
          {
            path: 'unsolved',
            component: UnSolvedTodosComponent,
            data: { title: ToDoUnSolvedTitle }
          },
          {
            path: 'solved',
            component: SolvedTodosComponent,
            data: { title: ToDoSolvedTitle }
          }
        ]
      },
      {
        path: 'movies',
        component: MoviesComponent,
        children: [
          { path: '', pathMatch: 'full', redirectTo: 'my' },
          {
            path: 'all',
            component: MoviesAllComponent,
            data: { title: MoviesAllTitle }
          },
          {
            path: 'my',
            component: MoviesMyComponent,
            data: { title: MoviesMyTitle }
          }
        ]
      },
      {
        path: 'series',
        component: SeriesComponent,
        children: [
          { path: '', pathMatch: 'full', redirectTo: 'my' },
          { path: 'all', component: SeriesAllComponent },
          { path: 'my', component: SeriesMyComponent }
        ]
      }
    ]
  },
  {
    path: 'oacts',
    component: UserComponent,
    children: [
      { path: '', pathMatch: 'full', redirectTo: 'login' },
      { path: 'login', component: LoginComponent, data: { title: LoginTitle } },
      {
        path: 'registration',
        component: RegistrationComponent,
        data: { title: RegistrationTitle }
      }
    ]
  },
  { path: '**', pathMatch: 'full', redirectTo: '' }
];

/**
 * App's routing module
 * @export
 * @class AppRoutingModule
 */
@NgModule({
  imports: [
    RouterModule.forRoot(routes, {
      useHash: true,
      onSameUrlNavigation: 'reload'
    })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule {}
